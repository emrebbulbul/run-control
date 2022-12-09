using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Emin;
using TMPro;
using UnityEngine.SceneManagement;




public class CustomizeManager : MonoBehaviour
{
    public Text PointText;
   
   
    public GameObject[] ProcessPanels;
    public GameObject ProcessCanvas;
    public GameObject[] GeneralPanel;
    public Button[] ProcessButtons;
    //public TextMeshProUGUI BuyText;
    public Text BuyText;
    int ActiveprocessPanelIndex;
    [Header("HATS")]
    public GameObject[] Hats;
    public Button[] HatButtons;
    public Text HotText;
    [Header("STIKS")]
    public GameObject[] Sticks;
    public Button[] StickButtons;
    public Text BatText;
    [Header("MATERIALS")]
    public Material[] Materials;
    public Material DefaultTheme;
    public Button[] MaterialsButtons;
    public Text MaterialText;
    public SkinnedMeshRenderer _Renderer;
    [SerializeField] AudioSource[] Musics;

    int HatIndex = -1;
    int BatIndex = -1;
    int MaterialIndex = -1;
    MemoryManagement _MemoryManagement = new MemoryManagement();
    DataManagement _DataManagement = new DataManagement();
    [Header("GENERAL DATAS")]
    public  List<Item›nformation> _item›nformation = new List<Item›nformation>();

    public Animator  WasRecorded_Animator;

    void Start()
    {
        PointText.text = _MemoryManagement.DataRead_i("Point").ToString();
        _DataManagement.Load();
        _item›nformation = _DataManagement.TransferList();
        CheckStatus(0,true);
        CheckStatus(1, true);
        CheckStatus(2, true);
        foreach (var item in Musics)
        {
            item.volume = _MemoryManagement.DataRead_f("MainFx");
        }
        
    }


    public void CheckStatus(int Episode,bool operation=false)
    {

        if (Episode==0)
        {
            if (_MemoryManagement.DataRead_i("ActiveHat") == -1)
            {
                foreach (var item in Hats)
                {
                    item.SetActive(false);
                }


                BuyText.text = "BUY";
                ProcessButtons[0].interactable = false;
                ProcessButtons[1].interactable = false;

                if (!operation)
                {
                    HatIndex = -1;
                    HotText.text = "Hat None";

                }

            }
            else
            {

                foreach (var item in Hats)
                {
                    item.SetActive(false);
                }
                HatIndex = _MemoryManagement.DataRead_i("ActiveHat");
                Hats[HatIndex].SetActive(true);

                HotText.text = _item›nformation[HatIndex].Item_Name;
                BuyText.text = "BUY";
                ProcessButtons[0].interactable = false;
                ProcessButtons[1].interactable = true;

            }
        }
        else if (Episode==1)
        {

            if (_MemoryManagement.DataRead_i("ActiveBat") == -1)
            {
                foreach (var item in Sticks)
                {
                    item.SetActive(false);
                }

                ProcessButtons[0].interactable = false;
                ProcessButtons[1].interactable = false;
                BuyText.text = "BUY";

                if (!operation)
                {
                    BatIndex = -1;
                    BatText.text = "Stick None";
                }
               

            }
            else
            {

                foreach (var item in Sticks)
                {
                    item.SetActive(false);
                }
                BatIndex = _MemoryManagement.DataRead_i("ActiveBat");
                Sticks[BatIndex].SetActive(true);

                BatText.text = _item›nformation[BatIndex + 3].Item_Name;
                BuyText.text = "BUY";
                ProcessButtons[0].interactable = false;
                ProcessButtons[1].interactable = true;
            }
        }
        else 
        {
            if (_MemoryManagement.DataRead_i("ActiveTheme") == -1)
            {
                if (!operation)
                {
                    BuyText.text = "BUY";
                    MaterialIndex = -1;
                    MaterialText.text = "Theme None";
                    ProcessButtons[0].interactable = false;
                    ProcessButtons[1].interactable = false;

                }
                else
                {
                    Material[] mats = _Renderer.materials;
                    mats[0] = DefaultTheme;
                    _Renderer.materials = mats;
                    BuyText.text = "BUY";

                }


            }
            else
            {

              
                MaterialIndex = _MemoryManagement.DataRead_i("ActiveTheme");
                Material[] mats = _Renderer.materials;
                mats[0] = Materials[MaterialIndex];
                _Renderer.materials = mats;


                MaterialText.text = _item›nformation[MaterialIndex + 6].Item_Name;
                BuyText.text = "BUY";
                ProcessButtons[0].interactable = false;
                ProcessButtons[1].interactable = true;


            }
        }
      
    }

  
    void BuyResult(int Index)
    {
        _item›nformation[Index].PurchaseStatus = true;
        _MemoryManagement.DataSave_int("Point", _MemoryManagement.DataRead_i("Point") - _item›nformation[Index].Point);
        BuyText.text = "BUY";
        ProcessButtons[0].interactable = false;
        ProcessButtons[1].interactable = true;
        PointText.text = _MemoryManagement.DataRead_i("Point").ToString();


    }

