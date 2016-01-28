using DATEXIIToolkit.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DATEXIIToolkit.Models
{
    public class DataStoreFactory
    {
        public enum DATA_STORES
        {
            ALTERNATE_ROUTE_STATIC_DATA_STORE,
            ANPR_DATA_STORE,
            ANPR_ROUTE_STATIC_DATA_STORE,
            ANPR_STATIC_DATA_STORE,
            EVENT_DATA_STORE,
            FUSED_FVD_AND_SENSOR_DATA_STORE,
            FUSED_SENSOR_ONLY_DATA_STORE,
            HATRIS_SECTION_STATIC_DATA_STORE,
            LINK_SHAPE_STATIC_DATA_STORE,
            MATRIX_SIGNAL_STATIC_DATA_STORE,
            MIDAS_DATA_STORE,
            MIDAS_STATIC_DATA_STORE,
            NWK_LINK_STATIC_DATA_STORE,
            NWK_NODE_STATIC_DATA_STORE,
            TAME_STATIC_DATA_STORE,
            TMU_DATA_STORE,
            TMU_STATIC_DATA_STORE,
            VMS_DATA_STORE,
            VMS_STATIC_DATA_STORE
        }

        private LogWrapper logWrapper;
        private static DataStoreFactory instance;
        private AlternateRouteStaticDataStore alternateRouteStaticDataStore;
        private ANPRDataStore anprDataStore;
        private ANPRStaticDataStore anprStaticDataStore;
        private ANPRRouteStaticDataStore anprRouteStaticDataStore;
        private EventDataStore eventDataStore;
        private FusedFVDAndSensorDataStore fusedFVDAndSensorDataStore;
        private FusedSensorOnlyDataStore fusedSensorOnlyDataStore;
        private HATRISSectionStaticDataStore hatrisSectionStaticDataStore;
        private LinkShapeStaticDataStore linkShapeStaticDataStore;
        private MatrixSignalStaticDataStore matrixSignalStaticDataStore;
        private MIDASDataStore midasDataStore;
        private MIDASStaticDataStore midasStaticDataStore;
        private NwkLinkStaticDataStore nwkLinkStaticDataStore;
        private NwkNodeStaticDataStore nwkNodeStaticDataStore;
        private TAMEStaticDataStore tameStaticDataStore;
        private TMUDataStore tmuDataStore;
        private TMUStaticDataStore tmuStaticDataStore;
        private VMSDataStore vmsDataStore;
        private VMSStaticDataStore vmsStaticDataStore;

        private DataStoreFactory()
        {
            logWrapper = new LogWrapper("DataStoreFactory");
            alternateRouteStaticDataStore = new AlternateRouteStaticDataStore();
            anprDataStore = new ANPRDataStore();
            anprRouteStaticDataStore = new ANPRRouteStaticDataStore();
            anprStaticDataStore = new ANPRStaticDataStore();
            eventDataStore = new EventDataStore();
            fusedFVDAndSensorDataStore = new FusedFVDAndSensorDataStore();
            fusedSensorOnlyDataStore = new FusedSensorOnlyDataStore();
            hatrisSectionStaticDataStore = new HATRISSectionStaticDataStore();
            linkShapeStaticDataStore = new LinkShapeStaticDataStore();
            matrixSignalStaticDataStore = new MatrixSignalStaticDataStore();
            midasDataStore = new MIDASDataStore();
            midasStaticDataStore = new MIDASStaticDataStore();
            nwkLinkStaticDataStore = new NwkLinkStaticDataStore();
            nwkNodeStaticDataStore = new NwkNodeStaticDataStore();
            tameStaticDataStore = new TAMEStaticDataStore();
            tmuDataStore = new TMUDataStore();
            tmuStaticDataStore = new TMUStaticDataStore();
            vmsDataStore = new VMSDataStore();
            vmsStaticDataStore = new VMSStaticDataStore();
            
    }

        public static DataStoreFactory GetInstance()
        {
            if (instance == null)
            {
                instance = new DataStoreFactory();
            }
            return instance;
        }

        public DataStore GetDataStore(DATA_STORES dataStoreType)
        {
            DataStore dataStore = null;

            switch (dataStoreType)
            {
                case DATA_STORES.ALTERNATE_ROUTE_STATIC_DATA_STORE:
                    { dataStore = alternateRouteStaticDataStore; }
                    break;
                case DATA_STORES.ANPR_DATA_STORE:
                    { dataStore = anprDataStore; }
                    break;
                case DATA_STORES.ANPR_ROUTE_STATIC_DATA_STORE:
                    { dataStore = anprRouteStaticDataStore; }
                    break;
                case DATA_STORES.ANPR_STATIC_DATA_STORE:
                    { dataStore = anprStaticDataStore; }
                    break;
                case DATA_STORES.EVENT_DATA_STORE:
                    { dataStore = eventDataStore; }
                    break;
                case DATA_STORES.FUSED_FVD_AND_SENSOR_DATA_STORE:
                    { dataStore = fusedFVDAndSensorDataStore; }
                    break;
                case DATA_STORES.FUSED_SENSOR_ONLY_DATA_STORE:
                    { dataStore = fusedSensorOnlyDataStore; }
                    break;
                case DATA_STORES.HATRIS_SECTION_STATIC_DATA_STORE:
                    { dataStore = hatrisSectionStaticDataStore; }
                    break;
                case DATA_STORES.LINK_SHAPE_STATIC_DATA_STORE:
                    { dataStore = linkShapeStaticDataStore; }
                    break;
                case DATA_STORES.MATRIX_SIGNAL_STATIC_DATA_STORE:
                    { dataStore = matrixSignalStaticDataStore; }
                    break;
                case DATA_STORES.MIDAS_DATA_STORE:
                    { dataStore = midasDataStore; }
                    break;
                case DATA_STORES.MIDAS_STATIC_DATA_STORE:
                    { dataStore = midasStaticDataStore; }
                    break;
                case DATA_STORES.NWK_LINK_STATIC_DATA_STORE:
                    { dataStore = nwkLinkStaticDataStore; }
                    break;
                case DATA_STORES.NWK_NODE_STATIC_DATA_STORE:
                    { dataStore = nwkNodeStaticDataStore; }
                    break;
                case DATA_STORES.TAME_STATIC_DATA_STORE:
                    { dataStore = tameStaticDataStore; }
                    break;
                case DATA_STORES.TMU_DATA_STORE:
                    { dataStore = tmuDataStore; }
                    break;
                case DATA_STORES.TMU_STATIC_DATA_STORE:
                    { dataStore = tmuStaticDataStore; }
                    break;
                case DATA_STORES.VMS_DATA_STORE:
                    { dataStore = vmsDataStore; }
                    break;
                case DATA_STORES.VMS_STATIC_DATA_STORE:
                    { dataStore = vmsStaticDataStore; }
                    break;
                default:
                    { logWrapper.Error("Cannot find datastore"); }
                    break;
            }


            return dataStore;
        }
    }
}