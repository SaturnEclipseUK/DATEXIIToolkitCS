using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DATEXIIToolkit.Models.DataObjects
{

    public class VMSStaticData : DataObject
    {
        public String vmsStaticIdentifier;
        public DateTime publicationTime;
        public VmsUnitRecord vmsStaticData;

        public VMSStaticData()
        {
        }

        public VMSStaticData(String vmsStaticIdentifier, DateTime publicationTime, VmsUnitRecord vmsStaticData)
        {
            this.publicationTime = publicationTime;
            this.vmsStaticIdentifier = vmsStaticIdentifier;
            this.vmsStaticData = vmsStaticData;
        }

        public String getVMSStaticIdentifier()
        {
            return vmsStaticIdentifier;
        }

        public DateTime getPublicationTime()
        {
            return publicationTime;
        }

        public VmsUnitRecord getVMSStaticData()
        {
            return vmsStaticData;
        }

    }

}