using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DATEXIIToolkit.Models.DataObjects
{
    public class ANPRRouteStaticData : DataObject
    {
        public String anprRouteStaticIdentifier;
        public DateTime publicationTime;
        public PredefinedLocation anprRouteStaticData;

        public ANPRRouteStaticData()
        {
        }

        public ANPRRouteStaticData(String anprRouteStaticIdentifier, DateTime publicationTime, PredefinedLocation anprRouteStaticData)
        {
            this.publicationTime = publicationTime;
            this.anprRouteStaticIdentifier = anprRouteStaticIdentifier;
            this.anprRouteStaticData = anprRouteStaticData;
        }

        public String getANPRRouteStaticIdentifier()
        {
            return anprRouteStaticIdentifier;
        }

        public DateTime getPublicationTime()
        {
            return publicationTime;
        }

        public PredefinedLocation getANPRRouteStaticData()
        {
            return anprRouteStaticData;
        }
    }

}