using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Flicker : MonoBehaviour
{
    [SerializeField] Light2D lite;

    void Start()
    {
        //StartCoroutine(FlickerLight());
    }

    IEnumerator FlickerLight()
    {
        while(ScreenTransition.loadingScene)
        {
            yield return null;
        }
        while(!ScreenTransition.loadingScene)
        {
            float rndIntensity = Random.Range(0.85f, 1.15f);
            float rndOuterRad = Random.Range(3f, 3.75f);
            float time = 0, maxTime = .75f;
            while(time <= maxTime)
            {
                lite.intensity = Mathf.Lerp(lite.intensity, rndOuterRad, time/maxTime);
                lite.pointLightOuterRadius = Mathf.Lerp(lite.pointLightOuterRadius, rndOuterRad, time/maxTime);
            }
            yield return new WaitForSeconds(Random.Range(0.25f, 1.25f));
        }
        yield return null;
    }
}
