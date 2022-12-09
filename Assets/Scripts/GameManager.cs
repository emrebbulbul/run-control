using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Emin;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    
    
    public static int playerNumber = 1;
    public List<GameObject> Players;
    public List<GameObject> formationEffects;
    public List<GameObject> extinctionEffect;

    public List<GameObject> _manStainEffects;

    [Header("LEVEL DATA")]
    public List<GameObject> Enemys;
    public int HowManyEnemies;
    public GameObject _MainCharacter;
    public bool theGameOver;
    bool _didWeCome;
    [Header("HATS")]
    public GameObject[] Hats;
    public GameObject[] Sticks;
    public Material[] Materials;
    public SkinnedMeshRenderer _Renderer;
    public Material DefaultTheme;
  

    Library _libraryMat = new Library();
    MemoryManagement _MemoryManagement = new MemoryManagement();
    Scene _Scene;
    public AudioSource[] Sounds;
    public GameObject[] ProcessPanel;
    public Slider GameSoundSetting;
    public GameObject LoadingPanel;
    public Slider LoadingSlider;
    CustomizeManager customizeManager = new CustomizeManager();

    AdMobScript addMobScript = new AdMobScript();
    private void Awake()
    {
        Sounds[0].volume = _MemoryManagement.DataRead_f("GameAudio");
        GameSoundSetting.value= _MemoryManagement.DataRead_f("GameAudio");
        Sounds[1].volume = _MemoryManagement.DataRead_f("MainFx");
        Destroy(GameObject.FindWithTag("MenuSound"));
        CheckItems();
    }
    void Start()
    {
        CreateEnemy();
        _Scene = SceneManager.GetActiveScene();
        addMobScript.RequestInterstitial();
       
    }
    public void CreateEnemy()
    {
        for (int i = 0; i < HowManyEnemies; i++)
        {
            Enemys[i].SetActive(true);
        }

    }
    public void TriggerEnemy()
    {

        foreach (var item in Enemys)
        {
            if (item.activeInHierarchy)
            {
                item.GetComponent<EnemyController>().AnimationTrigger();
            }
        }
        _didWeCome = true;
        WarSituation();
    }

    void Update()
    {
       
    }

    void WarSituation()
    {

        if (_didWeCome)
        {
            if (playerNumber == 1 || HowManyEnemies == 0)
            {
                theGameOver = true;
                foreach (var item in Enemys)
                {
                    if (item.activeInHierarchy)
                    {
                        item.GetComponent<Animator>().SetBool("Attack", false);
                    }
                }

                foreach (var item in Players)
                {
                    if (item.activeInHierarchy)
                    {
                        item.GetComponent<Animator>().SetBool("Attack", false);
                    }
                }
                _MainCharacter.GetComponent<Animator>().SetBool("Attack", false);
                
                StartCoroutine(AddMob());
                if (playerNumber < HowManyEnemies || playerNumber == HowManyEnemies)
                {
                    ProcessPanel[3].SetActive(true);
                    
                                             


                }
                else
                {
                    if(playerNumber>5)
                    {

                        if (_Scene.buildIndex == _MemoryManagement.DataRead_i("EndLevel"))
                        {
                            _MemoryManagement.DataSave_int("Point", _MemoryManagement.DataRead_i("Point") + 600);
                            _MemoryManagement.DataSave_int("EndLevel", _MemoryManagement.DataRead_i("EndLevel") + 1);
                        }
                        
                    }
                    else
                    {
                        if (_Scene.buildIndex == _MemoryManagement.DataRead_i("EndLevel"))
                        {
                            _MemoryManagement.DataSave_int("Point", _MemoryManagement.DataRead_i("Point") + 200);
                            _MemoryManagement.DataSave_int("EndLevel", _MemoryManagement.DataRead_i("EndLevel") + 1);
                        }
                     
                    }
                    ProcessPanel[2].SetActive(true);

                }
            }
        }
       
    }

    public void ManManagement(string islemTuru,int GelenSayi,Transform Position)
    { 
    switch(islemTuru)
        {
            case "Carpma":
                _libraryMat.Carpma(GelenSayi,Players,Position, formationEffects);
                break;

            case "Toplama":
                _libraryMat.Toplama(GelenSayi, Players, Position, formationEffects);
                break;

            case "Cikartma":
                _libraryMat.Cikartma(GelenSayi, Players,extinctionEffect);
                break;

            case "Bolme":
                _libraryMat.Bolme(GelenSayi, Players, extinctionEffect);
                break;

        }
    
    }

    public void CreateExtinctionEffect(Vector3 Position, bool Sledge=false, bool situation = false)
    {
        foreach (var item in extinctionEffect)
        {
            if (!item.activeInHierarchy)
            {
                item.SetActive(true);
                item.transform.position = Position;
                item.GetComponent<ParticleSystem>().Play();
                item.GetComponent<AudioSource>().Play();
                if (!situation)
                    playerNumber--;
                else
                    HowManyEnemies--;
                break;
            }
        }
        
        if (Sledge)
        {
            Vector3 newPoz = new Vector3(Position.x, 0.005f, Position.z);
            foreach (var item in _manStainEffects)
            {
                if (!item.activeInHierarchy)
                {
                    item.SetActive(true);
                    item.transform.position = newPoz;
                   
                    break;
                }
            }
        }
        if (!theGameOver)
            WarSituation();
        
    }
    
    IEnumerator AddMob()
    { 
        yield return new WaitForSeconds(2.9f);
        addMobScript.InterstitialAdShow();
    }
    public void CheckItems()
    {
        if (_MemoryManagement.DataRead_i("ActiveHat")!=-1)
        Hats[_MemoryManagement.DataRead_i("ActiveHat")].SetActive(true);
        if (_MemoryManagement.DataRead_i("ActiveBat") != -1)
        Sticks[_MemoryManagement.DataRead_i("ActiveBat")].SetActive(true);


        if(_MemoryManagement.DataRead_i("ActiveTheme") != -1)
        {
            Material[] mats = _Renderer.materials;
            mats[0] = Materials[_MemoryManagement.DataRead_i("ActiveTheme")];
            _Renderer.materials = mats;


        }
        else
        {
            Material[] mats = _Renderer.materials;
            mats[0] = DefaultTheme;
            _Renderer.materials = mats;

        }

        }

    public void ExitButton(string situation)
    {
        Sounds[1].Play();
        Time.timeScale = 0;

        if (situation == ("stop"))
        {
            ProcessPanel[0].SetActive(true);
        }
        else if (situation == ("goon"))
        {
            ProcessPanel[0].SetActive(false);
            Time.timeScale = 1;
        }
            
        else if (situation == ("replay"))
        {
            
            SceneManager.LoadScene(_Scene.buildIndex);
            Time.timeScale = 1;
            
        }
        else if (situation == ("homepage"))
        {
            SceneManager.LoadScene(0);
            Time.timeScale = 1;
        }
        


    }
    public void Settings(string situation)
    {
        if (situation=="setting")
        {
            ProcessPanel[1].SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            ProcessPanel[1].SetActive(false);
            Time.timeScale = 1;
        }
    }

    public void SoundSetting()
    {
        _MemoryManagement.DataSave_float("GameAudio", GameSoundSetting.value);
        Sounds[0].volume = GameSoundSetting.value;

          

    }
    public void NextLevel()
    {
        
        StartCoroutine(LoadAsync(_Scene.buildIndex + 1));
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



