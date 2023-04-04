using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // 사운드매니저 자신의 인스턴스를 담을 정적변수(static)을 만든다
    public static SoundManager instance;

    public AudioClip sndExplosion;
    AudioSource myAudio;

    private void Awake()
    {
        if (SoundManager.instance == null)
        {
            SoundManager.instance = this;
        }
    }

    private void Start()
    {
        myAudio = GetComponent<AudioSource>();
    }

    public void PlaySound()
    {
        myAudio.PlayOneShot(sndExplosion);
    }
}
