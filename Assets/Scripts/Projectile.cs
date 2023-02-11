using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float m_MoveSpeed;
    public Vector2 m_Direction;
    [SerializeField] private int m_Damage;

    private bool m_FromPlayer;
    private SpawnManager m_SpawnManager;
    private float m_LifeTime;
    // Start is called before the first frame update
    void Start()
    {
        m_SpawnManager = FindObjectOfType<SpawnManager>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(m_Direction * m_MoveSpeed * Time.deltaTime);

        if (m_LifeTime <= 0)
            Release();
        m_LifeTime -= Time.deltaTime;
    }
    public void Fire()
    {
        //Destroy(gameObject, 10f);
        m_LifeTime = 10f;
    }
    
    private void Release()
    {
        if (m_FromPlayer)
            m_SpawnManager.ReleasePlayerProjectile(this);
        else 
            m_SpawnManager.ReleaseEnemyProjectile(this);
    }

    public void SetFromPlayer(bool value)
    {
        m_FromPlayer = value;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Trigger" + collision.gameObject.name);

        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (m_FromPlayer)
                m_SpawnManager.ReleasePlayerProjectile(this);
            else
                m_SpawnManager.ReleaseEnemyProjectile(this);
            Vector3 hitPos = collision.ClosestPoint(transform.position);
            m_SpawnManager.SpawnHitFX(hitPos);

            Enemy enemy;
            collision.gameObject.TryGetComponent(out enemy);
            enemy.Hit(m_Damage);
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            if (m_FromPlayer)
                m_SpawnManager.ReleasePlayerProjectile(this);
            else
                m_SpawnManager.ReleaseEnemyProjectile(this);
            Vector3 hitPos = collision.ClosestPoint(transform.position);
            m_SpawnManager.SpawnHitFX(hitPos);
            Player player;
            collision.gameObject.TryGetComponent(out player);
            player.Hit(m_Damage);
        }
    }

}
