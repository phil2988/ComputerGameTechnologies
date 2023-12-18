
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyDeath : MonoBehaviour, IEnemyDeath
{
    public GameObject lootPrefab;
    public int lootAmount = 1;

    public Animator anim;

    private void Start()
    {
        lootAmount = Mathf.Max(1, lootAmount);
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
        anim.SetTrigger("DieTrigger");
        DropLoot();
        Destroy(gameObject);
    }
}