using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DATEXIIToolkit.Models.DataObjects
{
    public class EventData : DataObject
    {
        public String eventIdentifier;
        public DateTime publicationTime;
        public Situation eventData;

        public EventData()
        {
        }

        public EventData(String eventIdentifier, DateTime publicationTime, Situation eventData)
        {
            this.publicationTime = publicationTime;
            this.eventIdentifier = eventIdentifier;
            this.eventData = eventData;
        }

        public String getEventIdentifier()
        {
            return eventIdentifier;
        }

        public DateTime getPublicationTime()
        {
            return publicationTime;
        }

        public Situation getEventData()
        {
            return eventData;
        }
    }
}