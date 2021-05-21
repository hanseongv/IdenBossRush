using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHealth;
    public int curHealth;

    private Rigidbody rigid;
    private BoxCollider boxCollider;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Melee")
        {
            Weappon wepon = other.GetComponent<Weappon>();
            curHealth -= wepon.damage;
            Debug.Log("Melee : " + curHealth);
        }
        else if (other.tag == "Volt")
        {
            Volt volt = other.GetComponent<Volt>();
            curHealth -= volt.damage;
            Debug.Log("Range : " + curHealth);
        }
    }
}