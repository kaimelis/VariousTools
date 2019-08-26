#if UNITY_EDITOR
using Sirenix.OdinInspector;

namespace Custom.Tool.AutoBuild
{
    public class IOSParameter : Parameter
    {
        [BoxGroup("iOS")]
        public string BundleCode;
        public IOSParameter()
        {
            SetSettings();
            ParameterManager.Instance.RegisterParameter(this);
        }

        public override void SetSettings()
        {
            base.SetSettings();
            BundleCode = VersionManager.Instance.GetBundleCode();
        }

        public override void PrepareSettings()
        {
            base.PrepareSettings();
            BundleCode = VersionManager.Instance.GetBundleCode();

        }
    }
}
#endif
