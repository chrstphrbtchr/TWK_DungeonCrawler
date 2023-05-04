using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IntroTimeline : MonoBehaviour
{
    public TMP_Text txt;
    public RectTransform collapse;
    public Canvas sky;
    public ParticleSystem rocks;

    string[] mcdog =
    {
        "Mon\n\n\n\n",
        "Monroe\n\n\n\n",
        "Monroe\nCoun\n\n\n",
        "Monroe\nCounty\n\n\n",
        "Monroe\nCounty\nDep\n\n",
        "Monroe\nCounty\nDepart\n\n",
        "Monroe\nCounty\nDepartment\n\n",
        "Monroe\nCounty\nDepartment\nof",
        "Monroe\nCounty\nDepartment\nof Games"
    };

    public void StartMCDOG()
    {
        StartCoroutine(MCDOG());
    }

    IEnumerator MCDOG()
    {
        for(int i = 0; i < mcdog.Length; i++)
        {
            txt.text = mcdog[i];
            yield return new WaitForSeconds(0.25f);
        }
    }

    public void StartDescent()
    {
        StartCoroutine (Skyfall());
    }

    IEnumerator Skyfall()
    {
        Vector3 startPos = sky.transform.position;
        Vector3 endPos = startPos + new Vector3(0, 10, 0);
        float timer = 0, maxTimer = 4.5f;
        while(sky.transform.position.y < endPos.y)
        {
            sky.transform.position = Vector3.Lerp(startPos, endPos, timer/maxTimer);
            timer += Time.deltaTime;
            yield return null;
        }
    }

    public void CollapseStart()
    {
        collapse.GetComponent<TMP_Text>().text = "Collapse!";
        StartCoroutine(CollapseDrop());
    }

    IEnumerator CollapseDrop()
    {
        Vector3 startPos = collapse.anchoredPosition;
        Vector3 endPos = new Vector3(0, 60, 0);
        float timer = 0, maxTimer = 1f;
        while (collapse.anchoredPosition.y > endPos.y)
        {
            collapse.anchoredPosition = Vector3.Lerp(startPos, endPos, timer / maxTimer);
            timer += Time.deltaTime;
            yield return null;
        }
        collapse.anchoredPosition = endPos;
        rocks.Play();
        yield return null;
    }
}
