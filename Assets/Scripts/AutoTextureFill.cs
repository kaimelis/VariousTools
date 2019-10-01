#if UNITY_EDITOR
using Sirenix.OdinInspector;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class AutoTextureFill : MonoBehaviour
{
    private ParticleSystem particle;
    public Texture2D AnimatedSprite;

    [ButtonGroup]
    private void CleanSpriteList()
    {
        particle = GetComponent<ParticleSystem>();
        for (int i = 0; i <= particle.textureSheetAnimation.spriteCount; i++)
        {
            particle.textureSheetAnimation.RemoveSprite(i);
            if (particle.textureSheetAnimation.spriteCount == 0)
                break;
            else
                CleanSpriteList();
        }
    }

    [ButtonGroup]    
    private void FillSpriteList()
    {
        particle = GetComponent<ParticleSystem>();
        for (int i = 0; i < particle.textureSheetAnimation.spriteCount; i++)
        {
            particle.textureSheetAnimation.RemoveSprite(i);
        }
        
        string path = AssetDatabase.GetAssetPath(AnimatedSprite);
        string directory = Path.GetDirectoryName(path) + @"\";
        string[] aFilePaths = Directory.GetFiles(directory);

        foreach (var item in aFilePaths)
        {
            if (Path.GetExtension(item) == ".png" || Path.GetExtension(item) == ".jpg")
            {
                Sprite objAsset = AssetDatabase.LoadAssetAtPath(item, typeof(Sprite)) as Sprite;
                Object[] objects = AssetDatabase.LoadAllAssetRepresentationsAtPath(AssetDatabase.GetAssetPath(AnimatedSprite));
                Sprite[] sprites = objects.Select(x => (x as Sprite)).ToArray();
                Debug.Log("You have this many sprites :"  + sprites.Length);
                for (int i = 0; i < sprites.Length; i++)
                {
                    particle.textureSheetAnimation.AddSprite(sprites[i]);
                }
            }
        }
    }
}
#endif