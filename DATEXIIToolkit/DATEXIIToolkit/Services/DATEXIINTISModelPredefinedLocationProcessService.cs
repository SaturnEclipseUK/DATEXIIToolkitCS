using DATEXIIToolkit.Common;
using DATEXIIToolkit.Models;
using DATEXIIToolkit.Models.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace DATEXIIToolkit.Services
{
    /// <summary>
    /// This service processes PredefinedLocation DATEX II v2 messages (D2LogicalModel).
    /// The payloads are inserted into the NwkLinkStatic, NwkNodeStatic, NwkShapeStatic,
    /// ANPRRouteStatic, HATRISSectionStatic and AlternateRouteStatic data stores.
    /// </summary>
    public class DATEXIINTISModelPredefinedLocationProcessService : DATEXIIProcessService
    {
        LogWrapper logWrapper;
	
	    LinkShapeStaticDataStore linkShapeStaticDataStore;
        NwkLinkStaticDataStore nwkLinkStaticDataStore;
        ANPRRouteStaticDataStore anprRouteStaticDataStore;
        HATRISSectionStaticDataStore hatrisSectionStaticDataStore;
        NwkNodeStaticDataStore nwkNodeStaticDataStore;
        AlternateRouteStaticDataStore alternateRouteStaticDataStore;

        public DATEXIINTISModelPredefinedLocationProcessService() : base()
        {
            logWrapper = new LogWrapper("DATEXIINTISModelPredefinedLocationProcessService");

            linkShapeStaticDataStore = (LinkShapeStaticDataStore)DataStoreFactory.GetInstance().GetDataStore(DataStoreFactory.DATA_STORES.LINK_SHAPE_STATIC_DATA_STORE);
            nwkLinkStaticDataStore = (NwkLinkStaticDataStore)DataStoreFactory.GetInstance().GetDataStore(DataStoreFactory.DATA_STORES.NWK_LINK_STATIC_DATA_STORE);
            anprRouteStaticDataStore = (ANPRRouteStaticDataStore)DataStoreFactory.GetInstance().GetDataStore(DataStoreFactory.DATA_STORES.ANPR_ROUTE_STATIC_DATA_STORE);
            hatrisSectionStaticDataStore = (HATRISSectionStaticDataStore)DataStoreFactory.GetInstance().GetDataStore(DataStoreFactory.DATA_STORES.HATRIS_SECTION_STATIC_DATA_STORE);
            nwkNodeStaticDataStore = (NwkNodeStaticDataStore)DataStoreFactory.GetInstance().GetDataStore(DataStoreFactory.DATA_STORES.NWK_NODE_STATIC_DATA_STORE);
            alternateRouteStaticDataStore = (AlternateRouteStaticDataStore)DataStoreFactory.GetInstance().GetDataStore(DataStoreFactory.DATA_STORES.ALTERNATE_ROUTE_STATIC_DATA_STORE);
        }

        public override void processMessage(D2LogicalModel d2LogicalModel)
        {

            if (logWrapper.isDebug())
            {
                logWrapper.Debug("NTIS Model Predefined Location Update");
            }

            linkShapeStaticDataStore.clearDataStore();
            nwkLinkStaticDataStore.clearDataStore();
            anprRouteStaticDataStore.clearDataStore();
            hatrisSectionStaticDataStore.clearDataStore();
            nwkNodeStaticDataStore.clearDataStore();
            alternateRouteStaticDataStore.clearDataStore();

            PredefinedLocationsPublication predefinedLocationsPublication = (PredefinedLocationsPublication)d2LogicalModel.payloadPublication;
            if (predefinedLocationsPublication != null)
            {

                DateTime publicationTime = predefinedLocationsPublication.publicationTime;

                PredefinedLocationContainer[] predefinedLocationContainerList = predefinedLocationsPublication.predefinedLocationContainer;

                for (int predefinedLocationContainerListPos = 0; predefinedLocationContainerListPos < predefinedLocationContainerList.Length; predefinedLocationContainerListPos++)
                {
                    PredefinedLocationContainer predefinedLocationContainer = predefinedLocationContainerList[predefinedLocationContainerListPos];
                    processPredefinedLocationContainer(predefinedLocationContainer, publicationTime);
                }
            }

            if (logWrapper.isDebug())
            {
                logWrapper.Debug("NTIS Model Predefined Location Update Complete");
            }
        }

        private void processPredefinedLocationContainer(PredefinedLocationContainer predefinedLocationContainer, DateTime publicationTime)
        {

            if (predefinedLocationContainer.GetType() == typeof(PredefinedItinerary))
            {
                PredefinedItinerary predefinedItinerary = (PredefinedItinerary)predefinedLocationContainer;
                string predefinedLocationContainerId = predefinedItinerary.id;
                _PredefinedItineraryIndexPredefinedLocation[] predefinedLocationList = predefinedItinerary.predefinedLocation;
                for (int predefinedLocationListPos = 0; predefinedLocationListPos < predefinedLocationList.Length; predefinedLocationListPos++)
                {
                    _PredefinedItineraryIndexPredefinedLocation predefinedItineraryIndexPredefinedLocation = predefinedLocationList[predefinedLocationListPos];
                    processPredefinedLocation(predefinedItineraryIndexPredefinedLocation.predefinedLocation, publicationTime, predefinedLocationContainerId);
                }
            } else if (predefinedLocationContainer.GetType() == typeof(PredefinedNonOrderedLocationGroup))
            {
                PredefinedNonOrderedLocationGroup predefinedNonOrderedLocationGroup = (PredefinedNonOrderedLocationGroup)predefinedLocationContainer;
                string predefinedLocationContainerId = predefinedNonOrderedLocationGroup.id;
                PredefinedLocation[] predefinedLocationList = predefinedNonOrderedLocationGroup.predefinedLocation;
                for (int predefinedLocationListPos=0; predefinedLocationListPos < predefinedLocationList.Length; predefinedLocationListPos++)
                {
                    PredefinedLocation predefinedLocation = predefinedLocationList[predefinedLocationListPos];
                    processPredefinedLocation(predefinedLocation, publicationTime, predefinedLocationContainerId);
                }
            } else {
                logWrapper.Error("Unexpected data type for PredefinedLocationContainer: " + predefinedLocationContainer.GetType().ToString());
                return;
            }
        }

        private void processPredefinedLocation(PredefinedLocation predefinedLocation, DateTime publicationTime, string predefinedLocationContainerId)
        {
            string predefinedLocationIdentifier = predefinedLocation.id;

            if (logWrapper.isTrace())
            {
                logWrapper.Trace("Processing Predefined Location Identifier(" + predefinedLocationIdentifier + ")");
            }

            if (predefinedLocationContainerId.Equals("NTIS_Network_Links"))
            {
                NwkLinkStaticData nwkLinkStaticData = new NwkLinkStaticData(predefinedLocationIdentifier, publicationTime, predefinedLocation);
                nwkLinkStaticDataStore.updateData(nwkLinkStaticData);
            }
            else if (predefinedLocationContainerId.Equals("NTIS_Network_Nodes"))
            {
                NwkNodeStaticData nwkNodeStaticData = new NwkNodeStaticData(predefinedLocationIdentifier, publicationTime, predefinedLocation);
                nwkNodeStaticDataStore.updateData(nwkNodeStaticData);
            }
            else if (predefinedLocationContainerId.StartsWith("NTIS_Link_Shape_"))
            {
                LinkShapeStaticData linkShapeStaticData = new LinkShapeStaticData(predefinedLocationIdentifier, publicationTime, predefinedLocation);
                linkShapeStaticDataStore.updateData(linkShapeStaticData);
            }
            else if (predefinedLocationContainerId.StartsWith("NTIS_ANPR_Route_"))
            {
                ANPRRouteStaticData anprRouteStaticData = new ANPRRouteStaticData(predefinedLocationIdentifier, publicationTime, predefinedLocation);
                anprRouteStaticDataStore.updateData(anprRouteStaticData);
            }
            else if (predefinedLocationContainerId.StartsWith("NTIS_HATRIS_Section_"))
            {
                HATRISSectionStaticData hatrisSectionStaticData = new HATRISSectionStaticData(predefinedLocationIdentifier, publicationTime, predefinedLocation);
                hatrisSectionStaticDataStore.updateData(hatrisSectionStaticData);
            }
            else if (predefinedLocationContainerId.StartsWith("NTIS_Alternate_Route_"))
            {
                AlternateRouteStaticData alternateRouteStaticData = new AlternateRouteStaticData(predefinedLocationIdentifier, publicationTime, predefinedLocation);
                alternateRouteStaticDataStore.updateData(alternateRouteStaticData);
            }
            else {
                logWrapper.Error("Predefined Location Container not implemented: " + predefinedLocationContainerId);
            }
        }
    }
}

