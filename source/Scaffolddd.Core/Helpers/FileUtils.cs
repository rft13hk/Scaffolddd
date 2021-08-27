// For Directory.GetFiles and Directory.GetDirectories
// For File.Exists, Directory.Exists
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace Scaffolddd.Core.Helpers
{
    public static class FileUtils
    {

        // Process all files in the directory passed in, recurse on any directories
        // that are found, and process the files they contain.
        public static List<string> ProcessDirectory(string targetDirectory)
        {
            List<string> files = new List<string>();

            // Process the list of files found in the directory.
            string[] fileEntries = Directory.GetFiles(targetDirectory);
            foreach (string fileName in fileEntries) files.Add(fileName);

            return files;
        }

        public static string ExtractNameFromPath(string fullPathWithName)
        {
            var retorno = fullPathWithName;

            for (int l = fullPathWithName.Length-1; l>=0;l--)
            {
                if (fullPathWithName[l] == '/')
                {
                    retorno = fullPathWithName.Substring(l+1);
                    break;
                }
            }

            return retorno;
        }


    }
}