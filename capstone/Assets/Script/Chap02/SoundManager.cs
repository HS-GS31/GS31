using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioClip truck_sound;
    [SerializeField] private AudioClip bgm;
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioSource bgmSource;

    // Start is called before the first frame update
    void Start()
    {

        bgmSource.clip = bgm;
        bgmSource.Play();

        sfxSource.clip = truck_sound;
    }

    public void Play()
    {
        sfxSource.Play();
    }
    public void DontPlay()
    {
        sfxSource.Pause();
    }
    public void BGMDontPlay()
    {
        bgmSource.Pause();
    }
}
