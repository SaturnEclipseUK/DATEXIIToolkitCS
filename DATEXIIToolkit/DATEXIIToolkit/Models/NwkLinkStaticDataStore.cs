using DATEXIIToolkit.Models.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DATEXIIToolkit.Models
{
    
    public class NwkLinkStaticDataStore : DataStore
    {


    public NwkLinkStaticDataStore() : base()
    {
    }

    public override void updateData(DataObject data)
    {
            lock (this)
            {
                NwkLinkStaticData nwkLinkStaticData = (NwkLinkStaticData)data;
                String nwkLinkStaticIdentifier = nwkLinkStaticData.getNwkLinkStaticIdentifier();
                if (dataMap.ContainsKey(nwkLinkStaticIdentifier))
                {
                    dataMap.Remove(nwkLinkStaticIdentifier);
                    dataMap.Add(nwkLinkStaticIdentifier, nwkLinkStaticData);
                }
                else {
                    dataMap.Add(nwkLinkStaticIdentifier, nwkLinkStaticData);
                }
            }
    }


    public LinkedList<NwkLinkStaticData> getAllNwkLinkStaticData()
    {
            lock (this)
            {
                LinkedList<NwkLinkStaticData> returnDataObjectsList = new LinkedList<NwkLinkStaticData>();
                IList<DataObject> dataObjectList = new List<DataObject>(dataMap.Values.ToList());
                foreach (NwkLinkStaticData dataObject in dataObjectList)
                {
                    returnDataObjectsList.AddLast(dataObject);
                }
                return returnDataObjectsList;
            }
        }

    public override void removeData(String nwkLinkStaticIdentifier)
    {
            lock (this)
            {
                if (dataMap.ContainsKey(nwkLinkStaticIdentifier))
                {
                    dataMap.Remove(nwkLinkStaticIdentifier);
                }
            }
    }
}

}