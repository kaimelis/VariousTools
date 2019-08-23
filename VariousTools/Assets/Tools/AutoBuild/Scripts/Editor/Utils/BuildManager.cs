#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;

namespace Custom.Tool.AutoBuild
{
    public class BuildManager
    {
        private string _buildName = "unknown";
        private string _buildPath = "";
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


             BuildReport build = BuildPipeline.BuildPlayer(GetScenePaths(), "/Builds/" + _buildName, EditorUserBuildSettings.activeBuildTarget, BuildOptions.EnableHeadlessMode);
        }

        public void SetBuildPath(string pPath)
        {
            _buildPath = pPath;
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
