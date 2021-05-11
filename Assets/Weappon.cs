using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weappon : MonoBehaviour
{
    // ���� Ÿ��, ������, ���ݼӵ�, ���ݹ���, ȿ�� ���� ����
    public enum Type { Melee, Range };

    public Type type;
    public int damage;
    public float rate;
    public BoxCollider meleeArea;
    public TrailRenderer trailEffect;

    public void Use()
    {
        if (type == Type.Melee)
        {
            StopCoroutine("Swing");
            StartCoroutine("Swing");
        }
    }

    private IEnumerator Swing()
    {
        // yield ����� �����ϴ� Ű����
        //yield return null; // 1������ ���
        //yield return new WaitForSeconds(0.1f); // 0.1�� ���
        //yield break; //�����ϵ� Ż��
        yield return new WaitForSeconds(0.1f);
        meleeArea.enabled = true;
        trailEffect.enabled = true;

        yield return new WaitForSeconds(0.3f);
        meleeArea.enabled = false;

        yield return new WaitForSeconds(0.3f);
        trailEffect.enabled = false;
    }
}