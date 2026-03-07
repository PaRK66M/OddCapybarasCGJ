using UnityEngine;

public class VictoryDetection : MonoBehaviour
{
    [SerializeField]
    GameManager gameManager;

    [SerializeField]
    GameObject treasure;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            treasure.SetActive(false);
            gameManager.OnGameWin();
        }
    }
}
