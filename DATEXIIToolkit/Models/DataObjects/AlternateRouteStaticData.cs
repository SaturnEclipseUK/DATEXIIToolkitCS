using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DATEXIIToolkit.Models.DataObjects
{

    public class AlternateRouteStaticData : DataObject
    {
        public String alternateRouteStaticIdentifier;
        public DateTime publicationTime;
        public PredefinedLocation alternateRouteStaticData;

        public AlternateRouteStaticData()
        {
        }

        public AlternateRouteStaticData(String alternateRouteStaticIdentifier, DateTime publicationTime, PredefinedLocation alternateRouteStaticData)
        {
            this.publicationTime = publicationTime;
            this.alternateRouteStaticIdentifier = alternateRouteStaticIdentifier;
            this.alternateRouteStaticData = alternateRouteStaticData;
        }

        public String getAlternateRouteStaticIdentifier()
        {
            return alternateRouteStaticIdentifier;
        }

        public DateTime getPublicationTime()
        {
            return publicationTime;
        }

        public PredefinedLocation getAlternateRouteStaticData()
        {
            return alternateRouteStaticData;
        }

    }
}



