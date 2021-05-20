using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weappon : MonoBehaviour
{
    public enum Type { Melle, Range };

    public Type type;
    public int damage;
    public float rate;
    public BoxCollider meleeArea;
    public TrailRenderer trailEffect;
    public Transform voltPos;
    public GameObject volt;

    public void Use()
    {
        if (type == Type.Melle)
        {
            StopCoroutine("Swing");
            StartCoroutine("Swing");
        }
        else if (type == Type.Range)
        {
            //StopCoroutine("Swing");
            StartCoroutine("Shot");
        }
    }

    private IEnumerator Shot()
    {
        yield return new WaitForSeconds(0.1f);
        trailEffect.enabled = true;
        yield return new WaitForSeconds(0.25f);
        GameObject intantVolt = Instantiate(volt, voltPos.position, voltPos.rotation);
        Rigidbody voltRigid = intantVolt.GetComponent<Rigidbody>();
        voltRigid.velocity = voltPos.forward * 50;
        voltRigid.AddTorque(Vector3.up * 10, ForceMode.Impulse);
        yield return new WaitForSeconds(0.1f);
        trailEffect.enabled = false;
    }

    private IEnumerator Swing()
    {
        yield return new WaitForSeconds(0.1f);
        meleeArea.enabled = true;
        trailEffect.enabled = true;

        yield return new WaitForSeconds(0.3f);
        meleeArea.enabled = false;

        //yield return new WaitForSeconds(0.3f);
        trailEffect.enabled = false;
    }
}