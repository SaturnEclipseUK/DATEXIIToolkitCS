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
    /// This service processes MeasurementSites DATEX II v2 messages (D2LogicalModel).
    /// The payloads are inserted into the TAME, MIDAS, ANPR and TMU data stores.
    /// </summary>
    public class DATEXIINTISModelMeasurementSitesProcessService : DATEXIIProcessService
    {
        private LogWrapper logWrapper;
	
	    private TAMEStaticDataStore tameStaticDataStore;
        private MIDASStaticDataStore midasStaticDataStore;
        private ANPRStaticDataStore anprStaticDataStore;
        private TMUStaticDataStore tmuStaticDataStore;

        public DATEXIINTISModelMeasurementSitesProcessService() : base()
        {
            logWrapper = new LogWrapper("DATEXIINTISModelMeasurementSitesProcessService");
            tameStaticDataStore = (TAMEStaticDataStore)DataStoreFactory.GetInstance().GetDataStore(DataStoreFactory.DATA_STORES.TAME_STATIC_DATA_STORE); 
            midasStaticDataStore = (MIDASStaticDataStore)DataStoreFactory.GetInstance().GetDataStore(DataStoreFactory.DATA_STORES.MIDAS_STATIC_DATA_STORE);
            anprStaticDataStore = (ANPRStaticDataStore)DataStoreFactory.GetInstance().GetDataStore(DataStoreFactory.DATA_STORES.ANPR_STATIC_DATA_STORE);
            tmuStaticDataStore = (TMUStaticDataStore)DataStoreFactory.GetInstance().GetDataStore(DataStoreFactory.DATA_STORES.TMU_STATIC_DATA_STORE);
        }

        
        public override void processMessage(D2LogicalModel d2LogicalModel)
        {
            if (logWrapper.isDebug())
            {
                logWrapper.Debug("NTIS Model Measurement Site Tables Update");
            }

            tameStaticDataStore.clearDataStore();
            midasStaticDataStore.clearDataStore();
            anprStaticDataStore.clearDataStore();
            tmuStaticDataStore.clearDataStore();

            MeasurementSiteTablePublication measurementSiteTablePublication = (MeasurementSiteTablePublication)d2LogicalModel.payloadPublication;
            if (measurementSiteTablePublication != null)
            {
                DateTime publicationTime = measurementSiteTablePublication.publicationTime;
                MeasurementSiteTable[] measurementSiteTableList = measurementSiteTablePublication.measurementSiteTable;

                for (int measurementSiteTableListPos = 0; measurementSiteTableListPos < measurementSiteTableList.Length; measurementSiteTableListPos++)
                {
                    MeasurementSiteTable measurementSiteTable = measurementSiteTableList[measurementSiteTableListPos];
                    processMeasurementSiteTable(measurementSiteTable, publicationTime);
                }
            }

            if (logWrapper.isDebug())
            {
                logWrapper.Debug("NTIS Model Measurement Site Tables Update Complete");
            }
        }

        private void processMeasurementSiteTable(MeasurementSiteTable measurementSiteTable, DateTime publicationTime)
        {
            String measurementSiteTableId = measurementSiteTable.id;

            if (logWrapper.isDebug())
            {
                logWrapper.Debug("NTIS Model Measurement Site Tables Update(" + measurementSiteTableId + ")");
            }

            MeasurementSiteRecord[] measurementSiteRecordList = measurementSiteTable.measurementSiteRecord;

            for (int measurementSiteRecordListPos = 0; measurementSiteRecordListPos < measurementSiteRecordList.Length; measurementSiteRecordListPos++)
            {
                MeasurementSiteRecord measurementSiteRecord = measurementSiteRecordList[measurementSiteRecordListPos];
                processMeasurementSiteRecord(measurementSiteRecord, publicationTime, measurementSiteTableId);
            }
        }

        private void processMeasurementSiteRecord(MeasurementSiteRecord measurementSiteRecord, DateTime publicationTime, string measurementSiteTableId)
        {
            String measurementSiteRecordIdentifier = measurementSiteRecord.id;

            if (logWrapper.isTrace())
            {
                logWrapper.Trace("Processing Measurement Site Identifier(" + measurementSiteRecordIdentifier + ")");
            } 

            switch (measurementSiteTableId)
            {
                case "NTIS_TAME_Measurement_Sites":
                    TAMEStaticData tameStaticData = new TAMEStaticData(measurementSiteRecordIdentifier, publicationTime, measurementSiteRecord);
                    tameStaticDataStore.updateData(tameStaticData);
                    break;
                case "NTIS_MIDAS_Measurement_Sites":
                    MIDASStaticData midasStaticData = new MIDASStaticData(measurementSiteRecordIdentifier, publicationTime, measurementSiteRecord);
                    midasStaticDataStore.updateData(midasStaticData);
                    break;
                case "NTIS_ANPR_Measurement_Sites":
                    ANPRStaticData anprStaticData = new ANPRStaticData(measurementSiteRecordIdentifier, publicationTime, measurementSiteRecord);
                    anprStaticDataStore.updateData(anprStaticData);
                    break;
                case "NTIS_TMU_Measurement_Sites":
                    TMUStaticData tmuStaticData = new TMUStaticData(measurementSiteRecordIdentifier, publicationTime, measurementSiteRecord);
                    tmuStaticDataStore.updateData(tmuStaticData);
                    break;
                default:
                    logWrapper.Error("VMS Unit Table Id not implemented: " + measurementSiteTableId);
                    break;
            }

        }
    }
}
