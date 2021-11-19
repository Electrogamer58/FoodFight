using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MusicPlayer
{
    public class MusicPlayer : MonoBehaviour
    {
        private static MusicPlayer _instance;
        public AudioSource _audioSource;

        public static MusicPlayer Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<MusicPlayer>();

                    if (_instance == null) //if none are found in scene, create one
                    {
                        GameObject _gameObject = new GameObject("MusicPlayer (Singleton)");
                        _instance = _gameObject.AddComponent<MusicPlayer>();
                        //_gameObject.name = "Music Player";
                        //make object persistent
                        DontDestroyOnLoad(_gameObject);
                    }
                }
                return _instance;
            }
        }

        private void Awake()
        {
            SetupSourceDefaults();
        }

        private void SetupSourceDefaults()
        {
            _audioSource = gameObject.AddComponent<AudioSource>();
            _audioSource.volume = .8f;
            _audioSource.playOnAwake = false;
            _audioSource.loop = true;
            _audioSource.spatialBlend = 0;
        }

        public void Play(AudioClip clip)
        {
            _audioSource.clip = clip;
            _audioSource.Play();
        }
        
        public void Stop()
        {
            _audioSource.Stop();
        }



    }

}
