using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class heroController : MonoBehaviour
{
    private new Camera camera;
    private Animator anim;
    private Rigidbody rigid;
    private Vector3 destination;

    // 캐릭터 능력
    public float jumpPower = 2f;

    public float speed = 3.0f;

    public bool run;
    public bool wDown;

    public bool jDown;
    private bool isJump;
    private bool fDown;

    private bool isFireReady;
    private float fireDelay;

    private Weapon equippWeapon;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        camera = Camera.main;
        anim = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        //Attack();
        //Swap();
        GetInput();
        Move();
        Jump();

        //if (Input.GetKey(KeyCode.Space)) anim.Play("jump");
    }

    private void GetInput()
    {
        wDown = Input.GetButton("Walk");
        jDown = Input.GetButtonDown("Jump");
    }

    private void SetDestination(Vector3 dest) //지점 클릭
    {
        destination = dest;
        run = true;
        //animator.SetBool("isMove", true);
        //if (wDown == true)
        //    anim.SetBool("isWalk", true);
        //else
        //{
        //    //animator.SetBool("isWalk", false);
        //    anim.SetBool("isMove", true);
        //}
    }

    private void Move()
    {
        if (run)
        {
            var dir = destination - transform.position;
            var dirxz = new Vector3(dir.x, 0f, dir.z);

            if (!isJump)
                anim.transform.forward = dirxz;
            transform.position += dirxz.normalized * (wDown ? 0.3f : 1f) * Time.deltaTime * speed;
        }

        if (Vector3.Distance(transform.position, destination) <= 0.2f)//지점 도착
        {
            run = false;
            wDown = false;
            anim.SetBool("isMove", false);
            anim.SetBool("isWalk", false);
        }

        if (run == true)
        {
            if (wDown == true)
                anim.SetBool("isWalk", true);
            else
            {
                anim.SetBool("isWalk", false);
                anim.SetBool("isMove", true);
            }
            //if (wDown == false)
            //{
            //    anim.SetBool("isMove", true);
            //}
            //anim.SetBool("isWalk", false);
        }

        if (Input.GetMouseButton(1))
        {
            RaycastHit hit;
            if (Physics.Raycast(camera.ScreenPointToRay(Input.mousePosition), out hit))
            {
                SetDestination(hit.point);
            }
        }
        //if (Input.GetMouseButton(0))
        //{
        //    Attacking = true;
        //    run = false;
        //    anim.Play("NormalAttack01_SwordShield");
        //    Attacking = false;
        //}
    }

    private void Jump()
    {
        if (jDown && !isJump)
        {
            rigid.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
            anim.SetBool("isJump", true);
            anim.SetTrigger("doJump");
            isJump = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            anim.SetBool("isJump", false);
            isJump = false;
        }
    }

    private void Swap()
    {
    }

    //private void Attack()
    //{
    //    if (equippWeapon == null) return;
    //    fireDelay += Time.deltaTime;
    //    isFireReady = equippWeapon.rate < fireDelay;

    //    if (fDown && isFireReady /*&& !isDodge&& !isSwap*/)
    //    {
    //        equippWeapon.Use();
    //        animator.SetTrigger("doSwing");
    //        fireDelay = 0;
    //    }
    //}

    //키보드로 움직임

    //public float hAxis;
    //public float vAxis;
    //

    //private Vector3 moveVec;
    //private Animator animator;

    //private void Awake()
    //{
    //    animator = GetComponentInChildren<Animator>();
    //}

    //private void Update()
    //{
    //    // 속도 느리게 하는 거 보류
    //    //if (wDown == true)
    //    //    speed = fastSpeed / 3;
    //    //else
    //    //    speed = fastSpeed;

    //    hAxis = Input.GetAxisRaw("Horizontal");
    //    vAxis = Input.GetAxisRaw("Vertical");
    //

    //    if (Input.GetKey(KeyCode.Space)) animator.Play("jump");

    //    moveVec = new Vector3(hAxis, 0, vAxis).normalized;

    //    transform.position += moveVec * speed * (wDown ? 0.3f : 1f) * Time.deltaTime;

    //    animator.SetBool("isRun", moveVec != Vector3.zero);
    //    animator.SetBool("isWalk", wDown);

    //    //회전
    //    transform.LookAt(transform.position + moveVec);
    //}
}