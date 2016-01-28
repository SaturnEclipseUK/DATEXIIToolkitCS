using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DATEXIIToolkit.Models.DataObjects
{

    public class HATRISSectionStaticData : DataObject
    {
        public String hatrisSectionStaticIdentifier;
        public DateTime publicationTime;
        public PredefinedLocation hatrisSectionStaticData;

        public HATRISSectionStaticData()
        {
        }

        public HATRISSectionStaticData(String hatrisSectionStaticIdentifier, DateTime publicationTime, PredefinedLocation hatrisSectionStaticData)
        {
            this.publicationTime = publicationTime;
            this.hatrisSectionStaticIdentifier = hatrisSectionStaticIdentifier;
            this.hatrisSectionStaticData = hatrisSectionStaticData;
        }

        public String getHATRISSectionStaticIdentifier()
        {
            return hatrisSectionStaticIdentifier;
        }

        public DateTime getPublicationTime()
        {
            return publicationTime;
        }

        public PredefinedLocation getHATRISSectionStaticData()
        {
            return hatrisSectionStaticData;
        }

    }

}