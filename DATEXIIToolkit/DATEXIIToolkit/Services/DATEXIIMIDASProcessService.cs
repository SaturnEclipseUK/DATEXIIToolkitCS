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
    /// This service processes MIDAS DATEX II v2 messages (D2LogicalModel).
    /// The payloads are inserted into the MIDAS data store.
    /// </summary>
    public class DATEXIIMIDASProcessService : DATEXIIProcessService
    {
        LogWrapper logWrapper;
        private MIDASDataStore midasDataStore;

        public DATEXIIMIDASProcessService() : base()
        {
            logWrapper = new LogWrapper("DATEXIIMIDASProcessService");
            midasDataStore = (MIDASDataStore)DataStoreFactory.GetInstance().GetDataStore(DataStoreFactory.DATA_STORES.MIDAS_DATA_STORE);
        }

        public override void processMessage(D2LogicalModel d2LogicalModel)
        {
            if (logWrapper.isDebug())
            {
                logWrapper.Debug("MIDAS Update");
            }
            
            MeasuredDataPublication measuredDataPublication = (MeasuredDataPublication)d2LogicalModel.payloadPublication;

            if (measuredDataPublication != null)
            {
                DateTime publicationTime = measuredDataPublication.publicationTime;

                SiteMeasurements[] siteMeasurementsList = measuredDataPublication.siteMeasurements;

                if (logWrapper.isDebug())
                {
                    logWrapper.Debug("MIDAS Update(" + siteMeasurementsList.Length + " objects)");
                }

                for (int siteMeasurementsListPos = 0; siteMeasurementsListPos < siteMeasurementsList.Length; siteMeasurementsListPos++)
                {
                    SiteMeasurements siteMeasurements = siteMeasurementsList[siteMeasurementsListPos];
                    processSituation(siteMeasurements, publicationTime);
                }
            }

            if (logWrapper.isDebug())
            {
                logWrapper.Debug("MIDAS Update Complete");
            }
        }

        private void processSituation(SiteMeasurements siteMeasurements, DateTime publicationTime)
        {
            String midasIdentifier = siteMeasurements.measurementSiteReference.id;

            if (logWrapper.isTrace())
            {
                logWrapper.Trace("Processing MIDAS Identifier(" + midasIdentifier + ")");
            }

            MIDASData midasData = new MIDASData(midasIdentifier, publicationTime, siteMeasurements);

            midasDataStore.updateData(midasData);
        }
    }
}