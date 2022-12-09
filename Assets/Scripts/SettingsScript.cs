using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Emin;

public class SettingsScript : MonoBehaviour
{
    [SerializeField] AudioSource buttonMusic;
    public Slider MainSound;
    public Slider MainFx;
    public Slider GameAudio;
    MemoryManagement _MemoryManagement = new MemoryManagement();
    public GameObject ViewPanel;

    void Start()
    {
        buttonMusic.volume = _MemoryManagement.DataRead_f("MainFx");


        MainSound.value = _MemoryManagement.DataRead_f("MainSound");
        MainFx.value = _MemoryManagement.DataRead_f("MainFx");
        GameAudio.value = _MemoryManagement.DataRead_f("GameAudio");
    }
   public void SoundSetting(string Adjustment)
    {
        switch (Adjustment)
        {
            case "mainSound":
                _MemoryManagement.DataSave_float("MainSound", MainSound.value);

                break;
            case "mainFx":
                _MemoryManagement.DataSave_float("MainFx", MainFx.value);
                break;
            case "gameAudio":
                _MemoryManagement.DataSave_float("GameAudio", GameAudio.value);
                break;
        }
    }
    public void TurnBack()
    {
        buttonMusic.Play();
        SceneManager.LoadScene(0);
    }

    public void ChangeLanguage()
    {
        buttonMusic.Play();
    }
    public void ViewButton()
    {
        ViewPanel.SetActive(true);
    }
    public void ViewCancelButton()
    {
        ViewPanel.SetActive(false);
    }



}
