using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.AI
{
    public interface IEnemyMovement
    {
        void MoveTowardsPlayer(Transform player);
        void BeIdle();
    }
}
