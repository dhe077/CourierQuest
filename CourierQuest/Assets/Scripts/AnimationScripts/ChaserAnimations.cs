using System.Collections;
using System.Collections.Generic;
using Dreamteck.Splines;
using UnityEngine;

public class ChaserAnimations : MonoBehaviour
{
    private SplineFollower splineFollower;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        splineFollower = GetComponent<SplineFollower>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("WalkSpeed", splineFollower.followSpeed);
    }
}
