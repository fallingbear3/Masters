using UnityEngine;
using Assets.Scripts.model;
using DefaultNamespace;
using UnityEngine.UI;

public class Bar : MonoBehaviour
{
    public Sprite Janacek;
    public Sprite Wagner;
    public Sprite Martinu;
    public Sprite Strauss;

    void Start ()
    {
        var repo = FindObjectsOfType<Repository>()[0];
        switch (repo.FighterType)
        {
            case Fighter.Type.Janacek:
                GetComponent<Image>().sprite = Janacek;
                break;
            case Fighter.Type.Wagner:
                GetComponent<Image>().sprite = Wagner;
                break;
            case Fighter.Type.Martinu:
                GetComponent<Image>().sprite = Martinu;
                break;
            case Fighter.Type.Strauss:
                GetComponent<Image>().sprite = Strauss;
                break;
        }
    }
}
