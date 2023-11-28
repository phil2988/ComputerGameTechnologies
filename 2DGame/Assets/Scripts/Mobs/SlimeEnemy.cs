using Assets.Scripts.AI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Mobs
{
    public class SlimeEnemy : MonoBehaviour, IEnemy
    {
        private float health;
        private float movementSpeed;
        private float detectionRadius;

        // Attack
        public float damage;
        public float attackRange;
        public float attackSpeed;

        public GameObject smallerSlimePrefab;

        // Loot
        public GameObject lootPrefab;
        public int lootDropChance;

        public Transform _target;
        public Animator _animator;
        public Rigidbody2D _rigidbody;


        public void Start()
        {
            setTarget(GameObject.FindGameObjectWithTag("Player").transform);
        }
        
        //Refine logic here so idle does not get triggered repeatetly
        public void FixedUpdate()
        {
            _animator.SetTrigger("IdleTrigger");
            float distanceToPlayer = Vector2.Distance(transform.position, _target.position);

            if (distanceToPlayer < detectionRadius)
            {
                if (distanceToPlayer > attackRange)
                {
                    MoveTowardsTarget(_target);
                }
                else
                {
                    Attack();
                }
            }
        }

        public void MoveTowardsTarget(Transform target)
        {
            _animator.SetTrigger("WalkTrigger");
            transform.position = Vector2.MoveTowards(transform.position, target.position, movementSpeed * Time.deltaTime);
        }

        public void Attack()
        {
            float distanceToPlayer = Vector2.Distance(transform.position, _target.position);

            if (distanceToPlayer <= attackRange)
            {
                // Play attack animation
                _animator.SetTrigger("AttackTrigger");

                /*
                 * Need a script attached to player containing the below stats for calculation to work:
                 * 
                 * Armor calculation
                int playerArmor = player.GetComponent<PlayerStats>().armor;
                float damageReductionPercentage = 0.2f
                int finalDamage =  Mathf.Max(0, Mathf.RoundToInt(baseDamage - (playerArmor * damageReductionPercentage)));

                * Deal damage to player
                player.GetComponent<Health>.TakeDamage(finalDamage);
                */
            }
        }

        public void TakeDamage(int damage)
        {
            health -= damage;

            if (health <= 0)
            {
                Die();
            }
        }


        // TODO:
        //public void Split()
        //{
        //    // Instantiate smaller slimes
        //    Instantiate(smallerSlimePrefab, transform.position + Vector3.left, Quaternion.identity);
        //    Instantiate(smallerSlimePrefab, transform.position + Vector3.right, Quaternion.identity);
            
        //    DropLoot();
        //    Destroy(gameObject);
        //}

        public void DropLoot()
        {
            if (UnityEngine.Random.Range(0, 100) < lootDropChance && lootPrefab != null)
            {
                Instantiate(lootPrefab, transform.position, Quaternion.identity);
            }
        }

        public void Die()
        {
            if (_animator != null)
            {
                _animator.SetTrigger("Die");
                DropLoot();
                Destroy(gameObject);
            }
        }

        public SlimeEnemy setTarget(Transform target)
        {
            this._target = target;
            return this;
        }
    }
}
