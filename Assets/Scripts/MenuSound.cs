using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSound : MonoBehaviour
{
     private static GameObject instance;
    [SerializeField] AudioSource Sound;

    void Start()
    {

        Sound.volume = PlayerPrefs.GetFloat("MainSound");
        DontDestroyOnLoad(gameObject);
        if (instance==null)
        {
            instance = gameObject;

        }
        else
        {
            Destroy(gameObject);
        }



    }

  
    void Update()
    {
        Sound.volume = PlayerPrefs.GetFloat("MainSound");
    }
}
