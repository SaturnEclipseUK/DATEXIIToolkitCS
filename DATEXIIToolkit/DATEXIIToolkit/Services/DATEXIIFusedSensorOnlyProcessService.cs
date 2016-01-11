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
    public class DATEXIIFusedSensorOnlyProcessService : DATEXIIProcessService
    {
        private LogWrapper logWrapper;
        private FusedSensorOnlyDataStore fusedSensorOnlyDataStore;

        public DATEXIIFusedSensorOnlyProcessService() : base()
        {
            logWrapper = new LogWrapper("DATEXIIFusedSensorOnlyProcessService");
            fusedSensorOnlyDataStore = (FusedSensorOnlyDataStore)DataStoreFactory.GetInstance().GetDataStore(DataStoreFactory.DATA_STORES.FUSED_SENSOR_ONLY_DATA_STORE);
        }

        public override void processMessage(D2LogicalModel d2LogicalModel)
        {
            if (logWrapper.isDebug())
            {
                logWrapper.Debug("FusedSensorOnlyData Update");
            }

            fusedSensorOnlyDataStore.clearDataStore();

            ElaboratedDataPublication elaboratedDataPublication = (ElaboratedDataPublication)d2LogicalModel.payloadPublication;

            if (elaboratedDataPublication != null)
            {
                DateTime publicationTime = elaboratedDataPublication.publicationTime;
                DateTime timeDefault = elaboratedDataPublication.timeDefault;
                ElaboratedData[] elaboratedDataList = elaboratedDataPublication.elaboratedData;
               
                if (logWrapper.isDebug())
                {
                    logWrapper.Debug("FusedSensorOnlyData Update(" + elaboratedDataList.Length + " objects)");
                }

                for (int elaboratedDataListPos = 0; elaboratedDataListPos < elaboratedDataList.Length; elaboratedDataListPos++)
                {
                    ElaboratedData elaboratedData = elaboratedDataList[elaboratedDataListPos];
                    processSituation(elaboratedData, publicationTime, timeDefault);
                }
            }

            if (logWrapper.isTrace())
            {
                logWrapper.Trace("FusedSensorOnlyData Update Complete");
            }
        }

        private void processSituation(ElaboratedData elaboratedData, DateTime publicationTime, DateTime timeDefault)
        {

            LocationByReference locationByReference = null;
            BasicData basicData = elaboratedData.basicData;

            

            if (basicData.GetType() == typeof(TrafficHeadway)) { 
                TrafficHeadway data = (TrafficHeadway)basicData;
                locationByReference = (LocationByReference)data.pertinentLocation;
            }else if (basicData.GetType() == typeof(TrafficFlow)) { 
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

                if (logWrapper.isTrace())
                {
                    logWrapper.Trace("Processing Fused Sensor Only Identifier(" + linkIdentifier + ")");
                }

                FusedSensorOnlyData fusedSensorOnlyData = new FusedSensorOnlyData(linkIdentifier, publicationTime, timeDefault, elaboratedData);
                fusedSensorOnlyDataStore.updateData(fusedSensorOnlyData);
            }
            else {
                logWrapper.Error("Failed to Process elaboratedData, " + elaboratedData.ToString());
            }
        }
    }
}

/*

@Service
public class DATEXIIFusedSensorOnlyProcessService extends DATEXIIProcessService {

	final Logger log = LoggerFactory.getLogger(DATEXIIFusedSensorOnlyProcessService.class);
	
	private FusedSensorOnlyDataStore fusedSensorOnlyDataStore;
	
	@Autowired
	public DATEXIIFusedSensorOnlyProcessService(FusedSensorOnlyDataStore fusedSensorOnlyDataStore){
		super();
		this.fusedSensorOnlyDataStore = fusedSensorOnlyDataStore;
	}
	
	public DATEXIIFusedSensorOnlyProcessService(){
		super();
	}
	
	@Override
	public void processMessage(D2LogicalModel d2LogicalModel) {
		if (log.isDebugEnabled()){
            log.debug("FusedSensorOnlyData Update");
        }
        
		fusedSensorOnlyDataStore.clearDataStore();
		
        ElaboratedDataPublication elaboratedDataPublication = (ElaboratedDataPublication)d2LogicalModel.getPayloadPublication();
 
        if (elaboratedDataPublication != null) {
        	Date publicationTime = elaboratedDataPublication.getPublicationTime().toGregorianCalendar().getTime();
            Date timeDefault = elaboratedDataPublication.getTimeDefault().toGregorianCalendar().getTime();
            List<ElaboratedData> elaboratedDataList = elaboratedDataPublication.getElaboratedData();
            Iterator<ElaboratedData> iterator = elaboratedDataList.iterator();
            
    		if (log.isDebugEnabled()){
                log.debug("FusedSensorOnlyData Update("+ elaboratedDataList.size() + " objects)");
            }
    		
            while (iterator.hasNext()){
            	ElaboratedData elaboratedData = iterator.next();
                processSituation(elaboratedData, publicationTime, timeDefault);
            }
        }
        
		if (log.isTraceEnabled()){
            log.trace("FusedSensorOnlyData Update Complete");
        }
	}
	
	private void processSituation(ElaboratedData elaboratedData, Date publicationTime, Date timeDefault) {
		
		LocationByReference locationByReference = null;
		BasicData basicData = elaboratedData.getBasicData();
		if (basicData instanceof TrafficHeadway){
			TrafficHeadway data = (TrafficHeadway)basicData;
			locationByReference = (LocationByReference)data.getPertinentLocation();
		}else if (basicData instanceof TrafficFlow){
			TrafficFlow data = (TrafficFlow)basicData;
			locationByReference = (LocationByReference)data.getPertinentLocation();
		}else if (basicData instanceof TrafficConcentration){
			TrafficConcentration data = (TrafficConcentration)basicData;
			locationByReference = (LocationByReference)data.getPertinentLocation();
		}else if (basicData instanceof TrafficSpeed){
			TrafficSpeed data = (TrafficSpeed)basicData;
			locationByReference = (LocationByReference)data.getPertinentLocation();
		}else if (basicData instanceof TravelTimeData){
			TravelTimeData data = (TravelTimeData)basicData;
			locationByReference = (LocationByReference)data.getPertinentLocation();
		}else{
			log.warn("basicData instance of -" + basicData.getClass().getSimpleName());
		}
		
		String linkIdentifier=null;
		if (locationByReference != null){
			linkIdentifier = basicData.getClass().getSimpleName() +locationByReference.getPredefinedLocationReference().getId();
			
			if (log.isTraceEnabled()){
				log.trace("Processing Fused Sensor Only Identifier("+ linkIdentifier+")");
			}
			
			FusedSensorOnlyData fusedSensorOnlyData = new FusedSensorOnlyData(linkIdentifier, publicationTime, timeDefault, elaboratedData);
			fusedSensorOnlyDataStore.updateData(fusedSensorOnlyData);
		}else{
			this.log.error("Failed to Process elaboratedData, " + elaboratedData.toString());
		}
	}
	

}

*/
