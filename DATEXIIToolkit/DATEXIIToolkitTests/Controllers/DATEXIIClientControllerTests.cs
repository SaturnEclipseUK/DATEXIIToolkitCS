using Microsoft.VisualStudio.TestTools.UnitTesting;
using DATEXIIToolkit.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Configuration;
using System.IO;
using DATEXIIToolkit.Services;
using DATEXIIToolkit.Models.DataObjects;
using System.Web.Http;
using System.Web.Http.Results;

namespace DATEXIIToolkit.Controllers.Tests
{
    [TestClass()]
    public class DATEXIIClientControllerTests
    {
        /* 
            curl -vX POST -d @C:\Users\Oliver\Pictures\DATEXIIv2-samples\Example-XML\VMS_and_Matrix_Signal_Status_Data_-_Full_Refresh_3793697241479015.xml http://localhost:49519/api/DATEXIIClient/Post
        */
        private DATEXIIClientController datexIIClientController;
        private DataObjectController dataObjectController;
        private DATEXIIUpdateService datexIIUpdateService;

        private void Setup()
        {
            datexIIClientController = new DATEXIIClientController();
            dataObjectController = new DataObjectController();
            datexIIUpdateService = DATEXIIUpdateService.GetInstance();
        }
         
        [TestMethod()]
        [DeploymentItem("..\\..\\..\\DATEXIIToolkitTests\\Messages\\", "Messages")]
        public void VmsTest()
        {
            Setup();
            HttpRequestMessage httpRequest = new HttpRequestMessage(HttpMethod.Post, "test");

            string xml = string.Empty;
            using (StreamReader streamReader = new StreamReader(@"Messages\VMS_and_Matrix_Signal_Status_Data_-_Full_Refresh_3793697241479015.xml", Encoding.UTF8))
            {
                xml = streamReader.ReadToEnd();
            }

            StringContent content = new StringContent(xml, Encoding.UTF8, "application/xml");
            httpRequest.Content = content;
                        
            datexIIClientController.Post(httpRequest);
            while (datexIIUpdateService.workPending()){}

            
            OkNegotiatedContentResult<string> result = (OkNegotiatedContentResult<string>)dataObjectController.vmsDataCount();
            Assert.IsTrue(result.Content.Equals("14652"));

            result = (OkNegotiatedContentResult<string>)dataObjectController.vmsData("CFB613DE831C3254E0433CC411ACFD01");
            Assert.IsTrue(result.Content.Length > 5);

            result = (OkNegotiatedContentResult<string>)dataObjectController.vmsDataAll();
            Assert.IsTrue(result.Content.Length > 5);

            result = (OkNegotiatedContentResult<string>)dataObjectController.vmsStaticDataCount();
            Assert.IsTrue(result.Content.Equals("3476"));

            result = (OkNegotiatedContentResult<string>)dataObjectController.vmsStaticData("CFB613DE831C3254E0433CC411ACFD01");
            Assert.IsTrue(result.Content.Length > 5);
            
            result = (OkNegotiatedContentResult<string>)dataObjectController.vmsStaticDataAll();
            Assert.IsTrue(result.Content.Length > 5);

            result = (OkNegotiatedContentResult<string>)dataObjectController.matrixSignalStaticDataCount();
            Assert.IsTrue(result.Content.Equals("11433"));

            result = (OkNegotiatedContentResult<string>)dataObjectController.matrixSignalStaticData("D250950213C47952E0433CC411ACA994");
            Assert.IsTrue(result.Content.Length > 5);

            result = (OkNegotiatedContentResult<string>)dataObjectController.matrixSignalStaticDataAll();
            Assert.IsTrue(result.Content.Length > 5);
        }

