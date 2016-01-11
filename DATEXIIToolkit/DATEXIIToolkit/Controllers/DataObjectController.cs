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
        private IHttpActionResult Serialize(object obj)
        {
            JsonSerializerSettings jss = new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.None,
                TypeNameAssemblyFormat = FormatterAssemblyStyle.Simple,
                NullValueHandling = NullValueHandling.Ignore,
                ReferenceLoopHandling = ReferenceLoopHandling.Serialize,
            };

            string json = JsonConvert.SerializeObject(obj, Formatting.Indented, jss);

            if (json != null)
            {
                return Ok(json);
            }
            return NotFound();
        }


        [Route("api/dataobject/vms/all")]
        [HttpGet]
        public IHttpActionResult vmsDataAll()
        {
            VMSDataStore vmsDataStore = (VMSDataStore)DataStoreFactory.GetInstance().GetDataStore(DataStoreFactory.DATA_STORES.VMS_DATA_STORE);
            LinkedList<VMSData> vmsDataList = vmsDataStore.getAllVMSData();
            return Serialize(vmsDataList);
        }

        [Route("api/dataobject/vms/{id}")]
        [HttpGet]
        public IHttpActionResult vmsData(string id)
        {
            VMSDataStore vmsDataStore = (VMSDataStore)DataStoreFactory.GetInstance().GetDataStore(DataStoreFactory.DATA_STORES.VMS_DATA_STORE);
            return Serialize(vmsDataStore.getData(id));
        }

        [Route("api/dataobject/vms/count")]
        [HttpGet]
        public IHttpActionResult vmsDataCount()
        {
            VMSDataStore vmsDataStore = (VMSDataStore)DataStoreFactory.GetInstance().GetDataStore(DataStoreFactory.DATA_STORES.VMS_DATA_STORE);
            return Serialize(vmsDataStore.getAllVMSData().Count);
        }

        [Route("api/dataobject/event/{id}")]
        [HttpGet]
        public IHttpActionResult eventData(string id) {
            EventDataStore eventDataStore = (EventDataStore)DataStoreFactory.GetInstance().GetDataStore(DataStoreFactory.DATA_STORES.EVENT_DATA_STORE);
            return Serialize(eventDataStore.getData(id));
        }

        [Route("api/dataobject/event/all")]
        [HttpGet]
        public IHttpActionResult eventDataAll()
        {
            EventDataStore eventDataStore = (EventDataStore)DataStoreFactory.GetInstance().GetDataStore(DataStoreFactory.DATA_STORES.EVENT_DATA_STORE);
            return Serialize(eventDataStore.getAllEventData());
        }

        [Route("api/dataobject/event/count")]
        [HttpGet]
        public IHttpActionResult eventDataCount()
        {
            EventDataStore eventDataStore = (EventDataStore)DataStoreFactory.GetInstance().GetDataStore(DataStoreFactory.DATA_STORES.EVENT_DATA_STORE);
            return Serialize(eventDataStore.getAllEventData().Count);
        }

        [Route("api/dataobject/anpr/{id}")]
        [HttpGet]
        public IHttpActionResult anprData(string id) {
            ANPRDataStore anprDataStore = (ANPRDataStore)DataStoreFactory.GetInstance().GetDataStore(DataStoreFactory.DATA_STORES.ANPR_DATA_STORE);
            return Serialize(anprDataStore.getData(id));
        }

        [Route("api/dataobject/anpr/all")]
        [HttpGet]
        public IHttpActionResult anprDataAll()
        {
            ANPRDataStore anprDataStore = (ANPRDataStore)DataStoreFactory.GetInstance().GetDataStore(DataStoreFactory.DATA_STORES.ANPR_DATA_STORE);
            return Serialize(anprDataStore.getAllANPRData());
        }

        [Route("api/dataobject/anpr/count")]
        [HttpGet]
        public IHttpActionResult anprDataCount()
        {
            ANPRDataStore anprDataStore = (ANPRDataStore)DataStoreFactory.GetInstance().GetDataStore(DataStoreFactory.DATA_STORES.ANPR_DATA_STORE);
            return Serialize(anprDataStore.getAllANPRData().Count);
        }

        [Route("api/dataobject/fusedFVDAndSensor/{id}")]
        [HttpGet]
        public IHttpActionResult fusedFVDAndSensorData(string id) {
            FusedFVDAndSensorDataStore fusedFVDAndSensorDataStore = (FusedFVDAndSensorDataStore)DataStoreFactory.GetInstance().GetDataStore(DataStoreFactory.DATA_STORES.FUSED_FVD_AND_SENSOR_DATA_STORE);
            return Serialize(fusedFVDAndSensorDataStore.getData(id));
        }

        [Route("api/dataobject/fusedFVDAndSensor/all")]
        [HttpGet]
        public IHttpActionResult fusedFVDAndSensorDataAll()
        {
            FusedFVDAndSensorDataStore fusedFVDAndSensorDataStore = (FusedFVDAndSensorDataStore)DataStoreFactory.GetInstance().GetDataStore(DataStoreFactory.DATA_STORES.FUSED_FVD_AND_SENSOR_DATA_STORE);
            return Serialize(fusedFVDAndSensorDataStore.getAllFusedFVDAndSensorData());
        }

        [Route("api/dataobject/fusedFVDAndSensor/count")]
        [HttpGet]
        public IHttpActionResult fusedFVDAndSensorDataCount()
        {
            FusedFVDAndSensorDataStore fusedFVDAndSensorDataStore = (FusedFVDAndSensorDataStore)DataStoreFactory.GetInstance().GetDataStore(DataStoreFactory.DATA_STORES.FUSED_FVD_AND_SENSOR_DATA_STORE);
            return Serialize(fusedFVDAndSensorDataStore.getAllFusedFVDAndSensorData().Count);
        }

        [Route("api/dataobject/fusedSensorOnly/{id}")]
        [HttpGet]
        public IHttpActionResult fusedSensorOnlyData(string id)
        {
            FusedSensorOnlyDataStore fusedSensorOnlyDataStore = (FusedSensorOnlyDataStore)DataStoreFactory.GetInstance().GetDataStore(DataStoreFactory.DATA_STORES.FUSED_SENSOR_ONLY_DATA_STORE);
            return Serialize(fusedSensorOnlyDataStore.getData(id));
        }

        [Route("api/dataobject/fusedSensorOnly/all")]
        [HttpGet]
        public IHttpActionResult fusedSensorOnlyDataAll()
        {
            FusedSensorOnlyDataStore fusedSensorOnlyDataStore = (FusedSensorOnlyDataStore)DataStoreFactory.GetInstance().GetDataStore(DataStoreFactory.DATA_STORES.FUSED_SENSOR_ONLY_DATA_STORE);
            return Serialize(fusedSensorOnlyDataStore.getAllFusedSensorOnlyData());
        }

        [Route("api/dataobject/fusedSensorOnly/count")]
        [HttpGet]
        public IHttpActionResult fusedSensorOnlyDataCount()
        {
            FusedSensorOnlyDataStore fusedSensorOnlyDataStore = (FusedSensorOnlyDataStore)DataStoreFactory.GetInstance().GetDataStore(DataStoreFactory.DATA_STORES.FUSED_SENSOR_ONLY_DATA_STORE);
            return Serialize(fusedSensorOnlyDataStore.getAllFusedSensorOnlyData().Count);
        }

        [Route("api/dataobject/midas/{id}")]
        [HttpGet]
        public IHttpActionResult midasData(string id) {
            MIDASDataStore midasDataStore = (MIDASDataStore)DataStoreFactory.GetInstance().GetDataStore(DataStoreFactory.DATA_STORES.MIDAS_DATA_STORE);
            return Serialize(midasDataStore.getData(id));
        }

        [Route("api/dataobject/midas/all")]
        [HttpGet]
        public IHttpActionResult midasDataAll()
        {
            MIDASDataStore midasDataStore = (MIDASDataStore)DataStoreFactory.GetInstance().GetDataStore(DataStoreFactory.DATA_STORES.MIDAS_DATA_STORE);
            return Serialize(midasDataStore.getAllMIDASData());
        }

        [Route("api/dataobject/midas/count")]
        [HttpGet]
        public IHttpActionResult midasDataCount()
        {
            MIDASDataStore midasDataStore = (MIDASDataStore)DataStoreFactory.GetInstance().GetDataStore(DataStoreFactory.DATA_STORES.MIDAS_DATA_STORE);
            return Serialize(midasDataStore.getAllMIDASData().Count);
        }

        [Route("api/dataobject/tmu/{id}")]
        [HttpGet]
        public IHttpActionResult tmuData(string id)
        {
            TMUDataStore tmuDataStore = (TMUDataStore)DataStoreFactory.GetInstance().GetDataStore(DataStoreFactory.DATA_STORES.TMU_DATA_STORE);
            return Serialize(tmuDataStore.getData(id));
        }

        [Route("api/dataobject/tmu/all")]
        [HttpGet]
        public IHttpActionResult tmuDataAll()
        {
            TMUDataStore tmuDataStore = (TMUDataStore)DataStoreFactory.GetInstance().GetDataStore(DataStoreFactory.DATA_STORES.TMU_DATA_STORE);
            return Serialize(tmuDataStore.getAllTMUData());
        }

        [Route("api/dataobject/tmu/count")]
        [HttpGet]
        public IHttpActionResult tmuDataCount()
        {
            TMUDataStore tmuDataStore = (TMUDataStore)DataStoreFactory.GetInstance().GetDataStore(DataStoreFactory.DATA_STORES.TMU_DATA_STORE);
            return Serialize(tmuDataStore.getAllTMUData().Count);
        }


        [Route("api/dataobject/vmsStatic/{id}")]
        [HttpGet]
        public IHttpActionResult vmsStaticData(string id) {
            VMSStaticDataStore vmsStaticDataStore = (VMSStaticDataStore)DataStoreFactory.GetInstance().GetDataStore(DataStoreFactory.DATA_STORES.VMS_STATIC_DATA_STORE);
            return Serialize(vmsStaticDataStore.getData(id));
        }

        [Route("api/dataobject/vmsStatic/all")]
        [HttpGet]
        public IHttpActionResult vmsStaticDataAll()
        {
            VMSStaticDataStore vmsStaticDataStore = (VMSStaticDataStore)DataStoreFactory.GetInstance().GetDataStore(DataStoreFactory.DATA_STORES.VMS_STATIC_DATA_STORE);
            return Serialize(vmsStaticDataStore.getAllVMSStaticData());
        }

        [Route("api/dataobject/vmsStatic/count")]
        [HttpGet]
        public IHttpActionResult vmsStaticDataCount()
        {
            VMSStaticDataStore vmsStaticDataStore = (VMSStaticDataStore)DataStoreFactory.GetInstance().GetDataStore(DataStoreFactory.DATA_STORES.VMS_STATIC_DATA_STORE);
            return Serialize(vmsStaticDataStore.getAllVMSStaticData().Count);
        }

        [Route("api/dataobject/matrixSignalStatic/{id}")]
        [HttpGet]
        public IHttpActionResult matrixSignalStaticData(string id)
        {
            MatrixSignalStaticDataStore matrixSignalStaticDataStore = (MatrixSignalStaticDataStore)DataStoreFactory.GetInstance().GetDataStore(DataStoreFactory.DATA_STORES.MATRIX_SIGNAL_STATIC_DATA_STORE);
            return Serialize(matrixSignalStaticDataStore.getData(id));
        }

        [Route("api/dataobject/matrixSignalStatic/all")]
        [HttpGet]
        public IHttpActionResult matrixSignalStaticDataAll()
        {
            MatrixSignalStaticDataStore matrixSignalStaticDataStore = (MatrixSignalStaticDataStore)DataStoreFactory.GetInstance().GetDataStore(DataStoreFactory.DATA_STORES.MATRIX_SIGNAL_STATIC_DATA_STORE);
            return Serialize(matrixSignalStaticDataStore.getAllMatrixSignalStaticData());
        }

        [Route("api/dataobject/matrixSignalStatic/count")]
        [HttpGet]
        public IHttpActionResult matrixSignalStaticDataCount()
        {
            MatrixSignalStaticDataStore matrixSignalStaticDataStore = (MatrixSignalStaticDataStore)DataStoreFactory.GetInstance().GetDataStore(DataStoreFactory.DATA_STORES.MATRIX_SIGNAL_STATIC_DATA_STORE);
            return Serialize(matrixSignalStaticDataStore.getAllMatrixSignalStaticData().Count);
        }

        [Route("api/dataobject/tameStatic/{id}")]
        [HttpGet]
        public IHttpActionResult tameStaticData(string id)
        {
            TAMEStaticDataStore tameStaticDataStore = (TAMEStaticDataStore)DataStoreFactory.GetInstance().GetDataStore(DataStoreFactory.DATA_STORES.TAME_STATIC_DATA_STORE);
            return Serialize(tameStaticDataStore.getData(id));
        }

        [Route("api/dataobject/tameStatic/all")]
        [HttpGet]
        public IHttpActionResult tameStaticDataAll()
        {
            TAMEStaticDataStore tameStaticDataStore = (TAMEStaticDataStore)DataStoreFactory.GetInstance().GetDataStore(DataStoreFactory.DATA_STORES.TAME_STATIC_DATA_STORE);
            return Serialize(tameStaticDataStore.getAllTAMEStaticData());
        }

        [Route("api/dataobject/tameStatic/count")]
        [HttpGet]
        public IHttpActionResult tameStaticDataCount()
        {
            TAMEStaticDataStore tameStaticDataStore = (TAMEStaticDataStore)DataStoreFactory.GetInstance().GetDataStore(DataStoreFactory.DATA_STORES.TAME_STATIC_DATA_STORE);
            return Serialize(tameStaticDataStore.getAllTAMEStaticData().Count);
        }

        [Route("api/dataobject/midasStatic/{id}")]
        [HttpGet]
        public IHttpActionResult midasStaticData(string id) {
            MIDASStaticDataStore midasStaticDataStore = (MIDASStaticDataStore)DataStoreFactory.GetInstance().GetDataStore(DataStoreFactory.DATA_STORES.MIDAS_STATIC_DATA_STORE);
            return Serialize(midasStaticDataStore.getData(id));
        }

        [Route("api/dataobject/midasStatic/all")]
        [HttpGet]
        public IHttpActionResult midasStaticDataAll()
        {
            MIDASStaticDataStore midasStaticDataStore = (MIDASStaticDataStore)DataStoreFactory.GetInstance().GetDataStore(DataStoreFactory.DATA_STORES.MIDAS_STATIC_DATA_STORE);
            return Serialize(midasStaticDataStore.getAllMIDASStaticData());
        }

        [Route("api/dataobject/midasStatic/count")]
        [HttpGet]
        public IHttpActionResult midasStaticDataCount()
        {
            MIDASStaticDataStore midasStaticDataStore = (MIDASStaticDataStore)DataStoreFactory.GetInstance().GetDataStore(DataStoreFactory.DATA_STORES.MIDAS_STATIC_DATA_STORE);
            return Serialize(midasStaticDataStore.getAllMIDASStaticData().Count);
        }

        [Route("api/dataobject/anprStatic/{id}")]
        [HttpGet]
        public IHttpActionResult anprStaticData(string id) {

            ANPRStaticDataStore anprStaticDataStore = (ANPRStaticDataStore)DataStoreFactory.GetInstance().GetDataStore(DataStoreFactory.DATA_STORES.ANPR_STATIC_DATA_STORE);
            return Serialize(anprStaticDataStore.getData(id));
        }

        [Route("api/dataobject/anprStatic/all")]
        [HttpGet]
        public IHttpActionResult anprStaticDataAll()
        {
            ANPRStaticDataStore anprStaticDataStore = (ANPRStaticDataStore)DataStoreFactory.GetInstance().GetDataStore(DataStoreFactory.DATA_STORES.ANPR_STATIC_DATA_STORE);
            return Serialize(anprStaticDataStore.getAllANPRStaticData());
        }

        [Route("api/dataobject/anprStatic/count")]
        [HttpGet]
        public IHttpActionResult anprStaticDataCount()
        {
            ANPRStaticDataStore anprStaticDataStore = (ANPRStaticDataStore)DataStoreFactory.GetInstance().GetDataStore(DataStoreFactory.DATA_STORES.ANPR_STATIC_DATA_STORE);
            return Serialize(anprStaticDataStore.getAllANPRStaticData().Count);
        }

        [Route("api/dataobject/tmuStatic/{id}")]
        [HttpGet]
        public IHttpActionResult tmuStaticData(string id) {
            TMUStaticDataStore tmuStaticDataStore = (TMUStaticDataStore)DataStoreFactory.GetInstance().GetDataStore(DataStoreFactory.DATA_STORES.TMU_STATIC_DATA_STORE);
            return Serialize(tmuStaticDataStore.getData(id));
        }

        [Route("api/dataobject/tmuStatic/all")]
        [HttpGet]
        public IHttpActionResult tmuStaticDataAll()
        {
            TMUStaticDataStore tmuStaticDataStore = (TMUStaticDataStore)DataStoreFactory.GetInstance().GetDataStore(DataStoreFactory.DATA_STORES.TMU_STATIC_DATA_STORE);
            return Serialize(tmuStaticDataStore.getAllTMUStaticData());
        }

        [Route("api/dataobject/tmuStatic/count")]
        [HttpGet]
        public IHttpActionResult tmuStaticDataCount()
        {
            TMUStaticDataStore tmuStaticDataStore = (TMUStaticDataStore)DataStoreFactory.GetInstance().GetDataStore(DataStoreFactory.DATA_STORES.TMU_STATIC_DATA_STORE);
            return Serialize(tmuStaticDataStore.getAllTMUStaticData().Count);
        }

        [Route("api/dataobject/linkShapeStatic/{id}")]
        [HttpGet]
        public IHttpActionResult linkShapeStaticData(string id) {
            LinkShapeStaticDataStore linkShapeStaticDataStore = (LinkShapeStaticDataStore)DataStoreFactory.GetInstance().GetDataStore(DataStoreFactory.DATA_STORES.LINK_SHAPE_STATIC_DATA_STORE);
            return Serialize(linkShapeStaticDataStore.getData(id));
        }

        [Route("api/dataobject/linkShapeStatic/all")]
        [HttpGet]
        public IHttpActionResult linkShapeStaticDataAll()
        {
            LinkShapeStaticDataStore linkShapeStaticDataStore = (LinkShapeStaticDataStore)DataStoreFactory.GetInstance().GetDataStore(DataStoreFactory.DATA_STORES.LINK_SHAPE_STATIC_DATA_STORE);
            return Serialize(linkShapeStaticDataStore.getAllLinkShapeStaticData());
        }

        [Route("api/dataobject/linkShapeStatic/count")]
        [HttpGet]
        public IHttpActionResult linkShapeStaticDataCount()
        {
            LinkShapeStaticDataStore linkShapeStaticDataStore = (LinkShapeStaticDataStore)DataStoreFactory.GetInstance().GetDataStore(DataStoreFactory.DATA_STORES.LINK_SHAPE_STATIC_DATA_STORE);
            return Serialize(linkShapeStaticDataStore.getAllLinkShapeStaticData().Count);
        }

        [Route("api/dataobject/nwkLinkStatic/{id}")]
        [HttpGet]
        public IHttpActionResult nwkLinkStaticData(string id) {

            NwkLinkStaticDataStore nwkLinkStaticDataStore = (NwkLinkStaticDataStore)DataStoreFactory.GetInstance().GetDataStore(DataStoreFactory.DATA_STORES.NWK_LINK_STATIC_DATA_STORE);
            return Serialize(nwkLinkStaticDataStore.getData(id));
        }

        [Route("api/dataobject/nwkLinkStatic/all")]
        [HttpGet]
        public IHttpActionResult nwkLinkStaticDataAll()
        {
            NwkLinkStaticDataStore nwkLinkStaticDataStore = (NwkLinkStaticDataStore)DataStoreFactory.GetInstance().GetDataStore(DataStoreFactory.DATA_STORES.NWK_LINK_STATIC_DATA_STORE);
            return Serialize(nwkLinkStaticDataStore.getAllNwkLinkStaticData());
        }

        [Route("api/dataobject/nwkLinkStatic/count")]
        [HttpGet]
        public IHttpActionResult nwkLinkStaticDataCount()
        {
            NwkLinkStaticDataStore nwkLinkStaticDataStore = (NwkLinkStaticDataStore)DataStoreFactory.GetInstance().GetDataStore(DataStoreFactory.DATA_STORES.NWK_LINK_STATIC_DATA_STORE);
            return Serialize(nwkLinkStaticDataStore.getAllNwkLinkStaticData().Count);
        }

        [Route("api/dataobject/anprRouteStatic/{id}")]
        [HttpGet]
        public IHttpActionResult anprRouteStaticData(string id) {
            ANPRRouteStaticDataStore anprRouteStaticDataStore = (ANPRRouteStaticDataStore)DataStoreFactory.GetInstance().GetDataStore(DataStoreFactory.DATA_STORES.ANPR_ROUTE_STATIC_DATA_STORE);
            return Serialize(anprRouteStaticDataStore.getData(id));
        }
        
        [Route("api/dataobject/anprRouteStatic/all")]
        [HttpGet]
        public IHttpActionResult anprRouteStaticDataAll()
        {
            ANPRRouteStaticDataStore anprRouteStaticDataStore = (ANPRRouteStaticDataStore)DataStoreFactory.GetInstance().GetDataStore(DataStoreFactory.DATA_STORES.ANPR_ROUTE_STATIC_DATA_STORE);
            return Serialize(anprRouteStaticDataStore.getAllANPRRouteStaticData());
        }
        
        [Route("api/dataobject/anprRouteStatic/count")]
        [HttpGet]
        public IHttpActionResult anprRouteStaticDataCount()
        {
            ANPRRouteStaticDataStore anprRouteStaticDataStore = (ANPRRouteStaticDataStore)DataStoreFactory.GetInstance().GetDataStore(DataStoreFactory.DATA_STORES.ANPR_ROUTE_STATIC_DATA_STORE);
            return Serialize(anprRouteStaticDataStore.getAllANPRRouteStaticData().Count);
        }

        [Route("api/dataobject/hatrisSectionStatic/{id}")]
        [HttpGet]
        public IHttpActionResult hatrisSectionStaticData(string id) {
            HATRISSectionStaticDataStore hatrisSectionStaticDataStore = (HATRISSectionStaticDataStore)DataStoreFactory.GetInstance().GetDataStore(DataStoreFactory.DATA_STORES.HATRIS_SECTION_STATIC_DATA_STORE);
            return Serialize(hatrisSectionStaticDataStore.getData(id));
        }

        [Route("api/dataobject/hatrisSectionStatic/all")]
        [HttpGet]
        public IHttpActionResult hatrisSectionStaticDataAll()
        {
            HATRISSectionStaticDataStore hatrisSectionStaticDataStore = (HATRISSectionStaticDataStore)DataStoreFactory.GetInstance().GetDataStore(DataStoreFactory.DATA_STORES.HATRIS_SECTION_STATIC_DATA_STORE);
            return Serialize(hatrisSectionStaticDataStore.getAllHATRISSectionStaticData());
        }

        [Route("api/dataobject/hatrisSectionStatic/count")]
        [HttpGet]
        public IHttpActionResult hatrisSectionStaticDataCount()
        {
            HATRISSectionStaticDataStore hatrisSectionStaticDataStore = (HATRISSectionStaticDataStore)DataStoreFactory.GetInstance().GetDataStore(DataStoreFactory.DATA_STORES.HATRIS_SECTION_STATIC_DATA_STORE);
            return Serialize(hatrisSectionStaticDataStore.getAllHATRISSectionStaticData().Count);
        }

        [Route("api/dataobject/nwkNodeStatic/{id}")]
        [HttpGet]
        public IHttpActionResult nwkNodeStaticData(string id) {

            NwkNodeStaticDataStore nwkNodeStaticDataStore = (NwkNodeStaticDataStore)DataStoreFactory.GetInstance().GetDataStore(DataStoreFactory.DATA_STORES.NWK_NODE_STATIC_DATA_STORE);
            return Serialize(nwkNodeStaticDataStore.getData(id));
        }

        [Route("api/dataobject/nwkNodeStatic/all")]
        [HttpGet]
        public IHttpActionResult nwkNodeStaticDataAll()
        {
            NwkNodeStaticDataStore nwkNodeStaticDataStore = (NwkNodeStaticDataStore)DataStoreFactory.GetInstance().GetDataStore(DataStoreFactory.DATA_STORES.NWK_NODE_STATIC_DATA_STORE);
            return Serialize(nwkNodeStaticDataStore.getAllNwkNodeStaticData());
        }

        [Route("api/dataobject/nwkNodeStatic/count")]
        [HttpGet]
        public IHttpActionResult nwkNodeStaticDataCount()
        {
            NwkNodeStaticDataStore nwkNodeStaticDataStore = (NwkNodeStaticDataStore)DataStoreFactory.GetInstance().GetDataStore(DataStoreFactory.DATA_STORES.NWK_NODE_STATIC_DATA_STORE);
            return Serialize(nwkNodeStaticDataStore.getAllNwkNodeStaticData().Count);
        }

        [Route("api/dataobject/alternateRouteStatic/{id}")]
        [HttpGet]
        public IHttpActionResult alternateRouteStaticData(string id) {
            AlternateRouteStaticDataStore alternateRouteStaticDataStore = (AlternateRouteStaticDataStore)DataStoreFactory.GetInstance().GetDataStore(DataStoreFactory.DATA_STORES.ALTERNATE_ROUTE_STATIC_DATA_STORE);
            return Serialize(alternateRouteStaticDataStore.getData(id));
        }
        
        [Route("api/dataobject/alternateRouteStatic/all")]
        [HttpGet]
        public IHttpActionResult alternateRouteStaticDataAll()
        {
            AlternateRouteStaticDataStore alternateRouteStaticDataStore = (AlternateRouteStaticDataStore)DataStoreFactory.GetInstance().GetDataStore(DataStoreFactory.DATA_STORES.ALTERNATE_ROUTE_STATIC_DATA_STORE);
            return Serialize(alternateRouteStaticDataStore.getAllAlternateRouteStaticData());
        }

        [Route("api/dataobject/alternateRouteStatic/count")]
        [HttpGet]
        public IHttpActionResult alternateRouteStaticDataCount()
        { 
            AlternateRouteStaticDataStore alternateRouteStaticDataStore = (AlternateRouteStaticDataStore)DataStoreFactory.GetInstance().GetDataStore(DataStoreFactory.DATA_STORES.ALTERNATE_ROUTE_STATIC_DATA_STORE);
            return Serialize(alternateRouteStaticDataStore.getAllAlternateRouteStaticData().Count);
        }
    }

    public class NullPropertiesConverter : JavaScriptConverter
    {
        public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
        {
            Type propType = obj.GetType();
            FieldInfo[] fieldInfo = propType.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy);
            var result = new Dictionary<string, object>();
           
            foreach (FieldInfo fInfo in fieldInfo)
            {
                bool ignoreProp = fInfo.IsDefined(typeof(ScriptIgnoreAttribute), true);

                var value = fInfo.GetValue(obj);
                if (value != null && !ignoreProp)
                    result.Add(fInfo.Name, value);       
            }

            return result;
        }

        public override IEnumerable<Type> SupportedTypes
        {
            get { return GetType().Assembly.GetTypes(); }
        }
    }
}


