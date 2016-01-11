using DATEXIIToolkit.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace DATEXIIToolkit.Services
{
    public class DATEXIIModelUpdateNotificationProcessService : DATEXIIProcessService
    {
        LogWrapper logWrapper;

        DATEXIINetworkModelUpdateService datexiiNetworkModelUpdateService;
        private string ntisNetworkModelBaseURL;
        private string networkModelFolder;

        public DATEXIIModelUpdateNotificationProcessService(DATEXIIProcessServiceFactory datexIIProcessServiceFactory) : base()
        {
            logWrapper = new LogWrapper("DATEXIIModelUpdateNotificationProcessService");
            ntisNetworkModelBaseURL = ConfigurationManager.AppSettings["ntisNetworkModelBaseURL"];
            networkModelFolder = ConfigurationManager.AppSettings["nwkModelDirectory"];
            datexiiNetworkModelUpdateService = (DATEXIINetworkModelUpdateService)datexIIProcessServiceFactory.getDATEXIIProcessService(DATEXIIProcessServiceFactory.DATA_SERVICE_TYPE.NWK_MODEL_UPDATE);
        }

        public override void processMessage(D2LogicalModel d2LogicalModel)
        {
            GenericPublication genericPublication = (GenericPublication)d2LogicalModel.payloadPublication;
            NtisModelVersionInformation ntisModelVersionInformation = genericPublication.genericPublicationExtension.ntisModelVersionInformation;

            DateTime publicationTime = ntisModelVersionInformation.modelPublicationTime;
            String modelVersion = ntisModelVersionInformation.modelVersion;
            String modelFilename = ntisModelVersionInformation.modelFilename;

            if (ntisNetworkModelBaseURL != null)
            {
                datexiiNetworkModelUpdateService.updateNetworkModel(ntisNetworkModelBaseURL, networkModelFolder,  modelFilename);
            }
            else {
                logWrapper.Error("NTIS_NETWORK_MODEL_BASE_URL is not set in application.properties file");
            }
        }
    }
}

/*

@Service
public class DATEXIIModelUpdateNotificationProcessService extends DATEXIIProcessService {

	final Logger log = LoggerFactory.getLogger(DATEXIIModelUpdateNotificationProcessService.class);
	
	@Autowired
	DATEXIINetworkModelUpdateService datexiiNetworkModelUpdateService;
	
	@Value("${ntisNwkModelBaseUrl}")
	private String url;
	
	@Override
	public void processMessage(D2LogicalModel d2LogicalModel) {
		
		GenericPublication genericPublication = (GenericPublication)d2LogicalModel.getPayloadPublication();
		NtisModelVersionInformation ntisModelVersionInformation = genericPublication.getGenericPublicationExtension().getNtisModelVersionInformation();
		
		Date publicationTime = ntisModelVersionInformation.getModelPublicationTime().toGregorianCalendar().getTime();
		String modelVersion = ntisModelVersionInformation.getModelVersion();
		String modelFilename = ntisModelVersionInformation.getModelFilename();
		
		if (url != null){
			datexiiNetworkModelUpdateService.updateNetworkModel(url+modelFilename);	
		} else {
			log.error("NTIS_NETWORK_MODEL_BASE_URL is not set in application.properties file");
		}
	}

}

*/
