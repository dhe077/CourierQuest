using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplineTriggerAnimation : MonoBehaviour
{
    public GameObject animationObject;

    public void TriggerAnimation(string animationName)
    {
        Animator animator = animationObject.GetComponent<Animator>();
        animator.SetTrigger(animationName);
    }
}
