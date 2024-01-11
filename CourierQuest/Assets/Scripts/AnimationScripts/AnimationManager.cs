using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    public void PlayAnimation(string objectName, string animationName)
    {
        GameObject character = null;
        try
        {
            character = GameObject.Find(objectName);
        }
        catch (NullReferenceException)
        {
            Debug.Log("Character Object is null!");
        }
        
        if (character != null)
        {
            Debug.Log($"Play animation: {animationName}");
            Animator animator = character.GetComponent<Animator>();
            try
            {
                animator.SetTrigger(animationName);
            }
            catch (MissingComponentException)
            {
                Debug.Log($"Missing Animator Component on {character}");
            }
            
        }
    }
}
