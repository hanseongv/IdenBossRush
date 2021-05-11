using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weappon : MonoBehaviour
{
    // 무기 타입, 데미지, 공격속도, 공격범위, 효과 변수 생성
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
        // yield 결과를 전달하는 키워드
        //yield return null; // 1프레임 대기
        //yield return new WaitForSeconds(0.1f); // 0.1초 대기
        //yield break; //예ㅖ일드 탈출
        yield return new WaitForSeconds(0.1f);
        meleeArea.enabled = true;
        trailEffect.enabled = true;

        yield return new WaitForSeconds(0.3f);
        meleeArea.enabled = false;

        yield return new WaitForSeconds(0.3f);
        trailEffect.enabled = false;
    }
}