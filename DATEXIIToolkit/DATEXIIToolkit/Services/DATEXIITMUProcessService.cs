using DATEXIIToolkit.Common;
using DATEXIIToolkit.Models;
using DATEXIIToolkit.Models.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DATEXIIToolkit.Services
{
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

                for (int siteMeasurementsListPos = 0; siteMeasurementsListPos < siteMeasurementsList.Length; siteMeasurementsListPos++)
                {
                    SiteMeasurements siteMeasurements = siteMeasurementsList[siteMeasurementsListPos];
                    processSituation(siteMeasurements, publicationTime);
                }
            }

            if (logWrapper.isDebug())
            {
                logWrapper.Debug("TMU Update Complete");
            }
        }

        private void processSituation(SiteMeasurements siteMeasurements, DateTime publicationTime)
        {
            String tmuIdentifier = siteMeasurements.measurementSiteReference.id;

            if (logWrapper.isDebug())
            {
                logWrapper.Debug("Processing TMU Identifier(" + tmuIdentifier + ")");
            }

            TMUData tmuData = new TMUData(tmuIdentifier, publicationTime, siteMeasurements);

            tmuDataStore.updateData(tmuData);
        }


    }
}


/*

@Service
public class DATEXIITMUProcessService extends DATEXIIProcessService {

	final Logger log = LoggerFactory.getLogger(DATEXIITMUProcessService.class);
	
	private TMUDataStore tmuDataStore;
	
	@Autowired
	public DATEXIITMUProcessService(TMUDataStore tmuDataStore){
		super();
		this.tmuDataStore = tmuDataStore;
	}
	
	public DATEXIITMUProcessService(){
		super();
	}
	
	@Override
	public void processMessage(D2LogicalModel d2LogicalModel) {
		if (log.isDebugEnabled()){
            log.debug("TMU Update");
        }
        
		tmuDataStore.clearDataStore();
		
		MeasuredDataPublication measuredDataPublication = (MeasuredDataPublication)d2LogicalModel.getPayloadPublication();
 
        if (measuredDataPublication != null) {
        	Date publicationTime = measuredDataPublication.getPublicationTime().toGregorianCalendar().getTime();
            
            List<SiteMeasurements> siteMeasurementsList = measuredDataPublication.getSiteMeasurements();
            Iterator<SiteMeasurements> iterator = siteMeasurementsList.iterator();
            
    		if (log.isDebugEnabled()){
                log.debug("TMU Update("+ siteMeasurementsList.size() + " objects)");
            }
    		
            while (iterator.hasNext()){
            	SiteMeasurements siteMeasurements = iterator.next();
                processSituation(siteMeasurements, publicationTime);
            }
        }
        
		if (log.isDebugEnabled()){
            log.debug("TMU Update Complete");
        }
	}
	
	private void processSituation(SiteMeasurements siteMeasurements, Date publicationTime) {
		String tmuIdentifier = siteMeasurements.getMeasurementSiteReference().getId();

		if (log.isTraceEnabled()){
			log.trace("Processing TMU Identifier("+tmuIdentifier+")");
		}
		
		TMUData tmuData = new TMUData(tmuIdentifier, publicationTime, siteMeasurements);
		
		tmuDataStore.updateData(tmuData);
	}


}
*/