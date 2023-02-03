using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterProperty : MonoBehaviour //최상위 부모
{
    public CharacterStat myStat; //직렬화
    Animator _anim = null;
    public Animator myAnim
    {
        get
        {
            if(_anim == null)
            {
                _anim = GetComponent<Animator>();
                if (_anim == null)  _anim = GetComponentInChildren<Animator>();
            }
            return _anim;
        }
    }

    Rigidbody _rigid = null;
    protected Rigidbody myRigid
    {
        get
        {
            if(_rigid == null)
            {
                _rigid = GetComponent<Rigidbody>();
            }
            return _rigid;
        }
    }
}
