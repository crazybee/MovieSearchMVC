using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace MovieWebApi.UnitOfWork.Workers
{
    public class HttpActionResult : IHttpActionResult
    {

        /// <summary>
        /// 
        /// </summary>
        public HttpActionResult()
        {

        }

        /// <summary>
        /// message of the response
        /// </summary>
        public readonly string Message;
        /// <summary>
        /// http status code
        /// </summary>
        public readonly HttpStatusCode StatusCode;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="statusCode"></param>
        /// <param name="message"></param>
        public HttpActionResult(HttpStatusCode statusCode, string message)
        {
            StatusCode = statusCode;
            Message = message;
        }

        Task<HttpResponseMessage> IHttpActionResult.ExecuteAsync(CancellationToken cancellationToken)
        {
            HttpResponseMessage response = new HttpResponseMessage(StatusCode)
            {
                Content = new StringContent(Message)
            };
            return Task.FromResult(response);
        }

        /// <inheritdoc />
        public string GetMessage()
        {
            return Message;
        }
        public HttpStatusCode GetStatusCode()
        {
            return StatusCode;
        }
    }
}