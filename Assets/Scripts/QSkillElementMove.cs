using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

public class QSkillElementMove : MonoBehaviour
{
    public LayerMask myPlayer;
    float exp = 0.05f;

    public void FollowTarget(Transform t)
    {
        StartCoroutine(FollowingTarget(t));
    }

    IEnumerator FollowingTarget(Transform t)
    {
        Vector3 start = transform.position;
        float time = 0.0f;
        float speed = 1.0f;
        while (time < 1.0f)
        {
            speed = Mathf.Lerp(1.0f, 5.0f, time);
            transform.position = Vector3.Lerp(start, t.transform.position, time);
            //러프 사용이유 다시 알아보기.
            time += Time.deltaTime * 0.3f * speed;
            yield return null;
        }
    }

    private void OnTriggerEnter(Collider other) //플레이어의 콜라이더와 충돌했을 때
    {
        if ((myPlayer & 1 << other.gameObject.layer) != 0)
        {
            other.GetComponent<SkillCoolTime>().MySkill_IMG[1].fillAmount += exp;
            GameObject obj = Instantiate(Resources.Load("Prefabs/SkillEffect/QSkillbumbEffect")) as GameObject;
            obj.transform.position = transform.position;
            Destroy(obj, 2.0f);
            Destroy(gameObject);
        }
    }
}
