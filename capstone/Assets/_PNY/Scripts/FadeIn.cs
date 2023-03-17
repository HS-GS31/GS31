using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeIn : MonoBehaviour
{
    public Image image;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FadeInn());
    }

    // Update is called once per frame
    void Update()
    {

    }
    IEnumerator FadeInn()
    {
        for (float f = 1f; f > 0; f -= 0.01f)
        {
            Color c = image.color;
            c.a = f;
            image.color = c;
            yield return null;
        }
        yield return new WaitForSeconds(1);
    }
}
