using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Flicker : MonoBehaviour
{
    [SerializeField] Light2D lite;

    void Start()
    {
        StartCoroutine(FlickerLight());
    }

    IEnumerator FlickerLight()
    {
        while(ScreenTransition.loadingScene)
        {
            yield return null;
        }
        while(!ScreenTransition.loadingScene)
        {
            float rndIntensity = Random.Range(0.8f, 1.2f), rndOuterRad = Random.Range(3f, 4f),
                time = 0, maxTime = Random.Range(1.5f, 2.75f), oldIntensity = lite.intensity, 
                oldOuterRad = lite.pointLightOuterRadius;

            while(time <= maxTime)
            {
                lite.intensity = Mathf.Lerp(oldIntensity, rndIntensity, time/maxTime);
                lite.pointLightOuterRadius = Mathf.Lerp(oldOuterRad, rndOuterRad, time/maxTime);
                time += Time.deltaTime;
                yield return null;
            }
            yield return new WaitForSeconds(Random.Range(0.25f, 1.75f));
        }
        yield return null;
    }
}
