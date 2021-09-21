using System;
using System.Linq;
using System.Threading.Tasks;
using BlockchainSQL.Web.Code;
using BlockchainSQL.Web.DataObjects;
using BlockchainSQL.Web.Models;
using Sphere10.Framework;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Threading;
using BlockchainSQL.Processing;
using Microsoft.AspNetCore.Http;
using Sphere10.Framework.Application;
using Sphere10.Framework.Web.AspNetCore;

namespace BlockchainSQL.Web.Controllers {
	public class QueryController : BaseController {
		
		// GET: Query
		public ActionResult Index(int? templateId) {
			if (templateId > 0) {
				if (!DatabaseManager.DataCache.Templates.ContainsKey(templateId.Value)) {
					AddPageMessage("Query template not found", "Error", PageMessageSeverity.Error);
					return View("Index", QueryPageModel.Default);
				}
				var template = DatabaseManager.DataCache.Templates[templateId.Value];
			
				return View("Index", new QueryPageModel(template.MSSQL));
			}
			
			return View("Index", QueryPageModel.Default);
		}

		[HttpPost]
		public async Task<ActionResult> Execute(string sql, int page, int pageSize) {
			// validate SQL
			var result = new QueryResultModel();
			try {
				if (sql.Contains("@@"))
					throw new InvalidOperationException("Queries cannot refer to database environment variables");

				var repo = DatabaseManager.GetBlockchainRepository();
				var start = DateTime.UtcNow;
				try {
					result.Result = await repo.Execute(sql, page, pageSize);
				} finally {
					var end = DateTime.UtcNow;
					result.ExecutedOn = start;
					result.ExecutionDuration = end - start;
				}

				if (GlobalSettings.Get<WebSettings>().SaveQueries) {
					var typedHeaders = Request.GetTypedHeaders();
					var executedQuery = new ExecutedQuery {
						Query = sql,
						PageNumber = page,
						PageSize = pageSize,
						IP = Request.HttpContext.Connection.RemoteIpAddress?.ToString(),
						ExecutedOn = result.ExecutedOn,
						ExecutionDurationMS = (int)Math.Round(result.ExecutionDuration.TotalMilliseconds, 0)
					};
					// Save in background
					ThreadPool.QueueUserWorkItem(_ => {
						try {
							var session = DatabaseManager.NhSessionFactory.OpenSession();
							session.SaveOrUpdate(executedQuery);
						} catch (Exception error) {
							SystemLog.Exception(error);
						}
					});
				}
			} catch (Exception error) {
				SystemLog.Exception(error);
				result.Messages.Add(new PageMessage {
					Title = "Error", Description = error.ToDisplayString(), Dismissable = false, Severity = PageMessageSeverity.Error
				});
			}
			return PartialView("Result", result);
		}

		[HttpPost]
		public async Task<ActionResult> Save(string sql) {
			if (string.IsNullOrWhiteSpace(sql)) {
				return Redirect("/Query");
			}
			sql = sql.Trim();
			using (var session = base.OpenSession()) {

				// Don't save query if already saved
				var queryHash = Convert.ToBase64String(Hashers.Hash(CHF.SHA1_160, Encoding.UTF8.GetBytes(sql)));
				var query = session.Query<SavedQuery>().SingleOrDefault(q => q.ContentHash == queryHash);
				if (query != null)
					return Json(new { WebID = query.WebID });

				// Save query
				query = new SavedQuery {
					SQL = sql,
					DBMS = SupportedDBMS.SQLServer,
					DateTime = DateTime.UtcNow,
					ContentHash = queryHash,
					Result = string.Empty
				};
				await Task.Run(() => session.SaveOrUpdate(query));
				query.WebID = UrlID.Generate((uint)query.ID);
				session.Flush();
				await Task.Run(() => session.SaveOrUpdate(query));
				return Json(new { WebID = query.WebID });
			}
		}

		public async Task<ActionResult> Load(string queryID) {
			using (var session = OpenSession()) {
				var savedQuery = session.Query<SavedQuery>().SingleOrDefault(q => q.WebID == queryID);
				if (savedQuery == null)
					return Redirect("/");

				SaveQueryLoad(DateTime.Now, savedQuery.ID);

				return View("Index", new QueryPageModel(savedQuery.SQL));
			}
		}

		public ActionResult LoadTemplate() {
			return PartialView(new LoadTemplateModel(DatabaseManager.DataCache.QueryCategoriesWithTemplates));
		}

		private void SaveQueryLoad(DateTime dateTime, int savedQueryID) {
			using (var session = base.OpenSession()) {
				var savedQueryLoad = new SavedQueryLoad {
					SavedQuery = session.Load<SavedQuery>(savedQueryID),
					LoadTimeUTC = DateTime.UtcNow
				};
				session.Save(savedQueryLoad);
			}
		}
	}
}
