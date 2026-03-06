using UnityEngine;
using UnityEngine.InputSystem;

public class WorldManager : MonoBehaviour
{
    [SerializeField]
    private GameObject oldWorldObjects;
    [SerializeField]
    private GameObject newWorldObjects;

    [SerializeField]
    bool isInNewWorld = true;

    private void Start()
    {
        oldWorldObjects.SetActive(!isInNewWorld);
        newWorldObjects.SetActive(isInNewWorld);
    }

    public void SwapWorld()
    {
        isInNewWorld = !isInNewWorld;
        oldWorldObjects.SetActive(!isInNewWorld);
        newWorldObjects.SetActive(isInNewWorld);
    }

    public void SwapWorldInput(InputAction.CallbackContext context)
    {
        SwapWorld();
    }

    public void SetWorldToNewWorld()
    {
        isInNewWorld = true;
        oldWorldObjects.SetActive(!isInNewWorld);
        newWorldObjects.SetActive(isInNewWorld);
    }

    public void SetWorldToOldWorld()
    {
        isInNewWorld = false;
        oldWorldObjects.SetActive(!isInNewWorld);
        newWorldObjects.SetActive(isInNewWorld);
    }
}
