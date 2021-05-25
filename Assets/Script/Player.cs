using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    public float jumpPower = 15.0f;
    private float dodgeSpeed = 4.0f;
    public GameObject[] weapons;
    public bool[] hasWeapons;
    public GameObject[] shields;
    public int hasShields;
    public Camera followCamera;
    public int health;
    public int shield;
    public int coin;
    public int maxHealth;

    //private int maxShield = 4;
    private int maxCoin = 99999;

    private float hAxis;
    private float vAxis;

    private bool wDown;
    private bool jDown;
    private bool dDown;
    private bool iDown;
    private bool sDown1;
    private bool sDown2;
    private bool sDown3;
    private bool fDown;

    private bool skillDown1;
    private bool skillDown2;
    private bool skillDown3;

    private bool isJump;
    private bool isDodge;
    private bool isFireReady = true;
    private bool isBorder;
    private Vector3 moveVec;
    private Vector3 dodgeVec;

    private Rigidbody rigid;
    private Animator anim;

    private GameObject nearObject;
    private Weapon equipWeapon;
    private int weaponIndex = 0;
    private float fireDelay;

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
        Attack();
        Interation();
        Swap();
    }

    private void Attack()
    {
        if (equipWeapon == null)
            return;

        fireDelay += Time.deltaTime;
        isFireReady = equipWeapon.rate < fireDelay;

        if (fDown && isFireReady && !isDodge)
        {
            equipWeapon.Use();
            if (equipWeapon.type == Weapon.Type.Melle) anim.SetTrigger("doSwing");
            if (equipWeapon.type == Weapon.Type.Staff) anim.SetTrigger("doStaff");
            fireDelay = 0;
        }
    }

    private void GetInput()
    {
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");
        wDown = Input.GetButton("Walk");
        jDown = Input.GetButton("Jump");
        fDown = Input.GetButton("Fire1");
        dDown = Input.GetButton("Dodge");
        iDown = Input.GetButton("Interation");
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
        if (!isFireReady)
            moveVec = Vector3.zero;
        if (!isBorder)
            transform.position += moveVec * (wDown ? 0.2f : 1f) * speed * Time.deltaTime;
        anim.SetBool("isRun", moveVec != Vector3.zero);
        anim.SetBool("isWalk", wDown);
    }

    private void Trun()
    {
        // 키보드로 회전
        transform.LookAt(transform.position + moveVec);
        //마우스로 회전
        Ray ray = followCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit rayHit;
        if (fDown)
        {
            if (Physics.Raycast(ray, out rayHit, 100))
            {
                Vector3 nextVec = rayHit.point - transform.position;
                nextVec.y = 0;
                transform.LookAt(transform.position + nextVec);
            }
        }
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
            Invoke(nameof(DodgeOut), 0.5f);
        }
    }

    private void DodgeOut()
    {
        speed /= dodgeSpeed;
        isDodge = false;
    }

    private void Swap()
    {
        //int weaponIndex = 0;
        if (sDown1) weaponIndex = 1;
        if (sDown2) weaponIndex = 2;
        if (sDown3) weaponIndex = 3;
        // 그 무기를 가지고 있고, 동작 없을 때
        if (hasWeapons[weaponIndex] == true && (sDown1 || sDown2 || sDown3) && !isJump && !isDodge && isFireReady)
        {
            if (equipWeapon != null) // 현재 웨폰 있을 때
            {
                equipWeapon.gameObject.SetActive(false); // 아이템 off로
                Debug.Log("실행1");
            }

            if (equipWeapon != weapons[weaponIndex].GetComponent<Weapon>()/*weapons[weaponIndex]*/) // 현재 웨폰이 바꾸려는 웨폰과 같지 않을 때
            {
                equipWeapon = weapons[weaponIndex].GetComponent<Weapon>();
                equipWeapon.gameObject.SetActive(true); // 아이템 on으로
                Debug.Log("실행2");
            }
            else
            {
                equipWeapon = null;
                //equipWeapon = weapons[0].GetComponent<Weapon>();
                //equipWeapon.gameObject.SetActive(true);
                Debug.Log("실행3");
            }
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

    private void FreezeRotation()
    {
        // angularVelocity = 물리 회전 속도 Vector3.zero=0으로 만듦
        rigid.angularVelocity = Vector3.zero;
    }

    private void FixedUpdate()
    {
        //FreezeRotation 사용시 회전, 이동 값이 지속적으로 증가하는 오류(?)가 발생
        //FreezeRotation();
        StopToWall();
    }

    private void StopToWall()
    {
        Debug.DrawRay(transform.position, transform.forward * 1, Color.green);
        //wall을 만나면 Raycast가 트루로 변함 (Move 함수 확인)
        isBorder = Physics.Raycast(transform.position, transform.forward, 1, LayerMask.GetMask("Wall"));
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            anim.SetBool("isJump", false);
            isJump = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Item")
        {
            Item item = other.GetComponent<Item>();
            switch (item.type)
            {
                //case Item.Type.Shield:
                //    shields[hasShields].SetActive(true);
                //    hasShields += item.value;
                //    if (shield > maxShield)
                //        shield = maxShield;
                //    break;

                case Item.Type.Coin:
                    coin += item.value;
                    if (coin > maxCoin)
                        coin = maxCoin;
                    break;

                case Item.Type.Heart:
                    health += item.value;
                    if (health > maxHealth)
                        health = maxHealth;
                    break;
            }
            Destroy(other.gameObject);
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