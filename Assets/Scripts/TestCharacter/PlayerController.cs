using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : CharacterProperty
{
    [SerializeField]
    public float moveSpeed = 5.0f;

    void Start()
    {

    }

    void Update()
    {
        float _moveDirX = Input.GetAxisRaw("Horizontal");
        float _moveDirZ = Input.GetAxisRaw("Vertical");

        Vector3 _moveHoizontal = transform.right * _moveDirX;
        Vector3 _moveVertial = transform.forward * _moveDirZ;

        Vector3 _velocity = (_moveHoizontal + _moveVertial).normalized * moveSpeed;

        myRigid.MovePosition(transform.position + _velocity * Time.deltaTime);
    }
}
