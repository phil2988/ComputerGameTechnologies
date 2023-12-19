using UnityEngine;

public class SkeletonEnemyDeath : MonoBehaviour, IEnemyDeath
{
    public GameObject lootPrefab;
    public int lootAmount = 2;

    public Animator anim;
    private EnemyMovement enemyMovement;

    private void Start()
    {
        lootAmount = Mathf.Max(1, lootAmount);
        enemyMovement = GetComponent<EnemyMovement>();

    }

    public void DropLoot()
    {
        for (int i = 0; i < lootAmount; i++)
        {
            Instantiate(lootPrefab, transform.position, Quaternion.identity);
        }
    }

    public void Die()
    {
        //zombieAudio.PlayDeathClip(); Do skeleton audio here
        anim.SetTrigger("DieTrigger");
        Invoke("DropLootAndDestroy", 0.5f);
    }

    private void DropLootAndDestroy()
    {
        DropLoot();
        Destroy(gameObject);
    }
}