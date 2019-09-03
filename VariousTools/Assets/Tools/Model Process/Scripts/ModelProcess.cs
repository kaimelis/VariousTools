#if UNITY_EDITOR
using UnityEngine;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using Sirenix.Utilities.Editor;
using UnityEditor;
using System.IO;
using Sirenix.OdinInspector.Editor;
using System.Collections;
using System.Linq;

namespace Custom.Tool.ModelProcess
{
    public class ModelProcess : OdinEditorWindow
    {
        [MenuItem("KoEditor/Tools/Model Process", priority = 0)]
        public static void ShowWindow()
        {
            GetWindow<ModelProcess>().Show();
        }

        [TabGroup("MAIN", "MODELS")]
        [AssetList(CustomFilterMethod = "IsModel", Path = "/Models/")]
        [InlineEditor(InlineEditorModes.SmallPreview)]
        public List<Object> Models = new List<Object>();

        private bool IsModel(Object obj)
        {
            return PrefabUtility.GetPrefabAssetType(obj) == PrefabAssetType.Model;
        }

        [TabGroup("MAIN", "MODELS")]
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
            string folderPath = Path.GetDirectoryName(assetPath) + @"\";

            string fileNmae = Path.GetFileNameWithoutExtension(assetPath);
            string destinyPath = folderPath + fileNmae + ".prefab";
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
                GameObjectUtility.SetStaticEditorFlags(prefab, StaticEditorFlags.BatchingStatic | StaticEditorFlags.NavigationStatic | StaticEditorFlags.OccludeeStatic | StaticEditorFlags.OccluderStatic | StaticEditorFlags.OffMeshLinkGeneration | StaticEditorFlags.ReflectionProbeStatic);
                Debug.Log("Created prefab at " + destinyPath);
            }
            AssetDatabase.Refresh();
        }

        [TabGroup("MAIN", "MODELS")]
        [AssetList(Path = "/Models/")]
        [InlineEditor(InlineEditorModes.SmallPreview)]
        public List<GameObject> Prefabs = new List<GameObject>();

        private bool IsPrefab(Object obj)
        {
            return PrefabUtility.GetPrefabAssetType(obj) == PrefabAssetType.Regular;
        }

        [TabGroup("MAIN", "MODELS")]
        [Button("Assign materials to all selected models")]
        public void AssignMaterials()
        {
            foreach (GameObject m in Prefabs)
            {
                ProcessPrefabs(m);
            }
        }

        private void ProcessPrefabs(GameObject prefab)
        {
            string prefabName = prefab.name;
            string assetPath = AssetDatabase.GetAssetPath(prefab);
            string folderPath = Path.GetDirectoryName(assetPath) + @"\";
            string[] aFilePaths = Directory.GetFiles(folderPath);
            Material mat = null;
            foreach (var item in aFilePaths)
            {
                if (Path.GetExtension(item) == ".mat")
                {
                    Material objAsset = AssetDatabase.LoadAssetAtPath(item, typeof(Material)) as Material;
                    mat = objAsset;
                }
            }

            var meshRenderer = prefab.GetComponentsInChildren<Renderer>();
            foreach (var item in meshRenderer)
            {
                if (mat != null)
                    item.sharedMaterial = mat;
                Debug.Log(item.sharedMaterial.name);
            }
        }

        [TabGroup("MAIN", "MATERIALS")]
        [AssetList(Path = "Models/")]
        [InlineEditor(InlineEditorModes.SmallPreview)]
        public List<Material> Materials;

        [TabGroup("MAIN", "MATERIALS")]
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
            try
            {
                AssetDatabase.StartAssetEditing();
                string assetPath = AssetDatabase.GetAssetPath(m);
                string directory = Path.GetDirectoryName(assetPath) + @"\";

                string[] aFilePaths = Directory.GetFiles(directory);
                foreach (var item in aFilePaths)
                {
                    if (Path.GetExtension(item) == ".png" || Path.GetExtension(item) == ".jpg")
                    {
                        Texture2D objAsset = AssetDatabase.LoadAssetAtPath(item, typeof(Texture2D)) as Texture2D;
                        if (m.name + "D" == objAsset.name)
                        {
                            m.SetTexture("_MainTex", objAsset);
                            Debug.Log(objAsset.name);
                        }
                        if (m.name + "AO" == objAsset.name)
                        {
                            m.SetTexture("_OcclusionMap", objAsset);
                            Debug.Log(objAsset.name);
                        }
                        if (m.name + "M" == objAsset.name)
                        {
                            m.SetTexture("_MetallicGlossMap", objAsset);
                            Debug.Log(objAsset.name);
                        }
                        if (m.name + "N" == objAsset.name)
                        {
                            m.SetTexture("_BumpMap", objAsset);
                            Debug.Log(objAsset.name);
                        }
                    }
                }

            }
            finally
            {
                AssetDatabase.StopAssetEditing();
                AssetDatabase.SaveAssets();
            }
        }
    }
}
#endif