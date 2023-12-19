using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;

public class BossFightHandler : MonoBehaviour
{
    public GameObject smallSlimePrefab;
    public GameObject largeSlimePrefab;

    public UnityEvent onBossEncounterCompleted;
    public UnityEvent toggleWaitingText;

    public int bossBaseHealth = 100;

    [SerializeField] Vector3 largeSlimePosition;
    [SerializeField] int numberOfSmallSlimesToSpawn = 4;
    [SerializeField] List<GameObject> smallSlimes = new();
    [SerializeField] GameObject largeSlime;
    [SerializeField] Health slimeBossHealthScript;
    [SerializeField] PlayerStats playerStatsScript;
    [SerializeField] Money playerMoneyScript;

    [SerializeField] bool smallSlimesSpawned = false;

    bool bossEncounterCompleted = false;

    // Start is called before the first frame update
    void Start()
    {
        // Get the player stats script.
        var player = GameObject.FindGameObjectWithTag("Player");
        playerStatsScript = player.GetComponent<PlayerStats>();
        playerMoneyScript = player.GetComponent<Money>();

        // Spawn the large slime.
        SpawnLargeSlime();
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the slime boss is alive.
        if(largeSlime != null)
        {
            // Update the position of the slime boss so that the small slimes can be spawned at the correct position.
            largeSlimePosition = largeSlime.transform.position;
        }

        // Check if the large slime is alive and the small slimes have not been spawned.
        if (largeSlime == null && !smallSlimesSpawned)
        {
            // Spawn the small slimes.
            SpawnSmallSlimes();
            smallSlimesSpawned = true;
        }

        // Check if the boss encounter is completed.
        if (bossEncounterCompleted)
        {
            OnBossEncounterCompleted();

            toggleWaitingText.Invoke();

            bossEncounterCompleted = false;
        }

        // Check if the boss encounter is completed.
        if (largeSlime == null && smallSlimesSpawned && IsSmallSlimesDead())
        {
            bossEncounterCompleted = true;
            toggleWaitingText.Invoke();
        }

    }

    /// <summary>
    /// Respawns the player at the respawn point.
    /// Kills all mobs.
    /// Respawns the mobs.
    /// </summary>
    void OnBossEncounterCompleted()
    {
        Debug.Log("Boss encounter completed.");

        Thread.Sleep(1000);

        var enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (var enemy in enemies)
        {
            Destroy(enemy);
        }

        if(onBossEncounterCompleted != null)
        {
            onBossEncounterCompleted.Invoke();
        }

        // Give the player coins according to the boss's health.
        playerMoneyScript.addCoin(bossBaseHealth * 2);
        
        // Increase the next boss's health by 50%.
        bossBaseHealth = bossBaseHealth / 100 * 150;

        SpawnLargeSlime();
        smallSlimesSpawned = false;

        playerStatsScript.respawnPlayer();
    }

    void SpawnLargeSlime()
    {
        Debug.Log("Spawning large slime.");

        // Spawn the slime boss.
        largeSlime = Instantiate(largeSlimePrefab, transform.position, Quaternion.identity);
        slimeBossHealthScript = largeSlime.GetComponent<Health>();
        slimeBossHealthScript.InitializeHealth(bossBaseHealth);
    }

    void SpawnSmallSlimes()
    {
        Debug.Log("Spawning small slimes.");
        // Spawn the small slimes.
        for (int i = 0; i < numberOfSmallSlimesToSpawn; i++)
        {
            var slime = Instantiate(smallSlimePrefab, largeSlimePosition, Quaternion.identity);
            var slimeHealth = slime.GetComponent<Health>();
            slimeHealth.InitializeHealth(bossBaseHealth / 6);

            smallSlimes.Add(slime);
        }
    }

    bool IsSmallSlimesDead()
    {
        // Check if the small slimes are dead.
        for (int i = 0; i < smallSlimes.Count; i++)
        {
            // Check if the small slime is dead.
            if (smallSlimes[i] != null)
            {
                return false;
            }
        }
        return true;
    }
}
