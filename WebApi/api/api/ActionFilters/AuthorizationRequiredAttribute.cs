using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace api.ActionFilters
{
    public class AuthorizationRequiredAttribute : ActionFilterAttribute
    {
        private const string Token = "token";

        public override void OnActionExecuting(HttpActionContext filterContext)
        {
            //  Get API key provider


            //if (filterContext.ActionArguments.Count > 0)
            //{


            //    foreach (var arg in filterContext.ActionArguments)
            //    {
            //        var obj = arg.Value;
            //        if (arg.Key.ToUpper() == "TOKEN")
            //        {
            //            string tokenValue = arg.Value.ToString();

            //            // Validate Token
            //            TokenService svc = new TokenService();

            //            string statusToken = svc.ValidateToken(tokenValue);
            //            var responseMessage = new HttpResponseMessage(HttpStatusCode.Unauthorized);

            //            switch (statusToken.ToLower())
            //            {
            //                case "expired":
            //                    responseMessage.ReasonPhrase = "Token Expired ";
            //                    filterContext.Response = responseMessage;
            //                    break;
            //                case "notfound":
            //                    responseMessage.ReasonPhrase = "Unauthorized";
            //                    filterContext.Response = responseMessage;
            //                    break;
            //            }

            //            break;
            //        }


            //    }
            //}
            //else
            //{
            //    filterContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
            //}

            //base.OnActionExecuting(filterContext);





            if (filterContext.Request.Headers.Contains(Token))
            {
                var tokenValue = filterContext.Request.Headers.GetValues(Token).First();
                if (tokenValue != "1")
                {
                    filterContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
                }

                // Validate Token
                //TokenServices svc = new TokenServices();
                //if (!svc.ValidateToken(tokenValue))
                //{
                //    var responseMessage = new HttpResponseMessage(HttpStatusCode.Unauthorized) { ReasonPhrase = "Invalid Request" };
                //    filterContext.Response = responseMessage;
                //}
            }
            else
            {
                filterContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
            }

            base.OnActionExecuting(filterContext);

        }
    }
}