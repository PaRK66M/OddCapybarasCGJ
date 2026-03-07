using System;
using UnityEngine;

[Serializable]
public struct MaterialSwapValues
{
    [SerializeField]
    public GameObject objectToChange;
    [SerializeField]
    public Material oldMaterial;
    [SerializeField]
    public Material newMaterial;
}

public class MaterialSwap : MonoBehaviour
{
    [SerializeField]
    private MaterialSwapValues[] listOfObjectsToChange;
    public void SwapToOld()
    {
        for (int i = 0; i < listOfObjectsToChange.Length; i++)
        {
            MeshRenderer[] renderersToSwap = listOfObjectsToChange[i].objectToChange.GetComponentsInChildren<MeshRenderer>();
            for (int j = 0; j < renderersToSwap.Length; j++)
            {
                renderersToSwap[j].material = listOfObjectsToChange[i].oldMaterial;
            }
        }
    }

    public void SwapToNew()
    {
        for (int i = 0; i < listOfObjectsToChange.Length; i++)
        {
            MeshRenderer[] renderersToSwap = listOfObjectsToChange[i].objectToChange.GetComponentsInChildren<MeshRenderer>();
            for (int j = 0; j < renderersToSwap.Length; j++)
            {
                renderersToSwap[j].material = listOfObjectsToChange[i].newMaterial;
            }
        }
    }
}
