#if UNITY_EDITOR
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Custom.Tool.AutoBuild
{ 
    public class AndroidParameter : Parameter
    {
        [BoxGroup("Android")]
        public int BundleVersionCode;

        [BoxGroup("Android")]
        public bool SplitApplicationBinary;

        [BoxGroup("Android")]
        public string PackageName;

        [BoxGroup("Android")]
        public AndroidBuildSystem BuildSystem;

        [BoxGroup("Android")]
        [OdinSerialize]
        public AndroidArchitecture AndroidArchitecture;

        [BoxGroup("Android")]
        [OnValueChanged("OnValueChange")]
        public ScriptingImplementation ScriptingBackend;


        [BoxGroup("Android/Develop")]
        [OnValueChanged("OnValueChange")]
        public bool DevelopmentBuild;
        [BoxGroup("Android/Develop"), ShowIf("DevelopmentBuild")]
        public bool AutoconnectProfiler;
        [BoxGroup("Android/Develop"), ShowIf("DevelopmentBuild")]
        public bool ScriptDebugging;
        [BoxGroup("Android/Develop"), ShowIf("DevelopmentBuild")]
        public bool ScriptsOnlyBuild;

        public AndroidParameter ()
        {
            OnEnable();
        }

        public override void OnEnable()
        {
            base.OnEnable();

            BundleVersionCode = PlayerSettings.Android.bundleVersionCode;
            PackageName = PlayerSettings.GetApplicationIdentifier(BuildTargetGroup.Android);
            ScriptingBackend = PlayerSettings.GetScriptingBackend(BuildTargetGroup.Android);

            BuildSystem = EditorUserBuildSettings.androidBuildSystem;
            AndroidArchitecture = PlayerSettings.Android.targetArchitectures;
            DevelopmentBuild = EditorUserBuildSettings.development;
        }

        public override void OnPrepare()
        {
            base.OnPrepare();
            PlayerSettings.Android.bundleVersionCode = BundleVersionCode;
            //PackageName = PlayerSettings.GetApplicationIdentifier(BuildTargetGroup.Android);
            //ScriptingBackend = PlayerSettings.GetScriptingBackend(BuildTargetGroup.Android);

            EditorUserBuildSettings.androidBuildSystem = BuildSystem;
            PlayerSettings.Android.targetArchitectures = AndroidArchitecture;
            EditorUserBuildSettings.development = DevelopmentBuild;
        }

        protected override void OnValueChange()
        {
            OnPrepare();
            if(DevelopmentBuild)
            {
                SplitApplicationBinary = false;
            }
            else
            {
                AutoconnectProfiler = false;
                ScriptDebugging = false;
                ScriptsOnlyBuild = false;
            }
        }
    }
}
#endif
