using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    public float jumpPower = 15.0f;
    public float dodgeSpeed = 4.0f;

    private float hAxis;
    private float vAxis;

    private bool wDown;
    private bool jDown;
    private bool dDown;
    private bool isJump;
    private bool isDodge;

    private Vector3 moveVec;
    private Vector3 dodgeVec;

    private Rigidbody rigid;
    private Animator anim;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        rigid = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        GetInput();
        Move();
        Trun();
        Jump();
        Dodge();
    }

    private void GetInput()
    {
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");
        wDown = Input.GetButton("Walk");
        jDown = Input.GetButtonDown("Jump");
        dDown = Input.GetButtonDown("Dodge");
    }

    private void Move()
    {
        //normalized 방향값을 1로 바꿈
        moveVec = new Vector3(hAxis, 0, vAxis).normalized;
        if (isDodge)
            moveVec = dodgeVec;
        transform.position += moveVec * (wDown ? 0.2f : 1f) * speed * Time.deltaTime;
        anim.SetBool("isRun", moveVec != Vector3.zero);
        anim.SetBool("isWalk", wDown);
    }

    private void Trun()
    {
        transform.LookAt(transform.position + moveVec);
    }

    private void Jump()
    {
        if (jDown && !isJump) // && !isDodge && moveVec == Vector3.zero
        {
            rigid.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
            anim.SetBool("isJump", true);
            anim.SetTrigger("doJump");
            isJump = true;
        }
    }

    private void Dodge()
    {
        if (dDown && moveVec != Vector3.zero && !isJump && !isDodge)
        {
            dodgeVec = moveVec;
            speed *= dodgeSpeed;
            anim.SetTrigger("doDodge");
            isDodge = true;
            //Invoke() 함수로 시간차 함수 호출
            Invoke("DodgeOut", 0.5f);
        }
    }

    private void DodgeOut()
    {
        speed /= dodgeSpeed;
        isDodge = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            anim.SetBool("isJump", false);
            isJump = false;
        }
    }
}