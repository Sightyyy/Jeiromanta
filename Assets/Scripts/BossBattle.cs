using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBattle : MonoBehaviour
{
    [SerializeField] private GameObject laserPrefab;
    public float laserCooldown = 3f;
    private float laserCooldownTimer;

    void Update()
    {
        laserCooldownTimer -= Time.deltaTime;
        if (laserCooldownTimer <= 0)
        {
            FireHorizontalLaser();
            laserCooldownTimer = laserCooldown;
        }
    }

    void FireHorizontalLaser()
    {
        // Instantiate the laser prefab
        GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.identity);

        // Configure the laser line renderer
        LineRenderer lineRenderer = laser.GetComponent<LineRenderer>();

        if (lineRenderer != null)
        {
            // Set the laser's start and end points
            Vector3 startPosition = transform.position;
            Vector3 endPosition = new Vector3(Random.Range(-10f, 10f), transform.position.y, transform.position.z);

            lineRenderer.SetPosition(0, startPosition);
            lineRenderer.SetPosition(1, endPosition);

            // Optionally, add any additional effects like fading out the laser
            // You can add a coroutine here to handle the laser duration
            Destroy(laser, 1f); // Example: destroy laser after 1 second
        }
    }
}
