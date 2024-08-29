using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossTimer : MonoBehaviour
{
    public float countDown;
    private TMP_Text tMP_Text;

    private void Start()
    {
        tMP_Text = GetComponent<TMP_Text>();
    }

    private void Update()
    {
        countDown -= Time.deltaTime;
        if(countDown < 0f)
        {
            SceneManager.LoadScene("WinScene");
        }
        tMP_Text.text = Mathf.FloorToInt(countDown).ToString();
    }
}
