using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    Animator m_Animator;
    public InputAction MoveAction;
    public InputAction SprintAction;

    public float walkSpeed = 1.0f;
    public float sprintMultiplier = 2f;
    public float turnSpeed = 20f;

    Rigidbody m_Rigidbody;
    Vector3 m_Movement;
    Quaternion m_Rotation = Quaternion.identity;

 //AudioSource for footsteps
    AudioSource m_AudioSource;

    private List<string> m_OwnedKeys = new List<string>();

    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        MoveAction.Enable();
        SprintAction.Enable();
        m_Animator = GetComponent<Animator>();
        m_AudioSource = GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        var pos = MoveAction.ReadValue<Vector2>();
        float horizontal = pos.x;
        float vertical = pos.y;

        m_Movement.Set(horizontal, 0f, vertical);
        m_Movement.Normalize();

        bool isWalking = horizontal != 0f || vertical != 0f;
        m_Animator.SetBool("IsWalking", isWalking);

        // Rotate player towards movement direction
        if (isWalking)
        {
            Vector3 desiredForward = Vector3.RotateTowards(transform.forward, m_Movement, turnSpeed * Time.deltaTime, 0f);
            m_Rotation = Quaternion.LookRotation(desiredForward);
            m_Rigidbody.MoveRotation(m_Rotation);
        }

        // Move player
        bool isSprinting = SprintAction.IsPressed();
        float speed = isSprinting ? walkSpeed * sprintMultiplier : walkSpeed;

        m_Rigidbody.MovePosition(
            m_Rigidbody.position + m_Movement * speed * Time.deltaTime
        );

        //Play footstep audio when walking
        if (isWalking)
        {
            if (!m_AudioSource.isPlaying)
            {
                m_AudioSource.Play();
            }
        }
        else
        {
            m_AudioSource.Stop();
        }
    }

    // Key management
    public void AddKey(string keyName)
    {
        m_OwnedKeys.Add(keyName);
    }

    public bool OwnKey(string keyName)
    {
        return m_OwnedKeys.Contains(keyName);
    }
}