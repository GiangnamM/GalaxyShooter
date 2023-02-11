using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public class GameplayPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_TxtScore;
    [SerializeField] private Image m_ImgHpBar;
    private GameManager m_GameManager;

    // Start is called before the first frame update
    void Start()
    {
        m_GameManager = FindObjectOfType<GameManager>();
   
    }

    public void BtnPause_Pressed()
    {
        m_GameManager.Pause();
    }

    public void DisplayScore(int score)
    {
        m_TxtScore.text = "SCORE: " + score;
    }
   
}
