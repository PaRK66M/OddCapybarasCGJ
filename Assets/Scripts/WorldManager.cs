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
    private ShaderManager shaderManager;
    [SerializeField]
    private float swapDuration;
    [SerializeField]
    private float swapSpeed;

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

        oldWorldObjects.SetActive(true);
        oldCounterImage.SetActive(true);
        newWorldObjects.SetActive(true);
        newCounterImage.SetActive(true);

        counterText.text = currentSwapAmount.ToString();

        shaderManager.SwapMaterials(swapDuration, swapSpeed);
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

    public void ApplySwap()
    {
        oldWorldObjects.SetActive(!isInNewWorld);
        oldCounterImage.SetActive(!isInNewWorld);
        newWorldObjects.SetActive(isInNewWorld);
        newCounterImage.SetActive(isInNewWorld);

        counterText.text = currentSwapAmount.ToString();
    }
}
