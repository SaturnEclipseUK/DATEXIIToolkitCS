using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DATEXIIToolkit.Models.DataObjects
{
    public class NwkLinkStaticData : DataObject
    {
        public String nwkLinkStaticIdentifier;
        public DateTime publicationTime;
        public PredefinedLocation nwkLinkStaticData;

        public NwkLinkStaticData()
        {
        }

        public NwkLinkStaticData(String nwkLinkStaticIdentifier, DateTime publicationTime, PredefinedLocation nwkLinkStaticData)
        {
            this.publicationTime = publicationTime;
            this.nwkLinkStaticIdentifier = nwkLinkStaticIdentifier;
            this.nwkLinkStaticData = nwkLinkStaticData;
        }

        public String getNwkLinkStaticIdentifier()
        {
            return nwkLinkStaticIdentifier;
        }

        public DateTime getPublicationTime()
        {
            return publicationTime;
        }

        public PredefinedLocation getNwkLinkStaticData()
        {
            return nwkLinkStaticData;
        }
    }
}