using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DATEXIIToolkit.Models.DataObjects
{
    public class TMUStaticData : DataObject
    {
        public String tmuStaticIdentifier;
        public DateTime publicationTime;
        public MeasurementSiteRecord tmuStaticData;

        public TMUStaticData()
        {
        }

        public TMUStaticData(String tmuStaticIdentifier, DateTime publicationTime, MeasurementSiteRecord tmuStaticData)
        {
            this.publicationTime = publicationTime;
            this.tmuStaticIdentifier = tmuStaticIdentifier;
            this.tmuStaticData = tmuStaticData;
        }

        public String getTMUStaticIdentifier()
        {
            return tmuStaticIdentifier;
        }

        public DateTime getPublicationTime()
        {
            return publicationTime;
        }

        public MeasurementSiteRecord getTMUStaticData()
        {
            return tmuStaticData;
        }
    }
}