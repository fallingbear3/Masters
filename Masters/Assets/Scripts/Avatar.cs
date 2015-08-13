using Assets.Scripts;
using Assets.Shared.Scripts;
using UnityEngine;

[RequireComponent(typeof (Animator), typeof (Tween))]
public class Avatar : MonoBehaviour
{
    public KeyCode CrouchKeyCode;
    public KeyCode JumpKeyCode;
    public KeyCode PunchKeyCode;
    public KeyCode SpecialKeyCode;
    public float jumpDuration = 1;
    public float jumpHeight = 10;
    public float speed = 15;
    public PlayerProfile PlayerProfile;
    private Tween tween;

    private void Start()
    {
        tween = GetComponent<Tween>();
        tween.OnTween += OnTween;
        tween.OnFinish += () => GetComponent<Animator>().SetFloat("Jump", 0);
    }

    private void Update()
    {
        var axis = Input.GetAxis("Horizontal");
        transform.AddX(speed*axis*Time.deltaTime);
        if (axis < 0)
        {
            transform.localScale = Vector3.one.Multiply(new Vector3(-1, 1, 1));
        }
        if (axis > 0)
        {
            transform.localScale = Vector3.one.Multiply(new Vector3(1, 1, 1));
        }

        if (Input.GetKeyDown(PunchKeyCode))
        {
            GetComponent<Animator>().SetTrigger("Punch");
        }

        if (Input.GetKeyDown(CrouchKeyCode))
        {
            GetComponent<Animator>().SetBool("Crouch", true);
        }

        if (Input.GetKeyUp(CrouchKeyCode))
        {
            GetComponent<Animator>().SetBool("Crouch", false);
        }

        if (Input.GetKeyUp(JumpKeyCode))
        {
            tween.startTween(jumpDuration);
        }

        if (Input.GetKeyUp(SpecialKeyCode))
        {
            GetComponent<MusicWave>().execute();
        }
    }

    private void OnTween(float progress, float tweenvalue)
    {
        transform.SetY(tweenvalue*jumpHeight);
        GetComponent<Animator>().SetFloat("Jump", progress);
    }
}