using System.Linq;
using System.Web.Mvc;

namespace BlockchainSQL.Web.Code
{
    public class FormsShortcutViewEngine : RazorViewEngine {

        private static readonly string[] NEW_PARTIAL_VIEW_FORMATS = new[] {
            "~/Views/Forms/{0}.cshtml",
            "~/Views/Shared/Forms/{0}.cshtml"
        };

        public FormsShortcutViewEngine() {
            // Keep existing locations in sync
            base.PartialViewLocationFormats = base.PartialViewLocationFormats.Union(NEW_PARTIAL_VIEW_FORMATS).ToArray();
        }
    }
}