using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{   
    public static AudioManager instance;

    [SerializeField] AudioSource backgroundSFX,coinSFX,menuSelectSFX,playerHurtSFX,bombExlpodeSFX,robotBulbSFX;
    private void Awake() 
    {

     if(instance == null)
     {
        instance = this;
        DontDestroyOnLoad(gameObject);
     }  

     else
     {
        Destroy(gameObject);
     }

    }

    public void  PlayBackgroundSoundClip(AudioClip clip)
    {
        backgroundSFX.PlayOneShot(clip);
    }
    public void  PlayCoinsSoundClip(AudioClip clip)
    {
        coinSFX.PlayOneShot(clip);
    }
    public void  PlayMenuSelectSoundClip(AudioClip clip)
    {
        menuSelectSFX.PlayOneShot(clip);
    }
    public void  PlayPlayerHurtSoundClip(AudioClip clip)
    {
        playerHurtSFX.PlayOneShot(clip);
    }
    public void  PlayBombExplodeSoundClip(AudioClip clip)
    {
        bombExlpodeSFX.PlayOneShot(clip);
    }
   
    public void  PlayRobotBulbSoundClip(AudioClip clip)
    {
        robotBulbSFX.PlayOneShot(clip);
    }
   
}
