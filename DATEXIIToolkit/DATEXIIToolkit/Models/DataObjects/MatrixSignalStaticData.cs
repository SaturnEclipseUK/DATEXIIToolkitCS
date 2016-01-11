using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DATEXIIToolkit.Models.DataObjects
{

    public class MatrixSignalStaticData : DataObject
    {
        public String matrixSignalStaticIdentifier;
        public DateTime publicationTime;
        public VmsUnitRecord matrixSignalStaticData;

        public MatrixSignalStaticData()
        {
        }

        public MatrixSignalStaticData(String matrixSignalStaticIdentifier, DateTime publicationTime, VmsUnitRecord matrixSignalStaticData)
        {
            this.publicationTime = publicationTime;
            this.matrixSignalStaticIdentifier = matrixSignalStaticIdentifier;
            this.matrixSignalStaticData = matrixSignalStaticData;
        }

        public String getMatrixSignalStaticIdentifier()
        {
            return matrixSignalStaticIdentifier;
        }

        public DateTime getPublicationTime()
        {
            return publicationTime;
        }

        public VmsUnitRecord getMatrixSignalStaticData()
        {
            return matrixSignalStaticData;
        }
    }
}