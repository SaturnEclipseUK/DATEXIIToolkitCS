using DATEXIIToolkit.Models.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DATEXIIToolkit.Models
{
    public class TAMEStaticDataStore : DataStore
    {


    public TAMEStaticDataStore() : base()
    {
    }

    public override void updateData(DataObject data)
    {
            lock (this)
            {
                TAMEStaticData measurementSiteStaticData = (TAMEStaticData)data;
                String measurementSiteStaticIdentifier = measurementSiteStaticData.getTAMEStaticIdentifier();
                if (dataMap.ContainsKey(measurementSiteStaticIdentifier))
                {
                    dataMap.Remove(measurementSiteStaticIdentifier);
                    dataMap.Add(measurementSiteStaticIdentifier, measurementSiteStaticData);
                }
                else {
                    dataMap.Add(measurementSiteStaticIdentifier, measurementSiteStaticData);
                }
            }
    }


    public LinkedList<TAMEStaticData> getAllTAMEStaticData()
    {
            lock (this)
            {
                LinkedList<TAMEStaticData> returnDataObjectsList = new LinkedList<TAMEStaticData>();
                IList<DataObject> dataObjectList = new List<DataObject>(dataMap.Values.ToList());
                foreach (TAMEStaticData dataObject in dataObjectList)
                {
                    returnDataObjectsList.AddLast(dataObject);
                }
                return returnDataObjectsList;
            }
        }

    public override void removeData(String measurementSiteStaticIdentifier)
    {
            lock (this)
            {
                if (dataMap.ContainsKey(measurementSiteStaticIdentifier))
                {
                    dataMap.Remove(measurementSiteStaticIdentifier);
                }
            }
    }
}

}