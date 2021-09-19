using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{
    public Button ContinueButton;
    public Button NewGameButton;
    public Button ExitButton;
    public Button MusicButton;

    public GameObject MusicOnToggle;
    public GameObject MusicOffToggle;
    
    //public Sprite MusicOn;
    //public Sprite MusicOff;

    public TestSaveLoadScript SaveSystem;
    public AudioListener AudioListener;


    private bool SoundIsOn = true;
    private bool MusicIsOn = true;
    
 
    void Start()
    {
        ContinueButton.onClick.AddListener(ContinueClick);
        NewGameButton.onClick.AddListener(NewGameClick);
        ExitButton.onClick.AddListener(ExitClick);
        //SoundButton.onClick.AddListener(SoundClick);
        MusicButton.onClick.AddListener(MusicClick);
        Pause();
        MusicIsOn = false;
        MusicClick();
    }

    public void ContinueClick()
    {
        Time.timeScale = 1f;
        gameObject.SetActive(false);

    }

    public void NewGameClick()
    {
        Time.timeScale = 1f;
        gameObject.SetActive(false);
        SaveSystem.NewGame();
    }

    public void ExitClick()
    {
        Application.Quit();
    }

    /*
    public void SoundClick()
    {

    }
    */

    //TODO
    public void MusicClick()
    {
        MusicIsOn = !MusicIsOn;
        
        MusicOnToggle.SetActive(MusicIsOn);
        MusicOffToggle.SetActive(!MusicIsOn);
        if (MusicIsOn)
        {
            //MusicToggleImage.sprite = MusicOn;
            //MusicOnToggle.SetActive();
            AudioListener.volume = 1f;
        }
        else
        {
            //MusicToggleImage.sprite = MusicOff;
            AudioListener.volume = 0f;
        }
    }

    public void Pause()
    {
        gameObject.SetActive(true);
        Time.timeScale = 0f;
    }




}
