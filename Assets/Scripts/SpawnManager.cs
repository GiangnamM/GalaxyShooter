using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class EnemiesPool
{
    public Enemy prefab;
    public List<Enemy> inactiveObjs;
    public List<Enemy> activeObjs;
    public Enemy Spawn(Vector3 position, Transform parent)
    {
        if(inactiveObjs.Count == 0)
        {
            Enemy newObj = GameObject.Instantiate(prefab, parent);
            newObj.transform.position = position;
            activeObjs.Add(newObj);
            return newObj;
        }
        else
        {
            Enemy oldObj = inactiveObjs[0];
            oldObj.gameObject.SetActive(true);
            oldObj.transform.SetParent(parent);
            oldObj.transform.position = position;
            activeObjs.Add(oldObj);
            inactiveObjs.RemoveAt(0);
            return oldObj;

        }
    }
    public void Release(Enemy obj)
    {
        if(activeObjs.Contains(obj))
        {
            activeObjs.Remove(obj);
            inactiveObjs.Add(obj);
            obj.gameObject.SetActive(false);
        }
    }
    public void Clear()
    {
        while (activeObjs.Count > 0)
        {
            Enemy obj = activeObjs[0];
            obj.gameObject.SetActive(false);
            activeObjs.RemoveAt(0);
            inactiveObjs.Add(obj);
        }
    }
}

[System.Serializable]
public class ProjectilesPool
{
    public Projectile prefab;
    public List<Projectile> inactiveObjs;
    public List<Projectile> activeObjs;
    public Projectile Spawn(Vector3 position, Transform parent)
    {
        if (inactiveObjs.Count == 0)
        {
            Projectile newObj = GameObject.Instantiate(prefab, parent);
            newObj.transform.position = position;
            activeObjs.Add(newObj);
            return newObj;
        }
        else
        {
            Projectile oldObj = inactiveObjs[0];
            oldObj.gameObject.SetActive(true);
            oldObj.transform.SetParent(parent);
            oldObj.transform.position = position;
            activeObjs.Add(oldObj);
            inactiveObjs.RemoveAt(0);
            return oldObj;

        }
    }
    public void Release(Projectile obj)
    {
        if (activeObjs.Contains(obj))
        {
            activeObjs.Remove(obj);
            inactiveObjs.Add(obj);
            obj.gameObject.SetActive(false);
        }
    }
    public void Clear()
    {
        while (activeObjs.Count > 0)
        {
            Projectile obj = activeObjs[0];
            obj.gameObject.SetActive(false);
            activeObjs.RemoveAt(0);
            inactiveObjs.Add(obj);
        }
    }
}

