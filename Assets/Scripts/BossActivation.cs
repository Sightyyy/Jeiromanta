using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossActivation : MonoBehaviour
{
    public bool isBossStart;
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            isBossStart = true;
            Debug.Log("Start Boss Battle!");
        }
    }
}
