using DATEXIIToolkit.Models.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DATEXIIToolkit.Models
{
    public class VMSDataStore : DataStore
    {
        public VMSDataStore() : base(){}

    public override void updateData(DataObject data)
    {
            lock (this)
            {
                VMSData vmsData = (VMSData)data;
                String vmsIdentifier = vmsData.getVmsIdentifier();
                if (dataMap.ContainsKey(vmsIdentifier))
                {
                    dataMap.Remove(vmsIdentifier);
                    dataMap.Add(vmsIdentifier, vmsData);
                }
                else {
                    dataMap.Add(vmsIdentifier, vmsData);
                }
            }
    }


    public LinkedList<VMSData> getAllVMSData()
    {
            lock (this)
            {
                LinkedList<VMSData> returnDataObjectsList = new LinkedList<VMSData>();
                IList<DataObject> dataObjectList = new List<DataObject>(dataMap.Values.ToList());
                foreach (DataObject dataObject in dataObjectList)
                {
                    returnDataObjectsList.AddLast((VMSData)dataObject);
                }
                return returnDataObjectsList;
            }
        }

    public override void removeData(String vmsIdentifier)
    {
            lock (this)
            {
                if (dataMap.ContainsKey(vmsIdentifier))
                {
                    dataMap.Remove(vmsIdentifier);
                }
            }
    }
}

}