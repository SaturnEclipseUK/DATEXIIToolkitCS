using DATEXIIToolkit.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Timers;
using System.Web;

namespace DATEXIIToolkit.Services
{
    /// <summary>
    /// This service retrieves the latest network model and forwards it to 
    /// the DATEX II Update Service for processing.
    /// </summary>
    public class DATEXIINetworkModelUpdateService : DATEXIIProcessService
    {
        LogWrapper logWrapper;
        private string nwkModelDirectory;
        private string nwkModelPath;
        private DATEXIIUpdateService datexIIUpdateService;
        private const int MAX_STRING_LENGTH = 1000000;

        public DATEXIINetworkModelUpdateService(DATEXIIUpdateService datexIIUpdateService)
        {
            this.datexIIUpdateService = datexIIUpdateService;
            logWrapper = new LogWrapper("DATEXIINetworkModelUpdateService");
            nwkModelDirectory = ConfigurationManager.AppSettings["rootDirectory"] +
                ConfigurationManager.AppSettings["nwkModelDirectory"];
            nwkModelPath = ConfigurationManager.AppSettings["nwkModelPath"];
        }

        public bool updateNetworkModel(string url, string username, string password)
        {
            bool fetchedNetworkModel = false;
            removeExistingNetworkModel();
            fetchedNetworkModel = fetchNetworkModel(url, username, password);
            if (fetchedNetworkModel == true)
            {
                unzipNetworkModel();
                parseNetworkModelXMLFiles();
            }
            return fetchedNetworkModel;
        }

        private void removeExistingNetworkModel()
        {
            logWrapper.Info("Removing existing network model");
            try
            {
                if (Directory.Exists(nwkModelDirectory))
                {
                    Directory.Delete(nwkModelDirectory, true);
                }

            }
            catch (Exception e)
            {
                logWrapper.Error("Error removing directory: " + nwkModelDirectory + nwkModelPath);
                logWrapper.Error(e.StackTrace);
                return;
            }
        }

        private bool fetchNetworkModel(String url, string username, string password)
        {
            WebClient myWebClient = null;
            bool fetchedNetworkModel = false;
            try
            {
                logWrapper.Info("Downloading network model(" + url + ")");
                Directory.CreateDirectory(nwkModelDirectory);
                
                myWebClient = new WebClient();
                if (username != null && password != null)
                {
                    string credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes(username + ":" + password));
                    myWebClient.Headers[HttpRequestHeader.Authorization] = string.Format("Basic {0}", credentials);
                }
                myWebClient.DownloadFile(url, nwkModelDirectory + nwkModelPath);
                fetchedNetworkModel = true;
            }
            catch (Exception e)
            {
                logWrapper.Error("Failed to fetch Network Model file");
                logWrapper.Error(e.ToString());
            }
            finally
            {
                if (myWebClient != null)
                {
                    myWebClient.Dispose();
                }
            }
            return fetchedNetworkModel;
        }

        private void unzipNetworkModel()
        {
            logWrapper.Info("Unzipping network model.");
            ZipFile.ExtractToDirectory(nwkModelDirectory + nwkModelPath, nwkModelDirectory);
        }

        private string FindFile(string[] listOfFiles, string fileName)
        {
            for (int listOfFilesPos = 0; listOfFilesPos < listOfFiles.Length; listOfFilesPos++)
            {
                string returnFilename = listOfFiles[listOfFilesPos];
                if (returnFilename.Contains(fileName))
                {
                    return returnFilename;
                }
            }
            return null;
        }

        private void processNetworkModelXMLFiles(string filename)
        {
            FileStream nwkModelFileStream = null;
            try
            {
                nwkModelFileStream = File.OpenRead(filename);
                int fileLength = (int)nwkModelFileStream.Length;
                byte[] buffer = new byte[fileLength];

                int count = 0;
                int sum = 0;

                while (sum < fileLength)
                {
                    if (fileLength - sum > MAX_STRING_LENGTH)
                    {
                        count = MAX_STRING_LENGTH;
                    }
                    else {
                        count = fileLength - sum;
                    }
                    nwkModelFileStream.Read(buffer, sum, count);
                    sum += count;
                }

                logWrapper.Info("Added file to process queue: " + filename);
                datexIIUpdateService.addToMessageQueue(buffer);
                DATEXIIUpdateService.processDATEXIIUpdateXML(null, null);
            }
            catch (Exception e)
            {
                logWrapper.Error("Failed to read file: " + filename);
                logWrapper.Error(e.StackTrace);
            }
            finally
            {
                nwkModelFileStream.Close();
            }
        }

        private void parseNetworkModelXMLFiles()
        {
            logWrapper.Info("Parsing network model files.");
            string[] listOfFiles = Directory.GetFiles(nwkModelDirectory);
            string filename = "";
            logWrapper.Info("processing file: " + filename);

            logWrapper.Info("Found(" + listOfFiles.Length + ") files");

            string nwkModelFileName = FindFile(listOfFiles, "PredefinedLocations");
            if (nwkModelFileName != null)
            {
                processNetworkModelXMLFiles(nwkModelFileName);
            }
            else
            {
                logWrapper.Error("Failed to find PredefinedLocations file");
            }

            nwkModelFileName = FindFile(listOfFiles, "VMSTables");
            if (nwkModelFileName != null)
            {
                processNetworkModelXMLFiles(nwkModelFileName);
            }
            else
            {
                logWrapper.Error("Failed to find VMSTables file");
            }

            nwkModelFileName = FindFile(listOfFiles, "MeasurementSites");
            if (nwkModelFileName != null)
            {
                processNetworkModelXMLFiles(nwkModelFileName);
            }
            else
            {
                logWrapper.Error("Failed to find MeasurementSites file");
            }
        }

        public override void processMessage(D2LogicalModel d2LogicalModel)
        {
            throw new NotImplementedException();
        }
    }
}