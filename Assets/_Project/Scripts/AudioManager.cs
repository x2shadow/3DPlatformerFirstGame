using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace Platformer
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance;

        
        [HideInInspector] public AudioSource musicSource; 
        [HideInInspector] public AudioSource soundSource;
        public AudioMixer audioMixer;

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
            musicSource.playOnAwake = true; 
            musicSource.volume = musicVolume;
            musicSource.outputAudioMixerGroup = audioMixer.FindMatchingGroups("Music")[0];
            musicSource.Play();

            soundSource = gameObject.AddComponent<AudioSource>();
            soundSource.playOnAwake = false;
            soundSource.volume = soundVolume;
            soundSource.outputAudioMixerGroup = audioMixer.FindMatchingGroups("Sound")[0];

        }

        public void SetMusicVolume(float volume)
        {
            float dB = Mathf.Log10(Mathf.Max(volume, 0.0001f)) * 20;
            audioMixer.SetFloat("MusicVolume", dB);
            //musicSource.volume = volume;
        }

        public void SetSoundVolume(float volume)
        {
            float dB = Mathf.Log10(Mathf.Max(volume, 0.0001f)) * 20;
            audioMixer.SetFloat("SoundVolume", dB);
            //soundSource.volume = volume;
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
