using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPath : MonoBehaviour
{
    [SerializeField] private Transform[] m_WayPoint;
    [SerializeField] private Color m_color;
    [SerializeField] private bool m_show;
    public Transform[] WayPoint => m_WayPoint;
    private void OnDrawGizmos()
    {
        if (!m_show)
            return;
        if (m_WayPoint != null && m_WayPoint.Length > 1)
        {
            Gizmos.color = m_color;
            for (int i = 0; i < m_WayPoint.Length - 1; i++)
            {
                Transform from = m_WayPoint[i];
                Transform to = m_WayPoint[i + 1];
                Gizmos.DrawLine(from.position, to.position);
            }
            Gizmos.DrawLine(m_WayPoint[0].position, m_WayPoint[m_WayPoint.Length - 1].position);
        }
    }
}
