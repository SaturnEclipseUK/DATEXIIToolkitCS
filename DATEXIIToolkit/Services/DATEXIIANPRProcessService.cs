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
    /// This service processes ANPR DATEX II v2 messages (D2LogicalModel).
    /// The payloads are inserted into the ANPR data store.
    /// </summary>
    public class DATEXIIANPRProcessService : DATEXIIProcessService
    {
        private LogWrapper logWrapper;
	
	    private ANPRDataStore anprDataStore;

        public DATEXIIANPRProcessService() : base()
        {
            logWrapper = new LogWrapper("DATEXIIANPRProcessService");
            anprDataStore = (ANPRDataStore)DataStoreFactory.GetInstance().GetDataStore(DataStoreFactory.DATA_STORES.ANPR_DATA_STORE);
        }

        public override void processMessage(D2LogicalModel d2LogicalModel)
        {
            if (logWrapper.isDebug())
            {
                logWrapper.Debug("ANPR Update");
            }
            
            MeasuredDataPublication measuredDataPublication = (MeasuredDataPublication)d2LogicalModel.payloadPublication;

            if (measuredDataPublication != null)
            {
                DateTime publicationTime = measuredDataPublication.publicationTime;

                SiteMeasurements[] siteMeasurementsList = measuredDataPublication.siteMeasurements;

                if (logWrapper.isDebug())
                {
                    logWrapper.Debug("ANPR Update(" + siteMeasurementsList.Length + " objects)");
                }

                for (int siteMeasurementsListPos = 0; siteMeasurementsListPos < siteMeasurementsList.Length; siteMeasurementsListPos++)
                {
                    SiteMeasurements siteMeasurements = siteMeasurementsList[siteMeasurementsListPos];
                    processSituation(siteMeasurements, publicationTime);
                }
            }

            if (logWrapper.isDebug())
            {
                logWrapper.Debug("ANPR Update Complete");
            }
        }

        private void processSituation(SiteMeasurements siteMeasurements, DateTime publicationTime)
        {
            String anprIdentifier = siteMeasurements.measurementSiteReference.id;

            if (logWrapper.isDebug())
            {
                logWrapper.Debug("Processing ANPR Identifier(" + anprIdentifier + ")");
            }

            ANPRData anprData = (ANPRData)anprDataStore.getData(anprIdentifier);
            if (anprData == null)
            {
                anprData = anprData = new ANPRData(anprIdentifier, publicationTime);
            }
            anprData.addSiteMeasurements(siteMeasurements);
            anprDataStore.updateData(anprData);
        }
    }
}