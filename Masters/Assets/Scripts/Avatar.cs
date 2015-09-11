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

    private void Start()
    {
        tween = GetComponent<Tween>();
        tween.OnTween += OnTween;
        tween.OnFinish += () => GetComponent<Animator>().SetFloat("Jump", 0);

        if (PlayerProfile != null)
        {
            PlayerProfile.OnHealthChanged += value =>
            {
                if (value == 5)
                {
                    fall();
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

    public void crouch()
    {
        GetComponent<Animator>().SetBool("Crouch", true);
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
        transform.AddX(speed * direction * Time.deltaTime);
        if (direction < 0)
        {
            transform.localScale = Vector3.one.Multiply(new Vector3(-1, 1, 1));
        }
        if (direction > 0)
        {
            transform.localScale = Vector3.one.Multiply(new Vector3(1, 1, 1));
        }
        GetComponent<Animator>().SetFloat("Speed", speed * Math.Abs(direction));
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
        var newAttack = Instantiate(specialAttack);
        newAttack.transform.position = transform.position;
        newAttack.startAttack(this);
    }
}