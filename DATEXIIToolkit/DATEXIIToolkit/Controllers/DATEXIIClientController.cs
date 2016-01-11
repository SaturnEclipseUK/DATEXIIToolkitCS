using DATEXIIToolkit.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Timers;
using System.Web.Http;

namespace DATEXIIToolkit.Controllers
{
    public class DATEXIIClientController : ApiController
    {
        DATEXIIUpdateService datexIIUpdateService;

        public DATEXIIClientController() : base()
        {
            datexIIUpdateService = DATEXIIUpdateService.GetInstance();
        }
        
        [HttpPost]
        public HttpResponseMessage Post(HttpRequestMessage request)
        {
            var content = request.Content;
            byte[] xml = content.ReadAsByteArrayAsync().Result;

            HttpResponseMessage httpResponseMessage = request.CreateResponse(HttpStatusCode.OK);
            HttpContentHeaders headers = request.Content.Headers;
            datexIIUpdateService.addToMessageQueue(xml);

            return httpResponseMessage;
        }
    }

/*
    @RestController
    @RequestMapping("/subscriber")
public class DATEXIIClientController
    {
        final Logger logger = LoggerFactory.getLogger(DATEXIIClientController.class);

	@Autowired
    DATEXIIUpdateService datexIIUpdateService;


    @RequestMapping(value = "/update", method = RequestMethod.POST)

    public void update(@RequestBody String xml)
        {
            if (logger.isDebugEnabled())
            {
                logger.debug("DATEXII Update Message Received");
                if (logger.isTraceEnabled())
                {
                    logger.trace("DATEXII Update Message Received(Message Length = " + new Integer(xml.length()).toString() + ")");
                }
            }

            datexIIUpdateService.addToMessageQueue(xml);
            return;
        }
    }
    */

}
