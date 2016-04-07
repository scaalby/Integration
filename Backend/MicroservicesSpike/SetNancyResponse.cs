using Nancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MicroservicesSpike
{
    public static class SetNancyResponse
    {
        public static Response NancyResponse(Nancy.HttpStatusCode status, string message, string json)
        {
            var res = new Response();
            if (json != null)
                res = json;

            res.ContentType = "text/json; charset= utf-8";
            res.StatusCode = status;
            if (message != null)
                res.ReasonPhrase = message;
            return res;
            
        }
    }
}