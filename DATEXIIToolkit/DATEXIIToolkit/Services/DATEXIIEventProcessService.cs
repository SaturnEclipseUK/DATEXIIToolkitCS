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
    /// <summary>
    /// This service processes Event DATEX II v2 messages (D2LogicalModel).
    /// The payloads are inserted into the Event data store.
    /// </summary>
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