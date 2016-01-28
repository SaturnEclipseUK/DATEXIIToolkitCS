using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.IO.Compression;

namespace DATEXIIToolkit.Common
{
    public class UnZipUtils
    {

	/**
     * Unzip it
     * @param zipFile input zip file
     * @param output zip file output folder
     */
    public void unZipFile(String zipFile, String outputFolder)
        {
            //create output directory is not exists
            System.IO.Directory.CreateDirectory(outputFolder);
            ZipFile.ExtractToDirectory(zipFile, outputFolder);
        }
    }

}