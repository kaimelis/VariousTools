﻿#if UNITY_EDITOR
using System.IO;
using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;

namespace Custom.Tool.AutoBuild
{
    public class BuildManager
    {
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
            if(!EditorUserBuildSettings.development)
                 BuildName = VersionManager.Instance.GetVersion();
            else
                BuildName = VersionManager.Instance.GetVersion(true);

            BuildPopUpWindow.OpenWindow();
        }

        public void MakeABuild()
        {
            BuildPath += BuildName + "/";

            if (EditorUserBuildSettings.activeBuildTarget == BuildTarget.Android)
                BuildName += ".apk";
            else
                BuildName += ".exe";

            BuildReport build = BuildPipeline.BuildPlayer(GetScenePaths(), BuildPath + BuildName, EditorUserBuildSettings.activeBuildTarget, BuildOptions.None);
            if (File.Exists(BuildPath + BuildName) && build)
            {
                //check if windows and not development
                if(!EditorUserBuildSettings.development)
                   GitHande.RunGitCommand("/c/Users/kaime/Documents/00_MOKSLAI/Graduation/TBS/tbs/tbs unity production");

                Debug.Log("<b><color=green> Build has been sucesfully made </color></b>");
                return;
            }
            Debug.LogError("<b><color=red> Build has failed to be done </color></b>");

        }
        public string BuildPath { get; set; } = Directory.GetCurrentDirectory() + "/Builds/";

        public string BuildName { get; set; } = "unknown";
        public string NameBuildNoExt { get; set; } = "";

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
