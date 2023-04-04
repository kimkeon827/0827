using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // ����Ŵ��� �ڽ��� �ν��Ͻ��� ���� ��������(static)�� �����
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
