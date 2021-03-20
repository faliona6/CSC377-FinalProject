using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using DG.Tweening;

public class PlayerVR : MonoBehaviour
{
    public int startingHealth;

    public Action deathEvent;

    private int health;

    public Canvas UICanvas;
    public GameObject Heart1, Heart2, Heart3;

    void Start()
    {
        health = startingHealth;
        Heart1.transform.DOLocalMoveY(Heart1.transform.localPosition.y + 10f, 1.5f)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.InOutSine);
        Heart2.transform.DOLocalMoveY(Heart2.transform.localPosition.y - 10f, 1.4f)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.InOutSine);
        Heart3.transform.DOLocalMoveY(Heart3.transform.localPosition.y + 9f, 1.3f)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.InOutSine);
    }

    public void changeHealth(int deltaHealth)
    {
        health += deltaHealth;
        if (health == 2)
            Heart3.SetActive(false);
        if (health == 1)
            Heart2.SetActive(false);
        if (health == 0)
            Heart1.SetActive(false);
        if (health <= 0)
            deathEvent?.Invoke();
    }
}
