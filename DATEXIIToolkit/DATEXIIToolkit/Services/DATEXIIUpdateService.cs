using DATEXIIToolkit.Common;
using DATEXIIToolkit.DATEXII;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Timers;
using System.Web;
using System.Xml.Serialization;

namespace DATEXIIToolkit.Services
{

    public class DATEXIIUpdateService
    {
        private static LogWrapper logWrapper;
        private const int QUEUE_EMPTY = 0;
        private const int QUEUE_PROCESSING_TIMER_PERIOD = 1000;

        private volatile static DATEXIIUpdateService instance;
        private static DATEXIIProcessServiceFactory datexiiProcessServiceFactory;
        private static Queue<UpdateMessage> messageQueue;
        private static int working = 0;

        private DATEXIIUpdateService()
        {
            logWrapper = new LogWrapper("DATEXIIUpdateService");
            logWrapper.Info("Creating DATEXIIUpdateService object");
            messageQueue = new Queue<UpdateMessage>();

            Timer processQueueTimer = new Timer();
            processQueueTimer.Elapsed += new ElapsedEventHandler(processDATEXIIUpdateXML);
            processQueueTimer.Interval = QUEUE_PROCESSING_TIMER_PERIOD;
            processQueueTimer.Enabled = true;
        }

        public static DATEXIIUpdateService GetInstance()
        {
            if (instance == null)
            {
                instance = new DATEXIIUpdateService();
                datexiiProcessServiceFactory = DATEXIIProcessServiceFactory.GetInstance(instance);
                DATEXIINetworkModelUpdateService datexIINetworkModelUpdateService = (DATEXIINetworkModelUpdateService)datexiiProcessServiceFactory.getDATEXIIProcessService(DATEXIIProcessServiceFactory.DATA_SERVICE_TYPE.NWK_MODEL_UPDATE);
                datexIINetworkModelUpdateService.initialise();
            }
            return instance;
        }

        public void addToMessageQueue(byte[] xml)
        {
            if (xml != null)
            {
                lock (messageQueue)
                {
                    messageQueue.Enqueue(new UpdateMessage(xml));
                }
            }
        }

        public static void processDATEXIIUpdateXML(object source, ElapsedEventArgs e)
        {
            working++;
            if (logWrapper.isDebug() == true)
            {
                logWrapper.Debug("Polling for messages");
            }

            UpdateMessage xml = null;
            lock(messageQueue){
                if (messageQueue.Count() > QUEUE_EMPTY) {
                    xml = messageQueue.Dequeue();
                }
                    
            }
            while (xml != null)
            {
                byte[] byteArray = xml.Buffer;
                /*XmlSerializer mySoapSerializer = new XmlSerializer(typeof(Envelope));
                Envelope soapEnvelope = (Envelope)mySoapSerializer.Deserialize(new MemoryStream(byteArray));*/
                
                XmlSerializer myDIISerializer = new XmlSerializer(typeof(D2LogicalModel));
                D2LogicalModel d2lm = (D2LogicalModel)myDIISerializer.Deserialize(new MemoryStream(byteArray));
                string feedType = FeedType.getFeedType(d2lm.payloadPublication.feedType);
                DATEXIIProcessService datexiiProcessService = datexiiProcessServiceFactory.getServiceType(feedType);
                if (datexiiProcessService != null)
                {
                    datexiiProcessService.processMessage(d2lm);
                }

                lock(messageQueue){
                    if (messageQueue.Count() > QUEUE_EMPTY)
                    {
                        xml = messageQueue.Dequeue();
                    } else {
                        xml = null;
                    }
                }
            }
            working--;
        }

        private String getFeedType(D2LogicalModel d2lm)
        {
            String feedType = d2lm.payloadPublication.feedType;
            return FeedType.getFeedType(feedType);
        }

        public bool workPending()
        {
            lock(messageQueue){
                bool workPending = ((messageQueue.Count != QUEUE_EMPTY) || (working>0));
                return workPending;
            }
        }
    }

}