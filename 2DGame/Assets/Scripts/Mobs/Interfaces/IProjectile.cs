using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.AI
{
    public interface IProjectile
    {
        void Start();
        void FixedUpdate();
        Vector3 FindTargetPosition();
        void ShootTargetPosition();
    }
}