        [TestMethod()]
        [DeploymentItem("..\\..\\..\\DATEXIIToolkitTests\\Messages\\", "Messages")]
        public void TMUTest()
        {
            Setup();
            HttpRequestMessage httpRequest = new HttpRequestMessage(HttpMethod.Post, "test");

            string xml = string.Empty;
            using (StreamReader streamReader = new StreamReader(@"Messages\TMU_Loop_Traffic_Data_3793809993027605.xml", Encoding.UTF8))
            {
                xml = streamReader.ReadToEnd();
            }

            StringContent content = new StringContent(xml, Encoding.UTF8, "application/xml");
            httpRequest.Content = content;

            datexIIClientController.Post(httpRequest);
            while (datexIIUpdateService.workPending()) { }
            /*
            
measurementSiteReference id="C6E971CAD11D789BE0433CC411ACCCEA" version="1.0" targetClass="MeasurementSiteRecord"/>
<measurementTimeDefault>2015-04-29T09:36:00.000+01:00</measurementTimeDefault>
<measuredValue index="0">
<measuredValue>
<basicData xsi:type="TrafficSpeed">
<averageVehicleSpeed>
<dataError>false</dataError>
<speed>102.0</speed>
</averageVehicleSpeed>
</basicData>
</measuredValue>
</measuredValue>
<measuredValue index="1">
<measuredValue>
</measuredValue>
<measuredValue index="2">
<measuredValue>
<basicData xsi:type="TrafficConcentration">
<occupancy>
<dataError>false</dataError>
<percentage>2.0</percentage>
</occupancy>
</basicData>
</measuredValue>
</measuredValue>
<measuredValue index="3">
<measuredValue>
<basicData xsi:type="TrafficFlow">
<vehicleFlow>
<dataError>false</dataError>
<vehicleFlowRate>660</vehicleFlowRate>
</vehicleFlow>
</basicData>
</measuredValue>
</measuredValue>
<measuredValue index="4">
<measuredValue>
<basicData xsi:type="TrafficFlow">
<vehicleFlow>
<dataError>false</dataError>
<vehicleFlowRate>60</vehicleFlowRate>
</vehicleFlow>
</basicData>
</measuredValue>
</measuredValue>
<measuredValue index="5">
<measuredValue>
<basicData xsi:type="TrafficFlow">
<vehicleFlow>
<dataError>false</dataError>
<vehicleFlowRate>0</vehicleFlowRate>
</vehicleFlow>
</basicData>
</measuredValue>
</measuredValue>
<measuredValue index="6">
<measuredValue>
<basicData xsi:type="TrafficFlow">
<vehicleFlow>
<dataError>false</dataError>
<vehicleFlowRate>180</vehicleFlowRate>
</vehicleFlow>
</basicData>
</measuredValue>
</measuredValue>
</siteMeasurements>
<siteMeasurements>
*/

            OkNegotiatedContentResult<string> result = (OkNegotiatedContentResult<string>)dataObjectController.tmuDataCount();
            Assert.IsTrue(result.Content.Equals("111"));

            result = (OkNegotiatedContentResult<string>)dataObjectController.tmuData("C6E971CAD11D789BE0433CC411ACCCEA");
            Assert.IsTrue(result.Content.Length > 5);

            result = (OkNegotiatedContentResult<string>)dataObjectController.tmuDataAll();
            Assert.IsTrue(result.Content.Length > 5);

            result = (OkNegotiatedContentResult<string>)dataObjectController.tmuStaticDataCount();
            Assert.IsTrue(result.Content.Equals("2261"));

            result = (OkNegotiatedContentResult<string>)dataObjectController.tmuStaticData("C6E971CAD11D789BE0433CC411ACCCEA");
            Assert.IsTrue(result.Content.Length > 5);

            result = (OkNegotiatedContentResult<string>)dataObjectController.tmuStaticDataAll();
            Assert.IsTrue(result.Content.Length > 5);
        }

