using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTarget : MonoBehaviour
{
    public bool isDead = false;
    public void Hit()
    {
        isDead = true;
        Debug.Log("Hit by Laser!");
    }
}
