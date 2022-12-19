using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBattle //다중상속 불가능으로 인터페이스 사용
{
    //추상클래스
    void OnBigDamage(float Bigdmg);
    void OnSkillDamage(float SkillDamage);
    void OnDamage(float dmg);
    bool IsLive();
    void AddAttacker(IBattle ib);
    void RemoveAttacker(IBattle ib);
    void DeadMessage(Transform tr); //죽었을 때 알려주는 함수
}

public class BattleSystem : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
