using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BlockchainSQL.DataObjects;
using System.Web.Mvc.Html;
using BlockchainSQL.Web.Models;
using Omu.AwesomeMvc;
using Sphere10.Framework;


namespace BlockchainSQL.Web.Code {
    public sealed class FormScope<T> : IDisposable where T : FormModelBase {
        private const string FormControllerName = "Form";

        //private const string ErrorDivHtml = 
        //    @"<div class=""alert alert-danger""><button type=""button"" class=""close"" data-dismiss=""alert"" aria-hidden=""true"">×</button>{0}</div>";

        private const string ResultDivHtml = @"<div id=""{0}""></div>";

        private const string Javascript =
@"<script type=""text/javascript"">
    function F{0}_BeforeSubmit(o) {{        
        $('#{0} :input[type=""submit""]').prop('disabled', true).after('<img class=""formLoadingImage animated fadeIn margin-left-10"" src=""{2}""/>');
    }}

    function F{0}_Success(result) {{
        //alert(JSON.stringify(result));
        var alertType = result.Result ? ""success"" : ""danger"";
        var alertHeader = result.Result ? ""Okay!"" : ""Apologies"";
        var alertIcon = result.Result ? ""fa fa-check"" : ""fa fa-exclamation"";
        $('#{0} :input[type=""submit""]').prop('disabled', false);
        $('#{0} .formLoadingImage').remove();
        $(""#{1}"").replaceWith('<br/><div class=""form-result alert alert-' + alertType+'""><button type=""button"" class=""close"" data-dismiss=""alert"" aria-hidden=""true"">×</button><strong><i class=""' + alertIcon + '""></i> ' + alertHeader + '</strong> ' + result.Message + '</div>');
        if (result.Result) {{
            $('#{0}')[0].reset();
            $('#{0}').closest('form').find('input[type=text], textarea').val('');
        }}
    }}
</script>";

        private readonly string _formID;
        private readonly bool _omitForm;
        private readonly string _formName;
        private readonly string _formClass;
        private readonly string _formResultID;
        private readonly HtmlHelper<T> _htmlHelper;
        //private readonly string _jsResultFunc;

        public FormScope(HtmlHelper<T> htmlHelper, T formModel, string clientFormClass = null) {
            _formID = "_"+formModel.ID.ToStrictAlphaString().ToLowerInvariant();
            _htmlHelper = htmlHelper;
            _formName = formModel.FormName;
            _formClass = (_formName + "Form");
            _formResultID = _formID + "_Result";
            _omitForm = _htmlHelper.ViewData.ContainsKey(FormActionAttribute.OmitFormTag);
            if (!_omitForm) {
                htmlHelper.BeginForm(_formName, FormControllerName, FormMethod.Post, new {id = _formID, @class = _formClass + (!string.IsNullOrWhiteSpace(clientFormClass) ? (" " + clientFormClass) : string.Empty) });
                Write(htmlHelper.HiddenFor(o => o.ID, new { @Value = formModel.ID.ToString()}));
            } else {                
                //if (!htmlHelper.ViewData.ModelState.IsValid) {
                //    Write(ErrorDivHtml, htmlHelper.ValidationSummary());
                //}
            }
        }

        public void Dispose() {
            if (!_omitForm) {
                _htmlHelper.EndForm();
                Write(ResultDivHtml, _formResultID);
                Write(Javascript.FormatWith(_formID, _formResultID, "/Content/images/bx_loader.gif"));
                Write(
                    _htmlHelper
                        .Awe()
                        .Form()
                        .FormClass(_formClass)
                        .Confirm(false)
                        .Success("F" + _formID + "_Success")
                        .FillFormOnContent(true)
                        .BeforeSubmit("F" + _formID + "_BeforeSubmit")
                        
                        
                );
            }
        }


        private void Write(IHtmlString text) {
            Write(text.ToHtmlString());
        }

        private void Write(string text, params object[] formatArgs) {
            for (var i = 0; i < formatArgs.Length; i++) {
                if (formatArgs[i] is IHtmlString)
                    formatArgs[i] = ((IHtmlString) formatArgs[i]).ToHtmlString();
            }
            if (formatArgs.Length == 0)
                _htmlHelper.ViewContext.Writer.Write(text);
            else
                _htmlHelper.ViewContext.Writer.Write(text, formatArgs);
        }
    }
}