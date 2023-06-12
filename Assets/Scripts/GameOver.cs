using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public GameObject rocks;
    public GameObject cam;
    const int rocksY = -13;
    int times;
    // Start is called before the first frame update
    void Start()
    {
        rocks.transform.position = new Vector3(rocks.transform.position.x, rocksY, rocks.transform.position.z);
        times = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RocksGoUp(float amount)
    {
        rocks.transform.position += new Vector3(0f, amount, 0f);
    }

    public void ShakerStarter(float mag)
    {
        StartCoroutine(Shaker(mag));
    }

    IEnumerator Shaker(float mag)
    {
        float duration = .5f * (times < 3 ? 1 : 1.75f);
        float tempMag = mag;
        Vector3 original = cam.transform.position;

        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            float rX = Random.Range(-1f, 1f) * tempMag;
            float rY = Random.Range(-1f, 1f) * tempMag / 3.5f;
            cam.transform.position = new Vector3(original.x + rX, original.y + rY, original.z);

            elapsedTime += Time.deltaTime;
            tempMag = Mathf.Lerp(mag, 0, elapsedTime / duration);

            yield return null;
        }
        times++;
        cam.transform.position = original;
    }
}
