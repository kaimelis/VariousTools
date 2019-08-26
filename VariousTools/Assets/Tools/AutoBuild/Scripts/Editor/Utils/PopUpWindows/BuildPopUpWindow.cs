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
    public class BuildPopUpWindow : OdinEditorWindow
    {
        private static BuildPopUpWindow _window;

        public static void OpenWindow()
        {
            _window = GetWindow<BuildPopUpWindow>();
            _window.position = GUIHelper.GetEditorWindowRect().AlignCenter(600, 200);
            _window.titleContent = new GUIContent("Build popup window", EditorIcons.RulerRect.Active);
            _window.Show();
        }

        [OnInspectorGUI]
        private void GUI()
        {
            GUILayout.Space(10);
            EditorGUILayout.LabelField("Create new build.Ready?", SirenixGUIStyles.BoldLabelCentered);
            EditorGUILayout.LabelField("Build will be created in: " + BuildManager.Instance.GetBuildPath(), SirenixGUIStyles.CenteredWhiteMiniLabel);
            EditorGUILayout.LabelField("Build name will be:  " + BuildManager.Instance.GetBuildName(), SirenixGUIStyles.CenteredWhiteMiniLabel);
            EditorGUILayout.LabelField("Build is being made for:  " + EditorUserBuildSettings.activeBuildTarget.ToString(), SirenixGUIStyles.CenteredWhiteMiniLabel);
            GUILayout.Space(5);
        }

        
        [ButtonGroup("Group"), LabelText("Create"), GUIColor(0, 1, 0)]
        private void Create()
        {
            GetWindow<BuildPopUpWindow>().Close();
            BuildManager.Instance.MakeABuild();
        }

        [ButtonGroup("Group"), LabelText("Cancel"), GUIColor(0.9f, 0.2f, 0)]
        private void Cancel()
        {
            GetWindow<BuildPopUpWindow>().Close();
        }

    }
}
#endif
