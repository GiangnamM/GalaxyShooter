using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundController : MonoBehaviour
{
    [SerializeField] private Material m_BigStarsBg;
    [SerializeField] private Material m_MedStarsBg;
    [SerializeField] private float m_BigStarsBgScrollSpeed;
    [SerializeField] private float m_MediumStarsBgScrollSpeed;

    private int m_MainTexId;
    // Start is called before the first frame update
    void Start()
    {
        m_MainTexId  = Shader.PropertyToID("_MainTex");
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 offset = m_BigStarsBg.GetTextureOffset(m_MainTexId);
        offset +=new Vector2(0, m_BigStarsBgScrollSpeed * Time.deltaTime);
        m_BigStarsBg.SetTextureOffset(m_MainTexId, offset);

        offset = m_MedStarsBg.GetTextureOffset(m_MainTexId);
        offset +=new Vector2(0, m_MediumStarsBgScrollSpeed * Time.deltaTime);
        m_MedStarsBg.SetTextureOffset(m_MainTexId, offset);
    }
}
