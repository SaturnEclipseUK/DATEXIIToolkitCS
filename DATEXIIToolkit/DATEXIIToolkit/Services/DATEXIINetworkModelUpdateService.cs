using DATEXIIToolkit.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace DATEXIIToolkit.Services
{
    public class DATEXIINetworkModelUpdateService : DATEXIIProcessService
    {
        LogWrapper logWrapper;
        private bool loadCachedNwkModelOnStartup;
        private string nwkModelDirectory;
        private string nwkModelPath;
        private DATEXIIUpdateService datexIIUpdateService;
        private const int MAX_STRING_LENGTH = 1000000;

        public DATEXIINetworkModelUpdateService(DATEXIIUpdateService datexIIUpdateService)
        {
            this.datexIIUpdateService = datexIIUpdateService;
            logWrapper = new LogWrapper("DATEXIINetworkModelUpdateService");
        }

        public void initialise()
        {
            logWrapper.Info("Initialise network model update service");
            nwkModelDirectory = ConfigurationManager.AppSettings["nwkModelDirectory"];
            nwkModelPath = ConfigurationManager.AppSettings["nwkModelPath"];
            loadCachedNwkModelOnStartup = ConfigurationManager.AppSettings["loadCachedNwkModelOnStartup"].Equals("true");

            if (loadCachedNwkModelOnStartup && Directory.CreateDirectory(nwkModelDirectory).Exists)
            {
                parseNetworkModelXMLFiles();
            }
        }

        public void updateNetworkModel(string url, string networkModelFolder, string filename)
        {
            removeExistingNetworkModel();
            fetchNetworkModel(url, networkModelFolder, filename);
            unzipNetworkModel();
            parseNetworkModelXMLFiles();
        }

        private void removeExistingNetworkModel()
        {
            logWrapper.Info("Removing existing network model");
            try
            {
                File.Delete(nwkModelDirectory + nwkModelPath);
                string[] dirList = Directory.GetFiles(nwkModelDirectory);

                for (int dirListPos = 0; dirListPos < dirList.Length; dirListPos++)
                {
                    if (dirList[dirListPos].Contains(".xml"))
                    {
                        File.Delete(dirList[dirListPos]);
                    }
                }
                
            }
            catch (Exception e)
            {
                logWrapper.Error("Error removing directory: " + nwkModelDirectory + nwkModelPath);
                logWrapper.Error(e.StackTrace);
                return;
            }
        }

        private void fetchNetworkModel(String url, string networkModelFolder, string filename)
        {
            WebClient myWebClient=null;
            try
            {
                logWrapper.Info("Downloading network model(" + url + filename + ")");
                string myStringWebResource = null;

                myWebClient = new WebClient();
                myStringWebResource = url + filename;
                myWebClient.DownloadFile(myStringWebResource, networkModelFolder + filename);
                File.Copy(networkModelFolder + filename, networkModelFolder + nwkModelPath);
            }
            catch(Exception e)
            {
                logWrapper.Error("Failed to fetch Network Model file");
                logWrapper.Error(e.ToString());
            }
            finally
            {
                myWebClient.Dispose();
            }
        }

        private void unzipNetworkModel()
        {
            logWrapper.Info("Unzipping network model.");
            ZipFile.ExtractToDirectory(nwkModelDirectory + nwkModelPath, nwkModelDirectory);
        }

        private string FindFile(string[] listOfFiles, string fileName)
        {
            for (int listOfFilesPos=0; listOfFilesPos < listOfFiles.Length; listOfFilesPos++)
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

/*

@Service
public class DATEXIINetworkModelUpdateService {

	final Logger log = LoggerFactory.getLogger(DATEXIINetworkModelUpdateService.class);
		
	@Autowired
	DATEXIIUpdateService datexIIUpdateService;
	
	@Value("${loadCachedNwkModelOnStartup}")
	private Boolean loadCachedNwkModelOnStartup;
	
	String NWK_MODEL_PATH = "nwk_model.zip";
	String NWK_MODEL_DIRECTORY = "nwk_model";
    
	@PostConstruct
	public void initialise(){
		if (loadCachedNwkModelOnStartup && Files.exists(new File(NWK_MODEL_DIRECTORY).toPath())){
			this.parseNetworkModelXMLFiles();
		}
	}
	
	public void updateNetworkModel(String url){
		
		this.removeExistingNetworkModel();
		
		if (url.startsWith("file:///")){
			copyNetworkModel(url);
		} else {
			this.fetchNetworkModel(url);
		}
	    
	    this.unzipNetworkModel();
		 
		this.parseNetworkModelXMLFiles();
	}
	
	private void removeExistingNetworkModel(){
		log.info("Removing existing network model");
		try {
			FileUtils.deleteDirectory(new File(NWK_MODEL_DIRECTORY));
			new File(NWK_MODEL_PATH).delete();
		} catch (IOException e) {
			log.error("Error removing directory: "+e.toString(),e);
			return;
		}
	}
	
	private void copyNetworkModel(String path){
		path = path.replace("file:///", "");
		try {
			Files.copy(new File(path).toPath(), new File(NWK_MODEL_PATH).toPath());
		} catch (IOException e) {
			log.error("Error copying file: "+path, e);
		}
	}
	
	private void fetchNetworkModel(String url){
		// Create an instance of HttpClient.
	    HttpClient client = new HttpClient();
	
	    // Create a method instance.
	    GetMethod method = new GetMethod(url);
	    
	    // Provide custom retry handler is necessary
	    method.getParams().setParameter(HttpMethodParams.RETRY_HANDLER, 
	    		new DefaultHttpMethodRetryHandler(3, false));
	
	    try {
	      log.info("Downloading network model.");
		  // Execute the method.
		  int statusCode = client.executeMethod(method);
		
		  if (statusCode != HttpStatus.SC_OK) {
		    log.error("Download of network model failed: " + method.getStatusLine());
		    return;
		  }
		
		  // Read the response body.
		  InputStream inputStream = method.getResponseBodyAsStream();
		
		  FileOutputStream fos = new FileOutputStream(NWK_MODEL_PATH);
		  IOUtils.copy(inputStream, fos);
		  fos.close();	
	    } catch (HttpException e) {
	      log.error("Fatal protocol violation: " + e.getMessage(), e);	      
	    } catch (IOException e) {
	      log.error("Fatal transport error: " + e.getMessage(), e);	      
	    } finally {
	      // Release the connection.
	      method.releaseConnection();
	    }
	}
	
	private void unzipNetworkModel(){
		log.info("Unzipping network model.");
		UnZipUtils.unZipFile(NWK_MODEL_PATH, NWK_MODEL_DIRECTORY);
	}
	
	private void parseNetworkModelXMLFiles(){
		File folder = new File(NWK_MODEL_DIRECTORY);
		File[] listOfFiles = folder.listFiles();
		  
		for (int i = 0; i < listOfFiles.length; i++) {
			if (listOfFiles[i].isFile()) {
				String filename = listOfFiles[i].getName();	
				try {
					byte[] encoded = Files.readAllBytes(Paths.get(listOfFiles[i].getPath()));	  
					datexIIUpdateService.addToMessageQueue(new String(encoded, StandardCharsets.UTF_8));
					log.info("Added file to process queue: "+filename);
				} catch (IOException e ){
					log.error("Failed to read file: "+filename, e);
				}
		    } 
		}
	}
}

*/
