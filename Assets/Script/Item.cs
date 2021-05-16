using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public enum Type { Shield, Ammo, Coin, Grenade, Heart, Weapon };

    public Type type;
    public int value;
    //void Start()
    //{
    //}

    //// Update is called once per frame
    private void Update()
    {
        transform.Rotate(Vector3.up * 40 * Time.deltaTime);
    }
}