using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeastInterface : MonoBehaviour
{
    public int goldCount;

    [SerializeField] private GameObject eatPanel;
    [SerializeField] private GameObject drinkPanel;
    [SerializeField] private GameObject sleepPanel;
    [SerializeField] private GameObject wcPanel;
    [SerializeField] private int wishPeriodicity;
    [SerializeField] private GameObject beastGoesAwayImage;
    [SerializeField] private GameObject whoDisappear;

    [SerializeField] private GameObject star1;
    [SerializeField] private GameObject star2;
    [SerializeField] private GameObject star3;

    private float waitingStar;
    private float minusStar;
    private int randWish;
    private int newWish;
    private bool wantsSleeping;
    private bool wantsEating;
    private bool wantsDrinking;
    private bool wantsWCing;


    private void Start()
    {
        wantsSleeping = false;
        wantsEating = false;
        wantsDrinking = false;
        wantsWCing = false;

        minusStar = 10f;
        waitingStar = minusStar;

        goldCount = 0;
        
        StartCoroutine(SometimesWish());
    }
    private void Update()
    {
        MinusStar();
    }

    private void TurnOnEatPanel()
    {
        eatPanel.SetActive(true);
        wantsEating = true;
    }

    private void TurnOnDrinkPanel()
    {
        drinkPanel.SetActive(true);
        wantsDrinking = true;
    }

    private void TurnOnSleepPanel()
    {
        sleepPanel.SetActive(true);
        wantsSleeping = false;
    }

    private void TurnOnWCPanel()
    {
        wcPanel.SetActive(true);
        wantsWCing = true;
    }

    /// <summary>
    /// Рандомно срабатывает одна из панелей: есть, пить, спать, туалет
    /// </summary>
    private void WhatBeastWants()
    {
        randWish = Random.Range(0, 4);

        switch (randWish)
        {
            case 0:
                TurnOnEatPanel();
                break;
            case 1:
                TurnOnDrinkPanel();
                break;
            case 2:
                TurnOnSleepPanel();
                break;
            case 3:
                TurnOnWCPanel();
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// Рандомно, но в пределах заданного wishPeriodicity, запускает желания зверей
    /// </summary>
    IEnumerator SometimesWish()
    {
        while (true)
        {
            newWish = Random.Range(8, wishPeriodicity);
            yield return new WaitForSeconds(newWish);

            if (!wantsSleeping && !wantsEating && !wantsDrinking && !wantsWCing)
            {
                WhatBeastWants();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Bed"))
        {
            sleepPanel.SetActive(false);
            wantsSleeping = false;
            goldCount++;
        }
        else if (coll.gameObject.CompareTag("Bottle"))
        {
            drinkPanel.SetActive(false);
            wantsDrinking = false;
            goldCount++;
        }
        else if (coll.gameObject.CompareTag("Feeder"))
        {
            eatPanel.SetActive(false);
            wantsEating = false;
            goldCount++;
        }
        else if (coll.gameObject.CompareTag("WC"))
        {
            wcPanel.SetActive(false);
            wantsWCing = false;
            goldCount++;
        }
    }

    /// <summary>
    /// Удаляются звезды при ожидании 5сек
    /// </summary>
    private void MinusStar()
    {
        if (wantsDrinking || wantsEating || wantsSleeping || wantsWCing)
        {
            waitingStar -= Time.deltaTime;
        }

        if (waitingStar <= 0)
        {
            if (star3 != null) 
            {
                Destroy(star3, 1);
                waitingStar = minusStar;
            }
            else if (star2 != null) 
            {
                Destroy(star2, 1);
                waitingStar = minusStar;
            }
            else if (star1 != null) 
            {
                Destroy(star1, 1);
                waitingStar = minusStar;

                StartCoroutine(BeastGoesAway());
            }
        }
    }

    /// <summary>
    /// Попап предупреждает, что скоро один из зверей исчезнет. Затем с задержкой исчезает
    /// </summary>
    IEnumerator BeastGoesAway()
    {
        beastGoesAwayImage.gameObject.SetActive(true);

        yield return new WaitForSeconds(3f);

        beastGoesAwayImage.gameObject.SetActive(false);

        Destroy(whoDisappear, 1);
    }
}
