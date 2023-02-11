using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomePanel : MonoBehaviour
{
    private GameManager m_GameManager;

    // Start is called before the first frame update
    void Start()
    {
        m_GameManager = FindObjectOfType<GameManager>();
    }
    public void BtnPlay_Pressed()
    {
        m_GameManager.Play();
    }

}
