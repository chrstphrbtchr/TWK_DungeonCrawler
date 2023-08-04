using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerData : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

[System.Serializable]
public class UserSaveData
{
    // COLORS
    public Color blobColor;
    public Color shieldColor;
    public Color ghostColor;
    public Color graveColor;
    public Color keyColor;
    public Color ogreColor;
    public Color playerColor;
    public Color skeletonColor;
    public Color tileColor;
    public Color doorColor;
    public Color bkgdColor;
    public Color textColor;

    // BOOLS
    public bool screenShakeOK;
    public bool chromaticAbOK;
    public bool particlesOK;
    public bool scanlinesOK;
    public bool staticOK;
    public bool lightFlickerOK;
    public bool onShieldHitOK;

    // FLOATS
    public float shadowIntensity;
    public float shadowDistance;
    public float cameraFollowSpeed;
    public float audio_SFX;
    public float audio_MUSIC;
    public float audio_MASTER;

    public UserSaveData(Color c_blob, Color c_shield, Color c_ghost, Color c_grave, Color c_key,
        Color c_ogre, Color c_player, Color c_skeleton, Color c_tile, Color c_door, Color c_bkgd,
        Color c_text, bool b_screenshake, bool b_chroma, bool b_particle, bool b_scanlines,
        bool b_static, bool b_flashlight, bool b_shield, float f_shadowInt, float f_shadowDist,
        float f_camSPD, float f_SFX, float f_MUSIC, float f_VOLUME)
    {
        blobColor = c_blob;
        shieldColor = c_shield;
        ghostColor = c_ghost;
        graveColor = c_grave;
        keyColor = c_key;
        ogreColor = c_ogre;
        playerColor = c_player;
        skeletonColor = c_skeleton;
        tileColor = c_tile;
        doorColor = c_door;
        bkgdColor = c_bkgd;
        textColor = c_text;
        screenShakeOK = b_screenshake;  // still need methods for bools & floats (minus audio).
        chromaticAbOK = b_chroma;
        particlesOK = b_particle;
        scanlinesOK = b_scanlines;
        staticOK = b_static;
        lightFlickerOK = b_flashlight;
        onShieldHitOK = b_shield;
        shadowIntensity = f_shadowInt;
        shadowDistance = f_shadowDist;
        cameraFollowSpeed = f_camSPD;
        audio_SFX = f_SFX;
        audio_MUSIC = f_MUSIC;
        audio_MASTER = f_VOLUME;
    }

    public void SetItemColor(string i, Color c)
    {
        if(i != "Blob" && i != "Ghost")
        {
            c = new Color(c.r, c.g, c.b, 1);
        }

        switch (i)
        {
            case "Blob":
                blobColor = c;
                break;
            case "Shield":
                shieldColor = c;
                break;
            case "Ghost":
                ghostColor = c;
                break;
            case "Grave":
                graveColor = c;
                break;
            case "Key":
                keyColor = c;
                break;
            case "Ogre":
                ogreColor = c;
                break;
            case "Player":
                playerColor = c;
                break;
            case "Skeleton":
                skeletonColor = c;
                break;
            case "Tile":
                tileColor = c;
                break;
            case "Door":
                doorColor = c;
                break;
            case "Background":
                bkgdColor = c;
                break;
            case "Text":
                textColor = c;
                break;
            default:
                Debug.LogWarningFormat("Item {0} does not exist.", i);
                break;
        }
    }
    public void SetItemColor(string i, float r, float g, float b, float a)
    {
        switch (i)
        {
            case "Blob":
                blobColor = new Color(r, g, b, a);
                break;
            case "Shield":
                shieldColor = new Color(r, g, b, 1);
                break;
            case "Ghost":
                ghostColor = new Color(r, g, b, a);
                break;
            case "Grave":
                graveColor = new Color(r, g, b, 1);
                break;
            case "Key":
                keyColor = new Color(r, g, b, 1);
                break;
            case "Ogre":
                ogreColor = new Color(r, g, b, 1);
                break;
            case "Player":
                playerColor = new Color(r, g, b, 1);
                break;
            case "Skeleton":
                skeletonColor = new Color(r, g, b, 1);
                break;
            case "Tile":
                tileColor = new Color(r, g, b, 1);
                break;
            case "Door":
                doorColor = new Color(r, g, b, 1);
                break;
            case "Background":
                bkgdColor = new Color(r, g, b, 1);
                break;
            case "Text":
                textColor = new Color(r, g, b, 1);
                break;
            default:
                Debug.LogWarningFormat("Item {0} does not exist.", i);
                break;
        }
    }

    public void SetAudioValues(float sfx, float music, float master)
    {
        this.audio_MASTER = master;
        this.audio_MUSIC = music;
        this.audio_SFX = sfx;
    }

}

/*
 * SO!
 * What we're going to need is:
 * 1. A static class with getters and setters for...
 *  a. ALL COLORS that can change
 *      - blob
 *      - shield
 *      - ghost
 *      - grave
 *      - key
 *      - ogre
 *      - player
 *      - skeleton
 *      - tile
 *      - door
 *      - background
 *      - text
 *  b. BOOLEANS FOR
 *      - screen shake
 *      - chrom. ab.
 *      - particles
 *      - scanlines
 *      - static
 *      - flickering
 *          - PLAYER LIGHT
 *          - PLAYER ON SHIELD HIT
 *  c. FLOATS FOR
 *      - Shadow intensity / distance
 *      - Camera follow speed??
 *      - Audio
 *          - MASTER
 *          - MUSIC
 *          - SFX
 *  d. AN INSTANCE FOR
 *      - DEFAULT VALUES
 *      - PLAYER  VALUES
 *      - CURRENT VALUES
 * 2. THIS CLASS will save and load JSON data for these objects.
 *    It will also have events attached to update all necessary
 *    objects in the scene, called from the setters on current
 *    or player. Default never changes, obvs.
 *    It will also subscribe to events attached to buttons
 *    which will call the setters or call methods for resetting
 *    to default (either indiv. or ALL). Why not just set it to
 *    the buttons? Because it will need to call these on START!
 * 3. THIS CLASS SHOULD NOT BE DESTROYED ON LOAD!
 *      instance = this? cool. otherwise, delete.
*/