using System.Runtime.InteropServices;
using UnityEngine;

public class EnemyContoller : MonoBehaviour
{
    [SerializeField] private Transform[] patrolPoints;
    [SerializeField] private float  _moveSpeed; 

    [Header("Audio")]
    [SerializeField] private GameObject sfxEnemy;

    private int patrolPointIndex = 0;
    private Transform currentPatrolPoint;
    private Rigidbody2D rb;
    private int direction = 0;
    private SpriteRenderer spriteRenderer;
    private GameObject soundObject;
   

    private void Awake() {
        rb = GetComponent<Rigidbody2D>(); 
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentPatrolPoint = patrolPoints[patrolPointIndex];  
    }

    void Update()
    {
        UpdateDirection();
        MoveTowardsPatrolPoint();
        if(Inviewport())
        {
            PlayEenemyMusic();
        }
        else
        {
            Destroy(soundObject);
        }
        
    }

    private bool Inviewport(){
        Vector3 viewportPosition = Camera.main.WorldToViewportPoint(transform.position);
        
        bool isInViewport = viewportPosition.x >= 0 && viewportPosition.x <= 1 &&
                             viewportPosition.y >= 0 && viewportPosition.y <= 1;
        return isInViewport;
    }
    private void PlayEenemyMusic(){
        if(soundObject == null)
        {
            soundObject = Instantiate(sfxEnemy, transform.position, Quaternion.identity); 
        }
    }

    private void UpdateDirection(){
        if(currentPatrolPoint.position.x > transform.position.x)
        {
            direction = 1;
        }
        else
        {
            direction = -1;
        }

        spriteRenderer.flipX = direction > 0 ? true : false;
    }

    private void MoveTowardsPatrolPoint(){
        
        rb.linearVelocity = new Vector2(direction * _moveSpeed, rb.linearVelocity.y);

        if(Vector3.Distance(transform.position,currentPatrolPoint.position) < 0.5f)
        {
            UpdatePatrolPoint();
        }
    }
    private void UpdatePatrolPoint(){
        patrolPointIndex++;

        if(patrolPointIndex >= patrolPoints.Length)
        {
            patrolPointIndex = 0;
        }
        currentPatrolPoint = patrolPoints[patrolPointIndex];   
    }
}
