using DATEXIIToolkit.Models.DataObjects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DATEXIIToolkit.Models
{
    /// <summary>
    /// This data store contains the AlternateRouteStatic data indexed by predefined location ID.
    /// </summary>
    public class AlternateRouteStaticDataStore : DataStore
    {

        public AlternateRouteStaticDataStore() : base()
        {

        }

        public override void updateData(DataObject data)
        {
            lock (this)
            {
                AlternateRouteStaticData alternateRouteStaticData = (AlternateRouteStaticData)data;
                String alternateRouteStaticIdentifier = alternateRouteStaticData.getAlternateRouteStaticIdentifier();
                if (dataMap.ContainsKey(alternateRouteStaticIdentifier))
                {
                    dataMap.Remove(alternateRouteStaticIdentifier);
                    dataMap.Add(alternateRouteStaticIdentifier, alternateRouteStaticData);
                }
                else {
                    dataMap.Add(alternateRouteStaticIdentifier, alternateRouteStaticData);
                }
            }
        }

	    public List<AlternateRouteStaticData> getAllAlternateRouteStaticData()
        {
            lock (this)
            {
                List<AlternateRouteStaticData> returnAlternativeRoutesList = new List<AlternateRouteStaticData>();
                IList<DataObject> alternativeRoutesList = new List<DataObject>(dataMap.Values.ToList());
                foreach (AlternateRouteStaticData dataObject in alternativeRoutesList)
                {
                    returnAlternativeRoutesList.Add(dataObject);
                }
                return returnAlternativeRoutesList;
            }
        }

        public override void removeData(String alternateRouteStaticIdentifier)
        {
            lock (this)
            {
                if (dataMap.ContainsKey(alternateRouteStaticIdentifier))
                {
                    dataMap.Remove(alternateRouteStaticIdentifier);
                }
            }
        }
    }
}