using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyFOV : MonoBehaviour
{
    [SerializeField]public float viewRadius = 10f;
    [SerializeField] private GameObject DieScene;
    [Range(0, 360)]
    public float viewAngle = 120f;       // Vision angle
    public LayerMask playerMask;         // Player layer
    public LayerMask obstacleMask;       // Obstacles like walls
    
    public bool CanSeePlayer { get; private set; } // Visibility check

    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(FOVCheck());
        StartCoroutine(RotateEnemy());
    }

    IEnumerator FOVCheck()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.2f);
            CheckPlayerInView();
        }
    }
    
    void CheckPlayerInView()
    {
        CanSeePlayer = false;
        Collider[] targetsInView = Physics.OverlapSphere(transform.position, viewRadius, playerMask);
        foreach (Collider target in targetsInView)
        {
            
            Transform targetTransform = target.transform;
            Vector3 directionToTarget = (targetTransform.position - transform.position).normalized;
            
            // Check if the player is within the enemy's FOV angle
            if (Vector3.Angle(-transform.forward, directionToTarget) < viewAngle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, targetTransform.position);
                
                // Check if there are obstacles between enemy and player
                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstacleMask))
                {
                    CanSeePlayer = true;
                    GameOver();
                    break; // Stop checking other colliders once the player is found
                }
            }
        }
    }
    IEnumerator RotateEnemy()
    {
        float rotationSpeed = 1.5f; 
        float angleLimit = 40f;     

        while (true)
        {
            
            yield return StartCoroutine(RotateToAngle(-angleLimit, rotationSpeed));
            yield return new WaitForSeconds(0.7f);
            yield return StartCoroutine(RotateToAngle(angleLimit, rotationSpeed));
            yield return new WaitForSeconds(0.7f);
        }
    }

    IEnumerator RotateToAngle(float targetAngle, float duration)
    {
        Quaternion startRotation = transform.rotation;
        Quaternion targetRotation = Quaternion.Euler(0, targetAngle, 0);

        float time = 0f;
        while (time < duration)
        {
            transform.rotation = Quaternion.Slerp(startRotation, targetRotation, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        transform.rotation = targetRotation;
        
    }
    // ** Debugging: Visualizing the FOV in Scene View **
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, viewRadius);

        Vector3 viewAngleA = DirFromAngle(-viewAngle / 2);
        Vector3 viewAngleB = DirFromAngle(viewAngle / 2);

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + viewAngleA * viewRadius);
        Gizmos.DrawLine(transform.position, transform.position + viewAngleB * viewRadius);
    }

    private Vector3 DirFromAngle(float angleInDegrees)
    {
        return Quaternion.Euler(0, angleInDegrees, 0) * -transform.forward;
    }

    public void GameOver()
    {
        DieScene.SetActive(true);
        Time.timeScale = 0;
        
        
    }

    public void LoadSceneMode()
    {
        DieScene.SetActive(false);
        SceneManager.LoadScene("Level 3");
        Time.timeScale = 1;
    }
}
