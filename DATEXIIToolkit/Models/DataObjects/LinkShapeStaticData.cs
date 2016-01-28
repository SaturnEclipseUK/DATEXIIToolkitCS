using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DATEXIIToolkit.Models.DataObjects
{
    public class LinkShapeStaticData : DataObject
    {
        public String linkShapeStaticIdentifier;
        public DateTime publicationTime;
        public PredefinedLocation linkShapeStaticData;

        public LinkShapeStaticData()
        {
        }

        public LinkShapeStaticData(String linkShapeStaticIdentifier, DateTime publicationTime, PredefinedLocation linkShapeStaticData)
        {
            this.publicationTime = publicationTime;
            this.linkShapeStaticIdentifier = linkShapeStaticIdentifier;
            this.linkShapeStaticData = linkShapeStaticData;
        }

        public String getLinkShapeStaticIdentifier()
        {
            return linkShapeStaticIdentifier;
        }

        public DateTime getPublicationTime()
        {
            return publicationTime;
        }

        public PredefinedLocation getLinkShapeStaticData()
        {
            return linkShapeStaticData;
        }
    }
}