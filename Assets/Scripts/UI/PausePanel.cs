using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausePanel : MonoBehaviour
{

    private GameManager m_GameManager;

    // Start is called before the first frame update
    void Start()
    {
        m_GameManager = FindObjectOfType<GameManager>();

    }

    public void BtnHome_Pressed()
    {
        m_GameManager.Home();
    }
    public void BtnContinue_Pressed()
    {
        m_GameManager.Continue();
    }
   
}
