using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Emin;
using UnityEngine.EventSystems;


public class LevelManager : MonoBehaviour
{
    public Button[] Buttons;
    public int Level;
    MemoryManagement _MemoryManagement = new MemoryManagement();
    public Sprite lockbutton;
    public GameObject LoadingPanel;
    public Slider LoadingSlider;
    [SerializeField] AudioSource buttonMusic;
    
    

    void Start()
    {
        buttonMusic.volume = _MemoryManagement.DataRead_f("MainFx");
        int currentLevel = _MemoryManagement.DataRead_i("EndLevel") -4;
        int Index = 1;
        for (int i = 0; i < Buttons.Length; i++)
        {
            if (i + 1<= currentLevel)
            {
                Buttons[i].GetComponentInChildren<Text>().text = Index.ToString();
                int SceneIndex = Index + 4;
                Buttons[i].onClick.AddListener(delegate { SceneDowland(SceneIndex); });
                

            }
            else
            {
                Buttons[i].GetComponent<Image>().sprite = lockbutton;
                Buttons[i].enabled = false;
               // Buttons[i].interactable = false;
            }
            Index++;
        }
    }

    public void SceneDowland(int Index)
    {
        buttonMusic.Play();
        
        StartCoroutine(LoadAsync(Index));
        

    }

    /*public void SceneDowland()
    {
        Debug.Log(EventSystem.current.currentSelectedGameObject.GetComponentInChildren<Text>().text);
        SceneManager.LoadScene(int.Parse(EventSystem.current.currentSelectedGameObject.GetComponentInChildren<Text>().text) + 4);
    }*/

    public void TurnBack()
    {
        buttonMusic.Play();
        SceneManager.LoadScene(0);
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
