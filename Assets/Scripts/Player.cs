 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MonoBehaviour
{
    public Action<int, int> onHpChanged;

    public float m_moveSpeed;
    [SerializeField] private Projectile m_Projectile;
    [SerializeField] private Transform m_FiringPoint;
    [SerializeField] private float m_FiringCoolDown;
    [SerializeField] private int m_Hp;


    [SerializeField ]private int m_CurrentHp;
    private float m_TempCoolDown;
    private SpawnManager m_SpawnManager;
    private GameManager m_GameManager;


    // Start is called before the first frame update
    void Start()
    {
        m_CurrentHp = m_Hp;
        if (onHpChanged != null)
        {
            onHpChanged(m_CurrentHp, m_Hp);
        }
        m_SpawnManager = FindObjectOfType<SpawnManager>();
        m_GameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!m_GameManager.isActive())
            return;

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        if ((horizontal < 0 && transform.position.x <= -2.7)||(horizontal > 0 && transform.position.x >= 2.7))
           horizontal = 0;
        if ((vertical < 0 && transform.position.y <= -5.1) || (vertical > 0 && transform.position.y >= 5.1))
            vertical = 0;
        Vector2 direction = new Vector2(horizontal, vertical);
        transform.Translate(direction * Time.deltaTime * m_moveSpeed);

        if (Input.GetKey(KeyCode.Space))
        {
            if(m_TempCoolDown<=0)
            {
                Fire();
                m_TempCoolDown = m_FiringCoolDown;
            }
        }
        m_TempCoolDown -= Time.deltaTime;
    }
    private void Fire()
    {
        Projectile projectile = m_SpawnManager.SpawnPlayerProjectile(m_FiringPoint.position);
        projectile.Fire();
        m_SpawnManager.SpawnShootingFX(m_FiringPoint.position);
    }
    public void Hit(int damage)
    {
        m_CurrentHp -= damage;
        if (onHpChanged != null)
        {
            onHpChanged(m_CurrentHp, m_Hp);
        }
        if (m_CurrentHp <= 0)
        {

            Destroy(gameObject);
            m_SpawnManager.SpawnExplosionFX1(m_FiringPoint.position);
            m_SpawnManager.SpawnExplosionFX2(m_FiringPoint.position);
            m_GameManager.Gameover(false);
        }    

    }
}
