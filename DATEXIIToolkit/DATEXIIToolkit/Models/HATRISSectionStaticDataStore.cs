using DATEXIIToolkit.Models.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DATEXIIToolkit.Models
{
    public class HATRISSectionStaticDataStore : DataStore
    {

    public HATRISSectionStaticDataStore() : base()
    {
    }

    public override void updateData(DataObject data)
    {
        lock (this)
        {
            HATRISSectionStaticData hatrisSectionStaticData = (HATRISSectionStaticData)data;
            String hatrisSectionStaticIdentifier = hatrisSectionStaticData.getHATRISSectionStaticIdentifier();
            if (dataMap.ContainsKey(hatrisSectionStaticIdentifier))
            {
                dataMap.Remove(hatrisSectionStaticIdentifier);
                dataMap.Add(hatrisSectionStaticIdentifier, hatrisSectionStaticData);
            }
            else {
                dataMap.Add(hatrisSectionStaticIdentifier, hatrisSectionStaticData);
            }
        }
    }

    public LinkedList<HATRISSectionStaticData> getAllHATRISSectionStaticData()
    {
        lock (this)
        {
            LinkedList<HATRISSectionStaticData> returnDataObjectsList = new LinkedList<HATRISSectionStaticData>();
            IList<DataObject> dataObjectList = new List<DataObject>(dataMap.Values.ToList());
            foreach (HATRISSectionStaticData dataObject in dataObjectList)
            {
                returnDataObjectsList.AddLast(dataObject);
            }
            return returnDataObjectsList;
        }
    }

    public override void removeData(String hatrisSectionStaticIdentifier)
    {
        lock (this)
        {
            if (dataMap.ContainsKey(hatrisSectionStaticIdentifier))
            {
                dataMap.Remove(hatrisSectionStaticIdentifier);
            }
        }
    }
}

}