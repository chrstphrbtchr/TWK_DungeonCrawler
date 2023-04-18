using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenTransition : MonoBehaviour
{
    public static bool loadingScene, beginTransition;
    public Material transitionMat;
    private string progress = "_Progress";
    float threshold = 1.5f, transitionTime = 1.75f;

    // Start is called before the first frame update
    void Start()
    {
        beginTransition = false;
        transitionMat.SetFloat(progress, 0);
        StartCoroutine(Transition(true));
    }

    // Update is called once per frame
    void Update()
    {
        if(beginTransition && !loadingScene)
        {
            StartCoroutine(Transition(false));
        }
    }

    IEnumerator Transition(bool intro)
    { 
        beginTransition = false;
        loadingScene = true;

        float currentTime = 0;

        yield return new WaitForSeconds(0.1f);

        while (currentTime < transitionTime)
        {
            currentTime += Time.deltaTime;
            // if starting opaque
            if (intro)
            {
                transitionMat.SetFloat(progress, Mathf.Clamp((currentTime / transitionTime) * 1.5f, 0, threshold));
            }
            else
            {
                transitionMat.SetFloat(progress, Mathf.Clamp(1.5f - ((currentTime / transitionTime) * 1.5f), 0, threshold));
            }
            yield return null;
        }

        if (!intro) SceneManager.LoadScene(0);
        loadingScene = false;
    }
}
