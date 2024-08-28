using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeWin : MonoBehaviour
{
    public bool isWin = false;
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            isWin = true;
            Debug.Log("You Won!");
        }
    }
}
