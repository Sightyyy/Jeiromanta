using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTarget : MonoBehaviour
{
    public bool isDead = false;
    private bool isInvincible = false; // New flag to prevent immediate re-hit
    public float invincibilityDuration = 1f; // Duration of invincibility after respawn
    AudioCollection audioCollection;

    public void Awake()
    {
        audioCollection = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioCollection>();

    }

    public void Hit()
    {
        if (isInvincible) return; // Ignore hit if the player is invincible

        isDead = true;
        audioCollection.PlaySFX(audioCollection.zapped);
        Debug.Log("Hit by Laser!");
    }

    public void Respawn()
    {
        StartCoroutine(InvincibilityCooldown());
    }

    private IEnumerator InvincibilityCooldown()
    {
        isInvincible = true;
        yield return new WaitForSeconds(invincibilityDuration);
        isInvincible = false;
    }
}
