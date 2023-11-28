using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.AI
{
    public interface IEnemy
    {
        void Start();
        void FixedUpdate();
        void MoveTowardsTarget(Transform target);
        void Attack();
        void TakeDamage(int damage);
        void DropLoot();
        void Die();
    }
}
