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
        private const string MusicVolumeKey = "MusicVolume";
        private const string SoundVolumeKey = "SoundVolume";
        public AudioMixer audioMixer;

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
            musicSource.volume = 1f;
            float dB = Mathf.Log10(Mathf.Max(PlayerPrefs.GetFloat(MusicVolumeKey, 0.5f), 0.0001f)) * 20;
            audioMixer.SetFloat("MusicVolume", dB);
            musicSource.outputAudioMixerGroup = audioMixer.FindMatchingGroups("Music")[0];
            musicSource.Play();

            soundSource = gameObject.AddComponent<AudioSource>();
            soundSource.playOnAwake = false;
            soundSource.volume = 1f;
            dB = Mathf.Log10(Mathf.Max(PlayerPrefs.GetFloat(SoundVolumeKey, 0.5f), 0.0001f)) * 20;
            audioMixer.SetFloat("SoundVolume", dB);
            soundSource.outputAudioMixerGroup = audioMixer.FindMatchingGroups("Sound")[0];
        }

        public void SetMusicVolume(float volume)
        {
            float dB = Mathf.Log10(Mathf.Max(volume, 0.0001f)) * 20;
            audioMixer.SetFloat("MusicVolume", dB);

            PlayerPrefs.SetFloat(MusicVolumeKey, volume); 
            PlayerPrefs.Save(); 
        }

        public void SetSoundVolume(float volume)
        {
            float dB = Mathf.Log10(Mathf.Max(volume, 0.0001f)) * 20;
            audioMixer.SetFloat("SoundVolume", dB);

            PlayerPrefs.SetFloat(SoundVolumeKey, volume); 
            PlayerPrefs.Save(); 
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
