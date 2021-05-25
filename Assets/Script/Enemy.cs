using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public int maxHealth;
    public int curHealth;
    public Transform target;
    private Rigidbody rigid;
    private BoxCollider boxCollider;
    private Material mat;
    private NavMeshAgent nav;
    public bool isChase;
    private Animator anim;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
        // 메테리얼은 위처럼 바로 못가져옴 아래처럼 마지막에 .material을 붙여야함.
        mat = GetComponentInChildren<SkinnedMeshRenderer>().material;
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
        Invoke("ChaseStart", 2);
    }

    private void ChaseStart()
    {
        isChase = true;
        anim.SetBool("isWalk", true);
    }

    private void Update()
    {
        if (isChase)
            nav.SetDestination(target.position);
        // SetDestination() 도착할 목표 위치 지정 함수
    }

    private void FixedUpdate()
    {
        FreezeVelocity();
    }

    private void FreezeVelocity()
    {
        if (isChase)
        {
            rigid.velocity = Vector3.zero;
            rigid.angularVelocity = Vector3.zero;
        }
        else
        {
            rigid.velocity = Vector3.zero;
            rigid.angularVelocity = Vector3.zero;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Melee")
        {
            Weapon wepon = other.GetComponent<Weapon>();
            curHealth -= wepon.damage;
            Vector3 reactVec = transform.position - other.transform.position;
            StartCoroutine(onDamage(reactVec));
        }
        else if (other.tag == "Volt")
        {
            Volt volt = other.GetComponent<Volt>();
            curHealth -= volt.damage;
            Vector3 reactVec = other.transform.position - transform.position;
            reactVec.y = 0;
            Destroy(other.gameObject);
            StartCoroutine(onDamage(reactVec));
        }
    }

    private IEnumerator onDamage(Vector3 reactVec)
    {
        mat.color = Color.red;
        yield return new WaitForSeconds(0.1f);

        if (curHealth > 0)
        {
            mat.color = Color.white;
            reactVec = reactVec.normalized;
            reactVec += Vector3.up;
            rigid.AddForce(reactVec * 5, ForceMode.Impulse);
        }
        else
        {
            mat.color = Color.gray;
            gameObject.layer = 11;
            isChase = false;
            nav.enabled = false;
            anim.SetTrigger("doDie");

            if (true)
            {
                reactVec = reactVec.normalized;
                reactVec += Vector3.up;
                rigid.AddForce(reactVec * 10, ForceMode.Impulse);
            }

            Destroy(gameObject, 4);
        }
    }
}