    public void Buy()
    {
        Musics[1].Play();
        if (ActiveprocessPanelIndex!=-1)
        {
            switch (ActiveprocessPanelIndex)
            {
                case 0:
                    BuyResult(HatIndex);
                    break;
                case 1:
                    int Index = BatIndex + 3;
                    BuyResult(Index);
                    break;

                case 2:
                    int Index2 = MaterialIndex + 6;
                    BuyResult(Index2);
                    break;
            }
        }

    }
    public void SaveResult(string key,int Index)
    {
        _MemoryManagement.DataSave_int(key, Index);
        ProcessButtons[1].interactable = false;
        if (!WasRecorded_Animator.GetBool("ok"))
            WasRecorded_Animator.SetBool("ok", true);

    }
    public void Save()
    {
        Musics[2].Play();
        if (ActiveprocessPanelIndex != -1)
        {
            switch (ActiveprocessPanelIndex)
            {
                case 0:
                    SaveResult("ActiveHat", HatIndex);
                   
                    break;
                case 1:
                    SaveResult("ActiveBat", BatIndex);
                   
                    break;
                case 2:
                    SaveResult("ActiveTheme", MaterialIndex);
                    
                    break;
            }
        }
        
    }

    public void HatDirectionButtons(string process)
    {
        if (process == "Further")
        {
            if(HatIndex==-1)
            {
                HatIndex = 0;
                Hats[HatIndex].SetActive(true);
                HotText.text = _item›nformation[HatIndex].Item_Name;
                if (!_item›nformation[HatIndex].PurchaseStatus)
                {
                    BuyText.text = _item›nformation[HatIndex].Point + "- BUY";
                    ProcessButtons[1].interactable = false;

                    if (_MemoryManagement.DataRead_i("Point")< _item›nformation[HatIndex].Point)
                    ProcessButtons[0].interactable = false;
                    else
                        ProcessButtons[0].interactable = true;



                }
                else
                {
                    BuyText.text = "BUY";
                    ProcessButtons[0].interactable = false;
                    ProcessButtons[1].interactable = true;
                }
            }
            else
            {
                Hats[HatIndex].SetActive(false);
                HatIndex++;
                Hats[HatIndex].SetActive(true);
                HotText.text = _item›nformation[HatIndex].Item_Name;

                if (!_item›nformation[HatIndex].PurchaseStatus)
                {
                    BuyText.text = _item›nformation[HatIndex].Point + "- BUY";
                    ProcessButtons[1].interactable = false;

                    if (_MemoryManagement.DataRead_i("Point") < _item›nformation[HatIndex].Point)
                        ProcessButtons[0].interactable = false;
                    else
                        ProcessButtons[0].interactable = true;
                }
                else
                {
                    BuyText.text = "BUY";
                    ProcessButtons[0].interactable = false;
                    ProcessButtons[1].interactable = true;
                }
            }

            if (HatIndex == Hats.Length - 1)
                HatButtons[1].interactable = false;
            else
                HatButtons[1].interactable = true;
            if (HatIndex != -1)
                HatButtons[0].interactable = true;



        }
       
        else
        {
            if (HatIndex!=-1)
            {
                Hats[HatIndex].SetActive(false);
                HatIndex--;
                if (HatIndex!=-1)
                {
                    Hats[HatIndex].SetActive(true);
                    HatButtons[0].interactable = true;
                    HotText.text = _item›nformation[HatIndex].Item_Name;
                    if (!_item›nformation[HatIndex].PurchaseStatus)
                    {
                        BuyText.text = _item›nformation[HatIndex].Point + "- BUY";
                        ProcessButtons[1].interactable = false;

                        if (_MemoryManagement.DataRead_i("Point") < _item›nformation[HatIndex].Point)
                            ProcessButtons[0].interactable = false;
                        else
                            ProcessButtons[0].interactable = true;
                    }
                    else
                    {
                        BuyText.text = "BUY";
                        ProcessButtons[0].interactable = false;
                        ProcessButtons[1].interactable = true;
                    }
                }
                else
                {
                    HatButtons[0].interactable = false;
                    HotText.text = "Hat None";
                    BuyText.text = "BUY";
                    ProcessButtons[0].interactable = false;
                }
            }
            else
            {
                HatButtons[0].interactable = false;
                HotText.text = "Hat None";
                BuyText.text = "BUY";
                ProcessButtons[0].interactable = false;
            }


            if (HatIndex!= Hats.Length - 1)
                HatButtons[1].interactable = true;
        }

        Musics[0].Play();
    }

