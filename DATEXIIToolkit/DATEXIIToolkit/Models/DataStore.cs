using DATEXIIToolkit.Models.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DATEXIIToolkit.Models
{
    public abstract class DataStore
    {

        protected Dictionary<String, DataObject> dataMap;

        public DataStore()
        {
            dataMap = new Dictionary<String, DataObject>();
        }

        public void clearDataStore()
        {
            lock (this) {
                dataMap.Clear();
            }
        }

        public abstract void updateData(DataObject data);

        public DataObject getData(String identifier)
        {
            if (dataMap.ContainsKey(identifier))
            {
                return dataMap[identifier];
            }
            return null;
        }

        public virtual void removeData(String identifier)
        {
            lock (this)
            {
                if (dataMap.ContainsKey(identifier))
                {
                    dataMap.Remove(identifier);
                }
            }
        }
    }

}