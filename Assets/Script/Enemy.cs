using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHealth;
    public int curHealth;

    private Rigidbody rigid;
    private BoxCollider boxCollider;
    private Material mat;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
        // 메테리얼은 위처럼 바로 못가져옴 아래처럼 마지막에 .material을 붙여야함.
        mat = GetComponentInChildren<SkinnedMeshRenderer>().material;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Melee")
        {
            Weappon wepon = other.GetComponent<Weappon>();
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

            reactVec = reactVec.normalized;
            reactVec += Vector3.up;

            rigid.AddForce(reactVec * 10, ForceMode.Impulse);

            Destroy(gameObject, 4);
        }
    }
}