using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CreateCubes : MonoBehaviour
{
    public int worldWidth = 50;
    public int worldHeight = 50;
    public GameObject block1;
    public Material blue, yellow, red;
    public bool UseRandomColor = true;

    [Button]
    public void CleanChildren()
    {
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            DestroyImmediate(gameObject.transform.GetChild(i).gameObject);
        }
    }

    [ButtonGroup]
    public void CreateOnGrid()
    {
        for (int x = 0; x < worldWidth; x++)
        {
            for (int z = 0; z < worldHeight; z++)
            {
                GameObject block = Instantiate(block1, Vector3.zero, block1.transform.rotation) as GameObject;
                block.transform.parent = transform;
                block.transform.localPosition = new Vector3(x, 0, z);

                if (UseRandomColor)
                {
                    int randomRange = Random.Range(0, 3);
                    bool boolBlue = (randomRange == 0);
                    bool boolRed = (randomRange == 1);
                    bool boolYellow = (randomRange == 2);
                    if (boolBlue)
                    {
                        block.GetComponent<Renderer>().material = blue;

                    }
                    else if(boolRed)
                    {
                        block.GetComponent<Renderer>().material = red;
                    }
                    else if(boolYellow)
                    {
                        block.GetComponent<Renderer>().material = yellow;
                    }
                }
            }
        }
    }

    [ButtonGroup]
    public void CreateRandomGrid()
    {
        for (int x = 0; x < worldWidth; x++)
        {
            for (int z = 0; z < worldHeight; z++)
            {
                bool Boolean = (Random.Range(0, 2) == 0);
                if (Boolean)
                {
                    GameObject block = Instantiate(block1, Vector3.zero, block1.transform.rotation) as GameObject;
                    block.transform.parent = transform;
                    block.transform.localPosition = new Vector3(x, 0, z);
                    if (UseRandomColor)
                    {
                        int randomRange = Random.Range(0, 3);
                        bool boolBlue = (randomRange == 0);
                        bool boolRed = (randomRange == 1);
                        bool boolYellow = (randomRange == 2);
                        if (boolBlue)
                        {
                            block.GetComponent<Renderer>().material = blue;

                        }
                        else if (boolRed)
                        {
                            block.GetComponent<Renderer>().material = red;
                        }
                        else if (boolYellow)
                        {
                            block.GetComponent<Renderer>().material = yellow;
                        }
                    }
                }
            }
        }
    }
}
