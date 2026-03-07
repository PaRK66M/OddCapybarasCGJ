using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class WorldManager : MonoBehaviour
{
    [Header("Swap Mechanic")]
    [SerializeField]
    private int maxSwapAmount;

    private int currentSwapAmount;

    [Header("Material Swap")]
    [SerializeField]
    private MaterialSwap swapScript;

    [SerializeField]
    private GameObject oldWorldObjects;
    [SerializeField]
    private GameObject newWorldObjects;

    [SerializeField]
    bool isInNewWorld = true;

    [Header("UI Elements")]
    [SerializeField]
    private TMP_Text counterText;
    [SerializeField]
    private GameObject oldCounterImage;
    [SerializeField]
    private GameObject newCounterImage;

    private void Start()
    {
        currentSwapAmount = maxSwapAmount;
        ApplySwap();
    }

    public void SwapWorld()
    {
        if (currentSwapAmount <= 0)
        {
            FailedSwap();
            return;
        }

        isInNewWorld = !isInNewWorld;

        currentSwapAmount--;

        ApplySwap();
    }

    public void FailedSwap()
    {

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
        oldCounterImage.SetActive(!isInNewWorld);
        newWorldObjects.SetActive(isInNewWorld);
        newCounterImage.SetActive(isInNewWorld);
        if (isInNewWorld)
        {
            swapScript.SwapToNew();
        }
        else
        {
            swapScript.SwapToOld();
        }

        counterText.text = currentSwapAmount.ToString();
    }
}
