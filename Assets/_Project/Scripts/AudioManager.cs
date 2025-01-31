using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Platformer
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance;

        
        [HideInInspector] public AudioSource musicSource; 
        [HideInInspector] public AudioSource soundSource;

        [Header("Volume")]
        [SerializeField, Range(0f, 1f)] float musicVolume = 1f;
        [SerializeField, Range(0f, 1f)] float soundVolume = 1f;

        [Header("AudioClips")]
        public AudioClip musicClip; 
        public AudioClip soundCheckpoint;
        public AudioClip soundCoin;  
        public AudioClip soundJump;  
        public AudioClip soundLose;  
        public AudioClip soundWin;
        
        void Awake()
        {
            if(Instance != null && Instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
        }

        void Start()
        {
            musicSource = gameObject.AddComponent<AudioSource>();
            musicSource.clip = musicClip;
            musicSource.loop = true; 
            musicSource.playOnAwake = true; // Музыка начнет играть автоматически
            musicSource.volume = musicVolume;
            musicSource.Play();

            soundSource = gameObject.AddComponent<AudioSource>();
            soundSource.playOnAwake = false;
            soundSource.volume = soundVolume;

        }

        public void SetMusicVolume(float volume)
        {
            musicSource.volume = volume;
        }

        public void SetSoundVolume(float volume)
        {
            soundSource.volume = volume;
        }

        public void PlaySoundCheckpoint()
        {
            soundSource.PlayOneShot(soundCheckpoint);
        }

        public void PlaySoundCoin()
        {
            soundSource.PlayOneShot(soundCoin);
        }

        public void PlaySoundJump()
        {
            soundSource.PlayOneShot(soundJump);
        }

        public void PlaySoundLose()
        {
            soundSource.PlayOneShot(soundLose);
        }

        public void PlaySoundWin()
        {
            soundSource.PlayOneShot(soundWin);
        }
    }
}
