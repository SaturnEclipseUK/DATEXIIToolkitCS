using DATEXIIToolkit.Models.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DATEXIIToolkit.Models
{
    /// <summary>
    /// This data store contains the FusedFVDAndSensor data indexed by link ID.
    /// </summary>
    public class FusedFVDAndSensorDataStore : DataStore
    {
        
        public FusedFVDAndSensorDataStore() : base()
        {
        }

        public override void updateData(DataObject data)
        {
            lock (this)
            {
                FusedFVDAndSensorData fusedFVDAndSensorData = (FusedFVDAndSensorData)data;
                String linkIdentifier = fusedFVDAndSensorData.getFusedFVDSensorIdentifier();
                if (dataMap.ContainsKey(linkIdentifier))
                {
                    dataMap.Remove(linkIdentifier);
                    dataMap.Add(linkIdentifier, fusedFVDAndSensorData);
                }
                else {
                    dataMap.Add(linkIdentifier, fusedFVDAndSensorData);
                }
            }
        }

        public LinkedList<FusedFVDAndSensorData> getAllFusedFVDAndSensorData()
        {
            lock (this)
            {
                LinkedList<FusedFVDAndSensorData> returnDataObjectsList = new LinkedList<FusedFVDAndSensorData>();
                IList<DataObject> dataObjectList = new List<DataObject>(dataMap.Values.ToList());
                foreach (FusedFVDAndSensorData dataObject in dataObjectList)
                {
                    returnDataObjectsList.AddLast(dataObject);
                }
                return returnDataObjectsList;
            }
        }

        public override void removeData(String linkIdentifier)
        {
            lock (this)
            {
                if (dataMap.ContainsKey(linkIdentifier))
                {
                    dataMap.Remove(linkIdentifier);
                }
            }
        }
    }

}