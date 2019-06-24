using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource EssenceFX;
    public AudioSource SpikeHitFX;
    public AudioSource BoatExplodedFX;
    public AudioSource BGMusic;

    public void PlayEssenceFX()
    {
        EssenceFX.Play();
    }

    public void PlaySpikeHitFX()
    {
        SpikeHitFX.Play();
    }

    public void PlayBoatExplodedFX()
    {
        BoatExplodedFX.Play();
    }

    public void PlayBGMusic()
    {
        BGMusic.Play();
    }
}
