using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    private CharacterController _controller;
    [SerializeField]
    private float _playerSpeed = 2.5f;
    [SerializeField]
    private float _runSpeed = 1.5f;
    [SerializeField]
    private float _mouseSensibility = 3.5f;
    [SerializeField]
    private float _gravity = 9.81f;

    public bool _hasCoin = false;

    // Use this for initialization
    void Start()
    {
        _controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        LookX();
        Jump();
    }

    void Movement()
    {
        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        Vector3 velocity = direction * _playerSpeed;
        velocity.y -= _gravity;
        //converte de local space para global space
        velocity = transform.transform.TransformDirection(velocity);
        _controller.Move(velocity * Time.deltaTime);
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("pulou");
        }
    }

    void LookX()
    {
        float _xAxis = Input.GetAxis("Mouse X");
        Vector3 rotation = transform.localEulerAngles;
        rotation.y += _xAxis * _mouseSensibility;
        transform.localEulerAngles = rotation;

    }
}
