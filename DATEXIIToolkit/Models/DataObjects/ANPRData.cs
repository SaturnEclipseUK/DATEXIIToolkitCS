using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DATEXIIToolkit.Models.DataObjects
{

    public class ANPRData : DataObject
    {

        public String anprIdentifier;
        public DateTime publicationTime;
        public List<SiteMeasurements> anprData;

    public ANPRData()
    {
    }

    public ANPRData(String anprIdentifier, DateTime publicationTime)
    {
        this.anprIdentifier = anprIdentifier;
        this.publicationTime = publicationTime;
        this.anprData = new LinkedList<SiteMeasurements>().ToList();
    }

    public String getAnprIdentifier()
    {
        return anprIdentifier;
    }

    public DateTime getPublicationTime()
    {
        return publicationTime;
    }

    public List<SiteMeasurements> getAnprData()
    {
        return anprData;
    }

    public void addSiteMeasurements(SiteMeasurements siteMeasurements)
    {
        anprData.Add(siteMeasurements);
    }

}
}


