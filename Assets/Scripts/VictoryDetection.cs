using UnityEngine;

public class VictoryDetection : MonoBehaviour
{
    [SerializeField]
    GameManager gameManager;

    [SerializeField]
    GameObject treasure;

    [SerializeField]
    GameObject textObject;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            treasure.SetActive(false);
            gameManager.GainKey();
        }
    }

    private void DisplayKeyGained()
    {
        textObject.SetActive(true);
        Invoke("HideKey", 4);
    }

    private void HideKey()
    {
        textObject.SetActive(false);
    }
}