    public void BatDirectionButtons(string process)
    {
        if (process == "Further")
        {
            if (BatIndex == -1)
            {
                BatIndex = 0;
                Sticks[BatIndex].SetActive(true);
                BatText.text = _item›nformation[BatIndex + 3].Item_Name;

                if (!_item›nformation[BatIndex + 3].PurchaseStatus)
                {
                    BuyText.text = _item›nformation[BatIndex + 3].Point + "- BUY";
                    ProcessButtons[1].interactable = false;

                    if (_MemoryManagement.DataRead_i("Point") < _item›nformation[BatIndex + 3].Point)
                        ProcessButtons[0].interactable = false;
                    else
                        ProcessButtons[0].interactable = true;
                }
                else
                {
                    BuyText.text = "BUY";
                    ProcessButtons[0].interactable = false;
                    ProcessButtons[1].interactable = true;
                }
            }
            else
            {
                Sticks[BatIndex].SetActive(false);
                BatIndex++;
                Sticks[BatIndex].SetActive(true);
                BatText.text = _item›nformation[BatIndex + 3].Item_Name;

                if (!_item›nformation[BatIndex + 3].PurchaseStatus)
                {
                    BuyText.text = _item›nformation[BatIndex + 3].Point + "- BUY";
                    ProcessButtons[1].interactable = false;

                    if (_MemoryManagement.DataRead_i("Point") < _item›nformation[BatIndex + 3].Point)
                        ProcessButtons[0].interactable = false;
                    else
                        ProcessButtons[0].interactable = true;
                }
                else
                {
                    BuyText.text = "BUY";
                    ProcessButtons[0].interactable = false;
                    ProcessButtons[1].interactable = true;
                }
            }

            if (BatIndex == Hats.Length - 1)
                StickButtons[1].interactable = false;
            else
                StickButtons[1].interactable = true;
            if (BatIndex != -1)
                StickButtons[0].interactable = true;



        }

        else
        {
            if (BatIndex != -1)
            {
                Sticks[BatIndex].SetActive(false);
                BatIndex--;
                if (BatIndex != -1)
                {
                    Sticks[BatIndex].SetActive(true);
                    StickButtons[0].interactable = true;
                    BatText.text = _item›nformation[BatIndex + 3].Item_Name;
                    if (!_item›nformation[BatIndex + 3].PurchaseStatus)
                    {
                        BuyText.text = _item›nformation[BatIndex + 3].Point + "- BUY";
                        ProcessButtons[1].interactable = false;

                        if (_MemoryManagement.DataRead_i("Point") < _item›nformation[BatIndex + 3].Point)
                            ProcessButtons[0].interactable = false;
                        else
                            ProcessButtons[0].interactable = true;
                    }
                    else
                    {
                        BuyText.text = "BUY";
                        ProcessButtons[0].interactable = false;
                        ProcessButtons[1].interactable = true;
                    }
                }
                else
                {
                    StickButtons[0].interactable = false;
                    BatText.text = "Stick None";
                    BuyText.text = "BUY";
                    ProcessButtons[0].interactable = false;
                }
            }
            else
            {
                StickButtons[0].interactable = false;
                BatText.text = "Stick None";
                BuyText.text = "BUY";
                ProcessButtons[0].interactable = false;
            }


            if (BatIndex != Hats.Length - 1)
                StickButtons[1].interactable = true;
        }

        Musics[0].Play();
    }

