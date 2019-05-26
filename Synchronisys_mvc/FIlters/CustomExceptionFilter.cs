using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Filters;

namespace Synchronisys_mvc.FIlters
{
    public class CustomExceptionFilter:ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            var response = new HttpResponseMessage(HttpStatusCode.BadRequest)
            {
                Content = new StringContent("{\"StatusCode\":0,\"Description\":\"Unable to process your request.\"}")
            };
            throw new HttpResponseException(response);
        }
    }
}