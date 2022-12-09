using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public GameObject attack_Aim;
   public NavMeshAgent _navMeshAgent;
    public Animator _Animator;
    public GameManager _Gamemanager;
    bool _attackdidStart;

    public void AnimationTrigger()
    {
        _Animator.SetBool("Attack", true);
        _attackdidStart = true;
    }

    void LateUpdate()
    {
        if (_attackdidStart)
            _navMeshAgent.SetDestination(attack_Aim.transform.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("LowPlayer"))
        {
            Vector3 newPoz = new Vector3(transform.position.x, 0.23f, transform.position.z);
            _Gamemanager.CreateExtinctionEffect(newPoz,false,true);
            gameObject.SetActive(false);
        }

    }
}