        [TestMethod()]
        [DeploymentItem("..\\..\\..\\DATEXIIToolkitTests\\Messages\\", "Messages")]
        public void MIDASTest()
        {
            Setup();
            HttpRequestMessage httpRequest = new HttpRequestMessage(HttpMethod.Post, "test");

            string xml = string.Empty;
            using (StreamReader streamReader = new StreamReader(@"Messages\MIDAS_Loop_Traffic_Data_3793830663507513.xml", Encoding.UTF8))
            {
                xml = streamReader.ReadToEnd();
            }

            StringContent content = new StringContent(xml, Encoding.UTF8, "application/xml");
            httpRequest.Content = content;

            datexIIClientController.Post(httpRequest);
            while (datexIIUpdateService.workPending()) { }

            OkNegotiatedContentResult<string> result = (OkNegotiatedContentResult<string>)dataObjectController.midasDataCount();
            Assert.IsTrue(result.Content.Equals("113"));

            result = (OkNegotiatedContentResult<string>)dataObjectController.midasData("3E7A28670A4145689D0106E0257790F9");
            Assert.IsTrue(result.Content.Length > 5);

            result = (OkNegotiatedContentResult<string>)dataObjectController.midasDataAll();
            Assert.IsTrue(result.Content.Length > 5);

            result = (OkNegotiatedContentResult<string>)dataObjectController.midasStaticDataCount();
            Assert.IsTrue(result.Content.Equals("6128"));

            result = (OkNegotiatedContentResult<string>)dataObjectController.midasStaticData("3E7A28670A4145689D0106E0257790F9");
            Assert.IsTrue(result.Content.Length > 5);

            result = (OkNegotiatedContentResult<string>)dataObjectController.midasStaticDataAll();
            Assert.IsTrue(result.Content.Length > 5);
        }

        [TestMethod()]
        [DeploymentItem("..\\..\\..\\DATEXIIToolkitTests\\Messages\\", "Messages")]
        public void FVDSensorOnlyTest()
        {
            Setup();
            HttpRequestMessage httpRequest = new HttpRequestMessage(HttpMethod.Post, "test");

            string xml = string.Empty;
            using (StreamReader streamReader = new StreamReader(@"Messages\Fused_Sensor-only_PTD_3793863544426177.xml", Encoding.UTF8))
            {
                xml = streamReader.ReadToEnd();
            }

            StringContent content = new StringContent(xml, Encoding.UTF8, "application/xml");
            httpRequest.Content = content;

            datexIIClientController.Post(httpRequest);
            while (datexIIUpdateService.workPending()) { }

            OkNegotiatedContentResult<string> result = (OkNegotiatedContentResult<string>)dataObjectController.fusedSensorOnlyDataCount();
            Assert.IsTrue(result.Content.Equals("27345"));

            result = (OkNegotiatedContentResult<string>)dataObjectController.fusedSensorOnlyData("TravelTimeData101000101");
            Assert.IsTrue(result.Content.Length > 5);

            result = (OkNegotiatedContentResult<string>)dataObjectController.fusedSensorOnlyDataAll();
            Assert.IsTrue(result.Content.Length > 5);
        }

        [TestMethod()]
        [DeploymentItem("..\\..\\..\\DATEXIIToolkitTests\\Messages\\", "Messages")]
        public void FVDAndSensorTest()
        {
            Setup();
            HttpRequestMessage httpRequest = new HttpRequestMessage(HttpMethod.Post, "test");

            string xml = string.Empty;
            using (StreamReader streamReader = new StreamReader(@"Messages\Fused_FVD_and_Sensor_PTD_3793869820373818.xml", Encoding.UTF8))
            {
                xml = streamReader.ReadToEnd();
            }

            StringContent content = new StringContent(xml, Encoding.UTF8, "application/xml");
            httpRequest.Content = content;

            datexIIClientController.Post(httpRequest);
            while (datexIIUpdateService.workPending()) { }

            OkNegotiatedContentResult<string> result = (OkNegotiatedContentResult<string>)dataObjectController.fusedFVDAndSensorDataCount();
            Assert.IsTrue(result.Content.Equals("31406"));

            result = (OkNegotiatedContentResult<string>)dataObjectController.fusedFVDAndSensorData("TrafficSpeed102003601");
            Assert.IsTrue(result.Content.Length > 5);

            result = (OkNegotiatedContentResult<string>)dataObjectController.fusedFVDAndSensorDataAll();
            Assert.IsTrue(result.Content.Length > 5);
        }

