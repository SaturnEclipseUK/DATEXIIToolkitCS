using DATEXIIToolkit.Common;
using DATEXIIToolkit.Models;
using DATEXIIToolkit.Models.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace DATEXIIToolkit.Services
{
    /// <summary>
    /// This service processes FusedFVDAndSensor DATEX II v2 messages (D2LogicalModel).
    /// The payloads are inserted into the FusedFVDAndSensor data store.
    /// </summary>
    public class DATEXIIFusedFVDAndSensorProcessService : DATEXIIProcessService
    {
        private LogWrapper logWrapper;
		private FusedFVDAndSensorDataStore fusedFVDAndSensorDataStore;

        public DATEXIIFusedFVDAndSensorProcessService() : base()
        {
            logWrapper = new LogWrapper("DATEXIIFusedFVDAndSensorProcessService");
            fusedFVDAndSensorDataStore = (FusedFVDAndSensorDataStore)DataStoreFactory.GetInstance().GetDataStore(DataStoreFactory.DATA_STORES.FUSED_FVD_AND_SENSOR_DATA_STORE);
        }

        public override void processMessage(D2LogicalModel d2LogicalModel)
        {
            if (logWrapper.isDebug())
            {
                logWrapper.Debug("FusedFVDAndSensorData Update");
            }

            fusedFVDAndSensorDataStore.clearDataStore();

            ElaboratedDataPublication elaboratedDataPublication = (ElaboratedDataPublication)d2LogicalModel.payloadPublication;

            if (elaboratedDataPublication != null)
            {
                DateTime publicationTime = elaboratedDataPublication.publicationTime;
                DateTime timeDefault = elaboratedDataPublication.timeDefault;
                ElaboratedData[] elaboratedDataList = elaboratedDataPublication.elaboratedData;
                
                if (logWrapper.isDebug())
                {
                    logWrapper.Debug("FusedFVDAndSensorData Update(" + elaboratedDataList.Length + " objects)");
                }

                for (int elaboratedDataListPos = 0; elaboratedDataListPos < elaboratedDataList.Length; elaboratedDataListPos++)
                {
                    ElaboratedData elaboratedData = elaboratedDataList[elaboratedDataListPos];
                    processSituation(elaboratedData, publicationTime, timeDefault);
                }
            }

            if (logWrapper.isDebug())
            {
                logWrapper.Debug("FusedFVDAndSensorData Update Complete");
            }
        }

        private void processSituation(ElaboratedData elaboratedData, DateTime publicationTime, DateTime timeDefault)
        {

            LocationByReference locationByReference = null;
            BasicData basicData = elaboratedData.basicData;

            if (basicData.GetType() == typeof(TrafficHeadway))
            {
                TrafficHeadway data = (TrafficHeadway)basicData;
                locationByReference = (LocationByReference)data.pertinentLocation;
            }else if (basicData.GetType() == typeof(TrafficFlow)){
                TrafficFlow data = (TrafficFlow)basicData;
                locationByReference = (LocationByReference)data.pertinentLocation;
            }else if (basicData.GetType() == typeof(TrafficConcentration)){
                TrafficConcentration data = (TrafficConcentration)basicData;
                locationByReference = (LocationByReference)data.pertinentLocation;
            }else if (basicData.GetType() == typeof(TrafficSpeed)){
                TrafficSpeed data = (TrafficSpeed)basicData;
                locationByReference = (LocationByReference)data.pertinentLocation;
            }else if (basicData.GetType() == typeof(TravelTimeData)){
                TravelTimeData data = (TravelTimeData)basicData;
                locationByReference = (LocationByReference)data.pertinentLocation;
            }else{
                logWrapper.Warning("basicData instance of -" + basicData.GetType().ToString());
            }

            String linkIdentifier = null;
            if (locationByReference != null)
            {
                linkIdentifier = basicData.GetType().ToString() + locationByReference.predefinedLocationReference.id;

                if (logWrapper.isDebug())
                {
                    logWrapper.Debug("Processing Fused FVD And Sensor Identifier(" + linkIdentifier + ")");
                }

                FusedFVDAndSensorData fusedFVDAndSensorData = new FusedFVDAndSensorData(linkIdentifier, publicationTime, timeDefault, elaboratedData);
                fusedFVDAndSensorDataStore.updateData(fusedFVDAndSensorData);
            }
            else {
                logWrapper.Error("Failed to Process elaboratedData, " + elaboratedData.ToString());
            }
        }
    }
}