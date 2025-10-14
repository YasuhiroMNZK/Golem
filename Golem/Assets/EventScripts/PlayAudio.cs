using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(AudioSource))]
public class PlayAudio : MonoBehaviour,ActionBase
{
    AudioSource audiosource;
    private void Start()
    {
        audiosource = GetComponent<AudioSource>();
    }
    public void Action()
    {
        audiosource.Play();
    }
}
