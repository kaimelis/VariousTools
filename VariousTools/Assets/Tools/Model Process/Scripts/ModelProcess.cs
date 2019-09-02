#if UNITY_EDITOR
using UnityEngine;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using Sirenix.Utilities.Editor;
using UnityEditor;
using System.IO;

namespace Custom.Tool.ModelProcess
{
    public class ModelProcess : MonoBehaviour
    {
        [FolderPath(RequireExistingPath = true)]
        public string pathPropPrefabs = "Assets/Prefabs/";

        [AssetList(CustomFilterMethod = "IsModel")]
        [InlineEditor(InlineEditorModes.SmallPreview)]
        public List<Object> Models;

        private bool IsModel(Object obj)
        {
            return PrefabUtility.GetPrefabAssetType(obj) == PrefabAssetType.Model;
        }

        [Button("FBX->Prefab - All Selected Models")]
        public void ProcessModels()
        {
            foreach (GameObject m in Models)
            {
                ProcessModel(m);
            }
        }

        private void ProcessModel(GameObject m)
        {
            string assetPath = AssetDatabase.GetAssetPath(m);
            string folderPath = Path.GetDirectoryName(assetPath);
            string desetinyPathRoot = pathPropPrefabs;

            if (string.IsNullOrEmpty(desetinyPathRoot)) desetinyPathRoot = folderPath;

            string fileNmae = Path.GetFileNameWithoutExtension(assetPath);
            string destinyPath = desetinyPathRoot  + fileNmae + ".prefab";

            if (AssetDatabase.LoadAssetAtPath(destinyPath, typeof(GameObject)))
            {
                if (EditorUtility.DisplayDialog("Overwrite Prefab",
                    "A prefab " + fileNmae + " already exists at " + destinyPath + ".\nDo you want to overwite it?",
                    "Yes",
                    "No"))
                {
                    GameObject existingPrefab = AssetDatabase.LoadAssetAtPath(destinyPath, typeof(GameObject)) as GameObject;
                    GameObject fbx = AssetDatabase.LoadAssetAtPath(assetPath, typeof(GameObject)) as GameObject;
                    // GameObject prefab = PrefabUtility.ReplacePrefab(fbx, existingPrefab, ReplacePrefabOptions.ConnectToPrefab);
                    GameObject prefab = PrefabUtility.SaveAsPrefabAssetAndConnect(fbx, destinyPath, InteractionMode.AutomatedAction);
                    //prefab.RemoveComponentIfExists<Animator>();
                    GameObjectUtility.SetStaticEditorFlags(prefab, StaticEditorFlags.BatchingStatic | StaticEditorFlags.NavigationStatic | StaticEditorFlags.OccludeeStatic | StaticEditorFlags.OccluderStatic | StaticEditorFlags.OffMeshLinkGeneration | StaticEditorFlags.ReflectionProbeStatic);
                    Debug.Log("Overwritten prefab at " + destinyPath);
                }
            }
            else
            {
                GameObject go = AssetDatabase.LoadAssetAtPath(assetPath, typeof(GameObject)) as GameObject;

                GameObject objSource = (GameObject)PrefabUtility.InstantiatePrefab(go);
                GameObject prefab = PrefabUtility.SaveAsPrefabAsset(objSource, destinyPath);
                DestroyImmediate(objSource);
               // GameObject prefab = PrefabUtility.SaveAsPrefabAsset(go,destinyPath);

                GameObjectUtility.SetStaticEditorFlags(prefab, StaticEditorFlags.BatchingStatic | StaticEditorFlags.NavigationStatic | StaticEditorFlags.OccludeeStatic | StaticEditorFlags.OccluderStatic | StaticEditorFlags.OffMeshLinkGeneration | StaticEditorFlags.ReflectionProbeStatic);
                Debug.Log("Created prefab at " + destinyPath);
            }
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        [AssetList]
        [InlineEditor(InlineEditorModes.SmallPreview)]
        public List<Material> Materials;

        [Button("Materials Prepare - All Selected Materials")]
        public void ProcessMaterials()
        {
            foreach (Material m in Materials)
            {
                ProcessMaterial(m);
            }
        }

        private void ProcessMaterial(Material m)
        {
            string assetPath = AssetDatabase.GetAssetPath(m);
            string folderPath = Path.GetDirectoryName(assetPath);
            Object[] textures = AssetDatabase.LoadAllAssetsAtPath(folderPath);
            foreach (var item in textures)
            {
                Debug.Log(item.name);
            }
        }
    }
}
#endif