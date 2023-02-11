using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float m_MoveSpeed;
    [SerializeField] private Transform[] m_WayPoint;
    [SerializeField] private Projectile m_Projectile;
    [SerializeField] private Transform m_FiringPoint;
    [SerializeField] private float m_MinFiringCoolDown;
    [SerializeField] private float m_MaxFiringCoolDown;
    [SerializeField] private int m_Hp;


    private int m_CurrentHp;
    private float m_TempCoolDown;
    private int m_CurrentWayPointIndex;
    private bool m_Active;
    private SpawnManager m_SpawnManager;
    private GameManager m_GameManager;

    // Start is called before the first frame update
    void Start()
    {
        m_SpawnManager = FindObjectOfType<SpawnManager>();
        m_GameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!m_Active)
            return;
        int nextWayPoint = m_CurrentWayPointIndex + 1;
        if (nextWayPoint > m_WayPoint.Length - 1)
            nextWayPoint = 0;
        transform.position = Vector3.MoveTowards(transform.position, m_WayPoint[nextWayPoint].position, m_MoveSpeed * Time.deltaTime);
        if (transform.position ==m_WayPoint[nextWayPoint].position)
        {
            m_CurrentWayPointIndex = nextWayPoint;
        }
        Vector3 direction = m_WayPoint[nextWayPoint].position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);

            if (m_TempCoolDown <= 0)
            {
                Fire();
            m_TempCoolDown = Random.Range(m_MinFiringCoolDown, m_MaxFiringCoolDown);
            }
       
        m_TempCoolDown -= Time.deltaTime;
    }
    public void Init(Transform[] wayPoints)
    {
        m_WayPoint = wayPoints;
        m_Active = true;
        transform.position = wayPoints[0].position;
        m_TempCoolDown = Random.Range(m_MinFiringCoolDown, m_MaxFiringCoolDown);
        m_CurrentHp = m_Hp;
    }
    private void Fire()
    {
        Projectile projectile = m_SpawnManager.SpawnEnemyProjectile(m_FiringPoint.position);
        projectile.Fire();
    }
    public void Hit(int damege)
    {
        m_CurrentHp -= damege;
        if (m_CurrentHp <=0)
        {
            //Destroy(gameObject);
            m_SpawnManager.ReleaseEnemy(this);
            m_SpawnManager.SpawnExplosionFX1(m_FiringPoint.position);
            m_SpawnManager.SpawnExplosionFX2(m_FiringPoint.position);
            m_GameManager.AddScore(1);
        }
    }
}
