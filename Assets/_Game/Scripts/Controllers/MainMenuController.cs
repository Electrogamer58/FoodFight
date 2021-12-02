using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] GameObject MainPanel;
    [SerializeField] GameObject SettingsPanel;
    [SerializeField] GameObject CreditsPanel;

    [SerializeField] AudioClip _musicA;
    [SerializeField] GameObject _firstButton;

    private void Awake()
    {
        MusicPlayer.MusicPlayer.Instance.Play(_musicA);

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(_firstButton);
    }

    public void NewGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ToCredits()
    {
        MainPanel.SetActive(false);
        CreditsPanel.SetActive(true);
    }

    public void ToSettings()
    {
        MainPanel.SetActive(false);
        SettingsPanel.SetActive(true);
    }

    public void ToMenu()
    {
        MainPanel.SetActive(true);
        CreditsPanel.SetActive(false);
        SettingsPanel.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
