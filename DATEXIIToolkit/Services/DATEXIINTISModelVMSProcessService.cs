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
    /// This service processes VMS DATEX II v2 messages (D2LogicalModel).
    /// The payloads are inserted into the MatrixSignalStatic and VMSStatic data stores.
    /// </summary>
    public class DATEXIINTISModelVMSProcessService : DATEXIIProcessService
    {
        LogWrapper logWrapper;
	
	    private VMSStaticDataStore vmsStaticDataStore;
        private MatrixSignalStaticDataStore matrixSignalStaticDataStore;

        public DATEXIINTISModelVMSProcessService() : base()
        {
            logWrapper = new LogWrapper("DATEXIINTISModelVMSProcessService");

            vmsStaticDataStore = (VMSStaticDataStore)DataStoreFactory.GetInstance().GetDataStore(DataStoreFactory.DATA_STORES.VMS_STATIC_DATA_STORE);
            matrixSignalStaticDataStore = (MatrixSignalStaticDataStore)DataStoreFactory.GetInstance().GetDataStore(DataStoreFactory.DATA_STORES.MATRIX_SIGNAL_STATIC_DATA_STORE);
        }

        public override void processMessage(D2LogicalModel d2LogicalModel)
        {

            if (logWrapper.isDebug())
            {
                logWrapper.Debug("NTIS Model VMS Tables Update");
            }

            vmsStaticDataStore.clearDataStore();
            matrixSignalStaticDataStore.clearDataStore();

            VmsTablePublication vmsTablePublication = (VmsTablePublication)d2LogicalModel.payloadPublication;
            if (vmsTablePublication != null)
            {
                DateTime publicationTime = vmsTablePublication.publicationTime;

                VmsUnitTable[] vmsUnitTableList = vmsTablePublication.vmsUnitTable;

                for (int vmsUnitTableListPos = 0; vmsUnitTableListPos < vmsUnitTableList.Length; vmsUnitTableListPos++)
                {
                    VmsUnitTable vmsUnitTable = vmsUnitTableList[vmsUnitTableListPos];
                    processVmsUnitTable(vmsUnitTable, publicationTime);
                }
            }

            if (logWrapper.isDebug())
            {
                logWrapper.Debug("NTIS Model VMS Tables Update Complete");
            }
        }

        private void processVmsUnitTable(VmsUnitTable vmsUnitTable, DateTime publicationTime)
        {
            String vmsUnitTableId = vmsUnitTable.id;

            if (logWrapper.isDebug())
            {
                logWrapper.Debug("NTIS Model VMS Tables Update(" + vmsUnitTableId + ")");
            }

            VmsUnitRecord[] vmsUnitRecordList = vmsUnitTable.vmsUnitRecord;

            for (int vmsUnitRecordListPos = 0; vmsUnitRecordListPos < vmsUnitRecordList.Length; vmsUnitRecordListPos++)
            {
                VmsUnitRecord vmsUnitRecord = vmsUnitRecordList[vmsUnitRecordListPos];
                processVmsUnitRecord(vmsUnitRecord, publicationTime, vmsUnitTableId);
            }
        }

        private void processVmsUnitRecord(VmsUnitRecord vmsUnitRecord, DateTime publicationTime, String vmsUnitTableId)
        {
            String vmsUnitIdentifier = vmsUnitRecord.id;

            if (logWrapper.isTrace())
            {
                logWrapper.Trace("Processing VMS Identifier(" + vmsUnitIdentifier + ")");
            }

            switch (vmsUnitTableId)
            {
                case "NTIS_Matrix_Units":
                    MatrixSignalStaticData matrixSignalStaticData = new MatrixSignalStaticData(vmsUnitIdentifier, publicationTime, vmsUnitRecord);
                    matrixSignalStaticDataStore.updateData(matrixSignalStaticData);
                    break;
                case "NTIS_VMS_Units":
                    VMSStaticData vmsStaticData = new VMSStaticData(vmsUnitIdentifier, publicationTime, vmsUnitRecord);
                    vmsStaticDataStore.updateData(vmsStaticData);
                    break;
                default:
                    logWrapper.Error("VMS Unit Table Id not implemented: " + vmsUnitTableId);
                    break;
            }
        }
    }
}