using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //Running Backward
        if (PlayerController.instance.verticalInput < 0 || PlayerController.instance.horizontalInput < 0)
        {
            animator.SetBool("isRunningBackWard", true);
            animator.SetBool("isRunningForward", false);
        }

        //Running forward
        if ((PlayerController.instance.horizontalInput != 0 && PlayerController.instance.verticalInput > 0) || PlayerController.instance.verticalInput > 0 || PlayerController.instance.horizontalInput > 0)
        {
            animator.SetBool("isRunningForward", true);
            animator.SetBool("isRunningBackWard", false);
        }

        //Idle:
        if (PlayerController.instance.horizontalInput == 0 && PlayerController.instance.verticalInput == 0)
        {

            animator.SetBool("isRunningForward", false);
            animator.SetBool("isRunningBackWard", false);
        }

        //Shoot:
        if (Input.GetMouseButtonDown(0))
        {
            animator.SetBool("isShootingWP1", true);
        }
        else
        {
            animator.SetBool("isShootingWP1", false);
        }


        //Dead:
        if(PlayerStats.instance.currentHealth <=0)
        {
            animator.SetBool("isDead", true);
        }

        //Reload:

    }
}
