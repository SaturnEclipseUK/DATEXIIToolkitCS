using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DATEXIIToolkit.Models.DataObjects
{

    public class MIDASData : DataObject
    {
        public String midasIdentifier;
        public DateTime publicationTime;
        public SiteMeasurements midasData;

        public MIDASData()
        {
        }

        public MIDASData(String midasIdentifier, DateTime publicationTime,
                SiteMeasurements midasData)
        {

            this.midasIdentifier = midasIdentifier;
            this.publicationTime = publicationTime;
            this.midasData = midasData;
        }

        public String getMIDASIdentifier()
        {
            return midasIdentifier;
        }

        public DateTime getPublicationTime()
        {
            return publicationTime;
        }

        public SiteMeasurements getMIDASData()
        {
            return midasData;
        }
    }
}