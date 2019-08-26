#if UNITY_EDITOR
using System.IO;
using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;

namespace Custom.Tool.AutoBuild
{
    public class BuildManager
    {
        private string _buildName = "unknown";
        private string _buildPath = Directory.GetCurrentDirectory() + "/Builds/";
        private static BuildManager _instance;
        public static BuildManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new BuildManager();
                return _instance;
            }
            private set { _instance = new BuildManager(); }
        }

        public void Build()
        {

            ///c/Users/kaime/Documents/00_MOKSLAI/Graduation/TBS/tbs/
            _buildName = VersionManager.Instance.GetVersion();
            BuildPopUpWindow.OpenWindow();
        }

        public void MakeABuild()
        {
            BuildReport build = BuildPipeline.BuildPlayer(GetScenePaths(), "/Builds/" + _buildName, EditorUserBuildSettings.activeBuildTarget, BuildOptions.EnableHeadlessMode);

            if(File.Exists(_buildPath + _buildName) && build)
            {
                //check if windows and not development
                if (!EditorUserBuildSettings.development)
                    GitHande.RunGitCommand("c/Users/kaime/Documents/00_MOKSLAI/Graduation/TBS/tbs/tbs unity production ");

                Debug.Log("<b><color=green> Build has been sucesfully made </b></color>");
            }
        }

        public void SetBuildPath(string pPath)
        {
            _buildPath = pPath;
        }

        public string GetBuildPath()
        {
            return _buildPath;
        }

        public string GetBuildName()
        {
            return _buildName;
        }
        
        public void SwitchPlatform(string platform)
        {
            if(platform.Contains("Android"))
            {
                EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.Android,BuildTarget.Android);
            }
            else if(platform.Contains("iOS"))
            {
                EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.iOS, BuildTarget.iOS);
            }
            else
            {
                EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.Standalone, BuildTarget.StandaloneWindows);
            }
        }

        private string[] GetScenePaths()
        {
            string[] scenes = new string[EditorBuildSettings.scenes.Length];
            for (int i = 0; i < scenes.Length; i++)
            {
                scenes[i] = EditorBuildSettings.scenes[i].path;
            }
            return scenes;
        }
    }
}

#endif