        [TestMethod()]
        [DeploymentItem("..\\..\\..\\DATEXIIToolkitTests\\Messages\\", "Messages")]
        public void EventTest()
        {
            Setup();
            HttpRequestMessage httpRequest = new HttpRequestMessage(HttpMethod.Post, "test");

            string xml = string.Empty;
            using (StreamReader streamReader = new StreamReader(@"Messages\Event_Data_-_Full_Refresh_3794850927130403.xml", Encoding.UTF8))
            {
                xml = streamReader.ReadToEnd();
            }

            StringContent content = new StringContent(xml, Encoding.UTF8, "application/xml");
            httpRequest.Content = content;

            datexIIClientController.Post(httpRequest);
            while (datexIIUpdateService.workPending()) { }

            OkNegotiatedContentResult<string> result = (OkNegotiatedContentResult<string>)dataObjectController.eventDataCount();
            Assert.IsTrue(result.Content.Equals("2276"));

            result = (OkNegotiatedContentResult<string>)dataObjectController.eventData("RW-15-04-28-000517");
            Assert.IsTrue(result.Content.Length > 5);

            result = (OkNegotiatedContentResult<string>)dataObjectController.eventDataAll();
            Assert.IsTrue(result.Content.Length > 5);
        }

        [TestMethod()]
        [DeploymentItem("..\\..\\..\\DATEXIIToolkitTests\\Messages\\", "Messages")]
        public void ANPRTest()
        {
            Setup();
            HttpRequestMessage httpRequest = new HttpRequestMessage(HttpMethod.Post, "test");

            string xml = string.Empty;
            using (StreamReader streamReader = new StreamReader(@"Messages\ANPR_Journey_Time_Data_3794915029662987.xml", Encoding.UTF8))
            {
                xml = streamReader.ReadToEnd();
            }

            StringContent content = new StringContent(xml, Encoding.UTF8, "application/xml");
            httpRequest.Content = content;

            datexIIClientController.Post(httpRequest);
            while (datexIIUpdateService.workPending()) { }

            OkNegotiatedContentResult<string> result = (OkNegotiatedContentResult<string>)dataObjectController.anprDataCount();
            Assert.IsTrue(result.Content.Equals("227"));

            result = (OkNegotiatedContentResult<string>)dataObjectController.anprData("ANPR_Measurement_Site_30071387");
            Assert.IsTrue(result.Content.Length > 5);

            result = (OkNegotiatedContentResult<string>)dataObjectController.anprDataAll();
            Assert.IsTrue(result.Content.Length > 5);
        }

        [TestMethod()]
        [DeploymentItem("..\\..\\..\\DATEXIIToolkitTests\\Messages\\", "Messages")]
        public void NetworkModelUpdateNotificationTest()
        {
            Setup();
            HttpRequestMessage httpRequest = new HttpRequestMessage(HttpMethod.Post, "test");

            string xml = string.Empty;
            using (StreamReader streamReader = new StreamReader(@"Messages\NTIS_Model_Update_Notification_3707368431094454.xml", Encoding.UTF8))
            {
                xml = streamReader.ReadToEnd();
            }

            StringContent content = new StringContent(xml, Encoding.UTF8, "application/xml");
            httpRequest.Content = content;

            datexIIClientController.Post(httpRequest);
            while (datexIIUpdateService.workPending()) { }
        }
    }

    public interface IConfigurationManager
    {
        string GetAppSetting(string key);
    }

    public class ConfigurationManager : IConfigurationManager
    {
        public string GetAppSetting(string key)
        {
            return "test";
        }
    }
}