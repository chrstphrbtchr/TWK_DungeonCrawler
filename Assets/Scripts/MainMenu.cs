using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public enum MenuState
    {
        Main, Options, Story, Credits
    }

    public MenuState state;

    public GameObject swords;

    const float swordDiff = -0.625f;
    public int selectorNum = 0;
    bool inMenu = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        NavigateMenu();
        if(Input.GetKeyDown(KeyCode.Return))
        {
            MakeSelection();
        }
    }

    void MakeSelection()
    {
        switch(selectorNum)
        {
            case 0:     // Start Game
                // lower camera
                // transition
                SceneManager.LoadScene(1);
                break;
            case 1:     // Options
                // camera to right? -- OR -- open options UI on top
                break;
            case 2:     // Story

                break;
            case 3:     // Credits & Thanks
                break;
            case 4:     // Exit Game
                Application.Quit();
                break;
            default:
                break;
        }
    }

    void NavigateMenu()
    {
        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            if(selectorNum > 0)
            {
                selectorNum--;
            }
            else
            {
                selectorNum = 4;
            }
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            selectorNum++;
        }
        selectorNum %= 5;
        //Debug.Log(selectorNum);
        swords.transform.position = new Vector3(0, (selectorNum * swordDiff), 10);
    }
}
