using DATEXIIToolkit.Models.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DATEXIIToolkit.Models
{
    public class ANPRDataStore : DataStore
    {

        public ANPRDataStore() : base()
        {
        }

        public override void updateData(DataObject data)
        {
            lock (this)
            {
                ANPRData anprData = (ANPRData)data;
                String anprIdentifier = anprData.getAnprIdentifier();
                if (dataMap.ContainsKey(anprIdentifier))
                {
                    dataMap.Remove(anprIdentifier);
                    dataMap.Add(anprIdentifier, anprData);
                }
                else {
                    dataMap.Add(anprIdentifier, anprData);
                }
            }
        }

	    public LinkedList<ANPRData> getAllANPRData()
        {
            lock (this)
            {
               LinkedList<ANPRData> returnDataObjectsList = new LinkedList<ANPRData>();
                IList<DataObject> dataObjectList = new List<DataObject>(dataMap.Values.ToList());
                foreach (ANPRData dataObject in dataObjectList)
                {
                    returnDataObjectsList.AddLast(dataObject);
                }
                return returnDataObjectsList;
            }
        }

        public override void removeData(String anprIdentifier)
        {
            lock (this)
            {
                if (dataMap.ContainsKey(anprIdentifier))
                {
                    dataMap.Remove(anprIdentifier);
                }
            }
        }
    }

}