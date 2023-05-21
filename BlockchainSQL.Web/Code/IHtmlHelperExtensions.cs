using System;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using BlockchainSQL.Web;
using Hydrogen;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BlockchainSQL.Web {
	public static class HtmlHelperExtensions {

		public static IDisposable BeginAweFormEx<T>(this IHtmlHelper<T> htmlHelper, T formModel, string formClass = null, bool resetOnSuccess = true) where T : AweFormModelBase, new() {
			return new AweFormScope<T>(htmlHelper, formModel ?? new T(), formClass, resetOnSuccess);
		}

		public static IDisposable BeginAweFormEx<T>(this IHtmlHelper<T> htmlHelper, string action, string controller, T formModel, string formClass = null, bool resetOnSuccess = true) where T : AweFormModelBase, new() {
			return new AweFormScope<T>(htmlHelper, action, controller, formModel ?? new T(), formClass, resetOnSuccess);
		}
	}
}
