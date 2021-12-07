using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioSource Wrong;
    public AudioSource Ticking;
    public AudioSource TimeOut;

    public void PlayWrong()
    {
        Wrong.Play();
    }

    public void PlayTicking()
    {
        Ticking.Play();
    }

    public void PlayTimeOut()
    {
        TimeOut.Play();
    }
}
