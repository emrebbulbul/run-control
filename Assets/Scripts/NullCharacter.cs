using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NullCharacter : MonoBehaviour
{
    public SkinnedMeshRenderer _Renderer;
    public Material _tobeAppointedMaterial;
    public NavMeshAgent navMesh;
    public Animator _anim;
    public GameObject target;
    public GameManager _Gamemanager;
    bool Contact;


    private void LateUpdate()
    {
        if (Contact)
        navMesh.SetDestination(target.transform.position);
    }
    Vector3 PositionGive()
    {
        return new Vector3(transform.position.x, 0.23f, transform.position.z);

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("LowPlayer") || other.CompareTag("Player"))
        {

            if(gameObject.CompareTag("NullCharacter"))
            {
                ChangeMaterialAndTrigger();
                Contact = true;
                GetComponent<AudioSource>().Play();

            }

        }

        else if (other.CompareTag("PinBox"))
        {

            _Gamemanager.CreateExtinctionEffect(PositionGive());
            gameObject.SetActive(false);
        }


        else if (other.CompareTag("Saw"))
        {

            _Gamemanager.CreateExtinctionEffect(PositionGive());
            gameObject.SetActive(false);
        }


        else if (other.CompareTag("Sledge"))
        {

            _Gamemanager.CreateExtinctionEffect(PositionGive(), true);
            gameObject.SetActive(false);
        }


        else if (other.CompareTag("Enemy"))
        {
          
            _Gamemanager.CreateExtinctionEffect(PositionGive(), false, false);
            gameObject.SetActive(false);
        }
    }
    void ChangeMaterialAndTrigger()
    {
        Material[] mats = _Renderer.materials;
        mats[0] = _tobeAppointedMaterial;
        _Renderer.materials = mats;
        _anim.SetBool("Attack", true);
        gameObject.tag = "LowPlayer";
        GameManager.playerNumber++;

    }

}
