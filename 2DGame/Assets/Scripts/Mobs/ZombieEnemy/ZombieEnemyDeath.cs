using UnityEngine;

public class ZombieEnemyDeath : MonoBehaviour, IEnemyDeath
{
    public GameObject lootPrefab;
    public int lootAmount = 1;

    [SerializeField]
    private Animator anim;
    private EnemySoundsManager soundManager;

    private void Start()
    {
        lootAmount = Mathf.Max(1, lootAmount);
        soundManager = GetComponent<EnemySoundsManager>();
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
        soundManager.PlayDeathSound();
        anim.SetTrigger("DieTrigger");
        Invoke("DropLootAndDestroy", 0.5f);
    }

    private void DropLootAndDestroy()
    {
        DropLoot();
        Destroy(gameObject);
    }
}