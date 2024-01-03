using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorseScript : MonoBehaviour
{
    public PlayerMovement playerMovement;

    private GameObject horse;
    private Transform horseTransform;
    private Animator animator;


    public bool startGrazing = false;

    [Header("----Target RPM for Animations----")]
    [SerializeField] private float trotRPMTarget = 70.0f;
    [SerializeField] private float canterRPMTarget = 80.0f;
    [SerializeField] private float gallopRPMTarget = 90.0f;

    private void Start()
    {
        horse = this.transform.GetChild(0).gameObject;
        horseTransform = this.transform.GetChild(0).transform;
        animator = horse.GetComponent<Animator>();
    }

    private void Update()
    {   
        if (playerMovement != null)
        {
            horseTransform.position = this.transform.position;
            MovementAnimation();
        }
        else
        {
            if (startGrazing)
            {
                Grazing();
            }
        }
    }

    public void EnableHorse(bool enable)
    {
        horse.SetActive(enable);
    }


    // ----Animator---- //
    public void Grazing()
    {
        animator.SetTrigger("Graze");
    }

    public void MovementAnimation()
    {
        if (playerMovement.GetBikeRPM() == 0)
        {
            animator.SetBool("Walk", false);
        }
        else if (playerMovement.GetBikeRPM() > 0 && playerMovement.GetBikeRPM() < trotRPMTarget)
        {
            animator.SetBool("Walk", true);
            animator.SetBool("Trot", false);
        }
        else if (playerMovement.GetBikeRPM() >= trotRPMTarget && playerMovement.GetBikeRPM() < canterRPMTarget)
        {
            animator.SetBool("Walk", false);
            animator.SetBool("Trot", true);
            animator.SetBool("Canter", false);
        }
        else if (playerMovement.GetBikeRPM() >= canterRPMTarget && playerMovement.GetBikeRPM() < gallopRPMTarget)
        {
            animator.SetBool("Trot", false);
            animator.SetBool("Canter", true);
            animator.SetBool("Gallop", false);
        }
        else if (playerMovement.GetBikeRPM() > gallopRPMTarget)
        {
            animator.SetBool("Canter", false);
            animator.SetBool("Gallop", true);
        }
    }
}
