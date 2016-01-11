using DATEXIIToolkit.Common;
using DATEXIIToolkit.Models;
using DATEXIIToolkit.Models.DataObjects;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Timers;
using System.Web;

namespace DATEXIIToolkit.Services
{
    public class DATEXIIEventProcessService : DATEXIIProcessService
    {
        private static LogWrapper logWrapper;
	
	    private string fullRefreshText = "full refresh";

        private static EventDataStore eventDataStore;

        private static int expireClearedEventsAfterMins;
        private const int SECONDS_IN_MINUTE = 60;
        private int checkForExpiredEventsPeriod;

        public DATEXIIEventProcessService() : base()
        {
            logWrapper = new LogWrapper("DATEXIIEventProcessService");    
            eventDataStore = (EventDataStore)DataStoreFactory.GetInstance().GetDataStore(DataStoreFactory.DATA_STORES.EVENT_DATA_STORE);
            expireClearedEventsAfterMins = Int32.Parse(ConfigurationManager.AppSettings["expireClearedEventsAfterMins"]);
            checkForExpiredEventsPeriod = Int32.Parse(ConfigurationManager.AppSettings["checkForExpiredEventsPeriod"]);

            Timer processQueueTimer = new Timer();
            processQueueTimer.Elapsed += new ElapsedEventHandler(processClearedEvents);
            processQueueTimer.Interval = checkForExpiredEventsPeriod;
            processQueueTimer.Enabled = true;
        }

        public override void processMessage(D2LogicalModel d2LogicalModel)
        {

            if (logWrapper.isDebug())
            {
                logWrapper.Debug("Event Update");
            }

            bool fullRefresh = false;
            String feedType = d2LogicalModel.payloadPublication.feedType;
            if (feedType.ToLower().Contains(fullRefreshText))
            {
                logWrapper.Info("Event Full Refresh received");
                fullRefresh = true;
                lock(eventDataStore){
                    eventDataStore.clearDataStore();
                }
            }

            SituationPublication situationPublication = (SituationPublication)d2LogicalModel.payloadPublication;
            DateTime publicationTime = situationPublication.publicationTime;
            if (situationPublication != null)
            {

                Situation[] situationList = situationPublication.situation;

                if (logWrapper.isDebug())
                {
                    logWrapper.Debug("Event Update(" + situationList.Length + " objects)");
                }

                  for (int situationListPos = 0; situationListPos < situationList.Length; situationListPos++)
                {
                    Situation situation = situationList[situationListPos];
                    processSituation(situation, publicationTime, fullRefresh);
                }
            }

            if (logWrapper.isDebug())
            {
                logWrapper.Debug("Event Update Complete");
            }
        }

        private void processSituation(Situation situation, DateTime publicationTime, bool fullRefresh)
        {
            String eventIdentifier = situation.situationRecord[0].situationRecordCreationReference;

            if (logWrapper.isTrace())
            {
                logWrapper.Trace("Processing Event Identifier(" + eventIdentifier + ")");
            }

            EventData eventData = new EventData(eventIdentifier, publicationTime, situation);

            lock(eventDataStore){
                eventDataStore.updateData(eventData);
            }
        }

        private static void processClearedEvents(object source, ElapsedEventArgs e)
        {
            lock(eventDataStore){
                foreach (EventData eventData in eventDataStore.getAllEventData())
                {
                    DateTime publicationTime = eventData.getPublicationTime();
                    bool isClear = false;
                    SituationRecord situationRecord = eventData.getEventData().situationRecord[0];
                    LifeCycleManagement lifeCycleManagement;
                    if (situationRecord.management != null)
                    {
                        lifeCycleManagement = situationRecord.management.lifeCycleManagement;
                    }
                    else {
                        lifeCycleManagement = null;
                    }
                    if (lifeCycleManagement != null && (lifeCycleManagement.cancel || lifeCycleManagement.end))
                    {
                        isClear = true;
                    }

                    DateTime currentTime = DateTime.Now;
                    DateTime expireTime = publicationTime.AddSeconds(expireClearedEventsAfterMins * SECONDS_IN_MINUTE);
                    if (isClear && currentTime.CompareTo(expireTime) >= 0)
                    {
                        eventDataStore.removeData(eventData.getEventIdentifier());
                        logWrapper.Info("Removed Expired Event: " + eventData.getEventIdentifier());
                    }
                }
            }
        }
    }
}

