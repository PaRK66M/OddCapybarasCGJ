using System.Net;
using Unity.VisualScripting;
using UnityEngine;

public class DetectorManager : MonoBehaviour
{
    [SerializeField]
    GameObject detectionField;

    [SerializeField]
    GameObject player;

    [SerializeField]
    LayerMask blockingObjects;

    [SerializeField]
    GameObject[] patrolPoints;
    private int patrolIndex;

    private float range;

    private float detectionTime;
    [SerializeField]
    private float detectionLoseTime;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        range = detectionField.transform.localScale.x * transform.localScale.x * 0.5f;

        detectionTime = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if((player.transform.position - transform.position).magnitude > range)
        {
            PatrolUpdate();
            return;
        }

        if(Physics.Raycast(transform.position, (player.transform.position - transform.position).normalized, range, blockingObjects))
        {
            PatrolUpdate();
            return;
        }

        DetectedUpdate();
    }

    private void PatrolUpdate()
    {
        detectionTime = 0.0f;
    }

    private void DetectedUpdate()
    {
        detectionTime += Time.deltaTime;
        if (detectionTime > detectionLoseTime)
        {
            GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
            gameManager.OnGameLoss();
        }
    }

    private void OnDrawGizmos()
    {
        float gizmoRange = detectionField.transform.localScale.x * transform.localScale.x * 0.5f;
        // Set the color with custom alpha
        Gizmos.color = new Color(1f, 0f, 0f, 1f); // Yellow with custom alpha

        // Draw the line
        Gizmos.DrawLine(transform.position, transform.position + (player.transform.position - transform.position).normalized * gizmoRange);
    }
}
