using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DATEXIIToolkit.Models.DataObjects
{
    public class FusedFVDAndSensorData : DataObject
    {
        public String fusedFVDSensorIdentifier;
        public DateTime publicationTime;
        public DateTime timeDefault;
        public ElaboratedData elaboratedData;

        public FusedFVDAndSensorData()
        {
        }

        public FusedFVDAndSensorData(String fusedFVDSensorIdentifier, DateTime publicationTime, DateTime timeDefault,
                ElaboratedData elaboratedData)
        {
            this.fusedFVDSensorIdentifier = fusedFVDSensorIdentifier;
            this.publicationTime = publicationTime;
            this.timeDefault = timeDefault;
            this.elaboratedData = elaboratedData;
        }

        public String getFusedFVDSensorIdentifier()
        {
            return fusedFVDSensorIdentifier;
        }

        public DateTime getPublicationTime()
        {
            return publicationTime;
        }

        public DateTime getTimeDefault()
        {
            return timeDefault;
        }

        public ElaboratedData getElaboratedData()
        {
            return elaboratedData;
        }

    }

}