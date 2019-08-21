#if UNITY_EDITOR
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Custom.Tool.AutoBuild
{
    public class AutoBuildEditorWindow : OdinMenuEditorWindow
    {
        /// <summary>
        /// 
        /// </summary>
        private static AutoBuildEditorWindow _window;

        /// <summary>
        /// Method that gets called by Menu and created main window
        /// </summary>
        [MenuItem("Custom/AutoBuild")]
        private static void OpenEditorWindow()
        {
            _window = GetWindow<AutoBuildEditorWindow>();
            _window.Show();
            _window.position = GUIHelper.GetEditorWindowRect().AlignCenter(800, 500);
            _window.titleContent = new GUIContent("Automatic Build Tool");
        }

        /// <summary>
        /// Method that created a tree for editor window that is just a list.
        /// </summary>
        /// <returns>returns a OdingMneuTree that is created in this window</returns>
        protected override OdinMenuTree BuildMenuTree()
        {
            OdinMenuTree tree = new OdinMenuTree(true);
            tree.DefaultMenuStyle.IconSize = 28.00f;
            tree.Config.DrawSearchToolbar = true;
            tree.DefaultMenuStyle = OdinMenuStyle.TreeViewStyle;
            tree.Add("Menu Style", tree.DefaultMenuStyle);

            tree.Add("Android Parameters", new AndroidParameter());
            tree.EnumerateTree().AddThumbnailIcons();
            return tree;
        }

        /// <summary>
        /// 
        /// </summary>
        protected override void OnBeginDrawEditors()
        {
            base.OnBeginDrawEditors();
            OdinMenuItem selected = this.MenuTree.Selection.FirstOrDefault();
            int toolbarHeight = this.MenuTree.Config.SearchToolbarHeight;
            SirenixEditorGUI.BeginHorizontalToolbar(toolbarHeight);
            {
                if (selected != null)
                    GUILayout.Label(selected.Name);

                if (SirenixEditorGUI.ToolbarButton(new GUIContent("Make a build", "Button used to make a build for current selected platform")))
                {
                    _window = GetWindow<AutoBuildEditorWindow>();
                    _window.Close();
                    BuildManager.Instance.Build();
                   // ManageBuild.Instance.MakeBuild();
                }
            }
            SirenixEditorGUI.EndHorizontalToolbar();
        }




    }
}
#endif
