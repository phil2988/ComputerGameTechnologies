using Assets.Scripts.AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonProjectile : MonoBehaviour, IProjectile
{
    public Vector3 targetPosition;
    public float projectileSpeed;

    public void Start()
    {
        FindTargetPosition();
    }

    public void FixedUpdate()
    {
        ShootTargetPosition();
    }

    public Vector3 FindTargetPosition()
    {
        return GameObject.FindGameObjectWithTag("Player").transform.position;
    }

    public void ShootTargetPosition()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, projectileSpeed * Time.deltaTime);

        if (transform.position == targetPosition) // TODO: Use RigidBody to cause collision instead?
        {
            Destroy(gameObject);
        }
    }
}
