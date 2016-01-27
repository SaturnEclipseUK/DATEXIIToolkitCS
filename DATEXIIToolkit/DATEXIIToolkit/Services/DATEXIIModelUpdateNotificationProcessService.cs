using DATEXIIToolkit.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;

namespace DATEXIIToolkit.Services
{
    /// <summary>
    /// This service processes ModelUpdateNotification DATEX II v2 messages (D2LogicalModel).
    /// When a notification is received, the Network Model Update function is invoked.  
    /// </summary>
    public class DATEXIIModelUpdateNotificationProcessService : DATEXIIProcessService
    {
        LogWrapper logWrapper;

        DATEXIINetworkModelUpdateService datexiiNetworkModelUpdateService;
        private bool loadNwkModelOnStartup;
        private string ntisNwkModelUsername;
        private string ntisNwkModelPassword;
        private string ntisNetworkModelBaseURL;
        private string networkModelFolder;

        public DATEXIIModelUpdateNotificationProcessService(DATEXIIProcessServiceFactory datexIIProcessServiceFactory) : base()
        {
            logWrapper = new LogWrapper("DATEXIIModelUpdateNotificationProcessService");
            loadNwkModelOnStartup = ConfigurationManager.AppSettings["loadNwkModelOnStartup"].Equals("true");
            ntisNwkModelUsername = ConfigurationManager.AppSettings["ntisNwkModelUsername"];
            ntisNwkModelPassword = ConfigurationManager.AppSettings["ntisNwkModelPassword"];
            ntisNetworkModelBaseURL = ConfigurationManager.AppSettings["ntisNetworkModelBaseURL"];
            networkModelFolder = ConfigurationManager.AppSettings["nwkModelDirectory"];
            datexiiNetworkModelUpdateService = (DATEXIINetworkModelUpdateService)datexIIProcessServiceFactory.getDATEXIIProcessService(DATEXIIProcessServiceFactory.DATA_SERVICE_TYPE.NWK_MODEL_UPDATE);
        }

        public void initialise()
        {
            logWrapper.Info("Initialise network model update service");
            
            if (loadNwkModelOnStartup)
            {
                datexiiNetworkModelUpdateService.updateNetworkModel(ntisNetworkModelBaseURL, ntisNwkModelUsername, ntisNwkModelPassword);
            }
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
                datexiiNetworkModelUpdateService.updateNetworkModel(ntisNetworkModelBaseURL, ntisNwkModelUsername, ntisNwkModelPassword);
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
