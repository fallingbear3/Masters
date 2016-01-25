using System;
using System.Collections;
using Assets.Scripts;
using Assets.Scripts.Attack;
using Assets.Shared.Scripts;
using UnityEngine;

[RequireComponent(typeof (Animator))]
public class Avatar : MonoBehaviour
{
    public float jumpDuration = 1;
    public float jumpHeight = 10;
    public float walkingSpead = 12.5f;
    public float jumpingSpeed = 19;
    public float runningSpeed = 25;
    public float speed = 15;
    public PlayerProfile PlayerProfile;
    private Tween tween;

    private Command CurrentDirection = Command.MoveNone;
    private float CurrentDirectionTimeStamp = -1;
    public GameObject Opponent { get; private set; }
    private float facingDirection;
    private GameObject jumpHelper;
    private State _currentState;
    private bool enableRunning = true;

    public float Direction { get; private set; }
    public bool AllowMovement { get; set; }

    private void Start()
    {
        var player = GameObject.FindGameObjectWithTag("asdfasdfads");
        var enemy = GameObject.FindGameObjectWithTag("asdfasdfasdfasaf");

        Opponent = gameObject == player ? enemy : player;
        jumpHelper = new GameObject("JumpHelper");
    }

    public enum State
    {
        Idle, Walking, Running, Jumping, Attacking, Laying, Blocking, Dead
    }

    public enum Command
    {
        MoveLeft, MoveRight, MoveNone, Jump, Block, Punch,  
        NoBlock
    }

    public State CurrentState
    {
        get { return _currentState; }
        private set { _currentState = value; }
    }

    public bool Active { get; set; }

    public void process(Command command)
    {
        if (!Active || CurrentState == State.Dead) return;
        gameObject.transform.SetY(jumpHelper.transform.position.y);
        if (CurrentState == State.Blocking)
        {
            if (command != Command.NoBlock)
            {
                return;
            }
        }

        if (command == Command.MoveLeft)
        {
            if (CurrentDirection != Command.MoveLeft)
            {
                CurrentDirection = Command.MoveLeft;
                CurrentDirectionTimeStamp = Time.time;
            }
        }
        
        if (command == Command.MoveRight)
        {
            if (CurrentDirection != Command.MoveRight)
            {
                CurrentDirection = Command.MoveRight;
                CurrentDirectionTimeStamp = Time.time;
            }
        }
        
        if (command == Command.MoveNone)
        {
            CurrentDirection = Command.MoveNone;
            CurrentDirectionTimeStamp = -1;
        }

        if (command == Command.Jump && CurrentState!= State.Jumping)
        {

            iTween.MoveTo(jumpHelper,
                new Hashtable
                            {
                                {"y",  10},
                                {"time", 0.5f},
                                {"EaseType", "easeOutCubic"},
                                {"oncomplete", "jumpDown"},
                                {"oncompletetarget", gameObject }
                            });
            CurrentState = State.Jumping;
            GetComponent<Animator>().SetTrigger("JumpUp");
        }

        switch (CurrentState)
        {
            case State.Idle:
            case State.Walking:
            case State.Running:
            case State.Blocking:
                if (command == Command.Punch)
                {
                    GetComponent<Animator>().SetTrigger("Punch");
                    CurrentState = State.Attacking;
                }
                if (command == Command.Jump)
                {
                    CurrentState = State.Jumping;
                }
                if (command == Command.Block)
                {
                    CurrentState = State.Blocking;
                    GetComponent<Animator>().SetBool("Block", true);
                }
                if (command == Command.NoBlock)
                {
                    GetComponent<Animator>().SetBool("Block", false);
                    CurrentState = State.Idle;
                }
                break;
            case State.Attacking:
                // Attack animation or whatever is playing out.
                break;
        }
    }

