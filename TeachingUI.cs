using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeachingUI : MonoBehaviour
{
    public GameObject teachingCanvas;
    [SerializeField] private GameObject[] teachingPanel;
    
    private int index;


    private void Start()
    {
        index = 0;
        teachingPanel[0].SetActive(true);
    }

    public void Next()
    {
        if (index == teachingPanel.Length - 1) 
        {
            teachingPanel[index].SetActive(false);

            Destroy (teachingCanvas);
        }
        else
        {
            if (teachingPanel[index + 1] != null) teachingPanel[index + 1].SetActive(true);
            teachingPanel[index].SetActive(false);
            index++;
        }
    }

    public void Skip()
    {
        teachingCanvas.SetActive(false);
        
        Destroy (teachingCanvas);
    }
}
