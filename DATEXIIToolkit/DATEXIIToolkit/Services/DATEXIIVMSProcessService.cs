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
    /// The payloads are inserted into the VMS data store.
    /// </summary>
    public class DATEXIIVMSProcessService : DATEXIIProcessService
    {
        private LogWrapper logWrapper;
        private static string FULL_REFRESH_TEXT = "full refresh";

        private VMSDataStore vMSDataStore;

        public DATEXIIVMSProcessService() : base() {
            logWrapper = new LogWrapper("DATEXIIUpdateService");
            vMSDataStore = (VMSDataStore)DataStoreFactory.GetInstance().GetDataStore(DataStoreFactory.DATA_STORES.VMS_DATA_STORE);
        }
            
        public override void processMessage(D2LogicalModel d2LogicalModel)
        {
            if (logWrapper.isInfo())
            {
                logWrapper.Info("VMSAndMatrix Update");
            }
            
            bool fullRefresh = false;
            string feedType = d2LogicalModel.payloadPublication.feedType;
            if (feedType.ToLower().Contains(FULL_REFRESH_TEXT))
            {
                if (logWrapper.isInfo())
                {
                    logWrapper.Info("VMS Full Refresh received");
                }
                fullRefresh = true;
                vMSDataStore.clearDataStore();
            }

            VmsPublication payloadPublication = (VmsPublication)d2LogicalModel.payloadPublication;
            DateTime publicationTime = payloadPublication.publicationTime;
            if (payloadPublication != null)
            {
                VmsUnit[] vmsUnits = payloadPublication.vmsUnit;

                if (logWrapper.isInfo())
                {
                    logWrapper.Info("VMS Update(" + vmsUnits.Length + " objects)");
                }            

                for (int vmsUnitsPos=0; vmsUnitsPos < vmsUnits.Length; vmsUnitsPos++)
                {
                    VmsUnit vmsUnit = vmsUnits[vmsUnitsPos];
                    processVmsUnit(vmsUnit, publicationTime, fullRefresh);
                }
            }

            if (logWrapper.isInfo())
            {
                logWrapper.Info("VMSAndMatrix Update Complete");
            }
        }

        private void processVmsUnit(VmsUnit vmsUnit, DateTime publicationTime, bool fullRefresh)
        {
            string vmsIdentifier = vmsUnit.vmsUnitReference.id;

            if (logWrapper.isInfo())
            {
                logWrapper.Info("Processing VMSIdentifier(" + vmsIdentifier + ")");
            }
    
            VMSData vmsData = new VMSData(vmsIdentifier, publicationTime, vmsUnit);
            vMSDataStore.updateData(vmsData);
        }
    }
}