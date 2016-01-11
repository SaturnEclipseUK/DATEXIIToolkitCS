using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DATEXIIToolkit.Models.DataObjects
{
    public class TAMEStaticData : DataObject
    {
        public String tameStaticIdentifier;
        public DateTime publicationTime;
        public MeasurementSiteRecord tameStaticData;

        public TAMEStaticData()
        {
        }

        public TAMEStaticData(String tameStaticIdentifier, DateTime publicationTime, MeasurementSiteRecord tameStaticData)
        {
            this.publicationTime = publicationTime;
            this.tameStaticIdentifier = tameStaticIdentifier;
            this.tameStaticData = tameStaticData;
        }

        public String getTAMEStaticIdentifier()
        {
            return tameStaticIdentifier;
        }

        public DateTime getPublicationTime()
        {
            return publicationTime;
        }

        public MeasurementSiteRecord getTAMEStaticData()
        {
            return tameStaticData;
        }

    }

}