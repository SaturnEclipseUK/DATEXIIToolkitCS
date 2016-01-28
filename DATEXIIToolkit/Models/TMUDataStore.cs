using DATEXIIToolkit.Models.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DATEXIIToolkit.Models
{
    /// <summary>
    /// This data store contains the TMU data indexed by measurement site reference ID.
    /// </summary>
    public class TMUDataStore : DataStore
    {
 
        public TMUDataStore() : base()
        {
        }

        public override void updateData(DataObject data)
        {
                lock (this)
                {
                    TMUData tmuData = (TMUData)data;
                    String tmuIdentifier = tmuData.getTMUIdentifier();
                    if (dataMap.ContainsKey(tmuIdentifier))
                    {
                        dataMap.Remove(tmuIdentifier);
                        dataMap.Add(tmuIdentifier, tmuData);
                    }
                    else {
                        dataMap.Add(tmuIdentifier, tmuData);
                    }
                }
        }


        public LinkedList<TMUData> getAllTMUData()
        {
                lock (this)
                {
                    LinkedList<TMUData> returnDataObjectsList = new LinkedList<TMUData>();
                    IList<DataObject> dataObjectList = new List<DataObject>(dataMap.Values.ToList());
                    foreach (TMUData dataObject in dataObjectList)
                    {
                        returnDataObjectsList.AddLast(dataObject);
                    }
                    return returnDataObjectsList;
                }
        }

        public override void removeData(String tmuIdentifier)
        {
                lock (this)
                {
                    if (dataMap.ContainsKey(tmuIdentifier))
                    {
                        dataMap.Remove(tmuIdentifier);
                    }
                }
        }
    }

}