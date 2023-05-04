using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IntroTimeline : MonoBehaviour
{
    public TMP_Text txt;

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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartMCDOG()
    {
        StartCoroutine(MCDOG());
    }

    public IEnumerator MCDOG()
    {
        for(int i = 0; i < mcdog.Length; i++)
        {
            txt.text = mcdog[i];
            yield return new WaitForSeconds(0.25f);
        }
    }
}
