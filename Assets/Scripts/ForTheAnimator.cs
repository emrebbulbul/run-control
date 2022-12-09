using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForTheAnimator : MonoBehaviour
{
    public Animator WasRecorded_Animator;
   public void PacifyYourself()
    {
        WasRecorded_Animator.SetBool("ok", false);
    }

}
