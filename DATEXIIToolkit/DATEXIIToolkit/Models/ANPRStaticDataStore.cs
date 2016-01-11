using DATEXIIToolkit.Models.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DATEXIIToolkit.Models
{
    public class ANPRStaticDataStore : DataStore
    {

        public ANPRStaticDataStore() : base()
        {
        }

        public override void updateData(DataObject data)
        {
            lock (this)
            {
                ANPRStaticData measurementSiteStaticData = (ANPRStaticData)data;
                String measurementSiteStaticIdentifier = measurementSiteStaticData.getANPRStaticIdentifier();
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


        public LinkedList<ANPRStaticData> getAllANPRStaticData()
        {
            lock (this)
            {
                LinkedList<ANPRStaticData> returnDataObjectsList = new LinkedList<ANPRStaticData>();
                IList<DataObject> dataObjectList = new List<DataObject>(dataMap.Values.ToList());
                foreach (ANPRStaticData dataObject in dataObjectList)
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