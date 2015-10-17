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
    public float speed = 15;
    public PlayerProfile PlayerProfile;
    private Tween tween;

    public SpecialAttack specialAttackPrefabA;
    public SpecialAttack specialAttackPrefabB;
    public SpecialAttack specialAttackPrefabC;

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
    }

    private void fall()
    {
        GetComponent<Animator>().SetTrigger("Fall");
    }

    public void musicWave()
    {
        GetComponent<MusicWave>().execute();
    }

    public void jump()
    {
        tween.startTween(jumpDuration);
    }

    public void crouchDown()
    {
        GetComponent<Animator>().SetBool("Crouch", true);
    }

    public void crouchUp()
    {
        GetComponent<Animator>().SetBool("Crouch", false);
    }

    public void punch()
    {
        GetComponent<Animator>().SetTrigger("Punch");
    }

    private void OnTween(float progress, float tweenvalue)
    {
        transform.SetY(tweenvalue*jumpHeight);
        GetComponent<Animator>().SetFloat("Jump", progress);
    }

    public void move(float direction)
    {
        Direction = direction;
        GetComponent<Animator>().SetFloat("Speed", speed * Math.Abs(direction));

        if (AllowMovement)
        {
            transform.AddX(speed*direction*Time.deltaTime);
            if (direction < 0)
            {
                transform.localScale = Vector3.one.Multiply(new Vector3(-1, 1, 1));
            }
            if (direction > 0)
            {
                transform.localScale = Vector3.one.Multiply(new Vector3(1, 1, 1));
            }
        }
    }

    public void executeSpecialAttackA()
    {
        executeSpecialAttack(specialAttackPrefabA);
    }
    public void executeSpecialAttackB()
    {
        executeSpecialAttack(specialAttackPrefabB);
    }
    public void executeSpecialAttackC()
    {
        executeSpecialAttack(specialAttackPrefabC);
    }

    private void executeSpecialAttack(SpecialAttack specialAttack)
    {
        if (PlayerProfile.PowerBar.Value == 100)
        {
            PlayerProfile.PowerBar.Value = 0;

            var newAttack = Instantiate(specialAttack);
            newAttack.transform.position = transform.position;
            newAttack.startAttack(this);
        }
    }

    public void takeDamage()
    {
        GetComponent<Animator>().SetTrigger("TakeDamage");
    }
}