    private void jumpDown()
    {
        iTween.MoveTo(jumpHelper,
             new Hashtable
                            {
                                {"y", 0},
                                {"time", 0.5f},
                                {"EaseType", "easeInCubic"},
                                {"oncomplete", "jumpExit"},
                                {"oncompletetarget", gameObject }
                            });
        GetComponent<Animator>().SetTrigger("JumpDown");
    }

    private void jumpExit()
    {
        GetComponent<Animator>().SetTrigger("JumpExit");
    }

    private void Update()
    {
        if (Opponent == null || CurrentState == State.Dead) return;
        facingDirection = Math.Sign(Opponent.transform.position.x - gameObject.transform.position.x);
        int movingDirection = 0;
        if (CurrentDirection == Command.MoveLeft)
        {
            movingDirection = -1;
        }
        else if (CurrentDirection == Command.MoveRight)
        {
            movingDirection = 1;
        }
        else if (CurrentDirection == Command.MoveNone)
        {
            movingDirection = 0;
            
        }

        if (CurrentState == State.Blocking || CurrentState == State.Laying) return;
        if (CurrentState != State.Attacking && CurrentState != State.Jumping)
        {
            if (CurrentDirectionTimeStamp == -1)
            {
                CurrentState = State.Idle;
            }
            else if (Time.time - CurrentDirectionTimeStamp > 0.5f && enableRunning)
            {
                CurrentState = State.Running;
            }
            else
            {
                CurrentState = State.Walking;
            }
        }
        switch (CurrentState)
        {
            case State.Idle:
                turnPlayer(facingDirection);
                GetComponent<Animator>().SetFloat("Speed", 0);
                break;
            case State.Walking:
                turnPlayer(facingDirection);
                move(movingDirection*walkingSpead);
                GetComponent<Animator>().SetFloat("Speed", 1);
                break;
            case State.Running:
                turnPlayer(movingDirection);
                move(movingDirection*runningSpeed);
                GetComponent<Animator>().SetFloat("Speed", 2);
                break;
                case State.Jumping:
                if (movingDirection != 0) turnPlayer(movingDirection);
                move(movingDirection * jumpingSpeed);
                break;
            case State.Attacking:
                turnPlayer(facingDirection);
                break;

            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void turnPlayer(float direction)
    {
        transform.localScale = new Vector3(Math.Sign(direction), 1, 1);
    }

    private void resetState()
    {
        if (CurrentDirectionTimeStamp != -1) CurrentDirectionTimeStamp = Time.time;
        if(CurrentState!=State.Blocking)CurrentState = State.Idle;
    }

    private void move(float speed)
    {
        Direction = Mathf.Sign(speed);
        if (AllowMovement || CurrentState == State.Jumping)
        {
            transform.AddX(speed*Time.deltaTime);
        }
    }

    public void damage(float damage, Avatar opponent)
    {
        if (CurrentState == State.Dead) return;
        if (CurrentState == State.Blocking)
        {
            PlayerProfile.HealthBar.Value -= damage/5;
            iTween.MoveTo(gameObject,
                new Hashtable
                            {
                                {"x", gameObject.transform.position.x + -1*facingDirection},
                                {"time", 0.25f},
                                {"EaseType", "easeOutQuad"}
                            });
        }
        else if (CurrentState == State.Jumping)
        {
            //TODO fix this avatar should fall down
        }
        else 
        {
            PlayerProfile.HealthBar.Value -= damage;
            if (damage <= 10)
            {
                iTween.MoveTo(gameObject,
                    new Hashtable
                            {
                                {"x", gameObject.transform.position.x + -5*facingDirection},
                                {"time", 0.25f},
                                {"EaseType", "easeOutQuad"}
                            });
                GetComponent<Animator>().SetTrigger("Hurt");
            }
            else
            {
                CurrentState = State.Laying;
                GetComponent<Animator>().SetTrigger("Fall");
            }
        }

        if (PlayerProfile.HealthBar.Value == 0)
        {
            CurrentState = State.Dead;
            GetComponent<Animator>().SetTrigger("Die");
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    public void disableRunning()
    {
        enableRunning = false;
    }
}