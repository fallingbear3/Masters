using System;
using Assets.Scripts;
using Assets.Scripts.Attack;
using Assets.Shared.Scripts;
using UnityEngine;

[RequireComponent(typeof (Animator), typeof (Tween))]
public class Avatar : MonoBehaviour
{
    public float jumpDuration = 1;
    public float jumpHeight = 10;
    public float walkingSpead = 7.5f;
    public float runningSpeed = 15;
    public float speed = 15;
    public PlayerProfile PlayerProfile;
    private Tween tween;

    public SpecialAttack specialAttackPrefab;
    private Command CurrentDirection;
    private float CurrentDirectionTimeStamp = -1;
    private GameObject other;

    public float Direction { get; private set; }
    public bool AllowMovement { get; set; }

    private void Start()
    {
        AllowMovement = true;
        tween = GetComponent<Tween>();
        tween.OnTween += OnTween;
        tween.OnFinish += () => GetComponent<Animator>().SetFloat("Jump", 0);

        if (PlayerProfile != null)
        {
            PlayerProfile.HealthBar.OnValueChanged += value =>
            {
                if (value == 5)
                {
                    //TODO fix
                    // fall();
                }
            };
        }

        var player = GameObject.FindGameObjectWithTag("Player");
        var enemy = GameObject.FindGameObjectWithTag("Enemy");

        other = gameObject == player ? enemy : player;
    }

    protected enum State
    {
        Idle, Walking, Running, Jumping, Attacking
    }

    public enum Command
    {
        MoveLeft, MoveRight, MoveNone, Jump, Block, Punch, Kick, Special
    }
    protected State CurrentState { get; private set; }
    
    public void process(Command command)
    {
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

        switch (CurrentState)
        {
            case State.Idle:
            case State.Walking:
            case State.Running:
                if (command == Command.Punch)
                {
                    punch();
                    CurrentState = State.Attacking;
                }
                if (command == Command.Kick)
                {
                    kick();
                    CurrentState = State.Attacking;
                }
                if (command == Command.Special)
                {
                    special();
                    CurrentState = State.Attacking;
                }
                if (command == Command.Jump)
                {
                    CurrentState = State.Jumping;
                }
                break;
            case State.Attacking:
                // Attack animation or whatever is playing out.
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void Update()
    {
        if (CurrentDirectionTimeStamp == -1)
        {
            CurrentState = State.Idle;
        }
        else if (Time.time - CurrentDirectionTimeStamp > 0.5f)
        {
            CurrentState = State.Running;
        }
        else
        {
            CurrentState = State.Walking;
        }

        var facingDirection = other.transform.position.x - gameObject.transform.position.x;
        var movingDirection = CurrentDirection == Command.MoveLeft ? -1 : 1;
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
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void idle()
    {
        GetComponent<Animator>().SetBool("Idle", true);
    }

    private void turnPlayer(float direction)
    {
        transform.localScale = new Vector3(Math.Sign(direction), 1, 1);
    }

    private void fall()
    {
        GetComponent<Animator>().SetTrigger("Fall");
    }

    private void jump()
    {
        tween.startTween(jumpDuration);
    }

    private void crouchDown()
    {
        GetComponent<Animator>().SetBool("Crouch", true);
    }

    private void crouchUp()
    {
        GetComponent<Animator>().SetBool("Crouch", false);
    }

    private void punch()
    {
        GetComponent<Animator>().SetTrigger("Punch");
    }
    private void kick()
    {
        GetComponent<Animator>().SetTrigger("Kick");
    }

    private void OnTween(float progress, float tweenvalue)
    {
        transform.SetY(tweenvalue*jumpHeight);
        GetComponent<Animator>().SetFloat("Jump", progress);
    }

    private void move(float speed)
    {
        Direction = Mathf.Sign(speed);

        if (AllowMovement)
        {
            transform.AddX(speed*Time.deltaTime);
        }
    }

    private void special()
    {
        if (PlayerProfile.PowerBar.Value == 100)
        {
            PlayerProfile.PowerBar.Value = 0;

            var newAttack = Instantiate(specialAttackPrefab);
            newAttack.transform.position = transform.position;
            newAttack.startAttack(this);
        }
    }

    public void takeDamage()
    {
        GetComponent<Animator>().SetTrigger("TakeDamage");
    }
}