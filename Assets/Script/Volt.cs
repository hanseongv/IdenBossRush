using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Volt : MonoBehaviour
{
    public int damage;

    // ��Ʈ���� ����Ʈ���� ������ �߱� ������ �Ʒ� �ݸ������͸��� Ʈ���� ���ͷ� �ٲ�.
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