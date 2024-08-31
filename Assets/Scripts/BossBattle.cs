using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBattle : MonoBehaviour
{
    [SerializeField] private GameObject laserPrefab;
    [SerializeField] private GameObject shadowPrefab;
    [SerializeField] private GameObject player;
    [SerializeField] private Transform respawnPoint;
    [SerializeField] private GameObject bossCanvas;
    [SerializeField] private GameObject timer;
    [SerializeField] private GameObject Entrance;
    [SerializeField] private MeshRenderer bossMesh;
    [SerializeField] private CapsuleCollider bossCollider;
    public float laserCooldown = 1f;
    private float laserCooldownTimer;
    private LaserNode laserNode;
    private bool deathStatus;
    AudioCollection audioCollection;
    private BossActivation bossActivation; // Reference to BossActivation
    BossTimer bossTimer;

    public void Awake()
    {
        audioCollection = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioCollection>();
        // Find the BossActivation component on the same GameObject or on a different one if necessary
        bossActivation = FindObjectOfType<BossActivation>();
        if (timer != null)
        {
            bossTimer = timer.GetComponent<BossTimer>();
            if (bossTimer == null)
            {
                Debug.LogError("BossTimer component not found on timer object.");
            }
        }
        else
        {
            Debug.LogError("Timer object is not assigned.");
        }
    }

    void Start()
    {
        laserNode = laserPrefab.GetComponent<LaserNode>();
    }

    void Update()
    {
        bossCanvas.SetActive(true);
        deathStatus = player.GetComponent<LaserTarget>().isDead;
        if (deathStatus)
        {
            player.transform.position = respawnPoint.position;
            bossCanvas.SetActive(false);

            // Set isBossStart to false when the player respawns
            if (bossActivation != null)
            {
                bossActivation.isBossStart = false;
            }

            Entrance.SetActive(false);
            player.GetComponent<LaserTarget>().isDead = false;
            player.GetComponent<LaserTarget>().Respawn(); // Trigger the invincibility cooldown
            if (bossTimer != null)
            {
                bossTimer.Restart();
                Debug.Log("BossTimer Restart called");
            }
            else
            {
                Debug.LogError("BossTimer reference is null.");
            }
            bossMesh.enabled = true;
            bossCollider.enabled = true;
            Debug.Log("Respawned");
            this.enabled = false;
        }
        FirstSkill();
    }

    void FirstSkill()
    {
        for (int i = 0; i < 5; i++)
        {
            laserCooldownTimer -= Time.deltaTime;
            if (laserCooldownTimer <= 0)
            {
                SummonHorizontalLaser();
                audioCollection.PlaySFX(audioCollection.laser);
                laserCooldownTimer = laserCooldown;
            }
        }
    }

    void SummonHorizontalLaser()
    {
        laserNode.laserDistance = 45;
        Vector3 laserPos = new Vector3(Random.Range(-20f, 20f), transform.position.y - 1f, transform.position.z - 5f);
        GameObject laser = Instantiate(laserPrefab, laserPos, Quaternion.identity);
        LineRenderer lineRenderer = laser.GetComponent<LineRenderer>();
        if (lineRenderer != null)
        {
            // Destroy laser after 1 second
            Destroy(laser, 1f);
        }
    }

    void SummonVerticalLaser()
    {
        laserNode.transform.rotation = Quaternion.Euler(90, 0, 0);
        float randX = Random.Range(-20f, 20f);
        float randZ = Random.Range(-20f, 20f);
        Vector3 laserPos = new Vector3(randX, transform.position.y, randZ);
        Vector3 shadowPos = new Vector3(randX, transform.position.y, randZ);
        GameObject laser = Instantiate(laserPrefab, laserPos, Quaternion.identity);
        LineRenderer lineRenderer = laser.GetComponent<LineRenderer>();
        if (lineRenderer != null)
        {
            // Destroy laser after 1 second
            Destroy(laser, 1f);
        }
    }
}
