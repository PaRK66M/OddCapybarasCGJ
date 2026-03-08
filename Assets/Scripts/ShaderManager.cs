using System;
using System.Collections;
using System.Net;
using UnityEngine;

public class ShaderManager : MonoBehaviour
{
    // If I had time I would make this a data object that is shared with World Manager
    // but I'm doing something quick so make sure these two are the SAME!!!
    [SerializeField]
    bool isInNewWorld = true;
    [SerializeField]
    WorldManager worldManager;

    [SerializeField]
    private Transform playerPosition;

    [SerializeField]
    private Material[] swappingMaterials;
    [SerializeField]
    private Material[] oldWorldExclusiveMaterials;
    [SerializeField]
    private Material[] newWorldExclusiveMaterials;

    [SerializeField]
    private float blendDistance; // The distance that will blend the textures together
    [SerializeField]
    private float worldSize; // How far the shader should be applying so it affects all the objects


    private float blendMinimum;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // The inside world is the old world
        // The outside world is the new world

        blendMinimum = isInNewWorld ?
            -blendDistance :
            worldSize;

        UpdatePlayerPositionInMaterials();

        for (int i = 0; i < swappingMaterials.Length; i++)
        {
            swappingMaterials[i].SetFloat("_BlendMin", blendMinimum);
            swappingMaterials[i].SetFloat("_BlendMax", blendMinimum + blendDistance);
        }

        for (int i = 0; i < oldWorldExclusiveMaterials.Length; i++)
        {
            oldWorldExclusiveMaterials[i].SetFloat("_BlendMin", blendMinimum);
            oldWorldExclusiveMaterials[i].SetFloat("_BlendMax", blendMinimum + blendDistance);
            oldWorldExclusiveMaterials[i].SetInt("_isOutsideTexture", 0);
        }

        for (int i = 0; i < newWorldExclusiveMaterials.Length; i++)
        {
            newWorldExclusiveMaterials[i].SetFloat("_BlendMin", blendMinimum);
            newWorldExclusiveMaterials[i].SetFloat("_BlendMax", blendMinimum + blendDistance);
            newWorldExclusiveMaterials[i].SetInt("_isOutsideTexture", 1);
        }

    }

    public void UpdatePlayerPositionInMaterials()
    {
        for (int i = 0; i < swappingMaterials.Length; i++)
        {
            swappingMaterials[i].SetVector("_PlayerPosition", playerPosition.position);
        }

        for (int i = 0; i < oldWorldExclusiveMaterials.Length; i++)
        {
            oldWorldExclusiveMaterials[i].SetVector("_PlayerPosition", playerPosition.position);
        }

        for (int i = 0; i < newWorldExclusiveMaterials.Length; i++)
        {
            newWorldExclusiveMaterials[i].SetVector("_PlayerPosition", playerPosition.position);
        }
    }

    public void SwapMaterials(float duration, float speed)
    {
        isInNewWorld = !isInNewWorld;

        UpdatePlayerPositionInMaterials();
        StartCoroutine(SwapMaterialsEffect(duration, speed));
    }

    // We have a duration and speed as to cover the whole world would take too long
    // The duration will state how long the effect will last
    // The speed is how fast the transition range travels
    IEnumerator SwapMaterialsEffect(float duration, float speed)
    {
        float currentDuration = 0.0f;

        if (isInNewWorld)
        {
            blendMinimum = (speed * duration) - blendDistance;
        }

        while (currentDuration < duration)
        {
            blendMinimum += 
                speed * Time.deltaTime
                * (isInNewWorld ?
                    -1.0f :
                    1.0f);

            for (int i = 0; i < swappingMaterials.Length; i++)
            {
                swappingMaterials[i].SetFloat("_BlendMin", blendMinimum);
                swappingMaterials[i].SetFloat("_BlendMax", blendMinimum + blendDistance);
            }

            for (int i = 0; i < oldWorldExclusiveMaterials.Length; i++)
            {
                oldWorldExclusiveMaterials[i].SetFloat("_BlendMin", blendMinimum);
                oldWorldExclusiveMaterials[i].SetFloat("_BlendMax", blendMinimum + blendDistance);
            }

            for (int i = 0; i < newWorldExclusiveMaterials.Length; i++)
            {
                newWorldExclusiveMaterials[i].SetFloat("_BlendMin", blendMinimum);
                newWorldExclusiveMaterials[i].SetFloat("_BlendMax", blendMinimum + blendDistance);
            }

            currentDuration += Time.deltaTime;

            yield return null;
        }

        blendMinimum = isInNewWorld ?
            -blendDistance :
            worldSize;

        for (int i = 0; i < swappingMaterials.Length; i++)
        {
            swappingMaterials[i].SetFloat("_BlendMin", blendMinimum);
            swappingMaterials[i].SetFloat("_BlendMax", blendMinimum + blendDistance);
        }

        for (int i = 0; i < oldWorldExclusiveMaterials.Length; i++)
        {
            oldWorldExclusiveMaterials[i].SetFloat("_BlendMin", blendMinimum);
            oldWorldExclusiveMaterials[i].SetFloat("_BlendMax", blendMinimum + blendDistance);
        }

        for (int i = 0; i < newWorldExclusiveMaterials.Length; i++)
        {
            newWorldExclusiveMaterials[i].SetFloat("_BlendMin", blendMinimum);
            newWorldExclusiveMaterials[i].SetFloat("_BlendMax", blendMinimum + blendDistance);
        }


        Debug.Log(blendMinimum);

        worldManager.ApplySwap();
    }
}
