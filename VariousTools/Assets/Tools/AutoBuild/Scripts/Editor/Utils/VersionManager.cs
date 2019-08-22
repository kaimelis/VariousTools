#if UNITY_EDITOR
using Sirenix.OdinInspector;
using System.Diagnostics;
using System.IO;
using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;

namespace Custom.Tool.AutoBuild
{
    public class VersionManager
    {
        #region Instance
        private static VersionManager _instance = null;
        public static VersionManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new VersionManager();
                return _instance;
            }
            private set
            {
                _instance = new VersionManager();
            }
        }
        #endregion
        private string _version;
        private string _developmentVersion;
        private string _iosVersion;

        private string _pathVersion = Path.GetDirectoryName(Application.dataPath) + "/VERSION";
        private string _pathDevelopVersion = Path.GetDirectoryName(Application.dataPath) + "/tmp/local_version";

        public void UpdateVersion()
        {
            _version = GetVersionFromFile();
            UpgradeVersionPopWindow.OpenWindow();
        }
        
        /// <summary>
        /// Creates a VERSION file. If repository does not have a tag then it will create a tag and a file.
        /// </summary>
        public void CreateNewVersionFile()
        {
             GitHande.RunGitCommand("tbs unity version v0.1.0");

            UnityEngine.Debug.Log("<b><color=green> File was created.</color></b>");

            UpgradeVersionPopWindow.OpenWindow();
        }

        public string GetVersion()
        {
            _version = GetVersionFromFile();
            return _version;
        }

        public void SetVersion(string version)
        {
            _version = version;
            PlayerSettings.bundleVersion = _version;
            UnityEngine.Debug.Log("<b><color=Green> Version set to be : </color></b>" + _version);
            FileReaderWriter.WriteToFile(_pathVersion,_version);
        }

        private string GetVersionFromFile()
        {
            string fileVersion = "";
            if(EditorUserBuildSettings.development)
            {
                if (FileReaderWriter.CheckIfFileExists(_pathDevelopVersion))
                {
                    fileVersion = FileReaderWriter.ReadLineFromFile(_pathDevelopVersion);
                    UnityEngine.Debug.Log("<b><color=blue> Develop version is : </color></b>" + fileVersion);
                }
                else
                {
                    UnityEngine.Debug.LogError("<b><color=red> Develop version file does not exists </color></b>");
                    fileVersion = GitHande.GetGitOutput("git describe");
                }
            }
            else
            {
                if (FileReaderWriter.CheckIfFileExists(_pathVersion))
                {
                    fileVersion = FileReaderWriter.ReadLineFromFile(_pathVersion);
                    UnityEngine.Debug.Log("<b><color=blue> Version is : </color></b>" + fileVersion);
                }
                else
                {
                    UnityEngine.Debug.LogError("<b><color=red> Version file does not exists. </color></b>");
                    VersionPopUpWindow.OpenWindow();
                }
            }
            return fileVersion;
        }
    }
}
#endif
