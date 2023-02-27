using UnityEngine;

[CreateAssetMenu(fileName = "Monster Data", menuName = "Scriptable Object/Monster Data", order = 1)]
public class MonsterData : ScriptableObject
{
    [SerializeField] string monsterName; //몬스터의 이름
    public string MonsterName { get { return monsterName; } }

    [SerializeField] float hp;
    public float HP { get { return hp; } }

    [SerializeField] float ap;
    public float AP { get { return ap; } }

    [SerializeField] float walkSpeed;
    public float WalkSpeed { get { return walkSpeed; } }

    [SerializeField] float runSpeed;
    public float RunSpeed { get { return runSpeed; } }

    [SerializeField] float rotSpeed;
    public float RotSpeed { get { return rotSpeed; } }

    [SerializeField] float attackRange;
    public float AttackRange { get { return attackRange; } }

    [SerializeField] float attackDelay;
    public float AttackDelay { get { return attackDelay; } }

    [SerializeField] float attackRadius;
    public float AttackRadius { get { return attackRadius; } }

    [SerializeField] float qSkillStiffTime;
    public float QSkillStiffTime { get { return qSkillStiffTime; } }

    [SerializeField] float exp;
    public float EXP { get { return exp; } }
}
