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
    public class PasswordPopUpWindow : OdinEditorWindow
    {
        private string _keystorePassword;
        private string _keyAliasPassword;
        public static void OpenWindow()
        {
            PasswordPopUpWindow _window = GetWindow<PasswordPopUpWindow>();
            _window.position = GUIHelper.GetEditorWindowRect().AlignCenter(400, 200);
            _window.titleContent = new GUIContent("Pop Up window", EditorIcons.CharGraph.Active);
            _window.Show();
        }

        [OnInspectorGUI]
        private void CreateLabel()
        {
            EditorGUILayout.LabelField("Type in passwords", SirenixGUIStyles.BoldLabelCentered);
            GUILayout.Space(30);
            _keystorePassword = EditorGUILayout.PasswordField("Key Store Password",_keystorePassword);
            EditorGUILayout.LabelField(_keystorePassword, SirenixGUIStyles.CenteredGreyMiniLabel);
            _keyAliasPassword = EditorGUILayout.PasswordField("Key Alias Password", _keyAliasPassword);
            EditorGUILayout.LabelField(_keyAliasPassword, SirenixGUIStyles.CenteredGreyMiniLabel);
            GUILayout.Space(10);
        }

        [ButtonGroup("Group"), LabelText("Save"), GUIColor(0, 1, 0)]
        private void Save()
        {
            PasswordManager.SavePassword(_keystorePassword, "KEYSTORE_PASSWORD");
            PasswordManager.SavePassword(_keyAliasPassword, "ALIAS_PASSWORD");
            ParameterManager.Instance.UpdateAndroidPasswords(_keystorePassword, _keyAliasPassword);
            _keyAliasPassword = "";
            _keystorePassword = "";           
            GetWindow<PasswordPopUpWindow>().Close();
        }
        [ButtonGroup("Group"), LabelText("Cancel"), GUIColor(1, 0, 0)]
        private void Cancel()
        {
            _keyAliasPassword = "";
            _keystorePassword = "";
            GetWindow<PasswordPopUpWindow>().Close();
            GetWindow<AutoBuildEditorWindow>().Close();
        }
    }
}
#endif