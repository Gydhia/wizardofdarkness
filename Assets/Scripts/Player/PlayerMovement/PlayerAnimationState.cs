using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationState : MonoBehaviour
{
    public static PlayerAnimationState Instance;

    public Animator playerAnimator;
    private void Awake()
    {
        Instance = this;
    }
    private void OnLevelWasLoaded()
    {
        if (playerAnimator == null && GameObject.FindGameObjectWithTag("PlayerAnim"))
        {
            GameObject go = GameObject.FindGameObjectWithTag("PlayerAnim");
            playerAnimator = go.GetComponent<Animator>();
        }
    }
    private void Start()
    {
        if (playerAnimator == null && GameObject.FindGameObjectWithTag("PlayerAnim"))
        {
            GameObject go = GameObject.FindGameObjectWithTag("PlayerAnim");
            playerAnimator = go.GetComponent<Animator>();
        }
    }
}