/*

@Service
public class DATEXIIEventProcessService extends DATEXIIProcessService {
	final Logger log = LoggerFactory.getLogger(DATEXIIEventProcessService.class);
	
	final static private CharSequence fullRefreshText = "full refresh";
	
	private EventDataStore eventDataStore;
	
	@Value("${expireClearedEventsAfterMins}")
	private Integer expireClearedEventsAfterMins;
	
	@Autowired
	public DATEXIIEventProcessService(EventDataStore eventDataStore){
		super();
		this.eventDataStore = eventDataStore;
	}
	
	@Override
	public void processMessage(D2LogicalModel d2LogicalModel) {
		
		if (log.isDebugEnabled()){
            log.debug("Event Update");
        }
        
        boolean fullRefresh = false;
        String feedType = d2LogicalModel.getPayloadPublication().getFeedType();
        if (feedType.toLowerCase().contains(fullRefreshText)) {
            log.info("Event Full Refresh received");
            fullRefresh=true;
            synchronized(eventDataStore){
            	eventDataStore.clearDataStore();
            }
        }
		
        SituationPublication situationPublication = (SituationPublication)d2LogicalModel.getPayloadPublication();
        Date publicationTime = situationPublication.getPublicationTime().toGregorianCalendar().getTime();
        if (situationPublication != null) {
        	
            List<Situation> situationList = situationPublication.getSituation();
            
            if (log.isDebugEnabled()){
                log.debug("Event Update("+ situationList.size() + " objects)");
            }
            
            Iterator<Situation> iterator = situationList.iterator();
            while (iterator.hasNext()){
            	Situation situation = iterator.next();
                processSituation(situation, publicationTime, fullRefresh);
            }
        }
        
		if (log.isDebugEnabled()){
            log.debug("Event Update Complete");
        }
	}
	
	private void processSituation(Situation situation, Date publicationTime, boolean fullRefresh) {
		String eventIdentifier = situation.getSituationRecord().get(0).getSituationRecordCreationReference();

		if (log.isTraceEnabled()){
			log.trace("Processing Event Identifier("+eventIdentifier+")");
		}
		
		EventData eventData = new EventData(eventIdentifier, publicationTime, situation);
		
		synchronized(eventDataStore){
			eventDataStore.updateData(eventData);
		}
	}
	
	@Scheduled(fixedRate = 1*60*1000)
	public void processClearedEvents(){
		synchronized(eventDataStore){
			for (EventData eventData : eventDataStore.getAllEventData()){
				Date publicationTime = eventData.getPublicationTime();
				boolean isClear = false;
				SituationRecord situationRecord = eventData.getEventData().getSituationRecord().get(0);
				LifeCycleManagement lifeCycleManagement;
		        if (situationRecord.getManagement() != null){
		            lifeCycleManagement = situationRecord.getManagement().getLifeCycleManagement();
		        } else {
		            lifeCycleManagement = null;
		        }
				if (lifeCycleManagement != null && (lifeCycleManagement.isCancel() || lifeCycleManagement.isEnd())){
		            isClear = true;
		        }				
				if (isClear && new Date().getTime() > publicationTime.getTime() + expireClearedEventsAfterMins*60*1000){
					eventDataStore.removeData(eventData.getEventIdentifier());		
					log.info("Removed Expired Event: "+eventData.getEventIdentifier());					
				}
			}
		}
	}

}

*/
