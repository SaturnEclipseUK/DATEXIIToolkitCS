using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DATEXIIToolkit.Models.DataObjects
{

    public class VMSData : DataObject
    {
        public String vmsIdentifier;
        public DateTime publicationTime;
        public VmsUnit vmsData;

        public VMSData()
        {
        }

        public VMSData(String vmsIdentifier, DateTime publicationTime, VmsUnit vmsData)
        {
            this.vmsIdentifier = vmsIdentifier;
            this.publicationTime = publicationTime;
            this.vmsData = vmsData;
        }

        public String getVmsIdentifier()
        {
            return vmsIdentifier;
        }

        public DateTime getPublicationTime()
        {
            return publicationTime;
        }

        public VmsUnit getVmsData()
        {
            return vmsData;
        }
    }

}