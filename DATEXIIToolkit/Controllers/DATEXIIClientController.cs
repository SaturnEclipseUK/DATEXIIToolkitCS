using DATEXIIToolkit.Common;
using DATEXIIToolkit.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Timers;
using System.Web.Http;

namespace DATEXIIToolkit.Controllers
{
    /// <summary>
    /// A DATEX II HTTP controller to receive raw XML strings and add to the update service queue.
    /// </summary>
    public class DATEXIIClientController : ApiController
    {
        private static LogWrapper logWrapper;
        DATEXIIUpdateService datexIIUpdateService;

        public DATEXIIClientController() : base()
        {
            logWrapper = new LogWrapper("DATEXIIClientController");
            datexIIUpdateService = DATEXIIUpdateService.GetInstance();
        }

        [Route("subscriber/update")]
        [HttpPost]
        public HttpResponseMessage Post(HttpRequestMessage request)
        {
            try {
                var content = request.Content;
                byte[] xml;
                if (request.Content.Headers.Contains("Content-Encoding") 
                    && request.Content.Headers.GetValues("Content-Encoding").Contains("gzip"))
                {
                    xml = Decompress(content.ReadAsByteArrayAsync().Result);                    
                } else
                {
                    xml = content.ReadAsByteArrayAsync().Result;
                }
                
                datexIIUpdateService.addToMessageQueue(xml);
            } catch (Exception e)
            {
                logWrapper.Error(e.ToString());
            }
            HttpResponseMessage httpResponseMessage = request.CreateResponse(HttpStatusCode.OK);           
            
            return httpResponseMessage;
        }

        static byte[] Decompress(byte[] data)
        {
            using (var compressedStream = new MemoryStream(data))
            using (var zipStream = new GZipStream(compressedStream, CompressionMode.Decompress))
            using (var resultStream = new MemoryStream())
            {
                zipStream.CopyTo(resultStream);
                return resultStream.ToArray();
            }
        }
    }
}
