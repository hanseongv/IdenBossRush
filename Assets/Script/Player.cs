using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    public float jumpPower = 15.0f;
    public float dodgeSpeed = 4.0f;
    public GameObject[] weapons;
    public bool[] hasWeapons;

    private float hAxis;
    private float vAxis;

    private bool wDown;
    private bool jDown;
    private bool dDown;
    private bool isJump;
    private bool isDodge;
    private bool iDown;
    private bool sDown1;
    private bool sDown2;
    private bool sDown3;

    private Vector3 moveVec;
    private Vector3 dodgeVec;

    private Rigidbody rigid;
    private Animator anim;

    private GameObject nearObject;
    private GameObject equipWeapon;
    private int equiqWeaponIndex = 0;

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
        Interation();
        Swap();
    }

    private void GetInput()
    {
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");
        wDown = Input.GetButton("Walk");
        jDown = Input.GetButtonDown("Jump");
        dDown = Input.GetButtonDown("Dodge");
        iDown = Input.GetButtonDown("Interation");
        sDown1 = Input.GetButtonDown("Swap1");
        sDown2 = Input.GetButtonDown("Swap2");
        sDown3 = Input.GetButtonDown("Swap3");
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

    private void Swap()
    {
        int weaponIndex = 0;
        if (sDown1) weaponIndex = 1;
        if (sDown2) weaponIndex = 2;
        if (sDown3) weaponIndex = 3;

        if (hasWeapons[weaponIndex] == true && (sDown1 || sDown2 || sDown3) && !isJump && !isDodge)
        {
            if (equipWeapon != null) // 현재 웨폰 있을 때
                equipWeapon.SetActive(false); // 아이템 off로

            if (equipWeapon != weapons[weaponIndex]) // 현재 웨폰이 바꾸려는 웨폰과 같지 않을 때
            {
                equipWeapon = weapons[weaponIndex];
                equipWeapon.SetActive(true); // 아이템 on으로
            }
            else
                equipWeapon = weapons[0];
        }
    }

    private void Interation()
    {
        if (iDown && nearObject != null && !isJump && !isDodge)
        {
            if (nearObject.tag == "Weapon")
            {
                Item item = nearObject.GetComponent<Item>();
                int weaponIndex = item.value;
                hasWeapons[weaponIndex] = true;
                Destroy(nearObject);
            }
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

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Weapon")
            nearObject = other.gameObject;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Weapon")
            nearObject = null;
    }
}