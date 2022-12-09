using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


namespace Emin
{
    public class Library
    {
        public  void Carpma(int GelenSayi, List<GameObject> Players, Transform Position, List<GameObject> CreateEffects)

        {
            int loopNumber = (GameManager.playerNumber * GelenSayi) - GameManager.playerNumber;
            int number = 0;
            foreach (var item in Players)
            {

                if (number < loopNumber)
                {
                    if (!item.activeInHierarchy)
                    {
                        foreach (var item2 in CreateEffects)
                        {
                            if (!item2.activeInHierarchy)
                            {
                              
                                item2.SetActive(true);
                                item2.transform.position = Position.position; 
                                item2.GetComponent<ParticleSystem>().Play();
                                item2.GetComponent<AudioSource>().Play();
                                break;
                            }
                        }
                        item.transform.position = Position.position;
                        item.SetActive(true);
                        number++;

                    }
                }


                else
                {
                    number = 0;
                    break;
                }

            }

            GameManager.playerNumber *= GelenSayi;
        }
        public  void Toplama(int GelenSayi, List<GameObject> Players, Transform Position, List<GameObject> CreateEffects)

        {
            int number1 = 0;
            foreach (var item in Players)
            {

                if (number1 < GelenSayi)
                {
                    if (!item.activeInHierarchy)
                    {
                        foreach (var item2 in CreateEffects)
                        {
                            if (!item2.activeInHierarchy)
                            {

                                item2.SetActive(true);
                                item2.transform.position = Position.position;
                                item2.GetComponent<ParticleSystem>().Play();
                                item2.GetComponent<AudioSource>().Play();
                                break;
                            }
                        }
                        item.transform.position = Position.position;
                        item.SetActive(true);
                        number1++;

                    }
                }


                else
                {
                    number1 = 0;
                    break;
                }


            }
            GameManager.playerNumber += GelenSayi;

        }
        public  void Cikartma(int GelenSayi, List<GameObject> Players, List<GameObject> ExtinctionEffects)

        {
            if (GameManager.playerNumber < GelenSayi)
            {


                foreach (var item in Players)
                {
                    foreach (var item2 in ExtinctionEffects)
                    {
                        if(!item2.activeInHierarchy)
                        {
                            Vector3 newPoz = new Vector3(item.transform.position.x, 0.23f, item.transform.position.z);
                            item2.SetActive(true);
                            item2.transform.position = newPoz;
                            item2.GetComponent<ParticleSystem>().Play();
                            item2.GetComponent<AudioSource>().Play();

                            break;
                        }
                    }



                    item.transform.position = Vector3.zero;
                    item.SetActive(false);
                }
                GameManager.playerNumber = 1;
            }
            else
            {
                int number2 = 0;
                foreach (var item in Players)
                {

                    if (number2 != GelenSayi)
                    {
                        if (item.activeInHierarchy)
                        {
                            foreach (var item2 in ExtinctionEffects)
                            {
                                if (!item2.activeInHierarchy)
                                {
                                    Vector3 newPoz = new Vector3(item.transform.position.x, 0.23f, item.transform.position.z);
                                    item2.SetActive(true);
                                    item2.transform.position = item.transform.position;
                                    item2.GetComponent<ParticleSystem>().Play();
                                    item2.GetComponent<AudioSource>().Play();

                                    break;
                                }
                            }



                            item.transform.position = Vector3.zero;
                            item.SetActive(false);
                            number2++;

                        }

                    }


                    else
                    {
                        number2 = 0;
                        break;
                    }


                }
                GameManager.playerNumber -= GelenSayi;

            }

        }
        public  void Bolme(int GelenSayi, List<GameObject> Players, List<GameObject> ExtinctionEffects)
        {
            if (GameManager.playerNumber <= GelenSayi)
            {
                foreach (var item in Players)
                {
                    foreach (var item2 in ExtinctionEffects)
                    {
                        if (!item2.activeInHierarchy)
                        {
                            Vector3 newPoz = new Vector3(item.transform.position.x, 0.23f, item.transform.position.z);
                            item2.SetActive(true);
                            item2.transform.position = newPoz;
                            item2.GetComponent<ParticleSystem>().Play();
                            item2.GetComponent<AudioSource>().Play();

                            break;
                        }
                    }
                    item.transform.position =Vector3.zero;
                    item.SetActive(false);
                }
                GameManager.playerNumber = 1;
            }
            else
            {
                int dividing = GameManager.playerNumber / GelenSayi;
                int number3 = 0;
                foreach (var item in Players)
                {

                    if (number3 != dividing)
                    {
                        if (item.activeInHierarchy)
                        {
                            foreach (var item2 in ExtinctionEffects)
                            {
                                if (!item2.activeInHierarchy)
                                {
                                    Vector3 newPoz = new Vector3(item.transform.position.x, 0.23f, item.transform.position.z);
                                    item2.SetActive(true);
                                    item2.transform.position = newPoz;
                                    item2.GetComponent<ParticleSystem>().Play();
                                    item2.GetComponent<AudioSource>().Play();

                                    break;
                                }
                            }
                            item.transform.position = Vector3.zero;
                            item.SetActive(false);
                            number3++;

                        }

                    }


                    else
                    {
                        number3 = 0;
                        break;
                    }


                }
                if (GameManager.playerNumber % GelenSayi == 0)
                {
                    GameManager.playerNumber /= GelenSayi;

                }
                else if (GameManager.playerNumber % GelenSayi == 1)
                {
                    GameManager.playerNumber /= GelenSayi;
                    GameManager.playerNumber++;
                }
                else if(GameManager.playerNumber % GelenSayi == 2)
                {
                    GameManager.playerNumber /= GelenSayi;
                    GameManager.playerNumber+=2;
                }


            }

        }

        }
    public class MemoryManagement
    {
        
