using UnityEngine;

[CreateAssetMenu(fileName = "QSkillElement", menuName = "Scriptable Object/QSkillExp Data")]
public class QSkillElementOrb : ScriptableObject
{
    [SerializeField] string OrbName;
    public string Orbname { get { return OrbName; } }
    [SerializeField] float QSkillExp;
    public float QskillExp { get { return QSkillExp; } }
}
