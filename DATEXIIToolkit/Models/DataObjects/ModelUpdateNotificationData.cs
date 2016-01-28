using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DATEXIIToolkit.Models.DataObjects
{
    public class ModelUpdateNotificationData : DataObject
    {

        public String anprIdentifier;
        public DateTime publicationTime;
        public SiteMeasurements anprData;

        public ModelUpdateNotificationData()
        {
        }

        public ModelUpdateNotificationData(String anprIdentifier, DateTime publicationTime,
                SiteMeasurements anprData)
        {
            this.anprIdentifier = anprIdentifier;
            this.publicationTime = publicationTime;
            this.anprData = anprData;
        }

        public String getAnprIdentifier()
        {
            return anprIdentifier;
        }

        public DateTime getPublicationTime()
        {
            return publicationTime;
        }

        public SiteMeasurements getAnprData()
        {
            return anprData;
        }
    }

}