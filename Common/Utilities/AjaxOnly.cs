using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Utilities
{
    public static class HttpRequestExtensionMethods
    {

        public static bool IsAjax(this HttpRequest request, string httpVerb = "")
        {
            if (request == null)
            {
                throw new ArgumentNullException("Request object is Null.");
            }

            if (!string.IsNullOrEmpty(httpVerb))
            {
                if (request.Method != httpVerb)
                {
                    return false;
                }
            }

            if (request.Headers != null)
            {
                return request.Headers["X-Requested-With"] == "XMLHttpRequest";
            }

            return false;
        }
    }
    public class AjaxAttribute : ActionMethodSelectorAttribute
    {
        public string HttpVerb { get; set; }

        public override bool IsValidForRequest
    (RouteContext routeContext, ActionDescriptor action)
        {
            return routeContext.HttpContext.
    Request.IsAjax(HttpVerb);
        }
    }
}
