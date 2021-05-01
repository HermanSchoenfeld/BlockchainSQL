using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using BlockchainSQL.Web.Models;

namespace BlockchainSQL.Web.Code
{
    public static class HtmlHelperExtensions {

        public static IDisposable BeginFormEx<T>(this HtmlHelper<T> htmlHelper, T formModel, string formClass = null) where T : FormModelBase, new() {
            return new FormScope<T>(htmlHelper, formModel ?? new T(), formClass);
        }

        //<div class="alert alert-warning">
        //Repellendus est, optio atque culpa in fuga quod iure ratione dolor nam laborum consectetur fugiat.Mollitia aliquam maiores sit facere error natus.
        //</div>
        public static MvcHtmlString ValidationMessageForEx<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression) {
            return helper.ValidationMessageFor(expression, null, new {@class = "label label-warning" });
        }

    }
}