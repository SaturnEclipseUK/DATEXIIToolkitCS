using DATEXIIToolkit.Models.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DATEXIIToolkit.Models
{
    public class MIDASStaticDataStore : DataStore
    {


    public MIDASStaticDataStore() : base()
    {
    }

    public override void updateData(DataObject data)
    {
            lock (this)
            {
                MIDASStaticData measurementSiteStaticData = (MIDASStaticData)data;
                String measurementSiteStaticIdentifier = measurementSiteStaticData.getMIDASStaticIdentifier();
                if (dataMap.ContainsKey(measurementSiteStaticIdentifier))
                {
                    dataMap.Remove(measurementSiteStaticIdentifier);
                    dataMap.Add(measurementSiteStaticIdentifier, measurementSiteStaticData);
                }
                else {
                    dataMap.Add(measurementSiteStaticIdentifier, measurementSiteStaticData);
                }
            }
    }

    public LinkedList<MIDASStaticData> getAllMIDASStaticData()
    {
            lock (this)
            {
                LinkedList<MIDASStaticData> returnDataObjectsList = new LinkedList<MIDASStaticData>();
                IList<DataObject> dataObjectList = new List<DataObject>(dataMap.Values.ToList());
                foreach (MIDASStaticData dataObject in dataObjectList)
                {
                    returnDataObjectsList.AddLast(dataObject);
                }
                return returnDataObjectsList;
            }
        }

        public override void removeData(String measurementSiteStaticIdentifier)
        {
            lock (this)
            {
                if (dataMap.ContainsKey(measurementSiteStaticIdentifier))
                {
                    dataMap.Remove(measurementSiteStaticIdentifier);
                }
            }
        }
    }

}