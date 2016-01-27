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
/*

@Service
public class DATEXIIVMSProcessService extends DATEXIIProcessService
{
    final Logger log = LoggerFactory.getLogger(DATEXIIVMSProcessService.class);
	
	final static private CharSequence fullRefreshText = "full refresh";

private VMSDataStore vMSDataStore;

@Autowired
    public DATEXIIVMSProcessService(VMSDataStore vMSDataStore)
{
    super();
    this.vMSDataStore = vMSDataStore;
}

@Override
    public void processMessage(D2LogicalModel d2LogicalModel)
{

    if (log.isDebugEnabled())
    {
        log.debug("VMSAndMatrix Update");
    }

    boolean fullRefresh = false;
    String feedType = d2LogicalModel.getPayloadPublication().getFeedType();
    if (feedType.toLowerCase().contains(fullRefreshText))
    {
        log.info("VMS Full Refresh received");
        fullRefresh = true;
        vMSDataStore.clearDataStore();
    }

    VmsPublication payloadPublication = (VmsPublication)d2LogicalModel.getPayloadPublication();
    Date publicationTime = payloadPublication.getPublicationTime().toGregorianCalendar().getTime();
    if (payloadPublication != null)
    {
        List<VmsUnit> vmsUnits = payloadPublication.getVmsUnit();

        if (log.isDebugEnabled())
        {
            log.debug("VMS Update(" + vmsUnits.size() + " objects)");
        }

        Iterator<VmsUnit> iterator = vmsUnits.iterator();
        while (iterator.hasNext())
        {
            VmsUnit vmsUnit = iterator.next();
            processVmsUnit(vmsUnit, publicationTime, fullRefresh);
        }
    }

    if (log.isDebugEnabled())
    {
        log.debug("VMSAndMatrix Update Complete");
    }
}

private void processVmsUnit(VmsUnit vmsUnit, Date publicationTime,
        boolean fullRefresh)
{
    String vmsIdentifier = vmsUnit.getVmsUnitReference().getId();

    if (log.isTraceEnabled())
    {
        log.trace("Processing VMSIdentifier(" + vmsIdentifier + ")");
    }

    VMSData vmsData = new VMSData(vmsIdentifier, publicationTime, vmsUnit);

    vMSDataStore.updateData(vmsData);
}

}

*/