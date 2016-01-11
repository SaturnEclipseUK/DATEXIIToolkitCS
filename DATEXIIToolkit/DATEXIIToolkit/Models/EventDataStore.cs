using DATEXIIToolkit.Models.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DATEXIIToolkit.Models
{

    public class EventDataStore : DataStore
    {

        public EventDataStore() : base()
        {
        }

        public override void updateData(DataObject data)
        {
            lock (this)
            {
                EventData eventData = (EventData)data;
                String eventIdentifier = eventData.getEventIdentifier();
                if (dataMap.ContainsKey(eventIdentifier))
                {
                    dataMap.Remove(eventIdentifier);
                    dataMap.Add(eventIdentifier, eventData);
                }
                else {
                    dataMap.Add(eventIdentifier, eventData);
                }
            }
        }


        public LinkedList<EventData> getAllEventData()
        {
            lock (this)
            {
                LinkedList<EventData> returnDataObjectsList = new LinkedList<EventData>();
                IList<DataObject> dataObjectList = new List<DataObject>(dataMap.Values.ToList());
                foreach (EventData dataObject in dataObjectList)
                {
                    returnDataObjectsList.AddLast(dataObject);
                }
                return returnDataObjectsList;
            }
        }

        public override void removeData(String eventIdentifier)
        {
            lock (this)
            {
                if (dataMap.ContainsKey(eventIdentifier))
                {
                    dataMap.Remove(eventIdentifier);
                }
            }
        }
    }

}