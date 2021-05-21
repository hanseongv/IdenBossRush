using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Volt : MonoBehaviour
{
    public int damage;

    // 볼트에서 이즈트리거 설정을 했기 때문에 아래 콜리전엔터말고 트리거 엔터로 바꿈.
    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.tag == "Floor")
    //    {
    //        Destroy(gameObject);
    //    }
    //    else if (collision.gameObject.tag == "Wall")
    //    {
    //        Destroy(gameObject);
    //    }
    //}
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Floor")
        {
            Destroy(gameObject);
        }
        else if (other.gameObject.tag == "Wall")
        {
            Destroy(gameObject);
        }
    }
}