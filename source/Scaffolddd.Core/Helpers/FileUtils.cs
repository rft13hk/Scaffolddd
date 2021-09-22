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

        public static void WriteFile(string text, string pathFileDest, bool overWrite, bool backupOld)
        {
            #region Compare Old and New

            var fileExist = File.Exists(pathFileDest);

            if (fileExist)
            {
                var oldText = File.ReadAllText(pathFileDest);

                if (StringUtils.CompareString(text, oldText))
                {
                    //Is Equal then exit;
                    return;
                }
            }
            #endregion

            var nowName = DateTime.Now.ToString("yyyyMMdd-HHmmss");

            if (backupOld && fileExist && overWrite)
            {
                // Create Backup 
                File.Copy(pathFileDest, string.Concat(pathFileDest,"_Old_", nowName ),true);
            }

            if (fileExist && overWrite)
            {
                File.Delete(pathFileDest);
            }

            if (!File.Exists(pathFileDest))
            {
                File.WriteAllText(pathFileDest,text);
            }
            else
            {
                File.WriteAllText(string.Concat(pathFileDest,"_New_", nowName ),text);
            }
        }

    }
}