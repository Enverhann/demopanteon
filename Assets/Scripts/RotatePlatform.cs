using UnityEngine;
using DG.Tweening;

public class RotatePlatform : MonoBehaviour
{
    Vector3 rot = new Vector3(0, 0, 360);
    Vector3 rotStick = new Vector3(0, 360, 0);
    public bool stick = false;
    public int reverseRot;
    public bool reverseRotPlatform = false;
    void Start()
    {
        
    }
    void Update()
    {
        if (stick)
        {
            transform.DOLocalRotate(rotStick, 15, RotateMode.FastBeyond360).SetRelative(true).SetEase(Ease.Linear);
        }
        else
        {
            transform.DOLocalRotate(rot * reverseRot, 25, RotateMode.FastBeyond360).SetRelative(true).SetEase(Ease.Linear);
        }
    }
}
