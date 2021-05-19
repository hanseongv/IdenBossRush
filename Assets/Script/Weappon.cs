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

    public void Use()
    {
        if (type == Type.Melle)
        {
            StopCoroutine("Swing");
            StartCoroutine("Swing");
        }
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