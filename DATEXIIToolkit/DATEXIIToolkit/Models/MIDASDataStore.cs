using DATEXIIToolkit.Models.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DATEXIIToolkit.Models
{
    public class MIDASDataStore : DataStore
    {

    public MIDASDataStore() : base()
    {
    }

    public override void updateData(DataObject data)
    {
            lock (this)
            {
                MIDASData midasData = (MIDASData)data;
                String midasIdentifier = midasData.getMIDASIdentifier();
                if (dataMap.ContainsKey(midasIdentifier))
                {
                    dataMap.Remove(midasIdentifier);
                    dataMap.Add(midasIdentifier, midasData);
                }
                else {
                    dataMap.Add(midasIdentifier, midasData);
                }
            }
    }

    public LinkedList<MIDASData> getAllMIDASData()
    {
            lock (this)
            {
                LinkedList<MIDASData> returnDataObjectsList = new LinkedList<MIDASData>();
                IList<DataObject> dataObjectList = new List<DataObject>(dataMap.Values.ToList());
                foreach (MIDASData dataObject in dataObjectList)
                {
                    returnDataObjectsList.AddLast(dataObject);
                }
                return returnDataObjectsList;
            }
        }

    public override void removeData(String midasIdentifier)
    {
            lock (this)
            {
                if (dataMap.ContainsKey(midasIdentifier))
                {
                    dataMap.Remove(midasIdentifier);
                }
            }
    }
}

}