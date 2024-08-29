using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForcefieldController : MonoBehaviour
{
    public float forcefieldCountdown;
    [SerializeField] private GameObject forcefield;

    private void Update()
    {
        if(forcefieldCountdown == 0)
        {
            forcefield.SetActive(false);
        }
    }
}
