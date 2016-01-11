using DATEXIIToolkit.Models.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DATEXIIToolkit.Models
{
    public class NwkNodeStaticDataStore : DataStore
    {


    public NwkNodeStaticDataStore() : base()
    {
    }

    public override void updateData(DataObject data)
    {
            lock (this)
            {
                NwkNodeStaticData nwkNodeStaticData = (NwkNodeStaticData)data;
                String nwkNodeStaticIdentifier = nwkNodeStaticData.getNwkNodeStaticIdentifier();
                if (dataMap.ContainsKey(nwkNodeStaticIdentifier))
                {
                    dataMap.Remove(nwkNodeStaticIdentifier);
                    dataMap.Add(nwkNodeStaticIdentifier, nwkNodeStaticData);
                }
                else {
                    dataMap.Add(nwkNodeStaticIdentifier, nwkNodeStaticData);
                }
            }
    }


    public LinkedList<NwkNodeStaticData> getAllNwkNodeStaticData()
    {
            lock (this)
            {
                LinkedList<NwkNodeStaticData> returnDataObjectsList = new LinkedList<NwkNodeStaticData>();
                IList<DataObject> dataObjectList = new List<DataObject>(dataMap.Values.ToList());
                foreach (NwkNodeStaticData dataObject in dataObjectList)
                {
                    returnDataObjectsList.AddLast(dataObject);
                }
                return returnDataObjectsList;
            }
        }

    public override void removeData(String nwkNodeStaticIdentifier)
    {
            lock (this)
            {
                if (dataMap.ContainsKey(nwkNodeStaticIdentifier))
                {
                    dataMap.Remove(nwkNodeStaticIdentifier);
                }
            }
    }
}

}