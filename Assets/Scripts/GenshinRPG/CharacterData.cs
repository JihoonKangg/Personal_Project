using UnityEngine;

[CreateAssetMenu(fileName = "Character Data", menuName = "Scriptable Object/Character Data", order = 2)]

public class CharacterData : ScriptableObject
{
    [SerializeField] string characterName; //캐릭터의 이름
    public string CharacterName { get => characterName; }

    [SerializeField] float[] hp;
    public float[] HP { get => hp; }

    [SerializeField] float[] ap;
    public float[] AP { get => ap; }

    public float ESkillAP;

    public float QSkillAP;

    [SerializeField] float rotSpeed;
    public float RotSpeed { get => rotSpeed; }

    [SerializeField] float attackRadius;
    public float AttackRadius { get => attackRadius; }

    [SerializeField] float eSkillCoolTime;
    public float ESkillCoolTime { get => eSkillCoolTime; }
}
