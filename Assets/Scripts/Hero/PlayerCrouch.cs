using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrouch : MonoBehaviour
{
    public Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Q - crouch
        if (Input.GetKey(KeyCode.Q))
        {
            animator.SetTrigger("Crouch");
        }
    }
}
