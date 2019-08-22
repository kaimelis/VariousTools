using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Custom.Tool
{
    /// <summary>
    /// Utility class that is used for purpose of not repeating code.
    /// </summary>
    public static class FileReaderWriter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public static void WriteToFile(string path, string text)
        {
            if (File.Exists(path))
            {
                File.WriteAllText(path, text);
            }
        }

        /// <summary>
        /// Function that handels creation of the file
        /// </summary>
        /// <param name="path"></param>
        /// <param name="name"></param>
        public static void CreateFile(string path)
        {
            if (!File.Exists(path))
            {
                FileInfo fi = new FileInfo(path);
                using (StreamWriter sw = fi.CreateText())
                {
                    sw.Close();
                }
            }
        }

        /// <summary>
        /// Function that handels deletion of the file
        /// </summary>
        /// <param name="path"></param>
        /// <param name="name"></param>
        public static void DeleteFile(string path, string name)
        {
            if (File.Exists(path + name))
            {
                File.Delete(path + name);
            }
        }

        public static bool CheckIfFileExists(string path)
        {
            return File.Exists(path);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string ReadLineFromFile(string path)
        {
            if (File.Exists(path))
            {
                string text = "";
                text = File.ReadAllText(path);

                return text;
            }
            else
                return "";
        }
    }
}
