using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Emin;
using UnityEngine.UI;

public class MainMenüManager : MonoBehaviour
{
    MemoryManagement _MemoryManagement = new MemoryManagement();
    DataManagement _DataManagement = new DataManagement();
    public GameObject _ExitPanel;
    public GameObject LoadingPanel;
    public Slider LoadingSlider;
    
    public List<Itemİnformation> _itemİnformation = new List<Itemİnformation>();
    [SerializeField] AudioSource buttonMusic;

    private void Start()
    {
        _MemoryManagement.ControlAndDefind();
        _DataManagement.InitialSetupFileCreation(_itemİnformation);
        buttonMusic.volume = _MemoryManagement.DataRead_f("MainFx");
    }
    public void SceneDowland(int Index)
    {
        buttonMusic.Play();
        SceneManager.LoadScene(Index);

    }

    public void Play()
    {
        //SceneManager.LoadScene(_MemoryManagement.DataRead_i("EndLevel"));
        //SceneManager.LoadScene(5);
        buttonMusic.Play();
        StartCoroutine(LoadAsync(5));
    }
   
    public void ExitButton(string situation)
    {
        buttonMusic.Play();
        if (situation == ("YES"))
            Application.Quit();
        else if (situation == ("Exit"))
             _ExitPanel.SetActive(true);
        else
            _ExitPanel.SetActive(false);


    }
    IEnumerator LoadAsync(int SceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(SceneIndex);
        LoadingPanel.SetActive(true);
       
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            LoadingSlider.value = progress;
            yield return null;
        }


    }
}





