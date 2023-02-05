using UnityEngine;

[CreateAssetMenu(fileName = "Character Data", menuName = "Scriptable Object/Character Data", order = 2)]

public class CharacterData : ScriptableObject
{
    [SerializeField] string characterName; //ĳ������ �̸�
    public string CharacterName { get { return characterName; } }

    [SerializeField] float hp;
    public float HP { get { return hp; } }

    [SerializeField] float ap;
    public float AP { get { return ap; } }

    [SerializeField] float eSkillap;
    public float ESkillAP { get { return eSkillap; } }

    [SerializeField] float qSkillap;
    public float QSkillAP { get { return qSkillap; } }

    [SerializeField] float rotSpeed;
    public float RotSpeed { get { return rotSpeed; } }

    [SerializeField] float attackRadius;
    public float AttackRadius { get { return attackRadius; } }

    [SerializeField] float eSkillCoolTime;
    public float ESkillCoolTime { get { return eSkillCoolTime; } }

    public float CharacterHP(int Level) //������ ?�϶� ü���� ��ȯ�ǰ� �ϱ�.
    {
        float ChaHp = HP;
        if (Level == 1) return ChaHp;

        for(int i = 1; i < Level; i++)
        {
            ChaHp = ChaHp * 1.2f;
        }
        
        return ChaHp;
    }
}
