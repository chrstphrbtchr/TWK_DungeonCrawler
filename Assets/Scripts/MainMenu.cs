using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public enum MenuState
    {
        Main, Options, Story, Credits
    }

    public MenuState state;

    public GameObject swords;
    public PlayableDirector descend;
    public ParticleSystem dust;
    public GameObject optionsScreen, creditsScreen;

    const float swordDiff = -0.625f;
    public int selectorNum = 0;
    bool inMenu = false;

    // Update is called once per frame
    void Update()
    {
        NavigateMenu();
        if(Input.GetKeyDown(KeyCode.Return))
        {
            if (inMenu)
            {
                inMenu = false;
                
                // Close Menus
                state = MenuState.Main;
                optionsScreen.SetActive(false); // in future, might want this to
                creditsScreen.SetActive(false); // not simply shut off ALL menus
            }
            else
            {
                MakeSelection();
            }
                
        }
    }

    void MakeSelection()
    {
        switch(selectorNum)
        {
            case 0:     // Start Game
                // lower camera
                // transition
                state = MenuState.Main;
                descend.Play();
                dust.Stop();
                //SceneManager.LoadScene(1);
                break;
            case 1:     // Options
                // camera to right? -- OR -- open options UI on top
                state = MenuState.Options;
                inMenu = true;
                break;
            case 2:     // Credits & Thanks
                state = MenuState.Credits;
                inMenu = true;
                break;
            case 3:     // Exit Game
                state = MenuState.Main;
                Application.Quit();
                break;
            default:
                break;
        }
        creditsScreen.SetActive(state == MenuState.Credits);
        optionsScreen.SetActive(state == MenuState.Options);
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
                selectorNum = 3;
            }
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            selectorNum++;
        }
        selectorNum %= 4;
        //Debug.Log(selectorNum);
        swords.transform.position = new Vector3(0, (selectorNum * swordDiff), 10);
    }
}
