using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBattle //���߻�� �Ұ������� �������̽� ���
{
    //�߻�Ŭ����
    void OnBigDamage(float Bigdmg);
    void OnSkillDamage(float SkillDamage);
    void OnDamage(float dmg);
    bool IsLive();
    void AddAttacker(IBattle ib);
    void RemoveAttacker(IBattle ib);
    void DeadMessage(Transform tr); //�׾��� �� �˷��ִ� �Լ�
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
