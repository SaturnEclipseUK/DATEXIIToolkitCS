using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DATEXIIToolkit.Models.DataObjects
{
    public class NwkNodeStaticData : DataObject
    {
        public String nwkNodeStaticIdentifier;
        public DateTime publicationTime;
        public PredefinedLocation nwkNodeStaticData;

        public NwkNodeStaticData()
        {
        }

        public NwkNodeStaticData(String nwkNodeStaticIdentifier, DateTime publicationTime, PredefinedLocation nwkNodeStaticData)
        {
            this.publicationTime = publicationTime;
            this.nwkNodeStaticIdentifier = nwkNodeStaticIdentifier;
            this.nwkNodeStaticData = nwkNodeStaticData;
        }

        public String getNwkNodeStaticIdentifier()
        {
            return nwkNodeStaticIdentifier;
        }

        public DateTime getPublicationTime()
        {
            return publicationTime;
        }

        public PredefinedLocation getNwkNodeStaticData()
        {
            return nwkNodeStaticData;
        }
    }
}