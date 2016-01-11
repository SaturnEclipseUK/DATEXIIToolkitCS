using DATEXIIToolkit.Models.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DATEXIIToolkit.Models
{

    public class LinkShapeStaticDataStore : DataStore
    {

        public LinkShapeStaticDataStore() : base()
        {

        }

        public override void updateData(DataObject data)
        {
            lock (this)
            {
                LinkShapeStaticData linkShapeStaticData = (LinkShapeStaticData)data;
                String linkShapeStaticIdentifier = linkShapeStaticData.getLinkShapeStaticIdentifier();
                if (dataMap.ContainsKey(linkShapeStaticIdentifier))
                {
                    dataMap.Remove(linkShapeStaticIdentifier);
                    dataMap.Add(linkShapeStaticIdentifier, linkShapeStaticData);
                }
                else {
                    dataMap.Add(linkShapeStaticIdentifier, linkShapeStaticData);
                }
            }
        }

        public LinkedList<LinkShapeStaticData> getAllLinkShapeStaticData()
        {
            lock (this)
            {
                LinkedList<LinkShapeStaticData> returnDataObjectsList = new LinkedList<LinkShapeStaticData>();
                IList<DataObject> dataObjectList = new List<DataObject>(dataMap.Values.ToList());
                foreach (LinkShapeStaticData dataObject in dataObjectList)
                {
                    returnDataObjectsList.AddLast(dataObject);
                }
                return returnDataObjectsList;
            }
        }

        public override void removeData(String linkShapeStaticIdentifier)
        {
            if (dataMap.ContainsKey(linkShapeStaticIdentifier))
            {
                dataMap.Remove(linkShapeStaticIdentifier);
            }
        }
    }

}