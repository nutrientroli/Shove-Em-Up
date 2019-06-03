using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// HOW DOES THIS WORK?
// Call "Load Scene" and then wait for the loading scene to be loaded and then the scene index you wanted to be loaded


public class SceneLoader : MonoBehaviour
{
    // setup
    private const string thisSceneName = "LoadingScene";       // the name of the scene we use as loading scene !!_IMPORTANT_!!
    private static int targetSceneIndex;
    
    // data output
    [SerializeField] private Slider loadingSlider;
    [SerializeField] private TextMeshProUGUI percentageText;

    public void Start()
    {
        StartCoroutine(LoadSceneAsync());
    }

    IEnumerator LoadSceneAsync()
    {
        Debug.Log("LOADING:_ Starting scene loading");
        AsyncOperation op = ScenesManager.ChangeSceneLoading();       
        while (!op.isDone)
        {
            float loadingProgress = Mathf.Clamp01(op.progress / 0.9f);

            loadingSlider.value = loadingProgress;
            percentageText.text = (loadingProgress * 100) + " " + "%";

            yield return new WaitForSeconds(0.1f);
        }
    }

}
