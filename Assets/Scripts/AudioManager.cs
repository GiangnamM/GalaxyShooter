using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource m_Music;
    [SerializeField] private AudioSource m_SFX;

    [SerializeField] private AudioClip m_HomeMusicClip;
    [SerializeField] private AudioClip m_battleMusicClip;
    [SerializeField] private AudioClip m_LazerSFXClip;
    [SerializeField] private AudioClip m_PlasmaSFXClip;
    [SerializeField] private AudioClip m_HitSFXClip;
    [SerializeField] private AudioClip m_ExplosionSFXClip;


    public void PlayHomeMusic()
    {
        m_Music.loop = true;
        m_Music.clip = m_HomeMusicClip;
        m_Music.Play();
    }
    public void PlayBattleMusic()
    {
        m_Music.loop = true;
        m_Music.clip = m_battleMusicClip;
        m_Music.Play();
    }

    public void PlayLazerSFX()
    {
        m_SFX.PlayOneShot(m_LazerSFXClip);
    }
    public void PlayPlasmaSFX()
    {
        m_SFX.PlayOneShot(m_PlasmaSFXClip);
    }
    public void PlayHitSFX()
    {
        m_SFX.PlayOneShot(m_HitSFXClip);
    }
    public void PlayExplosionSFX()
    {
        m_SFX.PlayOneShot(m_ExplosionSFXClip);
    }


}
