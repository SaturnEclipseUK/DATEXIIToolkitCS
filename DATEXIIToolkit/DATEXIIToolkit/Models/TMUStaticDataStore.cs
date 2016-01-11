using DATEXIIToolkit.Models.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DATEXIIToolkit.Models
{
    public class TMUStaticDataStore : DataStore
    {


    public TMUStaticDataStore() : base()
    {
    }

    public override void updateData(DataObject data)
    {
            lock (this)
            {
                TMUStaticData measurementSiteStaticData = (TMUStaticData)data;
                String measurementSiteStaticIdentifier = measurementSiteStaticData.getTMUStaticIdentifier();
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


    public LinkedList<TMUStaticData> getAllTMUStaticData()
    {
            lock (this)
            {
                LinkedList<TMUStaticData> returnDataObjectsList = new LinkedList<TMUStaticData>();
                IList<DataObject> dataObjectList = new List<DataObject>(dataMap.Values.ToList());
                foreach (TMUStaticData dataObject in dataObjectList)
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