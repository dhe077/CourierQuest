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
        animator.SetFloat("RPM", playerMovement.GetBikeRPM());
    }
}
