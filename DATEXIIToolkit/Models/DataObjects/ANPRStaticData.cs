using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DATEXIIToolkit.Models.DataObjects
{
    public class ANPRStaticData : DataObject
    {

        public String anprStaticIdentifier;
        public DateTime publicationTime;
        public MeasurementSiteRecord anprStaticData;

        public ANPRStaticData()
        {
        }

        public ANPRStaticData(String anprStaticIdentifier, DateTime publicationTime, MeasurementSiteRecord anprStaticData)
        {
            this.publicationTime = publicationTime;
            this.anprStaticIdentifier = anprStaticIdentifier;
            this.anprStaticData = anprStaticData;
        }

        public String getANPRStaticIdentifier()
        {
            return anprStaticIdentifier;
        }

        public DateTime getPublicationTime()
        {
            return publicationTime;
        }

        public MeasurementSiteRecord getANPRStaticData()
        {
            return anprStaticData;
        }

    }

}