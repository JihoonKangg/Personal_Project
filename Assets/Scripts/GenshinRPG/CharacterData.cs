using UnityEngine;

[CreateAssetMenu(fileName = "Character Data", menuName = "Scriptable Object/Character Data", order = 2)]

public class CharacterData : ScriptableObject
{
    [SerializeField] string characterName; //ĳ������ �̸�
    public string CharacterName { get => characterName; }

    [SerializeField] float[] hp;
    public float[] HP { get => hp; }

    [SerializeField] float[] ap;
    public float[] AP { get => ap; }

    [SerializeField] float[] eSkillap;
    public float[] ESkillAP { get => eSkillap; }

    [SerializeField] float[] qSkillap;
    public float[] QSkillAP { get => qSkillap; }

    [SerializeField] float rotSpeed;
    public float RotSpeed { get => rotSpeed; }

    [SerializeField] float attackRadius;
    public float AttackRadius { get => attackRadius; }

    [SerializeField] float eSkillCoolTime;
    public float ESkillCoolTime { get => eSkillCoolTime; }
}
