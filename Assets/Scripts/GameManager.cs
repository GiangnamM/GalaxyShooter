using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

    public enum GameState
    {
        Home,
        Gameplay,
        Pause,
        Gameover
    }
public class GameManager : MonoBehaviour
{

    public Action<int> onScoreChanged;


    [SerializeField] private HomePanel m_HomePanel;
    [SerializeField] private GameplayPanel m_GameplayPanel;
    [SerializeField] private PausePanel m_PausePanel;
    [SerializeField] private GameoverPanel m_GameoverPanel;


    private SpawnManager m_SpawnManager;
    private GameState m_GameState;
    private bool m_Win;
    private int m_score;


    // Start is called before the first frame update
    void Start()
    {
        m_SpawnManager = FindObjectOfType<SpawnManager>();
        m_HomePanel.gameObject.SetActive(false);
        m_GameplayPanel.gameObject.SetActive(false);
        m_PausePanel.gameObject.SetActive(false);
        m_GameoverPanel.gameObject.SetActive(false);
        SetState(GameState.Home);
    }

   
    private void SetState(GameState state)
    {
        m_GameState = state;
        m_HomePanel.gameObject.SetActive(m_GameState == GameState.Home);
        m_GameplayPanel.gameObject.SetActive(m_GameState == GameState.Gameplay);
        m_PausePanel.gameObject.SetActive(m_GameState == GameState.Pause);
        m_GameoverPanel.gameObject.SetActive(m_GameState == GameState.Gameover);

        if (m_GameState == GameState.Pause)
            Time.timeScale = 0;
        else Time.timeScale = 1;
    }
    public bool isActive()
    {
        return m_GameState == GameState.Gameplay;
    }
    public void Play()
    {
        m_SpawnManager.StartBalte();
        SetState(GameState.Gameplay);
        m_score = 0;
        if (onScoreChanged!=null)
         onScoreChanged(m_score);
        m_GameplayPanel.DisplayScore(m_score);
    }

    public void Pause()
    {
        SetState(GameState.Pause);
    }

    public void Home()
    {
        SetState(GameState.Home);
        m_SpawnManager.Clear();
    }
    public void Continue()
    {
        SetState(GameState.Gameplay);
    }
    public void Gameover(bool win)
    {
        m_Win = win;
        SetState(GameState.Gameover);
        m_GameoverPanel.DisplayResult(m_Win);
        m_GameoverPanel.DisplayHighScore(m_score);
    }

    public void AddScore(int value)
    {
        m_score += value;
        if (onScoreChanged != null)
            onScoreChanged(m_score);
        m_GameplayPanel.DisplayScore(m_score);
        if (m_SpawnManager.IsClear())
            Gameover(true);

    }
}
