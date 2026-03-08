using UnityEngine;

public class HideNote : MonoBehaviour
{

    [SerializeField]
    PlayerInput playerInput;

    public void HideThisObject()
    {
        Cursor.lockState = 
            CursorLockMode.Locked;

        Cursor.visible = 
            false;

        playerInput.EnableInput();

        gameObject.SetActive(false);
    }
}
