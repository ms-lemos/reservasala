using System.Web.Mvc;
using JetBrains.Annotations;

namespace Reserva.Web.Helpers
{
    public static class HmtlHelperExtensions
    {
        public static string IsSelected(this HtmlHelper html, [AspMvcController]string controller = null, [AspMvcAction]string action = null)
        {
            const string cssClass = "active";
            var routeData = html.ViewContext.RouteData;
            var currentAction = (string)routeData.Values["action"];
            var currentController = (string)routeData.Values["controller"];

            if (string.IsNullOrEmpty(controller))
                controller = currentController;

            if (string.IsNullOrEmpty(action))
                action = currentAction;

            return controller == currentController && action == currentAction ?
                cssClass : string.Empty;
        }

        public static string PageClass(this HtmlHelper html)
        {
            var routeData = html.ViewContext.RouteData;
            var currentAction = (string)routeData.Values["action"];
            return currentAction;
        }
    }
}
