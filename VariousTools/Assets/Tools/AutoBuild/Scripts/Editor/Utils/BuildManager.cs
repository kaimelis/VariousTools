#if UNITY_EDITOR
using Sirenix.OdinInspector;
using System.Diagnostics;
using System.IO;
using UnityEditor;
using UnityEditor.Build.Reporting;

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
            BuildReport build = BuildPipeline.BuildPlayer(GetScenePaths(), "Builds/123.apk", EditorUserBuildSettings.activeBuildTarget, BuildOptions.EnableHeadlessMode);
        }

        private void CheckPlatform()
        {

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
