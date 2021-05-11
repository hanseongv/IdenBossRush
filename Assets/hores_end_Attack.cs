using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hores_end_Attack : StateMachineBehaviour
{
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("isAttack", false);
    }
}