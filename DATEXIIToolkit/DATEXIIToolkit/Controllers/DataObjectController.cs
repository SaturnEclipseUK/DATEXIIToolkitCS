using DATEXIIToolkit.Common;
using DATEXIIToolkit.Models;
using DATEXIIToolkit.Models.DataObjects;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Runtime.Serialization.Formatters;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace DATEXIIToolkit.Controllers
{
    public class DataObjectController : ApiController
    {
        LogWrapper logWrapper;

        public DataObjectController()
        {
            logWrapper = new LogWrapper("DataObjectController");
        }               

        [Route("api/vms/all")]
        [HttpGet]
        public IHttpActionResult vmsDataAll()
        {
            VMSDataStore vmsDataStore = (VMSDataStore)DataStoreFactory.GetInstance().GetDataStore(DataStoreFactory.DATA_STORES.VMS_DATA_STORE);
            return Json(vmsDataStore.getAllVMSData(), new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });         
        }

        [Route("api/vms")]       
        [HttpGet]
        public IHttpActionResult vmsData(string id)
        {
            VMSDataStore vmsDataStore = (VMSDataStore)DataStoreFactory.GetInstance().GetDataStore(DataStoreFactory.DATA_STORES.VMS_DATA_STORE);
            return Json(vmsDataStore.getData(id), new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
        }

        [Route("api/vms/count")]
        [HttpGet]
        public IHttpActionResult vmsDataCount()
        {
            VMSDataStore vmsDataStore = (VMSDataStore)DataStoreFactory.GetInstance().GetDataStore(DataStoreFactory.DATA_STORES.VMS_DATA_STORE);
            return Json(vmsDataStore.getAllVMSData().Count, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
        }

        [Route("api/event")]
        [HttpGet]
        public IHttpActionResult eventData(string id) {
            EventDataStore eventDataStore = (EventDataStore)DataStoreFactory.GetInstance().GetDataStore(DataStoreFactory.DATA_STORES.EVENT_DATA_STORE);
            return Json(eventDataStore.getData(id), new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
        }

        [Route("api/event/all")]
        [HttpGet]
        public IHttpActionResult eventDataAll()
        {
            EventDataStore eventDataStore = (EventDataStore)DataStoreFactory.GetInstance().GetDataStore(DataStoreFactory.DATA_STORES.EVENT_DATA_STORE);
            return Json(eventDataStore.getAllEventData(), new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
        }

        [Route("api/event/count")]
        [HttpGet]
        public IHttpActionResult eventDataCount()
        {
            EventDataStore eventDataStore = (EventDataStore)DataStoreFactory.GetInstance().GetDataStore(DataStoreFactory.DATA_STORES.EVENT_DATA_STORE);
            return Json(eventDataStore.getAllEventData().Count, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
        }

        [Route("api/anpr")]
        [HttpGet]
        public IHttpActionResult anprData(string id) {
            ANPRDataStore anprDataStore = (ANPRDataStore)DataStoreFactory.GetInstance().GetDataStore(DataStoreFactory.DATA_STORES.ANPR_DATA_STORE);
            return Json(anprDataStore.getData(id), new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
        }

        [Route("api/anpr/all")]
        [HttpGet]
        public IHttpActionResult anprDataAll()
        {
            ANPRDataStore anprDataStore = (ANPRDataStore)DataStoreFactory.GetInstance().GetDataStore(DataStoreFactory.DATA_STORES.ANPR_DATA_STORE);
            return Json(anprDataStore.getAllANPRData(), new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
        }

        [Route("api/anpr/count")]
        [HttpGet]
        public IHttpActionResult anprDataCount()
        {
            ANPRDataStore anprDataStore = (ANPRDataStore)DataStoreFactory.GetInstance().GetDataStore(DataStoreFactory.DATA_STORES.ANPR_DATA_STORE);
            return Json(anprDataStore.getAllANPRData().Count, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
        }

        [Route("api/fusedFVDAndSensor")]
        [HttpGet]
        public IHttpActionResult fusedFVDAndSensorData(string id) {
            FusedFVDAndSensorDataStore fusedFVDAndSensorDataStore = (FusedFVDAndSensorDataStore)DataStoreFactory.GetInstance().GetDataStore(DataStoreFactory.DATA_STORES.FUSED_FVD_AND_SENSOR_DATA_STORE);
            return Json(fusedFVDAndSensorDataStore.getData(id), new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
        }

        [Route("api/fusedFVDAndSensor/all")]
        [HttpGet]
        public IHttpActionResult fusedFVDAndSensorDataAll()
        {
            FusedFVDAndSensorDataStore fusedFVDAndSensorDataStore = (FusedFVDAndSensorDataStore)DataStoreFactory.GetInstance().GetDataStore(DataStoreFactory.DATA_STORES.FUSED_FVD_AND_SENSOR_DATA_STORE);
            return Json(fusedFVDAndSensorDataStore.getAllFusedFVDAndSensorData(), new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
        }

        [Route("api/fusedFVDAndSensor/count")]
        [HttpGet]
        public IHttpActionResult fusedFVDAndSensorDataCount()
        {
            FusedFVDAndSensorDataStore fusedFVDAndSensorDataStore = (FusedFVDAndSensorDataStore)DataStoreFactory.GetInstance().GetDataStore(DataStoreFactory.DATA_STORES.FUSED_FVD_AND_SENSOR_DATA_STORE);
            return Json(fusedFVDAndSensorDataStore.getAllFusedFVDAndSensorData().Count, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
        }

        [Route("api/fusedSensorOnly")]
        [HttpGet]
        public IHttpActionResult fusedSensorOnlyData(string id)
        {
            FusedSensorOnlyDataStore fusedSensorOnlyDataStore = (FusedSensorOnlyDataStore)DataStoreFactory.GetInstance().GetDataStore(DataStoreFactory.DATA_STORES.FUSED_SENSOR_ONLY_DATA_STORE);
            return Json(fusedSensorOnlyDataStore.getData(id), new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
        }

        [Route("api/fusedSensorOnly/all")]
        [HttpGet]
        public IHttpActionResult fusedSensorOnlyDataAll()
        {
            FusedSensorOnlyDataStore fusedSensorOnlyDataStore = (FusedSensorOnlyDataStore)DataStoreFactory.GetInstance().GetDataStore(DataStoreFactory.DATA_STORES.FUSED_SENSOR_ONLY_DATA_STORE);
            return Json(fusedSensorOnlyDataStore.getAllFusedSensorOnlyData(), new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
        }

        [Route("api/fusedSensorOnly/count")]
        [HttpGet]
        public IHttpActionResult fusedSensorOnlyDataCount()
        {
            FusedSensorOnlyDataStore fusedSensorOnlyDataStore = (FusedSensorOnlyDataStore)DataStoreFactory.GetInstance().GetDataStore(DataStoreFactory.DATA_STORES.FUSED_SENSOR_ONLY_DATA_STORE);
            return Json(fusedSensorOnlyDataStore.getAllFusedSensorOnlyData().Count, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
        }

        [Route("api/midas")]
        [HttpGet]
        public IHttpActionResult midasData(string id) {
            MIDASDataStore midasDataStore = (MIDASDataStore)DataStoreFactory.GetInstance().GetDataStore(DataStoreFactory.DATA_STORES.MIDAS_DATA_STORE);
            return Json(midasDataStore.getData(id), new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
        }

        [Route("api/midas/all")]
        [HttpGet]
        public IHttpActionResult midasDataAll()
        {
            MIDASDataStore midasDataStore = (MIDASDataStore)DataStoreFactory.GetInstance().GetDataStore(DataStoreFactory.DATA_STORES.MIDAS_DATA_STORE);
            return Json(midasDataStore.getAllMIDASData(), new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
        }

        [Route("api/midas/count")]
        [HttpGet]
        public IHttpActionResult midasDataCount()
        {
            MIDASDataStore midasDataStore = (MIDASDataStore)DataStoreFactory.GetInstance().GetDataStore(DataStoreFactory.DATA_STORES.MIDAS_DATA_STORE);
            return Json(midasDataStore.getAllMIDASData().Count, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
        }

        [Route("api/tmu")]
        [HttpGet]
        public IHttpActionResult tmuData(string id)
        {
            TMUDataStore tmuDataStore = (TMUDataStore)DataStoreFactory.GetInstance().GetDataStore(DataStoreFactory.DATA_STORES.TMU_DATA_STORE);
            return Json(tmuDataStore.getData(id), new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
        }

        [Route("api/tmu/all")]
        [HttpGet]
        public IHttpActionResult tmuDataAll()
        {
            TMUDataStore tmuDataStore = (TMUDataStore)DataStoreFactory.GetInstance().GetDataStore(DataStoreFactory.DATA_STORES.TMU_DATA_STORE);
            return Json(tmuDataStore.getAllTMUData(), new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
        }

        [Route("api/tmu/count")]
        [HttpGet]
        public IHttpActionResult tmuDataCount()
        {
            TMUDataStore tmuDataStore = (TMUDataStore)DataStoreFactory.GetInstance().GetDataStore(DataStoreFactory.DATA_STORES.TMU_DATA_STORE);
            return Json(tmuDataStore.getAllTMUData().Count, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
        }


        [Route("api/vmsStatic")]
        [HttpGet]
        public IHttpActionResult vmsStaticData(string id) {
            VMSStaticDataStore vmsStaticDataStore = (VMSStaticDataStore)DataStoreFactory.GetInstance().GetDataStore(DataStoreFactory.DATA_STORES.VMS_STATIC_DATA_STORE);
            return Json(vmsStaticDataStore.getData(id), new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
        }

        [Route("api/vmsStatic/all")]
        [HttpGet]
        public IHttpActionResult vmsStaticDataAll()
        {
            VMSStaticDataStore vmsStaticDataStore = (VMSStaticDataStore)DataStoreFactory.GetInstance().GetDataStore(DataStoreFactory.DATA_STORES.VMS_STATIC_DATA_STORE);
            return Json(vmsStaticDataStore.getAllVMSStaticData(), new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
        }

        [Route("api/vmsStatic/count")]
        [HttpGet]
        public IHttpActionResult vmsStaticDataCount()
        {
            VMSStaticDataStore vmsStaticDataStore = (VMSStaticDataStore)DataStoreFactory.GetInstance().GetDataStore(DataStoreFactory.DATA_STORES.VMS_STATIC_DATA_STORE);
            return Json(vmsStaticDataStore.getAllVMSStaticData().Count, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
        }

        [Route("api/matrixSignalStatic")]
        [HttpGet]
        public IHttpActionResult matrixSignalStaticData(string id)
        {
            MatrixSignalStaticDataStore matrixSignalStaticDataStore = (MatrixSignalStaticDataStore)DataStoreFactory.GetInstance().GetDataStore(DataStoreFactory.DATA_STORES.MATRIX_SIGNAL_STATIC_DATA_STORE);
            return Json(matrixSignalStaticDataStore.getData(id), new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
        }

        [Route("api/matrixSignalStatic/all")]
        [HttpGet]
        public IHttpActionResult matrixSignalStaticDataAll()
        {
            MatrixSignalStaticDataStore matrixSignalStaticDataStore = (MatrixSignalStaticDataStore)DataStoreFactory.GetInstance().GetDataStore(DataStoreFactory.DATA_STORES.MATRIX_SIGNAL_STATIC_DATA_STORE);
            return Json(matrixSignalStaticDataStore.getAllMatrixSignalStaticData(), new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
        }

        [Route("api/matrixSignalStatic/count")]
        [HttpGet]
        public IHttpActionResult matrixSignalStaticDataCount()
        {
            MatrixSignalStaticDataStore matrixSignalStaticDataStore = (MatrixSignalStaticDataStore)DataStoreFactory.GetInstance().GetDataStore(DataStoreFactory.DATA_STORES.MATRIX_SIGNAL_STATIC_DATA_STORE);
            return Json(matrixSignalStaticDataStore.getAllMatrixSignalStaticData().Count, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
        }

        [Route("api/tameStatic")]
        [HttpGet]
        public IHttpActionResult tameStaticData(string id)
        {
            TAMEStaticDataStore tameStaticDataStore = (TAMEStaticDataStore)DataStoreFactory.GetInstance().GetDataStore(DataStoreFactory.DATA_STORES.TAME_STATIC_DATA_STORE);
            return Json(tameStaticDataStore.getData(id), new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
        }

        [Route("api/tameStatic/all")]
        [HttpGet]
        public IHttpActionResult tameStaticDataAll()
        {
            TAMEStaticDataStore tameStaticDataStore = (TAMEStaticDataStore)DataStoreFactory.GetInstance().GetDataStore(DataStoreFactory.DATA_STORES.TAME_STATIC_DATA_STORE);
            return Json(tameStaticDataStore.getAllTAMEStaticData(), new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
        }

        [Route("api/tameStatic/count")]
        [HttpGet]
        public IHttpActionResult tameStaticDataCount()
        {
            TAMEStaticDataStore tameStaticDataStore = (TAMEStaticDataStore)DataStoreFactory.GetInstance().GetDataStore(DataStoreFactory.DATA_STORES.TAME_STATIC_DATA_STORE);
            return Json(tameStaticDataStore.getAllTAMEStaticData().Count, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
        }

        [Route("api/midasStatic")]
        [HttpGet]
        public IHttpActionResult midasStaticData(string id) {
            MIDASStaticDataStore midasStaticDataStore = (MIDASStaticDataStore)DataStoreFactory.GetInstance().GetDataStore(DataStoreFactory.DATA_STORES.MIDAS_STATIC_DATA_STORE);
            return Json(midasStaticDataStore.getData(id), new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
        }

        [Route("api/midasStatic/all")]
        [HttpGet]
        public IHttpActionResult midasStaticDataAll()
        {
            MIDASStaticDataStore midasStaticDataStore = (MIDASStaticDataStore)DataStoreFactory.GetInstance().GetDataStore(DataStoreFactory.DATA_STORES.MIDAS_STATIC_DATA_STORE);
            return Json(midasStaticDataStore.getAllMIDASStaticData(), new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
        }

        [Route("api/midasStatic/count")]
        [HttpGet]
        public IHttpActionResult midasStaticDataCount()
        {
            MIDASStaticDataStore midasStaticDataStore = (MIDASStaticDataStore)DataStoreFactory.GetInstance().GetDataStore(DataStoreFactory.DATA_STORES.MIDAS_STATIC_DATA_STORE);
            return Json(midasStaticDataStore.getAllMIDASStaticData().Count, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
        }

        [Route("api/anprStatic")]
        [HttpGet]
        public IHttpActionResult anprStaticData(string id) {

            ANPRStaticDataStore anprStaticDataStore = (ANPRStaticDataStore)DataStoreFactory.GetInstance().GetDataStore(DataStoreFactory.DATA_STORES.ANPR_STATIC_DATA_STORE);
            return Json(anprStaticDataStore.getData(id), new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
        }

        [Route("api/anprStatic/all")]
        [HttpGet]
        public IHttpActionResult anprStaticDataAll()
        {
            ANPRStaticDataStore anprStaticDataStore = (ANPRStaticDataStore)DataStoreFactory.GetInstance().GetDataStore(DataStoreFactory.DATA_STORES.ANPR_STATIC_DATA_STORE);
            return Json(anprStaticDataStore.getAllANPRStaticData(), new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
        }

        [Route("api/anprStatic/count")]
        [HttpGet]
        public IHttpActionResult anprStaticDataCount()
        {
            ANPRStaticDataStore anprStaticDataStore = (ANPRStaticDataStore)DataStoreFactory.GetInstance().GetDataStore(DataStoreFactory.DATA_STORES.ANPR_STATIC_DATA_STORE);
            return Json(anprStaticDataStore.getAllANPRStaticData().Count, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
        }

        [Route("api/tmuStatic")]
        [HttpGet]
        public IHttpActionResult tmuStaticData(string id) {
            TMUStaticDataStore tmuStaticDataStore = (TMUStaticDataStore)DataStoreFactory.GetInstance().GetDataStore(DataStoreFactory.DATA_STORES.TMU_STATIC_DATA_STORE);
            return Json(tmuStaticDataStore.getData(id), new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
        }

        [Route("api/tmuStatic/all")]
        [HttpGet]
        public IHttpActionResult tmuStaticDataAll()
        {
            TMUStaticDataStore tmuStaticDataStore = (TMUStaticDataStore)DataStoreFactory.GetInstance().GetDataStore(DataStoreFactory.DATA_STORES.TMU_STATIC_DATA_STORE);
            return Json(tmuStaticDataStore.getAllTMUStaticData(), new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
        }

        [Route("api/tmuStatic/count")]
        [HttpGet]
        public IHttpActionResult tmuStaticDataCount()
        {
            TMUStaticDataStore tmuStaticDataStore = (TMUStaticDataStore)DataStoreFactory.GetInstance().GetDataStore(DataStoreFactory.DATA_STORES.TMU_STATIC_DATA_STORE);
            return Json(tmuStaticDataStore.getAllTMUStaticData().Count, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
        }

        [Route("api/linkShapeStatic")]
        [HttpGet]
        public IHttpActionResult linkShapeStaticData(string id) {
            LinkShapeStaticDataStore linkShapeStaticDataStore = (LinkShapeStaticDataStore)DataStoreFactory.GetInstance().GetDataStore(DataStoreFactory.DATA_STORES.LINK_SHAPE_STATIC_DATA_STORE);
            return Json(linkShapeStaticDataStore.getData(id), new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
        }

        [Route("api/linkShapeStatic/all")]
        [HttpGet]
        public IHttpActionResult linkShapeStaticDataAll()
        {
            LinkShapeStaticDataStore linkShapeStaticDataStore = (LinkShapeStaticDataStore)DataStoreFactory.GetInstance().GetDataStore(DataStoreFactory.DATA_STORES.LINK_SHAPE_STATIC_DATA_STORE);
            return Json(linkShapeStaticDataStore.getAllLinkShapeStaticData(), new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
        }

        [Route("api/linkShapeStatic/count")]
        [HttpGet]
        public IHttpActionResult linkShapeStaticDataCount()
        {
            LinkShapeStaticDataStore linkShapeStaticDataStore = (LinkShapeStaticDataStore)DataStoreFactory.GetInstance().GetDataStore(DataStoreFactory.DATA_STORES.LINK_SHAPE_STATIC_DATA_STORE);
            return Json(linkShapeStaticDataStore.getAllLinkShapeStaticData().Count, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
        }

        [Route("api/nwkLinkStatic")]
        [HttpGet]
        public IHttpActionResult nwkLinkStaticData(string id) {

            NwkLinkStaticDataStore nwkLinkStaticDataStore = (NwkLinkStaticDataStore)DataStoreFactory.GetInstance().GetDataStore(DataStoreFactory.DATA_STORES.NWK_LINK_STATIC_DATA_STORE);
            return Json(nwkLinkStaticDataStore.getData(id), new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
        }

        [Route("api/nwkLinkStatic/all")]
        [HttpGet]
        public IHttpActionResult nwkLinkStaticDataAll()
        {
            NwkLinkStaticDataStore nwkLinkStaticDataStore = (NwkLinkStaticDataStore)DataStoreFactory.GetInstance().GetDataStore(DataStoreFactory.DATA_STORES.NWK_LINK_STATIC_DATA_STORE);
            return Json(nwkLinkStaticDataStore.getAllNwkLinkStaticData(), new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
        }

        [Route("api/nwkLinkStatic/count")]
        [HttpGet]
        public IHttpActionResult nwkLinkStaticDataCount()
        {
            NwkLinkStaticDataStore nwkLinkStaticDataStore = (NwkLinkStaticDataStore)DataStoreFactory.GetInstance().GetDataStore(DataStoreFactory.DATA_STORES.NWK_LINK_STATIC_DATA_STORE);
            return Json(nwkLinkStaticDataStore.getAllNwkLinkStaticData().Count, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
        }

        [Route("api/anprRouteStatic")]
        [HttpGet]
        public IHttpActionResult anprRouteStaticData(string id) {
            ANPRRouteStaticDataStore anprRouteStaticDataStore = (ANPRRouteStaticDataStore)DataStoreFactory.GetInstance().GetDataStore(DataStoreFactory.DATA_STORES.ANPR_ROUTE_STATIC_DATA_STORE);
            return Json(anprRouteStaticDataStore.getData(id), new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
        }
        
        [Route("api/anprRouteStatic/all")]
        [HttpGet]
        public IHttpActionResult anprRouteStaticDataAll()
        {
            ANPRRouteStaticDataStore anprRouteStaticDataStore = (ANPRRouteStaticDataStore)DataStoreFactory.GetInstance().GetDataStore(DataStoreFactory.DATA_STORES.ANPR_ROUTE_STATIC_DATA_STORE);
            return Json(anprRouteStaticDataStore.getAllANPRRouteStaticData(), new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
        }
        
        [Route("api/anprRouteStatic/count")]
        [HttpGet]
        public IHttpActionResult anprRouteStaticDataCount()
        {
            ANPRRouteStaticDataStore anprRouteStaticDataStore = (ANPRRouteStaticDataStore)DataStoreFactory.GetInstance().GetDataStore(DataStoreFactory.DATA_STORES.ANPR_ROUTE_STATIC_DATA_STORE);
            return Json(anprRouteStaticDataStore.getAllANPRRouteStaticData().Count, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
        }

        [Route("api/hatrisSectionStatic")]
        [HttpGet]
        public IHttpActionResult hatrisSectionStaticData(string id) {
            HATRISSectionStaticDataStore hatrisSectionStaticDataStore = (HATRISSectionStaticDataStore)DataStoreFactory.GetInstance().GetDataStore(DataStoreFactory.DATA_STORES.HATRIS_SECTION_STATIC_DATA_STORE);
            return Json(hatrisSectionStaticDataStore.getData(id), new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
        }

        [Route("api/hatrisSectionStatic/all")]
        [HttpGet]
        public IHttpActionResult hatrisSectionStaticDataAll()
        {
            HATRISSectionStaticDataStore hatrisSectionStaticDataStore = (HATRISSectionStaticDataStore)DataStoreFactory.GetInstance().GetDataStore(DataStoreFactory.DATA_STORES.HATRIS_SECTION_STATIC_DATA_STORE);
            return Json(hatrisSectionStaticDataStore.getAllHATRISSectionStaticData(), new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
        }

        [Route("api/hatrisSectionStatic/count")]
        [HttpGet]
        public IHttpActionResult hatrisSectionStaticDataCount()
        {
            HATRISSectionStaticDataStore hatrisSectionStaticDataStore = (HATRISSectionStaticDataStore)DataStoreFactory.GetInstance().GetDataStore(DataStoreFactory.DATA_STORES.HATRIS_SECTION_STATIC_DATA_STORE);
            return Json(hatrisSectionStaticDataStore.getAllHATRISSectionStaticData().Count, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
        }

        [Route("api/nwkNodeStatic")]
        [HttpGet]
        public IHttpActionResult nwkNodeStaticData(string id) {

            NwkNodeStaticDataStore nwkNodeStaticDataStore = (NwkNodeStaticDataStore)DataStoreFactory.GetInstance().GetDataStore(DataStoreFactory.DATA_STORES.NWK_NODE_STATIC_DATA_STORE);
            return Json(nwkNodeStaticDataStore.getData(id), new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
        }

        [Route("api/nwkNodeStatic/all")]
        [HttpGet]
        public IHttpActionResult nwkNodeStaticDataAll()
        {
            NwkNodeStaticDataStore nwkNodeStaticDataStore = (NwkNodeStaticDataStore)DataStoreFactory.GetInstance().GetDataStore(DataStoreFactory.DATA_STORES.NWK_NODE_STATIC_DATA_STORE);
            return Json(nwkNodeStaticDataStore.getAllNwkNodeStaticData(), new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
        }

        [Route("api/nwkNodeStatic/count")]
        [HttpGet]
        public IHttpActionResult nwkNodeStaticDataCount()
        {
            NwkNodeStaticDataStore nwkNodeStaticDataStore = (NwkNodeStaticDataStore)DataStoreFactory.GetInstance().GetDataStore(DataStoreFactory.DATA_STORES.NWK_NODE_STATIC_DATA_STORE);
            return Json(nwkNodeStaticDataStore.getAllNwkNodeStaticData().Count, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
        }

        [Route("api/alternateRouteStatic")]
        [HttpGet]
        public IHttpActionResult alternateRouteStaticData(string id) {
            AlternateRouteStaticDataStore alternateRouteStaticDataStore = (AlternateRouteStaticDataStore)DataStoreFactory.GetInstance().GetDataStore(DataStoreFactory.DATA_STORES.ALTERNATE_ROUTE_STATIC_DATA_STORE);
            return Json(alternateRouteStaticDataStore.getData(id), new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
        }
        
        [Route("api/alternateRouteStatic/all")]
        [HttpGet]
        public IHttpActionResult alternateRouteStaticDataAll()
        {
            AlternateRouteStaticDataStore alternateRouteStaticDataStore = (AlternateRouteStaticDataStore)DataStoreFactory.GetInstance().GetDataStore(DataStoreFactory.DATA_STORES.ALTERNATE_ROUTE_STATIC_DATA_STORE);
            return Json(alternateRouteStaticDataStore.getAllAlternateRouteStaticData(), new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
        }

        [Route("api/alternateRouteStatic/count")]
        [HttpGet]
        public IHttpActionResult alternateRouteStaticDataCount()
        { 
            AlternateRouteStaticDataStore alternateRouteStaticDataStore = (AlternateRouteStaticDataStore)DataStoreFactory.GetInstance().GetDataStore(DataStoreFactory.DATA_STORES.ALTERNATE_ROUTE_STATIC_DATA_STORE);
            return Json(alternateRouteStaticDataStore.getAllAlternateRouteStaticData().Count, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
        }
    }    
}


