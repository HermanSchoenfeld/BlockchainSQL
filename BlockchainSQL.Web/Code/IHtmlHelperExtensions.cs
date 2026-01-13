// Copyright (c) Herman Schoenfeld 2018 - Present. All rights reserved. (https://sphere10.com/products/blockchainsql)
// Author: Herman Schoenfeld <herman@sphere10.com>
//
// Distributed under the GPLv3 software license, see the accompanying file LICENSE 
// or visit https://github.com/HermanSchoenfeld/blockchainsql/blob/master/LICENSE
//
// This notice must not be removed when duplicating this file or its contents, in whole or in part.

using System;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using BlockchainSQL.Web;
using Sphere10.Framework;
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
