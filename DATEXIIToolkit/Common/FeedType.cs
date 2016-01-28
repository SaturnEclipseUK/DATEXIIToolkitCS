using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DATEXIIToolkit.DATEXII
{
    public class FeedType
    {
        public static string ANPR = "ANPR Journey Time Data";
        public static string MIDAS = "MIDAS Loop Traffic Data";
        public static string TMU = "TMU Loop Traffic Data";
        public static string FUSED_SENSOR_ONLY = "Fused Sensor-only PTD";
        public static string FUSED_FVD_AND_SENSOR_PTD = "Fused FVD and Sensor PTD";
        public static string VMS = "VMS and Matrix Signal Status Data";
        public static string NTIS_MODEL_UPDATE_NOTIFICATION = "NTIS Model Update Notification";
        public static string EVENT = "Event Data";
        public static string TAME = "TAME Loop Traffic Data";
        public static string NTIS_MODEL_VMS_TABLES = "NTIS Model - VMS Tables";
        public static string NTIS_MODEL_PREDEFINED_LOCATIONS = "NTIS Model - Predefined Locations";
        public static string NTIS_MODEL_MEASUREMENT_SITES = "NTIS Model - Measurement Sites";

        private String feedTypeText;

        FeedType(String feedTypeText)
        {
            this.feedTypeText = feedTypeText;
        }

        public String Value()
        {
            return feedTypeText;
        }

    public static String getFeedType(String feedTypeText)
    {
        if (feedTypeText.Equals(FeedType.ANPR))
        {
            return "ANPR";
        }
        else if (feedTypeText.Equals(FeedType.MIDAS))
        {
            return "MIDAS";
        }
        else if (feedTypeText.Equals(FeedType.TMU))
        {
            return "TMU";
        }
        else if (feedTypeText.Equals(FeedType.FUSED_SENSOR_ONLY))
        {
            return "FUSED_SENSOR_ONLY";
        }
        else if (feedTypeText.Equals(FeedType.FUSED_FVD_AND_SENSOR_PTD))
        {
            return "FUSED_FVD_AND_SENSOR_PTD";
        }
        else if (feedTypeText.ToLower().Contains(FeedType.VMS.ToLower()))
        {
            return "VMS";
        }
        else if (feedTypeText.Equals(FeedType.NTIS_MODEL_UPDATE_NOTIFICATION))
        {
            return "NTIS_MODEL_UPDATE_NOTIFICATION";
        }
        else if (feedTypeText.ToLower().Contains(FeedType.EVENT.ToLower()))
        {
            return "EVENT";
        }
        else if (feedTypeText.Equals(FeedType.TAME))
        {
            return "TAME";
        }
        else if (feedTypeText.Equals(FeedType.NTIS_MODEL_VMS_TABLES))
        {
            return "NTIS_MODEL_VMS_TABLES";
        }
        else if (feedTypeText.Equals(FeedType.NTIS_MODEL_PREDEFINED_LOCATIONS))
        {
            return "NTIS_MODEL_PREDEFINED_LOCATIONS";
        }
        else if (feedTypeText.Equals(FeedType.NTIS_MODEL_MEASUREMENT_SITES))
        {
            return "NTIS_MODEL_MEASUREMENT_SITES";
        }
        return null;

    }
}

}