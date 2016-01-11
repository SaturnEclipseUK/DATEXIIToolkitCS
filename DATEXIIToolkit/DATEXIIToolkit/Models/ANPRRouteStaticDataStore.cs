using DATEXIIToolkit.Models.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DATEXIIToolkit.Models
{

    public class ANPRRouteStaticDataStore : DataStore
    {
        public ANPRRouteStaticDataStore() : base()
        {
        }

        public override void updateData(DataObject data)
        {
            lock (this)
            {
                ANPRRouteStaticData anprRouteStaticData = (ANPRRouteStaticData)data;
                String anprRouteStaticIdentifier = anprRouteStaticData.getANPRRouteStaticIdentifier();
                if (dataMap.ContainsKey(anprRouteStaticIdentifier))
                {
                    dataMap.Remove(anprRouteStaticIdentifier);
                    dataMap.Add(anprRouteStaticIdentifier, anprRouteStaticData);
                }
                else {
                    dataMap.Add(anprRouteStaticIdentifier, anprRouteStaticData);
                }
            }
        }

        public LinkedList<ANPRRouteStaticData> getAllANPRRouteStaticData()
        {
            lock (this)
            {
                LinkedList<ANPRRouteStaticData> returnDataObjectsList = new LinkedList<ANPRRouteStaticData>();
                IList<DataObject> dataObjectList = new List<DataObject>(dataMap.Values.ToList());
                foreach (ANPRRouteStaticData dataObject in dataObjectList)
                {
                    returnDataObjectsList.AddLast(dataObject);
                }
                return returnDataObjectsList;
            }
        }

        public override void removeData(String anprRouteStaticIdentifier)
        {
            lock (this)
            {
                if (dataMap.ContainsKey(anprRouteStaticIdentifier))
                {
                    dataMap.Remove(anprRouteStaticIdentifier);
                }
            }
        }
    }

}