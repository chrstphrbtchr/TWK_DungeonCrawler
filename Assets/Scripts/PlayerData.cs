using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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