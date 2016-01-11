using DATEXIIToolkit.Common;
using DATEXIIToolkit.Models;
using DATEXIIToolkit.Models.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DATEXIIToolkit.Services
{
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

            midasDataStore.clearDataStore();

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

/*

@Service
public class DATEXIIMIDASProcessService extends DATEXIIProcessService {
	final Logger log = LoggerFactory.getLogger(DATEXIIMIDASProcessService.class);
	
	private MIDASDataStore midasDataStore;
	
	@Autowired
	public DATEXIIMIDASProcessService(MIDASDataStore midasDataStore){
		super();
		this.midasDataStore = midasDataStore;
	}
	
	public DATEXIIMIDASProcessService(){
		super();
	}
	
	@Override
	public void processMessage(D2LogicalModel d2LogicalModel) {
		if (log.isDebugEnabled()){
            log.debug("MIDAS Update");
        }
        
		midasDataStore.clearDataStore();
		
		MeasuredDataPublication measuredDataPublication = (MeasuredDataPublication)d2LogicalModel.getPayloadPublication();
 
        if (measuredDataPublication != null) {
        	Date publicationTime = measuredDataPublication.getPublicationTime().toGregorianCalendar().getTime();
            
            List<SiteMeasurements> siteMeasurementsList = measuredDataPublication.getSiteMeasurements();
            
    		if (log.isDebugEnabled()){
                log.debug("MIDAS Update("+ siteMeasurementsList.size() + " objects)");
            }
    		
            Iterator<SiteMeasurements> iterator = siteMeasurementsList.iterator();
            while (iterator.hasNext()){
            	SiteMeasurements siteMeasurements = iterator.next();
                processSituation(siteMeasurements, publicationTime);
            }
        }
        
        if (log.isDebugEnabled()){
            log.debug("MIDAS Update Complete");
        }
	}
	
	private void processSituation(SiteMeasurements siteMeasurements, Date publicationTime) {
		String midasIdentifier = siteMeasurements.getMeasurementSiteReference().getId();

		if (log.isTraceEnabled()){
			log.trace("Processing MIDAS Identifier("+midasIdentifier+")");
		}
		
		MIDASData midasData = new MIDASData(midasIdentifier, publicationTime, siteMeasurements);
		
		midasDataStore.updateData(midasData);
	}

}

*/
