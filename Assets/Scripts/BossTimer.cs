using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
#if UNITY_EDITOR
using UnityEditor.SearchService;
#endif
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossTimer : MonoBehaviour
{
    public float countDown = 151f;
    public float currcountDown;
    private TMP_Text tMP_Text;

    
    private void Start()
    {
        tMP_Text = GetComponent<TMP_Text>();
        currcountDown = countDown;
    }

    private void Update()
    {
        currcountDown -= Time.deltaTime;
        if(currcountDown <= 1f)
        {
            SceneManager.LoadScene("WinScene");
        }
        tMP_Text.text = Mathf.FloorToInt(currcountDown).ToString();
    }
    public void Restart()
    {
        Start();
        Debug.Log("Timer Reseted");
    }
}
