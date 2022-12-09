using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Propeller : MonoBehaviour
{
    public Animator _animator;
    public float standbyTime;
    public BoxCollider _Wind;


    public void AnimationState(string state)
    {

        if (state == "true")
        {
            _animator.SetBool("Play", true);
            _Wind.enabled = true;

        }
            
        else
        {
            _animator.SetBool("Play", false);
            StartCoroutine(AnimationTrigger());
            _Wind.enabled = false;


        }
                 
    }


    IEnumerator AnimationTrigger()
    {


        yield return new WaitForSeconds(standbyTime);
        AnimationState("true");
    }

}
