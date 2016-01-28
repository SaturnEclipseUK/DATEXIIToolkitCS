using DATEXIIToolkit.Models.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DATEXIIToolkit.Models
{
    /// <summary>
    /// This data store contains the VMSStatic data indexed by VMS unit record ID.
    /// </summary>
    public class VMSStaticDataStore : DataStore
    {
        public VMSStaticDataStore() : base()
        {
        }

        public override void updateData(DataObject data)
        {
                lock (this)
                {
                    VMSStaticData vmsStaticData = (VMSStaticData)data;
                    String vmsStaticIdentifier = vmsStaticData.getVMSStaticIdentifier();
                    if (dataMap.ContainsKey(vmsStaticIdentifier))
                    {
                        dataMap.Remove(vmsStaticIdentifier);
                        dataMap.Add(vmsStaticIdentifier, vmsStaticData);
                    }
                    else {
                        dataMap.Add(vmsStaticIdentifier, vmsStaticData);
                    }
                }
        }
         

        public LinkedList<VMSStaticData> getAllVMSStaticData()
        {
                lock (this)
                {
                    LinkedList<VMSStaticData> returnDataObjectsList = new LinkedList<VMSStaticData>();
                    IList<DataObject> dataObjectList = new List<DataObject>(dataMap.Values.ToList());
                    foreach (VMSStaticData dataObject in dataObjectList)
                    {
                        returnDataObjectsList.AddLast(dataObject);
                    }
                    return returnDataObjectsList;
                }
            }

        public override void removeData(String vmsStaticIdentifier)
        {
            if (dataMap.ContainsKey(vmsStaticIdentifier))
            {
                dataMap.Remove(vmsStaticIdentifier);
            }
        }
    }

}