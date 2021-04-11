using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStateController : MonoBehaviour
{
    Animator animator;
    int isWalkingHash;
    int isRunningHash;
    int isWalkingBackwardHash;
    int isWalkingLeftHash;
    int isWalkingRightHash;
    int isJumpingHash;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        isWalkingHash = Animator.StringToHash("IsWalking");
        isRunningHash = Animator.StringToHash("IsRunning");
        isWalkingBackwardHash = Animator.StringToHash("IsWalkingBackward");
        isWalkingLeftHash = Animator.StringToHash("IsWalkingLeft");
        isWalkingRightHash = Animator.StringToHash("IsWalkingRight");
        isJumpingHash = Animator.StringToHash("IsJumping");
    }

    // Update is called once per frame
    void Update()
    {
        bool isrunning = animator.GetBool(isRunningHash);
        bool isjumping = animator.GetBool(isJumpingHash);
        bool iswalkingRight = animator.GetBool(isWalkingRightHash);
        bool iswalkingLeft = animator.GetBool(isWalkingLeftHash);
        bool isWalking = animator.GetBool(isWalkingHash);
        bool isBackward = animator.GetBool(isWalkingBackwardHash);
        bool forwardPressed = Input.GetKey("z");
        bool runPressed = Input.GetKey("left shift");
        bool backwardPressed = Input.GetKey("s");
        bool leftPressed = Input.GetKey("q");
        bool rightPressed = Input.GetKey("d");
        bool spacePressed = Input.GetKeyDown("space");

        if (!isjumping && spacePressed)
        {
            animator.SetBool(isJumpingHash, true);
        }

        if (isjumping && !spacePressed)
        {
            animator.SetBool(isJumpingHash, false);
        }

        if (!iswalkingRight && rightPressed)
        {
            animator.SetBool(isWalkingRightHash, true);
        }

        if (iswalkingRight && !rightPressed)
        {
            animator.SetBool(isWalkingRightHash, false);
        }

        if (!iswalkingLeft && leftPressed)
        {
            animator.SetBool(isWalkingLeftHash, true);
        }

        if (iswalkingLeft && !leftPressed)
        {
            animator.SetBool(isWalkingLeftHash, false);
        }

        if (!isBackward && backwardPressed)
        {
            animator.SetBool(isWalkingBackwardHash, true);           
        }

        if (isBackward && !backwardPressed)
        {
            animator.SetBool(isWalkingBackwardHash, false);
        }

        if (!isWalking && forwardPressed)
        {
            animator.SetBool(isWalkingHash, true);
        }

        if (isWalking && !forwardPressed)
        {
            animator.SetBool(isWalkingHash, false);
        }

        if (!isrunning && (forwardPressed && runPressed)) 
        {
            animator.SetBool(isRunningHash, true);
        }

        if (isrunning && (!forwardPressed || !runPressed)) 
        {
            animator.SetBool(isRunningHash, false);
        }
    }
}
