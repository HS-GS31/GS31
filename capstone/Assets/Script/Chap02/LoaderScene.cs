using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoaderScene : MonoBehaviour
{
    // Start is called before the first frame update
    public void GoScene3()
    {
        SceneManager.LoadScene("Chapter03Scene");
    }
}