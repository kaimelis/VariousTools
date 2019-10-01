#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;

public class KoEditor : OdinEditorWindow
{
    [MenuItem("KoEditor/Editor", priority = 0)]
    public static void ShowWindow()
    {
        GetWindow<KoEditor>().Show();
    }

    public static KoEditor Instance()
    {
        return GetWindow<KoEditor>();
    }

    [HideInEditorMode]
    [InfoBox("Warning: Application is playing.", InfoMessageType.Warning)]
    [ReadOnly]
    private readonly bool warningAppIsPlaying;


    [TabGroup("MAIN", "SCENE")]
    [InfoBox("Updates the GameView camera to match SceneView camera Only works if all CineMachine cameras are disabled.")]
    public bool syncEditorCam = false;

    private void Update()
    {
        if (syncEditorCam)
        {
            Camera mainCamera = Camera.main;
            Camera currentCamera = Camera.current;
            if (mainCamera != null && currentCamera != null)
            {
                mainCamera.transform.position = currentCamera.transform.position;
                mainCamera.transform.rotation = currentCamera.transform.rotation;
            }
        }
    }
}

#endif
