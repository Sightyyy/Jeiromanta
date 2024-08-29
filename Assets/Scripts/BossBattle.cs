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
    [SerializeField] private MeshRenderer bossMesh;
    [SerializeField] private CapsuleCollider bossCollider;
    public float laserCooldown = 3f;
    private float laserCooldownTimer;
    private LaserNode laserNode;
    private bool deathStatus;

    void Start()
    {
        laserNode = laserPrefab.GetComponent<LaserNode>();
        bossCanvas.SetActive(true);
    }

    void Update()
    {
        deathStatus = player.GetComponent<LaserTarget>().isDead;
        if(deathStatus == true)
        {
            player.transform.position = respawnPoint.position;
            bossCanvas.SetActive(false);
            player.GetComponent<LaserTarget>().isDead = false;
            bossMesh.enabled = true;
            bossCollider.enabled = true;
            this.enabled = false;
        }
        FirstSkill();
    }

    void FirstSkill()
    {
        for(int i = 0; i < 5; i++)
        {
            laserCooldownTimer -= Time.deltaTime;
            if (laserCooldownTimer <= 0)
            {
                SummonHorizontalLaser();
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
