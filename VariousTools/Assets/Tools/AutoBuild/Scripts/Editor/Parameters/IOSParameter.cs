#if UNITY_EDITOR
using Sirenix.OdinInspector;
using UnityEditor;

namespace Custom.Tool.AutoBuild
{
    public class IOSParameter : Parameter
    {
        [BoxGroup("iOS")]
        public string BundleCode;

        [BoxGroup("iOS"), OnValueChanged("OnValueChange")]
        public bool SplashScreen;
    
        public IOSParameter()
        {
            SetSettings();
            ParameterManager.Instance.RegisterParameter(this);
        }

        public override void SetSettings()
        {
            base.SetSettings();
            BundleCode = VersionManager.Instance.GetBundleCode();
            SplashScreen = PlayerSettings.SplashScreen.show;
        }

        public override void PrepareSettings()
        {
            base.PrepareSettings();
            BundleCode = VersionManager.Instance.GetBundleCode();

        }

        private void OnValueChange()
        {
            PlayerSettings.SplashScreen.show = SplashScreen;
        }
    }
}
#endif
