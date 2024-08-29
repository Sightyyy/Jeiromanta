using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossActivation : MonoBehaviour
{
    public bool isBossStart;
    [SerializeField] private GameObject bossWall;
    [SerializeField] private GameObject boss;
    private BossBattle bossBattle;
    [SerializeField] private MeshRenderer bossMesh;
    [SerializeField] private CapsuleCollider bossCollider;

    void Start()
    {
        bossBattle = boss.GetComponent<BossBattle>();
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            isBossStart = true;
            bossWall.SetActive(true);
            bossBattle.enabled = true;
            bossMesh.enabled = false;
            bossCollider.enabled = false;
            Debug.Log("Start Boss Battle!");
        }
    }
}
