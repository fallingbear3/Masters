using Assets.Scripts.model;
using DefaultNamespace;
using UnityEngine;

public class SwitchBackground : MonoBehaviour
{
    public Sprite Janacek;
    public Sprite Wagner;
    public Sprite Martinu;
    public Sprite Strauss;

    private void Start()
    {
        switch (FindObjectOfType<Repository>().FighterType)
        {
            case Fighter.Type.Janacek:
                GetComponent<SpriteRenderer>().sprite = Janacek;
                break;
            case Fighter.Type.Wagner:
                GetComponent<SpriteRenderer>().sprite = Wagner;
                break;
            case Fighter.Type.Martinu:
                GetComponent<SpriteRenderer>().sprite = Martinu;
                break;
            case Fighter.Type.Strauss:
                GetComponent<SpriteRenderer>().sprite = Strauss;
                break;
        }
    }
}