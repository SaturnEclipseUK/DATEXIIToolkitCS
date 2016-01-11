using DATEXIIToolkit.Models.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DATEXIIToolkit.Models
{

public class MatrixSignalStaticDataStore : DataStore
    {

    public MatrixSignalStaticDataStore() : base()
    {
    }

    public override void updateData(DataObject data)
    {
            lock (this)
            {
                MatrixSignalStaticData matrixSignalStaticData = (MatrixSignalStaticData)data;
                String matrixSignalIdentifier = matrixSignalStaticData.getMatrixSignalStaticIdentifier();
                if (dataMap.ContainsKey(matrixSignalIdentifier))
                {
                    dataMap.Remove(matrixSignalIdentifier);
                    dataMap.Add(matrixSignalIdentifier, matrixSignalStaticData);
                }
                else {
                    dataMap.Add(matrixSignalIdentifier, matrixSignalStaticData);
                }
            }
    }

    public LinkedList<MatrixSignalStaticData> getAllMatrixSignalStaticData()
    {
            lock (this)
            {
                LinkedList<MatrixSignalStaticData> returnDataObjectsList = new LinkedList<MatrixSignalStaticData>();
                IList<DataObject> dataObjectList = new List<DataObject>(dataMap.Values.ToList());
                foreach (MatrixSignalStaticData dataObject in dataObjectList)
                {
                    returnDataObjectsList.AddLast(dataObject);
                }
                return returnDataObjectsList;
            }
        }

    public override void removeData(String matrixSignalIdentifier)
    {
            lock (this)
            {
                if (dataMap.ContainsKey(matrixSignalIdentifier))
                {
                    dataMap.Remove(matrixSignalIdentifier);
                }
            }
    }
}

}