/*

@Service
public class DATEXIINTISModelPredefinedLocationProcessService extends DATEXIIProcessService {
	final Logger log = LoggerFactory.getLogger(DATEXIINTISModelPredefinedLocationProcessService.class);
	
	LinkShapeStaticDataStore linkShapeStaticDataStore;
	NwkLinkStaticDataStore nwkLinkStaticDataStore;
	ANPRRouteStaticDataStore anprRouteStaticDataStore;
	HATRISSectionStaticDataStore hatrisSectionStaticDataStore;
	NwkNodeStaticDataStore nwkNodeStaticDataStore;
	AlternateRouteStaticDataStore alternateRouteStaticDataStore;
	
	@Autowired
	public DATEXIINTISModelPredefinedLocationProcessService(
			LinkShapeStaticDataStore linkShapeStaticDataStore,
			NwkLinkStaticDataStore nwkLinkStaticDataStore,
			ANPRRouteStaticDataStore anprRouteStaticDataStore,
			HATRISSectionStaticDataStore hatrisSectionStaticDataStore,
			NwkNodeStaticDataStore nwkNodeStaticDataStore,
			AlternateRouteStaticDataStore alternateRouteStaticDataStore){
		super();
		this.linkShapeStaticDataStore = linkShapeStaticDataStore;
		this.nwkLinkStaticDataStore = nwkLinkStaticDataStore;
		this.anprRouteStaticDataStore = anprRouteStaticDataStore;
		this.hatrisSectionStaticDataStore = hatrisSectionStaticDataStore;
		this.nwkNodeStaticDataStore = nwkNodeStaticDataStore;
		this.alternateRouteStaticDataStore = alternateRouteStaticDataStore;
	}
	
	@Override
	public void processMessage(D2LogicalModel d2LogicalModel) {
		
		if (log.isDebugEnabled()){
            log.debug("NTIS Model Predefined Location Update");
        }
        
		linkShapeStaticDataStore.clearDataStore();		
		nwkLinkStaticDataStore.clearDataStore();
		anprRouteStaticDataStore.clearDataStore();
		hatrisSectionStaticDataStore.clearDataStore();
		nwkNodeStaticDataStore.clearDataStore();
		alternateRouteStaticDataStore.clearDataStore();
		
		PredefinedLocationsPublication predefinedLocationsPublication = (PredefinedLocationsPublication)d2LogicalModel.getPayloadPublication();        
        if (predefinedLocationsPublication != null) {
        	
        	Date publicationTime = predefinedLocationsPublication.getPublicationTime().toGregorianCalendar().getTime();
        	        	
            List<PredefinedLocationContainer> predefinedLocationContainerList = predefinedLocationsPublication.getPredefinedLocationContainer();
            
            Iterator<PredefinedLocationContainer> iterator = predefinedLocationContainerList.iterator();
            while (iterator.hasNext()){
            	PredefinedLocationContainer predefinedLocationContainer = iterator.next();
                processPredefinedLocationContainer(predefinedLocationContainer, publicationTime);
            }
        }
        
		if (log.isDebugEnabled()){
            log.debug("NTIS Model Predefined Location Update Complete");
        }
	}
	
	private void processPredefinedLocationContainer(PredefinedLocationContainer predefinedLocationContainer, Date publicationTime) {
		
		
		if (predefinedLocationContainer instanceof PredefinedItinerary){
			PredefinedItinerary predefinedItinerary = (PredefinedItinerary)predefinedLocationContainer;
			String predefinedLocationContainerId = predefinedItinerary.getId();			
			List<PredefinedItineraryIndexPredefinedLocation> predefinedLocationList = predefinedItinerary.getPredefinedLocation();			
			Iterator<PredefinedItineraryIndexPredefinedLocation> iterator = predefinedLocationList.iterator();
	        while (iterator.hasNext()){
	        	PredefinedItineraryIndexPredefinedLocation predefinedItineraryIndexPredefinedLocation = iterator.next();
	        	processPredefinedLocation(predefinedItineraryIndexPredefinedLocation.getPredefinedLocation(), publicationTime, predefinedLocationContainerId);
	        }
		} else if (predefinedLocationContainer instanceof PredefinedNonOrderedLocationGroup){
			PredefinedNonOrderedLocationGroup predefinedNonOrderedLocationGroup = (PredefinedNonOrderedLocationGroup)predefinedLocationContainer;
			String predefinedLocationContainerId = predefinedNonOrderedLocationGroup.getId();			
			List<PredefinedLocation> predefinedLocationList = predefinedNonOrderedLocationGroup.getPredefinedLocation();	
			Iterator<PredefinedLocation> iterator = predefinedLocationList.iterator();
	        while (iterator.hasNext()){
	        	PredefinedLocation predefinedLocation = iterator.next();
	        	processPredefinedLocation(predefinedLocation, publicationTime, predefinedLocationContainerId);
	        }
		} else {
			log.error("Unexpected data type for PredefinedLocationContainer: "+predefinedLocationContainer.getClass().getSimpleName());
			return;
		}		
	}
	
	private void processPredefinedLocation(PredefinedLocation predefinedLocation, Date publicationTime, String predefinedLocationContainerId){
		String predefinedLocationIdentifier = predefinedLocation.getId();
		
		if (log.isTraceEnabled()){
			log.trace("Processing Predefined Location Identifier("+predefinedLocationIdentifier+")");
		}
			
		if (predefinedLocationContainerId.equals("NTIS_Network_Links")){		
			NwkLinkStaticData nwkLinkStaticData = new NwkLinkStaticData(predefinedLocationIdentifier, publicationTime, predefinedLocation);
			nwkLinkStaticDataStore.updateData(nwkLinkStaticData);
		} else if (predefinedLocationContainerId.equals("NTIS_Network_Nodes")) {			
			NwkNodeStaticData nwkNodeStaticData = new NwkNodeStaticData(predefinedLocationIdentifier, publicationTime, predefinedLocation);
			nwkNodeStaticDataStore.updateData(nwkNodeStaticData);
		} else if (predefinedLocationContainerId.startsWith("NTIS_Link_Shape_")){
			LinkShapeStaticData linkShapeStaticData = new LinkShapeStaticData(predefinedLocationIdentifier, publicationTime, predefinedLocation);
			linkShapeStaticDataStore.updateData(linkShapeStaticData);
		} else if (predefinedLocationContainerId.startsWith("NTIS_ANPR_Route_")){		
			ANPRRouteStaticData anprRouteStaticData = new ANPRRouteStaticData(predefinedLocationIdentifier, publicationTime, predefinedLocation);
			anprRouteStaticDataStore.updateData(anprRouteStaticData);
		} else if (predefinedLocationContainerId.startsWith("NTIS_HATRIS_Section_")){
			HATRISSectionStaticData hatrisSectionStaticData = new HATRISSectionStaticData(predefinedLocationIdentifier, publicationTime, predefinedLocation);
			hatrisSectionStaticDataStore.updateData(hatrisSectionStaticData);
		} else if (predefinedLocationContainerId.startsWith("NTIS_Alternate_Route_")){
			AlternateRouteStaticData alternateRouteStaticData = new AlternateRouteStaticData(predefinedLocationIdentifier, publicationTime, predefinedLocation);
			alternateRouteStaticDataStore.updateData(alternateRouteStaticData);
		} else {
			log.error("Predefined Location Container not implemented: "+predefinedLocationContainerId);			
		}
	}

}

*/
