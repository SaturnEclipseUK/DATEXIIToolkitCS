using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DATEXIIToolkit.Models.DataObjects
{
    public class TMUData : DataObject
    {

        public String tmuIdentifier;
        public DateTime publicationTime;
        public LinkedList<SiteMeasurements> tmuData;

        public TMUData()
        {
        }

        public TMUData(String tmuIdentifier, DateTime publicationTime,
                LinkedList<SiteMeasurements> tmuData)
        {
            this.tmuIdentifier = tmuIdentifier;
            this.publicationTime = publicationTime;
            this.tmuData = tmuData;
        }

        public String getTMUIdentifier()
        {
            return tmuIdentifier;
        }

        public DateTime getPublicationTime()
        {
            return publicationTime;
        }

        public LinkedList<SiteMeasurements> getTMUData()
        {
            return tmuData;
        }

    }

}