    public void MaterialDirectionButtons(string process)
    {
        if (process == "Further")
        {
            if (MaterialIndex == -1)
            {
                MaterialIndex = 0;
                Material[] mats = _Renderer.materials;
                mats[0] = Materials[MaterialIndex];
                _Renderer.materials = mats;
              
                MaterialText.text = _item›nformation[MaterialIndex + 6].Item_Name;
                if (!_item›nformation[MaterialIndex + 6].PurchaseStatus)
                {
                    BuyText.text = _item›nformation[MaterialIndex + 6].Point + "- BUY";
                    ProcessButtons[1].interactable = false;

                    if (_MemoryManagement.DataRead_i("Point") < _item›nformation[MaterialIndex + 6].Point)
                        ProcessButtons[0].interactable = false;
                    else
                        ProcessButtons[0].interactable = true;
                }
                else
                {
                    BuyText.text = "BUY";
                    ProcessButtons[0].interactable = false;
                    ProcessButtons[1].interactable = true;
                }
            }
            else
            {
               
                MaterialIndex++;
                Material[] mats = _Renderer.materials;
                mats[0] = Materials[MaterialIndex];
                _Renderer.materials = mats;
                MaterialText.text = _item›nformation[MaterialIndex + 6].Item_Name;
                if (!_item›nformation[MaterialIndex + 6].PurchaseStatus)
                {
                    BuyText.text = _item›nformation[MaterialIndex + 6].Point + "- BUY";
                    ProcessButtons[1].interactable = false;

                    if (_MemoryManagement.DataRead_i("Point") < _item›nformation[MaterialIndex + 6].Point)
                        ProcessButtons[0].interactable = false;
                    else
                        ProcessButtons[0].interactable = true;
                }
                else
                {
                    BuyText.text = "BUY";
                    ProcessButtons[0].interactable = false;
                    ProcessButtons[1].interactable = true;
                }
            }

            if (MaterialIndex == Materials.Length - 1)
                MaterialsButtons[1].interactable = false;
            else
                MaterialsButtons[1].interactable = true;
            if (MaterialIndex != -1)
                MaterialsButtons[0].interactable = true;



        }

        else
        {
            if (MaterialIndex != -1)
            {
                
                MaterialIndex--;
                if (MaterialIndex != -1)
                {
                    Material[] mats = _Renderer.materials;
                    mats[0] = Materials[MaterialIndex];
                    _Renderer.materials = mats;
                    MaterialsButtons[0].interactable = true;
                    MaterialText.text = _item›nformation[MaterialIndex + 6].Item_Name;
                    if (!_item›nformation[MaterialIndex + 6].PurchaseStatus)
                    {
                        BuyText.text = _item›nformation[MaterialIndex + 6].Point + "- BUY";
                        ProcessButtons[1].interactable = false;

                        if (_MemoryManagement.DataRead_i("Point") < _item›nformation[MaterialIndex + 6].Point)
                            ProcessButtons[0].interactable = false;
                        else
                            ProcessButtons[0].interactable = true;
                    }
                    else
                    {
                        BuyText.text = "BUY";
                        ProcessButtons[0].interactable = false;
                        ProcessButtons[1].interactable = true;
                    }
                }
                else
                {
                    Material[] mats = _Renderer.materials;
                    mats[0] = DefaultTheme;
                    _Renderer.materials = mats;
                    MaterialsButtons[0].interactable = false;
                    MaterialText.text = "Theme None";
                    BuyText.text = "BUY";
                    ProcessButtons[0].interactable = false;
                }
            }
            else
            {
                Material[] mats = _Renderer.materials;
                mats[0] = DefaultTheme;
                _Renderer.materials = mats;
                MaterialsButtons[0].interactable = false;
                MaterialText.text = "Theme None";
                BuyText.text = "BUY";
                ProcessButtons[0].interactable = false;
            }


            if (MaterialIndex != Materials.Length - 1)
                MaterialsButtons[1].interactable = true;
        }
        Musics[0].Play();

    }


    public void ProcessPanelSticker(int Index)
    {
        Musics[0].Play();
        CheckStatus(Index);
        GeneralPanel[0].SetActive(true);
        ActiveprocessPanelIndex = Index;
        ProcessPanels[Index].SetActive(true);
        GeneralPanel[1].SetActive(true);
        ProcessCanvas.SetActive(false);
       
    }
    public void TurnBack()
    {
        Musics[0].Play();
        GeneralPanel[0].SetActive(false);
        ProcessCanvas.SetActive(true);
        GeneralPanel[1].SetActive(false);
        ProcessPanels[ActiveprocessPanelIndex].SetActive(false);
        CheckStatus(ActiveprocessPanelIndex,true);
        ActiveprocessPanelIndex = -1;
     
    }

    public void ReturnToMainMenu()
    {
        Musics[0].Play();
        _DataManagement.Save(_item›nformation);
        SceneManager.LoadScene(0);
    
    }
}
