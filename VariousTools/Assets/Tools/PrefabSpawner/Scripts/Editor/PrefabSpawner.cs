#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.IO;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using System.Collections.Generic;
public class PrefabSpawner : OdinEditorWindow
{
    [MenuItem("KoEditor/Tools/PrefabSpawner", priority = 0)]
    public static void ShowWindow()
    {
        GetWindow<PrefabSpawner>().Show();
    }

    public static PrefabSpawner Instance()
    {
        return GetWindow<PrefabSpawner>();
    }

   [AssetList(CustomFilterMethod = "IsPrefab", AssetNamePrefix = "prefab")]
   [InlineEditor(InlineEditorModes.SmallPreview)]
    public List<GameObject> Prefabs;

    private bool IsPrefab(GameObject obj)
    {
        return PrefabUtility.GetPrefabAssetType(obj) == PrefabAssetType.Regular;
    }

}

#endif
