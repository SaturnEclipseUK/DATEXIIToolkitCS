using DATEXIIToolkit.Common;
using DATEXIIToolkit.Models;
using DATEXIIToolkit.Models.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DATEXIIToolkit.Services
{
    /// <summary>
    /// This service processes TMU DATEX II v2 messages (D2LogicalModel).
    /// The payloads are inserted into the TMU data store.
    /// </summary>
    public class DATEXIITMUProcessService : DATEXIIProcessService
    {
        private LogWrapper logWrapper;
        private TMUDataStore tmuDataStore;

        public DATEXIITMUProcessService() : base() 
        {
            logWrapper = new LogWrapper("DATEXIITMUProcessService");
            tmuDataStore = (TMUDataStore)DataStoreFactory.GetInstance().GetDataStore(DataStoreFactory.DATA_STORES.TMU_DATA_STORE);
        }

        public override void processMessage(D2LogicalModel d2LogicalModel)
        {
            if (logWrapper.isInfo())
            {
                logWrapper.Info("TMU Update");
            }

            MeasuredDataPublication measuredDataPublication = (MeasuredDataPublication)d2LogicalModel.payloadPublication;

            if (measuredDataPublication != null)
            {

                DateTime publicationTime = measuredDataPublication.publicationTime;

                SiteMeasurements[] siteMeasurementsList = measuredDataPublication.siteMeasurements;
                
                if (logWrapper.isDebug())
                {
                    logWrapper.Debug("TMU Update(" + siteMeasurementsList.Length + " objects)");
                }

                Dictionary<String, LinkedList<SiteMeasurements>> siteMeasurementsIndex = 
                    new Dictionary<String, LinkedList<SiteMeasurements>>();

                for (int siteMeasurementsListPos = 0; siteMeasurementsListPos < siteMeasurementsList.Length; siteMeasurementsListPos++)
                {
                    SiteMeasurements siteMeasurements = siteMeasurementsList[siteMeasurementsListPos];
                    processSituation(siteMeasurements, publicationTime, siteMeasurementsIndex);
                }

                foreach (String tmuIdentifier in siteMeasurementsIndex.Keys)
                {
                    TMUData tmuData = new TMUData(tmuIdentifier, publicationTime, siteMeasurementsIndex[tmuIdentifier]);

                    tmuDataStore.updateData(tmuData);
                }
            }

            if (logWrapper.isDebug())
            {
                logWrapper.Debug("TMU Update Complete");
            }
        }

        private void processSituation(SiteMeasurements siteMeasurements, DateTime publicationTime,
            Dictionary<String, LinkedList<SiteMeasurements>> siteMeasurementsIndex)
        {
            String tmuIdentifier = siteMeasurements.measurementSiteReference.id;

            if (logWrapper.isDebug())
            {
                logWrapper.Debug("Processing TMU Identifier(" + tmuIdentifier + ")");
            }

            LinkedList<SiteMeasurements> siteMeasurementsList;
            if (siteMeasurementsIndex.ContainsKey(tmuIdentifier))
            {
                siteMeasurementsList = siteMeasurementsIndex[tmuIdentifier];
            }
            else {
                siteMeasurementsList = new LinkedList<SiteMeasurements>();
                siteMeasurementsIndex.Add(tmuIdentifier, siteMeasurementsList);
            }
            siteMeasurementsList.AddLast(siteMeasurements);            
        }        
    }
}