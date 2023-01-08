using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEffect : CharacterProperty
{
    [SerializeField] Material MySword;
    [SerializeField] float MaxSplitValue = 1.0f;
    [SerializeField] float SplitSpeed = 0.4f;
    [SerializeField] float mySplit = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        mySplit = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        MySword.SetFloat("_SplitValue", mySplit);
        SetMySword();
    }

    void SetMySword()
    {
        if(myAnim.GetFloat("Speed") > 0.5f)
        {
            StartCoroutine(WeaponDisapear());
        }
    }

    IEnumerator WeaponDisapear()
    {
        while(mySplit >= 0.0f)
        {
            mySplit -= SplitSpeed * Time.deltaTime;
            yield return null;
        }
        mySplit = 0.0f;
    }

    IEnumerator WeponSet()
    {
        while(mySplit <= 1.0f)
        {
            mySplit += SplitSpeed * Time.deltaTime;
            yield return null;
        }
        mySplit = 1.0f;
    }
}
