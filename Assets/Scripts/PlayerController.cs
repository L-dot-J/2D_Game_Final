using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;


namespace Lana
{
    public class PlayerController : MonoBehaviour
    {
        public UnityEvent onLandEvent;
        public enum CharachterState{Idle, Walk, Jump}
        private Transform _spawnPoint;
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _jumpForce;
        [SerializeField] private BoxCollider2D _boxCollider; 
        [SerializeField] private LayerMask _groundLayer;
        [SerializeField] private LayerMask _dieLayer;
        [SerializeField] private LayerMask _collidableLayers;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        private Rigidbody2D rb;
        private Animator animator;
        private CharachterState charachterState;
        private int potionCollected = 0;
        public Action<int>OnPotionCollected;
        // public int maxHealth = 4;
        // public int currentHealth;
        // public HUD healthBar;

        void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            // currentHealth = maxHealth;
            // healthBar.SetMaxHealth(maxHealth);

        }

        void Update()
        {
    
            if(IsCollidingWith(_dieLayer))
            {
                transform.position = _spawnPoint.position;
                // TakeDamage(1);
            }

            //move
            float moveInput = Input.GetAxis("Horizontal");
            _spriteRenderer.flipX = moveInput < 0 ? true : false;
            rb.linearVelocity = new Vector2(moveInput * _moveSpeed, rb.linearVelocity.y);

            charachterState = rb.linearVelocity.x != 0 ? CharachterState.Walk : CharachterState.Idle;
           

            //jump
            if (Input.GetKey(KeyCode.Space) && IsCollidingWith(_groundLayer))
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, _jumpForce);  
            }
            if(!IsCollidingWith(_collidableLayers))
            {
                onLandEvent.Invoke();
            }

            animator.SetInteger("state", (int)charachterState);
        }

        public void InAir() // kuzim da sam malo zakomplicirala sa eventom mogla sam stavit samo obican if ali sam htjela probat kako eventi rade
        {
            charachterState = CharachterState.Jump;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Potion"))
            {
                potionCollected++;
                OnPotionCollected?.Invoke(potionCollected);
                Destroy(collision.gameObject);
            }

            if (collision.CompareTag("Checkpoint"))
            {
                if(_spawnPoint != null)
                    _spawnPoint.GetComponentInParent<Checkpoint>().SetCheckPointState(false);

                _spawnPoint = collision.GetComponent<Checkpoint>().GetSpawnPoint();
                collision.GetComponent<Checkpoint>().SetCheckPointState(true); // koju komponentu ja ubiti ovdje dohvacam ?
            }
        }

        private bool IsCollidingWith(LayerMask mask)
        {
            float rayLength = 1f; 
            float sideRayLength = _boxCollider.bounds.extents.x + 0.05f;

            Vector2 bottomCenter = (Vector2)_boxCollider.bounds.center - new Vector2(0, _boxCollider.bounds.extents.y);

            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, rayLength, mask);
            RaycastHit2D hitLeft = Physics2D.Raycast(bottomCenter, Vector2.left, sideRayLength, mask);
            RaycastHit2D hitRight = Physics2D.Raycast(bottomCenter, Vector2.right, sideRayLength, mask);
            // Debug.DrawRay(bottomCenter, Vector2.down * 1f, hit.collider != null ? Color.green : Color.red);
            // Debug.DrawRay(bottomCenter, Vector2.left * sideRayLength, hitLeft.collider != null ? Color.green : Color.red);
            // Debug.DrawRay(bottomCenter, Vector2.right * sideRayLength, hitRight.collider != null ? Color.green : Color.red);
            return  hit.collider != null || hitLeft.collider != null || hitRight.collider != null;
        }
        private void TakeDamage(int damage)
        {
            // currentHealth -= damage;
            // healthBar.SetHealth(currentHealth);
        }
    }
}

