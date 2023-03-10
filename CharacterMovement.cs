using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private float MinGroundNormalY = .65f;
    [SerializeField] private float GravityModifier = 1f;
    [SerializeField] private float _jumpForse;
    [SerializeField] private float _speed;
    [SerializeField] private Vector2 _velocity;
    [SerializeField] private LayerMask _layerMask;

    protected Vector2 TargetVelocity;
    protected float horizontalMove;
    protected bool Grounded;
    protected Vector2 GroundNormal;
    protected Rigidbody2D Rb2d;
    protected Animator Animator;
    protected ContactFilter2D ContactFilter;
    protected RaycastHit2D[] HitBuffer = new RaycastHit2D[16];
    protected List<RaycastHit2D> HitBufferList = new List<RaycastHit2D>(16);
    protected const float MinMoveDistance = 0.001f;
    protected const float ShellRadius = 0.01f;
    private int _isJumping = Animator.StringToHash("IsJumping");
 

    void OnEnable()
    {
        Rb2d = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
    }

    void Start()
    {
        ContactFilter.useTriggers = false;
        ContactFilter.SetLayerMask(_layerMask);
        ContactFilter.useLayerMask = true;
    }

    void Update()
    {
        TargetVelocity = new Vector2(Input.GetAxis("Horizontal") * _speed, TargetVelocity.y);

        horizontalMove = Input.GetAxisRaw("Horizontal") * _speed;

        if (Input.GetKey(KeyCode.Space) && Grounded)
        {
            _velocity.y = _jumpForse;
            Animator.SetBool(_isJumping, true);
        }

        Animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        if (horizontalMove > 0)
        {
            transform.localScale = Vector3.one;
        }

        else if (horizontalMove < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);           
        }
    }

    void FixedUpdate()
    {
        _velocity += GravityModifier * Physics2D.gravity * Time.deltaTime;
        _velocity.x = TargetVelocity.x;
        Grounded = false;

        Vector2 deltaPosition = _velocity * Time.deltaTime;
        Vector2 moveAlongGround = new Vector2(GroundNormal.y, -GroundNormal.x);
        Vector2 move = moveAlongGround * deltaPosition.x;

        Move(move, false);

        move = Vector2.up * deltaPosition.y;

        Move(move, true);
    }

    void Move(Vector2 move, bool yMovement)
    {
        float distance = move.magnitude;

        if (distance > MinMoveDistance)
        {
            int count = Rb2d.Cast(move, ContactFilter, HitBuffer, distance + ShellRadius);

            HitBufferList.Clear();

            for (int i = 0; i < count; i++)
            {
                HitBufferList.Add(HitBuffer[i]);
            }

            for (int i = 0; i < HitBufferList.Count; i++)
            {
                Vector2 currentNormal = HitBufferList[i].normal;

                if (currentNormal.y > MinGroundNormalY)
                {
                    Grounded = true;

                    if (yMovement)
                    {
                        GroundNormal = currentNormal;
                        currentNormal.x = 0;
                    }
                }

                float projection = Vector2.Dot(_velocity, currentNormal);

                if (projection < 0)
                {
                    _velocity = _velocity - projection * currentNormal;
                }

                float modifiedDistance = HitBufferList[i].distance - ShellRadius;
                distance = modifiedDistance < distance ? modifiedDistance : distance;
            }
        }
        Rb2d.position = Rb2d.position + move.normalized * distance;
    }
}

