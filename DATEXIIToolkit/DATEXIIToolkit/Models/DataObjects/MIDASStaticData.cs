using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DATEXIIToolkit.Models.DataObjects
{
    public class MIDASStaticData : DataObject
    {
        public String midasStaticIdentifier;
        public DateTime publicationTime;
        public MeasurementSiteRecord midasStaticData;

        public MIDASStaticData()
        {
        }

        public MIDASStaticData(String midasStaticIdentifier, DateTime publicationTime, MeasurementSiteRecord midasStaticData)
        {
            this.publicationTime = publicationTime;
            this.midasStaticIdentifier = midasStaticIdentifier;
            this.midasStaticData = midasStaticData;
        }

        public String getMIDASStaticIdentifier()
        {
            return midasStaticIdentifier;
        }

        public DateTime getPublicationTime()
        {
            return publicationTime;
        }

        public MeasurementSiteRecord getMIDASStaticData()
        {
            return midasStaticData;
        }

    }

}