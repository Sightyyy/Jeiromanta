using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class ASyncLoader : MonoBehaviour
{
    [Header("Menu Screens")]
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private GameObject mainMenu;

    [Header("Slider")]
    [SerializeField] private Slider loadingSlider;

    public void LoadLevelBtn(string levelToLoad)
    {
        mainMenu.SetActive(false);
        loadingScreen.SetActive(true);

        StartCoroutine(LoadLevelAsync(levelToLoad));
    }

    IEnumerator LoadLevelAsync(string levelToLoad)
    {
        // Jeda 3 detik di awal sebelum mulai memuat scene
        yield return new WaitForSeconds(3f);

        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(levelToLoad);
        loadOperation.allowSceneActivation = false;  // Mencegah scene aktif otomatis

        while (loadOperation.progress < 0.9f)
        {
            float progressValue = Mathf.Clamp01(loadOperation.progress / 0.9f);
            loadingSlider.value = progressValue;
            yield return null;
        }

        // Pastikan progress bar mencapai 100%
        loadingSlider.value = 1f;

        // Jeda 1 detik setelah progress mencapai 100%
        yield return new WaitForSeconds(2f);

        // Aktifkan scene setelah jeda 1 detik
        loadOperation.allowSceneActivation = true;
    }
}
