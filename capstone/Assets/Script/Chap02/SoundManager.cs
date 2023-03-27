using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioClip truck_sound;
    [SerializeField] private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = truck_sound;
    }

    public void Play()
    {
        audioSource.Play();
    }
    public void DontPlay()
    {
        audioSource.Pause();
    }
}
