using DATEXIIToolkit.Models.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DATEXIIToolkit.Models
{
    /// <summary>
    /// This data store contains the FusedSensorOnly data indexed by link ID.
    /// </summary>
    public class FusedSensorOnlyDataStore : DataStore
    {

        public FusedSensorOnlyDataStore() : base()
        {

        }

        public override void updateData(DataObject data)
        {
            lock (this)
            {
                FusedSensorOnlyData fusedSensorOnlyData = (FusedSensorOnlyData)data;
                String linkIdentifier = fusedSensorOnlyData.getFusedSensorOnlyIdentifier();
                if (dataMap.ContainsKey(linkIdentifier))
                {
                    dataMap.Remove(linkIdentifier);
                    dataMap.Add(linkIdentifier, fusedSensorOnlyData);
                }
                else {
                    dataMap.Add(linkIdentifier, fusedSensorOnlyData);
                }
            }
        }

        public LinkedList<FusedSensorOnlyData> getAllFusedSensorOnlyData()
        {
            lock (this)
            {
                LinkedList<FusedSensorOnlyData> returnDataObjectsList = new LinkedList<FusedSensorOnlyData>();
                IList<DataObject> dataObjectList = new List<DataObject>(dataMap.Values.ToList());
                foreach (FusedSensorOnlyData dataObject in dataObjectList)
                {
                    returnDataObjectsList.AddLast(dataObject);
                }
                return returnDataObjectsList;
            }
        }

        public override void removeData(String linkIdentifier)
        {
            if (dataMap.ContainsKey(linkIdentifier))
            {
                dataMap.Remove(linkIdentifier);
            }
        }
    }

}