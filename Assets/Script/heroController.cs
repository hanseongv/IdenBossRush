using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class heroController : MonoBehaviour
{
    public float speed = 3.0f;

    public float hAxis;
    public float vAxis;
    public bool wDown;

    private Vector3 moveVec;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        // 속도 느리게 하는 거 보류
        //if (wDown == true)
        //    speed = fastSpeed / 3;
        //else
        //    speed = fastSpeed;

        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");
        wDown = Input.GetButton("Walk");

        if (Input.GetKey(KeyCode.Space)) animator.Play("jump");

        moveVec = new Vector3(hAxis, 0, vAxis).normalized;

        transform.position += moveVec * speed * (wDown ? 0.3f : 1f) * Time.deltaTime;

        animator.SetBool("isRun", moveVec != Vector3.zero);
        animator.SetBool("isWalk", wDown);

        //회전
        transform.LookAt(transform.position + moveVec);
    }
}