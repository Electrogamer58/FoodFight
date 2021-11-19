using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTester : MonoBehaviour
{
    [SerializeField] private AudioClip _musicA = null;
    [SerializeField] private AudioClip _musicB = null;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            MusicPlayer.MusicPlayer.Instance.Play(_musicA); //play music A
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            MusicPlayer.MusicPlayer.Instance.Play(_musicB); //play music B
        }
    }
}
