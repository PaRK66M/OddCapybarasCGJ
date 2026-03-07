using UnityEngine;
using UnityEngine.InputSystem;

public class WorldManager : MonoBehaviour
{
    [SerializeField]
    private MaterialSwap swapScript;

    [SerializeField]
    private GameObject oldWorldObjects;
    [SerializeField]
    private GameObject newWorldObjects;

    [SerializeField]
    bool isInNewWorld = true;

    private void Start()
    {
        ApplySwap();
    }

    public void SwapWorld()
    {
        isInNewWorld = !isInNewWorld;

        ApplySwap();
    }

    public void SwapWorldInput(InputAction.CallbackContext context)
    {
        SwapWorld();
    }

    public void SetWorldToNewWorld()
    {
        isInNewWorld = true;
        ApplySwap();
    }

    public void SetWorldToOldWorld()
    {
        isInNewWorld = false;
        ApplySwap();
    }

    private void ApplySwap()
    {
        oldWorldObjects.SetActive(!isInNewWorld);
        newWorldObjects.SetActive(isInNewWorld);
        if (isInNewWorld)
        {
            swapScript.SwapToNew();
        }
        else
        {
            swapScript.SwapToOld();
        }
    }
}
