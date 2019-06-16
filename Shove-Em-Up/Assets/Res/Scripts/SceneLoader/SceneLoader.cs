using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private Slider loadingSlider;
    [SerializeField] private TextMeshProUGUI percentageText;

    public void Start() {
        StartCoroutine(LoadSceneAsync());
    }

    IEnumerator LoadSceneAsync() {
        percentageText.text = "0 " + "%";
        yield return new WaitForSeconds(5f);
        AsyncOperation op = ScenesManager.ChangeSceneLoading();       
        while (!op.isDone) {
            float loadingProgress = Mathf.Clamp01(op.progress / 0.9f);
            loadingSlider.value = loadingProgress;
            //percentageText.text = ((loadingProgress * 100) + " " + "%").ToString();
            percentageText.text = string.Format("{0:0}", ((loadingProgress * 100) + " " + "%"));       
            yield return new WaitForSeconds(0.07f);
        }
    }

}
