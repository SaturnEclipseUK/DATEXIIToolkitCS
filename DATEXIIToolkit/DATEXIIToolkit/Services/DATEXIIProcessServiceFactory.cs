using DATEXIIToolkit.Common;
using DATEXIIToolkit.DATEXII;
using DATEXIIToolkit.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DATEXIIToolkit.Services
{
    /// <summary>
    /// This service factory returns the correct DATEX II process service for given Feed Type.
    /// </summary>
    public class DATEXIIProcessServiceFactory
    {
        private LogWrapper logWrapper;
        private volatile static DATEXIIProcessServiceFactory instance;

        private DATEXIIVMSProcessService datexIIVMSProcessService;
        private DATEXIIANPRProcessService datexIIANPRProcessService;
        private DATEXIIMIDASProcessService datexIIMIDASProcessService;
        private DATEXIITMUProcessService datexIITMUProcessService;
        private DATEXIIFusedSensorOnlyProcessService datexIIFusedSensorOnlyProcessService;
        private DATEXIIFusedFVDAndSensorProcessService datexIIFusedFVDAndSensorProcessService;
        private DATEXIIModelUpdateNotificationProcessService datexIIModelUpdateNotificationProcessService;
        private DATEXIINetworkModelUpdateService datexIINetworkModelUpdateService;
        private DATEXIIEventProcessService datexIIEventProcessService;
        private DATEXIINTISModelVMSProcessService datexIINTISModelVMSProcessService;
        private DATEXIINTISModelMeasurementSitesProcessService datexIINTISModelMeasurementSitesProcessService;
        private DATEXIINTISModelPredefinedLocationProcessService datexIINTISModelPredefinedLocationProcessService;

        private DATEXIIProcessServiceFactory(DATEXIIUpdateService datexIIUpdateService) {
            logWrapper = new LogWrapper("DATEXIIProcessServiceFactory");
            logWrapper.Info("Creating DATEXIIProcessServiceFactory object");

            datexIIVMSProcessService = new DATEXIIVMSProcessService();
            datexIIANPRProcessService = new DATEXIIANPRProcessService();
            datexIIMIDASProcessService = new DATEXIIMIDASProcessService();
            datexIITMUProcessService = new DATEXIITMUProcessService();
            datexIIFusedSensorOnlyProcessService = new DATEXIIFusedSensorOnlyProcessService();
            datexIIFusedFVDAndSensorProcessService = new DATEXIIFusedFVDAndSensorProcessService();
            datexIINetworkModelUpdateService = new DATEXIINetworkModelUpdateService(datexIIUpdateService);
            datexIIModelUpdateNotificationProcessService = new DATEXIIModelUpdateNotificationProcessService(this);
            datexIIEventProcessService = new DATEXIIEventProcessService();
            datexIINTISModelVMSProcessService = new DATEXIINTISModelVMSProcessService();
            datexIINTISModelMeasurementSitesProcessService = new DATEXIINTISModelMeasurementSitesProcessService();
            datexIINTISModelPredefinedLocationProcessService = new DATEXIINTISModelPredefinedLocationProcessService();
        }

        public enum DATA_SERVICE_TYPE
        {
            VMS,
            ANPR,
            MIDAS,
            TMU,
            FUSED_SENSOR_ONLY,
            FUSED_FVD_AND_SENSOR_PTD,
            NTIS_MODEL_UPDATE_NOTIFICATION,
            NWK_MODEL_UPDATE,
            EVENT,
            NTIS_MODEL_VMS_TABLES,
            NTIS_MODEL_MEASUREMENT_SITES,
            NTIS_MODEL_PREDEFINED_LOCATIONS
        }

        public static DATEXIIProcessServiceFactory GetInstance(DATEXIIUpdateService datexIIUpdateService)
        {
            if (instance == null)
            { 
                instance = new DATEXIIProcessServiceFactory(datexIIUpdateService);
            }
            return instance;
        }

        public DATEXIIProcessService getServiceType(string feedType)
        {
            DATEXIIProcessService datexiiProcessService;
            switch (feedType)
            {
                case "VMS":
                    datexiiProcessService = getDATEXIIProcessService(DATA_SERVICE_TYPE.VMS);
                    break;
                case "ANPR":
                    datexiiProcessService = getDATEXIIProcessService(DATA_SERVICE_TYPE.ANPR);
                    break;
                case "MIDAS":
                    datexiiProcessService = getDATEXIIProcessService(DATA_SERVICE_TYPE.MIDAS);
                    break;
                case "TMU":
                    datexiiProcessService = getDATEXIIProcessService(DATA_SERVICE_TYPE.TMU);
                    break;
                case "FUSED_SENSOR_ONLY":
                    datexiiProcessService = getDATEXIIProcessService(DATA_SERVICE_TYPE.FUSED_SENSOR_ONLY);
                    break;
                case "FUSED_FVD_AND_SENSOR_PTD":
                    datexiiProcessService = getDATEXIIProcessService(DATA_SERVICE_TYPE.FUSED_FVD_AND_SENSOR_PTD);
                    break;
                case "NTIS_MODEL_UPDATE_NOTIFICATION":
                    datexiiProcessService = getDATEXIIProcessService(DATA_SERVICE_TYPE.NTIS_MODEL_UPDATE_NOTIFICATION);
                    break;
                case "EVENT":
                    datexiiProcessService = getDATEXIIProcessService(DATA_SERVICE_TYPE.EVENT);
                    break;
                case "NTIS_MODEL_VMS_TABLES":
                    datexiiProcessService = getDATEXIIProcessService(DATA_SERVICE_TYPE.NTIS_MODEL_VMS_TABLES);
                    break;
                case "NTIS_MODEL_MEASUREMENT_SITES":
                    datexiiProcessService = getDATEXIIProcessService(DATA_SERVICE_TYPE.NTIS_MODEL_MEASUREMENT_SITES);
                    break;
                case "NTIS_MODEL_PREDEFINED_LOCATIONS":
                    datexiiProcessService = getDATEXIIProcessService(DATA_SERVICE_TYPE.NTIS_MODEL_PREDEFINED_LOCATIONS);
                    break;
                default:
                    datexiiProcessService = null;
                    logWrapper.Error("Unknown Feed Type Received:(" + feedType + ")");
                    break;
            }
            return datexiiProcessService;
        }

        public DATEXIIProcessService getDATEXIIProcessService(DATA_SERVICE_TYPE feedType)
        {
            DATEXIIProcessService datexiiProcessService = null;
            switch (feedType)
            {
                case DATA_SERVICE_TYPE.VMS:
                    datexiiProcessService = datexIIVMSProcessService;
                    break;
                case DATA_SERVICE_TYPE.ANPR:
                    datexiiProcessService = datexIIANPRProcessService;
                    break;
                case DATA_SERVICE_TYPE.MIDAS:
                    datexiiProcessService = datexIIMIDASProcessService;
                    break;
                case DATA_SERVICE_TYPE.TMU:
                    datexiiProcessService = datexIITMUProcessService;
                    break;
                case DATA_SERVICE_TYPE.FUSED_SENSOR_ONLY:
                    datexiiProcessService = datexIIFusedSensorOnlyProcessService;
                    break;
                case DATA_SERVICE_TYPE.FUSED_FVD_AND_SENSOR_PTD:
                    datexiiProcessService = datexIIFusedFVDAndSensorProcessService;
                    break;
                case DATA_SERVICE_TYPE.NTIS_MODEL_UPDATE_NOTIFICATION:
                    datexiiProcessService = datexIIModelUpdateNotificationProcessService;
                    break;
                case DATA_SERVICE_TYPE.NWK_MODEL_UPDATE:
                    datexiiProcessService = datexIINetworkModelUpdateService;
                    break;
                case DATA_SERVICE_TYPE.EVENT:
                    datexiiProcessService = datexIIEventProcessService;
                    break;
                case DATA_SERVICE_TYPE.NTIS_MODEL_VMS_TABLES:
                    datexiiProcessService = datexIINTISModelVMSProcessService;
                    break;
                case DATA_SERVICE_TYPE.NTIS_MODEL_MEASUREMENT_SITES:
                    datexiiProcessService = datexIINTISModelMeasurementSitesProcessService;
                    break;
                case DATA_SERVICE_TYPE.NTIS_MODEL_PREDEFINED_LOCATIONS:
                    datexiiProcessService = datexIINTISModelPredefinedLocationProcessService;
                    break;
                default:
                    logWrapper.Error("Unknown Feed Type Received");
                    break;
            }
            return datexiiProcessService;
        }
    }
}