using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DATEXIIToolkit.Models.DataObjects
{

    public class FusedSensorOnlyData : DataObject
    {
        public String fusedSensorOnlyIdentifier;
        public DateTime publicationTime;
        public DateTime timeDefault;
        public ElaboratedData elaboratedData;

        public FusedSensorOnlyData()
        {
        }

        public FusedSensorOnlyData(String fusedSensorOnlyIdentifier, DateTime publicationTime, DateTime timeDefault,
                ElaboratedData elaboratedData)
        {
            this.fusedSensorOnlyIdentifier = fusedSensorOnlyIdentifier;
            this.publicationTime = publicationTime;
            this.timeDefault = timeDefault;
            this.elaboratedData = elaboratedData;
        }

        public String getFusedSensorOnlyIdentifier()
        {
            return fusedSensorOnlyIdentifier;
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