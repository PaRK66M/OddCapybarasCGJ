using UnityEngine;

public class EntranceCollider : MonoBehaviour
{
    [SerializeField]
    private GameManager gameManager;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            gameManager.OnGameWin();
        }
    }
}
