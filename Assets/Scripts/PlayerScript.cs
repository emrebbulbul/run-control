using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerScript : MonoBehaviour
{

    public GameManager _Gamemanager;
    public GameObject _Camera;
    public bool _finishCome;
    public GameObject _whereToGo;
    public Slider _Slider;
    public GameObject wayPoint;

   
    private void FixedUpdate()
    {
        if(!_finishCome)
        transform.Translate(Vector3.forward * .5f * Time.deltaTime);
    }
    private void Start()
    {
        float difference = Vector3.Distance(transform.position, wayPoint.transform.position);
        _Slider.maxValue = difference;
    }
    void Update()
    {

        if (Time.timeScale != 0)
        {
            if (_finishCome)
            {
                transform.position = Vector3.Lerp(transform.position, _whereToGo.transform.position, .015f);
                if (_Slider.value != 0)
                    _Slider.value -= .005f;
            }
            else
            {
                float difference = Vector3.Distance(transform.position, wayPoint.transform.position);
                _Slider.value = difference;



                {
                    if (Input.GetKey(KeyCode.Mouse0))
                    {
                        if (Input.GetAxis("Mouse X") < 0)
                        {
                            transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x - 0.14f, transform.position.y, transform.position.z), .14f);
                        }
                        if (Input.GetAxis("Mouse X") > 0)
                        {
                            transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x + 0.14f, transform.position.y, transform.position.z), .14f);
                        }

                    }
                }

            }

        }

     

           
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Carpma") || other.CompareTag("Toplama") || other.CompareTag("Cikartma") || other.CompareTag("Bolme"))
        {
            int number = int.Parse(other.name);
            _Gamemanager.ManManagement(other.tag, number, other.transform);

        }

        else if (other.CompareTag("Trigger"))
        {

            _Camera.GetComponent<Camera>()._finishCome = true;
            _Gamemanager.TriggerEnemy();
            _finishCome = true;

        }
        else if (other.CompareTag("NullCharacter"))
        {

            _Gamemanager.Players.Add(other.gameObject);
           

        }

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Direct") || collision.gameObject.CompareTag("PinBox"))
        {

            if (transform.position.x > 0 )
            transform.position = new Vector3(transform.position.x - 0.2f, transform.position.y, transform.position.z);
            else
                transform.position = new Vector3(transform.position.x + 0.2f, transform.position.y, transform.position.z);


        }
    }
}
