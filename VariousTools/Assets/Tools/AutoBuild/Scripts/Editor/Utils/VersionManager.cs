using System;
using System.IO;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
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
        private string _versionSuggestion;
        private string _developmentVersion;
        private string _iosVersion;

        private string _pathVersion = Directory.GetCurrentDirectory() + "/VERSION";
        private string _pathDevelopVersion = Directory.GetCurrentDirectory() + "/tmp/local_version";
       
        public void UpdateVersion()
        {
            //check if we have version file
            if(FileReaderWriter.CheckIfFileExists(_pathVersion))
            {
                UpgradeVersionPopWindow.OpenWindow();
                Debug.Log("Version file exists");
            }
            else
            {
                VersionPopUpWindow.OpenWindow();
                Debug.LogError("Version file doesn't exists");
            }
        }

        /// <summary>
        /// Creates a VERSION file. If repository does not have a tag then it will create a tag and a file.
        /// </summary>
        public void CreateNewVersionFile()
        {
            GitHande.RunGitCommand("tbs unity version v0.1.0");

            Debug.Log("<b><color=green> File was created.</color></b>");

            UpgradeVersionPopWindow.OpenWindow();
        }

        public string GetSuggestionVersion()
        {
            string suggestionVersion = GetVersion();
            _versionSuggestion = SplitVersion(suggestionVersion);
            return _versionSuggestion;
        }

        public string GetVersion()
        {
            _version = PlayerSettings.bundleVersion;

            if (_version == GetVersionFromFile())
                _version = PlayerSettings.bundleVersion;
            else
                _version = PlayerSettings.bundleVersion;
            return _version;
        }

        public void SetVersion(string version)
        {
            _version = version;
            PlayerSettings.bundleVersion = _version;

            Debug.Log("<b><color=Green> Version set to be : </color></b>" + _version);
            FileReaderWriter.WriteToFile(_pathVersion,_version);

            ParameterManager.Instance.PrepareSettings();
        }

        private string SplitVersion(string version)
        {
            if (version == "" || !version.Contains("v"))
                version = PlayerSettings.bundleVersion;

            Debug.Log("<b><color=blue> Current version is: </color></b> = " + version);
            //v0 1 0
            //v0.1.0
            //v0 1 0b1
            var splitVersionDot = version.Split('.');
            var splitMajorV = splitVersionDot[0].Split('v');
            int buildVersion;
            int buildVersionMinor;
            var splitBuildB = splitVersionDot[2].Split('b');
            //need to check if there is b to split from or not
            
            if (splitBuildB.Length > 1)
            {
                buildVersionMinor = Convert.ToInt32(splitBuildB[1]) + 1;
                buildVersion = Convert.ToInt32(splitBuildB[0]);
                version = "v" + splitMajorV[0] + "." + splitVersionDot[1] + "." + buildVersion.ToString() + "b" + buildVersionMinor.ToString();
            }
            else
            {
                buildVersion = Convert.ToInt32(splitVersionDot[2]) + 1;
                version = "v" + splitMajorV[0] + "." + splitVersionDot[1] + "." + buildVersion.ToString();
            }

            Debug.Log("<b><color=green> Suggested version is: </color></b> = " + version);
            return version;
        }

        private string GetVersionFromFile()
        {
            string fileVersion = "";
            if(EditorUserBuildSettings.development)
            {
                if (FileReaderWriter.CheckIfFileExists(_pathDevelopVersion))
                {
                    fileVersion = FileReaderWriter.ReadLineFromFile(_pathDevelopVersion);
                    Debug.Log("<b><color=blue> Develop version is : </color></b>" + fileVersion);
                }
                else
                {
                    Debug.LogError("<b><color=red> Develop version file does not exists </color></b>");
                    fileVersion = GitHande.GetGitOutput("git describe");
                }
            }
            else
            {
                if (FileReaderWriter.CheckIfFileExists(_pathVersion))
                {
                    fileVersion = FileReaderWriter.ReadLineFromFile(_pathVersion);
                    //Debug.Log("<b><color=blue> Version is : </color></b>" + fileVersion);
                }
                else
                {
                    Debug.LogError("<b><color=red> Version file does not exists. </color></b>");
                    VersionPopUpWindow.OpenWindow();
                }
            }
            return fileVersion;
        }
    }
}