        public void  DataSave_string(string Key,string value)
        {
            PlayerPrefs.SetString(Key,value);
            PlayerPrefs.Save();

        }
        public void DataSave_int(string Key, int value)
        {
            PlayerPrefs.SetInt(Key, value);
            PlayerPrefs.Save();

        }
        public void DataSave_float(string Key, float value)
        {

            PlayerPrefs.SetFloat(Key, value);
            PlayerPrefs.Save();

        }
       
        public string  DataRead(string Key)
        {
            return PlayerPrefs.GetString(Key);
        }
        public int DataRead_i(string Key)
        {
            return PlayerPrefs.GetInt(Key);
        }
        public float DataRead_f(string Key)
        {
            return PlayerPrefs.GetFloat(Key);
        }
        
        public void ControlAndDefind()
        {
            if (!PlayerPrefs.HasKey("EndLevel"))
            {
                PlayerPrefs.SetInt("EndLevel", 5);
                PlayerPrefs.SetInt("Point", 100);
                PlayerPrefs.SetInt("ActiveHat", -1);
                PlayerPrefs.SetInt("ActiveBat", -1);
                PlayerPrefs.SetInt("ActiveTheme", -1);
                PlayerPrefs.SetFloat("MainSound", 1);
                PlayerPrefs.SetFloat("MainFx", 1);
                PlayerPrefs.SetFloat("GameAudio", 1);

            }
            
        }

    }
   
    [Serializable]
    public class Item›nformation
    {
        public int GroupIndex;
        public int ItemIndex;
        public string Item_Name;
        public int Point;
        public bool PurchaseStatus;
    }

    public class DataManagement
    {
        public void Save(List<Item›nformation> _item›nformation)
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.OpenWrite(Application.persistentDataPath + "/ItemDatas.gd");
            bf.Serialize(file, _item›nformation);
            file.Close();
        }
       
        List<Item›nformation> _itemInlist;
        public void Load()
        {
            if (File.Exists(Application.persistentDataPath + "/ItemDatas.gd"))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(Application.persistentDataPath + "/ItemDatas.gd", FileMode.Open);
                _itemInlist = (List<Item›nformation>)bf.Deserialize(file);
                file.Close();
            }
        }


        public List<Item›nformation> TransferList()
        {
            return _itemInlist;
           
        }

        public void InitialSetupFileCreation(List<Item›nformation> _item›nformation)
        {
            if (!File.Exists(Application.persistentDataPath + "/ItemDatas.gd"))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Create(Application.persistentDataPath + "/ItemDatas.gd");
                bf.Serialize(file, _item›nformation);
                file.Close();
            }

        }

    }
}

