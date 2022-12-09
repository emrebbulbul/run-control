using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LowPlayer : MonoBehaviour
{
    NavMeshAgent navMesh;
   public GameManager _Gamemanager;
    public GameObject target;

    private void Start()
    {
        navMesh = GetComponent<NavMeshAgent>();
       
    }

    private void LateUpdate()
    {
        navMesh.SetDestination(target.transform.position);
    }
    Vector3 PositionGive()
    {
       return  new Vector3(transform.position.x, 0.23f, transform.position.z);

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PinBox"))
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
        else if (other.CompareTag("NullCharacter"))
        {

            _Gamemanager.Players.Add(other.gameObject);
           

        }

    }
    
}
