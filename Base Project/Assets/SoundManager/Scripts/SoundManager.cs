using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public AudioClip ClickSound;
    public AudioClip ScrollSound;
    public AudioClip RightSound;
    public AudioClip WrongSound;
    public AudioClip MessageSound;
    AudioSource asc;

    void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        asc = gameObject.GetComponent<AudioSource>();
    }

    public void PlayClickSound()
    {
        SoundManager.instance.asc.clip = ClickSound;
        SoundManager.instance.asc.PlayOneShot(ClickSound);
    }

    public void PlayScrollSound()
    {
        SoundManager.instance.asc.clip = ScrollSound;
        SoundManager.instance.asc.PlayOneShot(ScrollSound);
    }

    public void PlayRightSound()
    {
        SoundManager.instance.asc.clip = RightSound;
        SoundManager.instance.asc.PlayOneShot(RightSound);
    }

    public void PlayWrongSound()
    {
        SoundManager.instance.asc.clip = WrongSound;
        SoundManager.instance.asc.PlayOneShot(WrongSound);
    }

    public void PlayMessageSound()
    {
        SoundManager.instance.asc.clip = MessageSound;
        SoundManager.instance.asc.PlayOneShot(MessageSound);
    }

    public void PlayAudioClip(AudioClip _clip)
    {
        SoundManager.instance.asc.clip = _clip;
        SoundManager.instance.asc.PlayOneShot(_clip);
    }
}