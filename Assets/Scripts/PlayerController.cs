using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;
using UnityEngine.UI;


namespace Lana
{
    public class PlayerController : MonoBehaviour
    {
        public UnityEvent onLandEvent;
        public enum CharachterState{Idle, Walk, Jump}
        public Action<int>OnPotionCollected;
        public HUD healthBar;
        public DeathMenu deathMenu;
        
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _jumpForce;
        [SerializeField] private BoxCollider2D _boxCollider; 
        [SerializeField] private LayerMask _groundLayer;
        [SerializeField] private LayerMask _dieLayer;
        [SerializeField] private LayerMask _collidableLayers;
        [SerializeField] private SpriteRenderer _spriteRenderer;

        [Header("Audio")]
        [SerializeField] private GameObject sfxPlayerDeath;
        [SerializeField] private GameObject sfxPotionCollected;
        [SerializeField] private GameObject sfxAmbiance;

        private Transform _spawnPoint;
        private Rigidbody2D rb;
        private Animator animator;
        private CharachterState charachterState;
        private int potionCollected = 0;
        private int maxHealth = 4;
        private int currentHealth;
        private float damageCooldown = 1f; 
        private float lastDamageTime;
        
        void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            currentHealth = maxHealth;
            healthBar.SetMaxHealth(4); 
            Instantiate(sfxAmbiance, transform.position, Quaternion.identity);
        }

        void Update()
        {
    
            if(IsCollidingWith(_dieLayer))
            {
                if (Time.time > lastDamageTime + damageCooldown)
                    {
                        lastDamageTime = Time.time;
                        TakeDamage(1);
                        if(currentHealth == 0 )
                        {
                            deathMenu.OpenMenu();
                        }
                        transform.position = _spawnPoint.position;
                        Instantiate(sfxPlayerDeath, transform.position, Quaternion.identity);
                    }
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

        public void InAir() 
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
                Instantiate(sfxPotionCollected, transform.position, Quaternion.identity);
            }

            if (collision.CompareTag("Checkpoint"))
            {
                if(_spawnPoint != null)
                    _spawnPoint.GetComponentInParent<Checkpoint>().SetCheckPointState(false);

                _spawnPoint = collision.GetComponent<Checkpoint>().GetSpawnPoint();
                collision.GetComponent<Checkpoint>().SetCheckPointState(true); 
            }
        }
        public int GetPotionCollected()
        {
            return potionCollected;
        }

        private bool IsCollidingWith(LayerMask mask)
        {
            float rayLength = 1f; 
            float sideRayLength = _boxCollider.bounds.extents.x + 0.05f;

            Vector2 bottomCenter = (Vector2)_boxCollider.bounds.center - new Vector2(0, _boxCollider.bounds.extents.y);

            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, rayLength, mask);
            RaycastHit2D hitLeft = Physics2D.Raycast(bottomCenter, Vector2.left, sideRayLength, mask);
            RaycastHit2D hitRight = Physics2D.Raycast(bottomCenter, Vector2.right, sideRayLength, mask);
            // Debug.DrawRay(bottomCenter, Vector2.right * sideRayLength, hitRight.collider != null ? Color.green : Color.red);
            return  hit.collider != null || hitLeft.collider != null || hitRight.collider != null;
        }
        private void TakeDamage(int damage)
        {
            currentHealth -= damage;
            healthBar.SetHealth(currentHealth);
        }
    }
}