[System.Serializable]
public class ParticleFXsPool
{
    public particleFX prefab;
    public List<particleFX> activeObjs;
    public List<particleFX> inactiveObjs;
    public particleFX Spawn(Vector3 position, Transform parent)
    {
        if (inactiveObjs.Count == 0)
        {
        particleFX Obj = GameObject.Instantiate(prefab, parent);
            Obj.transform.position = position;
            activeObjs.Add(Obj);
            return Obj;
        }
        else
        {
            particleFX Obj = inactiveObjs[0];
            Obj.gameObject.SetActive(true);
            Obj.transform.SetParent(parent);
            Obj.transform.position = position;
            inactiveObjs.RemoveAt(0);
            activeObjs.Add(Obj);
            return Obj;

        }
    }
    public void Release(particleFX obj)
    {
        if (activeObjs.Contains(obj))
        {
            activeObjs.Remove(obj);
            inactiveObjs.Add(obj);
            obj.gameObject.SetActive(false);
        }
    }
    public void Clear()
    {
        while (activeObjs.Count > 0)
        {
            particleFX obj = activeObjs[0];
            obj.gameObject.SetActive(false);
            activeObjs.RemoveAt(0);
            inactiveObjs.Add(obj);
        }
    }

}

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private bool m_Active;
    //[SerializeField] private Enemy m_EnemyPrefab;
    [SerializeField] private EnemiesPool m_EnemiesPool;
    [SerializeField] private ProjectilesPool m_PlayerProjectilesPool;
    [SerializeField] private ProjectilesPool m_EnemyProjectilesPool;

    [SerializeField] private int m_MinTotalEnemies;
    [SerializeField] private int m_MaxTotalEnemies;

    [SerializeField] private float m_EnemySpawnInterval;
    [SerializeField] private EnemyPath[] m_Paths;
    [SerializeField] private int m_TotalGroups;
    [SerializeField] private ParticleFXsPool m_HitFXsPool;
    [SerializeField] private ParticleFXsPool m_ShootingFXsPool;
    [SerializeField] private ParticleFXsPool m_Explosion1;
    [SerializeField] private ParticleFXsPool m_Explosion2;
    [SerializeField] private Player m_PlayerPrefab;

    public Player Player => m_Player;

    private Player m_Player;
    private bool m_isSpawningEnemies;

   
    public void StartBalte()
    {
        if (m_Player == null)
            m_Player = Instantiate(m_PlayerPrefab);
        m_Player.transform.position = Vector3.zero;

        StartCoroutine(IESpawnGroups(m_TotalGroups));
    }

    private IEnumerator IESpawnGroups(int pGroups)
    {
        m_isSpawningEnemies = true;
        for (int i=0;i<pGroups;i++)
        {
        int totalEnemies = Random.Range(m_MinTotalEnemies, m_MaxTotalEnemies);
            EnemyPath path = m_Paths[Random.Range(0, m_Paths.Length)];
            yield return StartCoroutine(IESpawnEnemies(totalEnemies, path));
            if (i < pGroups -1)
            yield return new WaitForSeconds(3f);


        }
        m_isSpawningEnemies = false;
    }

    private IEnumerator IESpawnEnemies(int totalEnemies, EnemyPath path)
    {

         for (int i = 0; i<totalEnemies; i++)
        {
            yield return new WaitUntil(() => m_Active);
            yield return new WaitForSeconds(m_EnemySpawnInterval);

            //Enemy enemy = Instantiate(m_EnemyPrefab, transform);
            Enemy enemy = m_EnemiesPool.Spawn(path.WayPoint[0].position, transform);
            enemy.Init(path.WayPoint);
        }
    }

    public void ReleaseEnemy(Enemy obj)
    {
        m_EnemiesPool.Release(obj);
    }

    public void ReleaseEnemyController(Enemy enemy)
    {
        m_EnemiesPool.Release(enemy);
    }

    public Projectile SpawnEnemyProjectile(Vector3 position)
    {
        Projectile obj = m_EnemyProjectilesPool.Spawn(position, transform);
        obj.SetFromPlayer(false);
        return obj;
    }
    public void ReleaseEnemyProjectile(Projectile projectile)
    {
        m_EnemyProjectilesPool.Release(projectile);
    }

    public Projectile SpawnPlayerProjectile(Vector3 position)
    {
        Projectile obj = m_PlayerProjectilesPool.Spawn(position, transform);
        obj.SetFromPlayer(true);
        return obj;
    }
    public void ReleasePlayerProjectile(Projectile projectile)
    {
        m_PlayerProjectilesPool.Release(projectile);
    }

    public  particleFX SpawnHitFX(Vector3 position)
    {
        particleFX fx = m_HitFXsPool.Spawn(position, transform);
        fx.SetPool(m_HitFXsPool);
        return fx;
    }

    public void ReleaseHitFX(particleFX obj)
    {
        m_HitFXsPool.Release(obj);
    }

    public particleFX SpawnShootingFX(Vector3 position)
    {
        particleFX fx = m_ShootingFXsPool.Spawn(position, transform);
        fx.SetPool(m_ShootingFXsPool);
        return fx;
    }

    public void ReleaseShootingFX(particleFX obj)
    {
        m_ShootingFXsPool.Release(obj);
    }

    public bool IsClear()
    {
        if (m_isSpawningEnemies || m_EnemiesPool.activeObjs.Count > 0)
            return false;
        return true;
    }
    public particleFX SpawnExplosionFX1(Vector3 position)
    {
        particleFX fx = m_Explosion1.Spawn(position, transform);
        fx.SetPool(m_Explosion1);
        return fx;
    }

    public void ReleaseExplosionFX1(particleFX obj)
    {
        m_Explosion1.Release(obj);
    }

    public particleFX SpawnExplosionFX2(Vector3 position)
    {
        particleFX fx = m_Explosion2.Spawn(position, transform);
        fx.SetPool(m_Explosion2);
        return fx;
    }

    public void ReleaseExplosionFX2(particleFX obj)
    {
        m_Explosion1.Release(obj);
    }

    public void Clear()
    {
        m_EnemiesPool.Clear();
        m_EnemyProjectilesPool.Clear();
        m_HitFXsPool.Clear();
        m_PlayerProjectilesPool.Clear();
        m_ShootingFXsPool.Clear();
        m_Explosion1.Clear();
        m_Explosion2.Clear();
    }

    /*private IEnumerator IETestCoroutine()
    {
        yield return new WaitUntil(() => m_Active);
        for (int i = 0; i<5; i++)
        {
            Debug.Log(i);
            yield return new WaitForSeconds(1f);
        }
    }*/
}
