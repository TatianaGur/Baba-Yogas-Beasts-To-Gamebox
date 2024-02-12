using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject[] allBeasts;
    [SerializeField] private GameObject newBeast;
    [SerializeField] private int goldCostForBeast;

    private GameObject isBeastOnScene;

    private int goldCount;//
    [SerializeField]private int goldSum1;//
    [SerializeField] private int goldSum2;//
    [SerializeField] private int goldMinus;//
    [SerializeField] private int goldFinal;//
    [SerializeField] private Text goldFinalText;
    [SerializeField] private GameObject teachingUI;
    

    private bool isPaused;
    private int index;


    private void Start()
    {
        isPaused = false;

        index = 0;
    }

    private void Update()
    {
        StopTimeInLearning();

        GoldCollect();
        ShowGold();

        goldFinal = goldSum2 - goldMinus;

        GameOver();
    }

    /// <summary>
    /// Рестарт
    /// </summary>
    public void RestartLevel()
    {
        SceneManager.LoadScene("BabaYogasBeasts");
    }

    /// <summary>
    /// Пауза
    /// </summary>
    public void Pause()
    {
        if (isPaused)
        {
            pausePanel.SetActive(false);
            Time.timeScale = 1;
        }
        else
        {
            pausePanel.SetActive(true);
            Time.timeScale = 0;
        }
        isPaused = !isPaused;
    }

    /// <summary>
    /// Считает золото и сохраняет в allGoldSum
    /// </summary>
    private void GoldCollect()
    {
        for (int i = 0; i < allBeasts.Length; i++)
        {
            if (allBeasts[i] != null) goldCount = allBeasts[i].GetComponent<BeastInterface>().goldCount;
            
            goldSum1 += goldCount;
        }
        goldSum2 = goldSum1;
        goldSum1 = 0;
    }

    /// <summary>
    /// Показывает сколько золота
    /// </summary>
    private void ShowGold()
    {
        goldFinalText.text = goldFinal.ToString();
    }

    /// <summary>
    /// Покупка нового зверя
    /// </summary>
    public void BuyNewBeast()
    {
        if (goldFinal >= goldCostForBeast)
        {
            goldMinus += goldCostForBeast;

            WhoIsNewBeast();
            if (newBeast != null) Instantiate(newBeast);
        }
    }

    /// <summary>
    /// Назначает следующего зверя
    /// </summary>
    private void WhoIsNewBeast()
    {
        if (index == allBeasts.Length) index = 0;

        newBeast = allBeasts[index];
        index++;
    }

    /// <summary>
    /// Game Over banner, если нет зверей на сцене и не за что купить
    /// </summary>
    private void GameOver()
    {
        isBeastOnScene = GameObject.FindWithTag("Beast");

        if (goldFinal < goldCostForBeast && isBeastOnScene == null)
        {
            gameOverPanel.SetActive(true);
        }
    }

    /// <summary>
    /// В период обучения время игры останавливается
    /// </summary>
    private void StopTimeInLearning()
    {
        if (teachingUI != null) Time.timeScale = 0;
        else Time.timeScale = 1;
    }
}
