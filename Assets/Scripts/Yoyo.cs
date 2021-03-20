using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class Yoyo : MonoBehaviour
{
    public float offset, time;
    // Start is called before the first frame update
    void Start()
    {
        transform.DOLocalMoveY(transform.position.y + offset, time)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.InOutSine);
    }
}
