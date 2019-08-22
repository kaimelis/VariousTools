#if UNITY_EDITOR
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;
using Custom.Tool.AutoBuild;

namespace Custom.Tool
{
    public class UpgradeVersionPopWindow : OdinEditorWindow
    {
        private static UpgradeVersionPopWindow _window;
        private string text;
        public static void OpenWindow()
        {
            _window = GetWindow<UpgradeVersionPopWindow>();
            _window.position = GUIHelper.GetEditorWindowRect().AlignCenter(270, 200);
            _window.titleContent = new GUIContent("Pop Up window", EditorIcons.RulerRect.Active);
            _window.Show();
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            text = VersionManager.Instance.GetVersion();
        }

        [OnInspectorGUI]
        private void CreateLabel()
        {
            EditorGUILayout.LabelField("What is the next version?", SirenixGUIStyles.BoldLabelCentered);
            EditorGUILayout.LabelField("Current version : " + text, SirenixGUIStyles.CenteredBlackMiniLabel);
            EditorGUILayout.LabelField("Suggested version : " + text, SirenixGUIStyles.CenteredGreyMiniLabel);
            GUILayout.Space(30);
            text = EditorGUILayout.TextField(text, SirenixGUIStyles.CenteredTextField);
            
            GUILayout.Space(10);
        }

        [ButtonGroup("Group"), LabelText("Save"), GUIColor(0, 1, 0)]
        private void Save()
        {
            VersionManager.Instance.SetVersion(text);
            GetWindow<UpgradeVersionPopWindow>().Close();
        }
        [ButtonGroup("Group"), LabelText("Cancel"), GUIColor(1, 0, 0)]
        private void Cancel()
        {
           // UnityEngine.Debug.LogError("<b><color=red> File not created because it was canceled. </color></b>");
            GetWindow<UpgradeVersionPopWindow>().Close();
        }
    }
}
#endif
