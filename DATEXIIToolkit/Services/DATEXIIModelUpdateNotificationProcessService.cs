using DATEXIIToolkit.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Timers;
using System.Web;

namespace DATEXIIToolkit.Services
{
    /// <summary>
    /// This service processes ModelUpdateNotification DATEX II v2 messages (D2LogicalModel).
    /// When a notification is received, the Network Model Update function is invoked.  
    /// </summary>
    public class DATEXIIModelUpdateNotificationProcessService : DATEXIIProcessService
    {
        static LogWrapper logWrapper;

        static DATEXIINetworkModelUpdateService datexiiNetworkModelUpdateService;
        private static bool loadNwkModelOnStartup;
        static private string ntisNwkModelUsername;
        static private string ntisNwkModelPassword;
        static private string ntisNetworkModelBaseURL;
        private string networkModelFolder;
        static private System.Timers.Timer networkModelRetryTimer;
        private const int MAX_NUMBER_OF_NETWORK_MODEL_RETRIES = 5;
        static private int numberOfNetworkModelRetries;
        const int DOWNLOAD_NETWORK_MODEL_RETRY_PERIOD = 60000;

        public DATEXIIModelUpdateNotificationProcessService(DATEXIIProcessServiceFactory datexIIProcessServiceFactory) : base()
        {
            logWrapper = new LogWrapper("DATEXIIModelUpdateNotificationProcessService");
            loadNwkModelOnStartup = ConfigurationManager.AppSettings["loadNwkModelOnStartup"].Equals("true");
            ntisNwkModelUsername = ConfigurationManager.AppSettings["ntisNwkModelUsername"];
            ntisNwkModelPassword = ConfigurationManager.AppSettings["ntisNwkModelPassword"];
            ntisNetworkModelBaseURL = ConfigurationManager.AppSettings["ntisNetworkModelBaseURL"];
            networkModelFolder = ConfigurationManager.AppSettings["nwkModelDirectory"];
            datexiiNetworkModelUpdateService = (DATEXIINetworkModelUpdateService)datexIIProcessServiceFactory.getDATEXIIProcessService(DATEXIIProcessServiceFactory.DATA_SERVICE_TYPE.NWK_MODEL_UPDATE);
            networkModelRetryTimer = new System.Timers.Timer();
            networkModelRetryTimer.Elapsed += new ElapsedEventHandler(updateNetworkModel);
            networkModelRetryTimer.Interval = DOWNLOAD_NETWORK_MODEL_RETRY_PERIOD;
        }

        public void initialise()
        {
            logWrapper.Info("Initialise network model update service");
            numberOfNetworkModelRetries = MAX_NUMBER_OF_NETWORK_MODEL_RETRIES;
            updateNetworkModel(null, null);
        }

        public override void processMessage(D2LogicalModel d2LogicalModel)
        {
            GenericPublication genericPublication = (GenericPublication)d2LogicalModel.payloadPublication;
            NtisModelVersionInformation ntisModelVersionInformation = genericPublication.genericPublicationExtension.ntisModelVersionInformation;

            DateTime publicationTime = ntisModelVersionInformation.modelPublicationTime;
            String modelVersion = ntisModelVersionInformation.modelVersion;
            String modelFilename = ntisModelVersionInformation.modelFilename;

            numberOfNetworkModelRetries = MAX_NUMBER_OF_NETWORK_MODEL_RETRIES;

            if (ntisNetworkModelBaseURL != null)
            {
                updateNetworkModel(null, null);
                //datexiiNetworkModelUpdateService.updateNetworkModel(ntisNetworkModelBaseURL, ntisNwkModelUsername, ntisNwkModelPassword);
            }
            else {
                logWrapper.Error("NTIS_NETWORK_MODEL_BASE_URL is not set in application.properties file");
            }
        }

        private static void updateNetworkModel(object source, ElapsedEventArgs e)
        {
            if (loadNwkModelOnStartup)
            {
                bool loadedNetworkModel = datexiiNetworkModelUpdateService.updateNetworkModel(ntisNetworkModelBaseURL, ntisNwkModelUsername, ntisNwkModelPassword);
                if (loadedNetworkModel == false)
                {
                    if (numberOfNetworkModelRetries > 0) {
                        logWrapper.Info("Download Network Model Retries remaining: " + numberOfNetworkModelRetries);
                        numberOfNetworkModelRetries--;
                        networkModelRetryTimer.Enabled = true;
                    }
                }
                else
                {
                    networkModelRetryTimer.Enabled = false;
                }
                
            }
        }
    }
}