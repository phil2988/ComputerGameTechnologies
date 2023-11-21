using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.AI
{
    public interface IEnemy
    {
        void Start();
        void FixedUpdate();
        void Attack();
        void TakeDamage(int damage);
        void DropLoot();
        void Die();
        void setCurrentState(EnemyState state);
        EnemyState getCurrentState();
    }

    public enum EnemyState
    {
        Idle,
        MovingTowardsPlayer,
        MovingAwayFromPlayer,
        ChasingPlayer,
        Attacking,
        TakingDamage,
        Dying
    }
}
