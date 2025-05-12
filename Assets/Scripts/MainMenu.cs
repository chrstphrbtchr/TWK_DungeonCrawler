using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
            if (inMenu)
            {
                inMenu = false;
                
                // Close Menus
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
                creditsScreen.SetActive(false);
                optionsScreen.SetActive(true);
                break;
            case 2:     // Credits & Thanks
                state = MenuState.Credits;
                inMenu = true;
                creditsScreen.SetActive(true);
                optionsScreen.SetActive(false);
                break;
            case 3:     // Exit Game
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
