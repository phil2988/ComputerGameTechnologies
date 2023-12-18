using Assets.Scripts.AI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Mobs
{
    public class SlimeBoss : MonoBehaviour
    {
        private float movementSpeed;
        private float detectionRadius; // TODO: Maybe remove this - For now just set a high value

        // Attack
        public float damage;
        public float attackRange;
        public float attackSpeed;
        public float attackInterval;
        private float attackTimer;

        public float health;

        public GameObject smallerSlimePrefab;
        public GameObject projectilePrefab;

        // Loot
        public GameObject lootPrefab;
        public int lootDropChance;

        public Transform _target;
        public Animator _animator;
        public Rigidbody2D _rigidbody;

        // TODO: Create a PlayerDamage script that is responsible for decrementing the players' health when an attack makes contact
        // Then use that script here


        public void Start()
        {
            setTarget(GameObject.FindGameObjectWithTag("Player").transform);
            ResetAttackTimer();
        }

        public void FixedUpdate()
        {
            float distanceToPlayer = Vector2.Distance(transform.position, _target.position);
            if (health > 0)
            {
                if (distanceToPlayer < detectionRadius)
                {
                    if (distanceToPlayer > attackRange)
                    {
                        MoveTowardsTarget(_target);
                    }
                    else
                    {
                        if (Time.time >= attackTimer)
                        {
                            ResetAttackTimer();
                            int randomAttack = UnityEngine.Random.Range(0, 3);
                            switch (randomAttack)
                            {
                                case 0:
                                    SingleTargetAttack();
                                    break;
                                case 1:
                                    AreaOfEffectAttack();
                                    break;
                                case 2:
                                    ProjectileAttack();
                                    break;
                            }
                        }
                    }
                }
            }
            else
            {
                Die();
            }
        }

        public void MoveTowardsTarget(Transform target)
        {
            _animator.SetTrigger("WalkTrigger");
            transform.position = Vector2.MoveTowards(transform.position, target.position, movementSpeed * Time.deltaTime);
        }

        public void SingleTargetAttack()
        {
            _animator.SetTrigger("AttackTrigger");
        }

        public void AreaOfEffectAttack()
        {
            _animator.SetTrigger("AOEAttackTrigger");
        }

        public void ProjectileAttack()
        {
            _animator.SetTrigger("ProjectileAttackTrigger");

            // Shoots projectiles in different directions
            Instantiate(projectilePrefab, transform.position + Vector3.up, Quaternion.identity);
            Instantiate(projectilePrefab, transform.position + Vector3.down, Quaternion.identity);
            Instantiate(projectilePrefab, transform.position + Vector3.left, Quaternion.identity);
            Instantiate(projectilePrefab, transform.position + Vector3.right, Quaternion.identity);
        }

        private void ResetAttackTimer()
        {
            attackTimer = Time.time + attackInterval;
        }

        public void TakeDamage(int damage)
        {
            health -= damage;

            if (health <= 0)
            {
                Die();
            }
        }

        public void Split()
        {
            // Instantiate smaller slimes
            Instantiate(smallerSlimePrefab, transform.position + Vector3.left, Quaternion.identity);
            Instantiate(smallerSlimePrefab, transform.position + Vector3.right, Quaternion.identity);

            DropLoot();
            Destroy(gameObject);
        }

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
                //_animator.SetTrigger("Die");
                DropLoot();
                Destroy(gameObject);
            }
        }

        public SlimeBoss setTarget(Transform target)
        {
            this._target = target;
            return this;
        }
    }
}
