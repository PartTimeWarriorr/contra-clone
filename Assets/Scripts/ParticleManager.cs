using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

[RequireComponent(typeof(ParticleSystem))]
public class ReturnToPool : MonoBehaviour
{
    public ParticleSystem system;
    public IObjectPool<ParticleSystem> pool;

    void Start()
    {
        system = GetComponent<ParticleSystem>();
        var main = system.main;
        main.stopAction = ParticleSystemStopAction.Callback;
    }

    void OnParticleSystemStopped()
    {
        pool.Release(system);
    }
}

public class ParticleManager : MonoBehaviour
{
    public ParticleSystem particles;
    private IObjectPool<ParticleSystem> objectPool;

    public int maxObjects = 10;
    public bool collectionChecks = true;

    private void Awake()
    {
        objectPool = new ObjectPool<ParticleSystem>(CreateParticles, OnTakeFromPool, OnReturnedToPool, OnDestroyPoolObject, collectionChecks, maxObjects, maxObjects);
    }

    ParticleSystem CreateParticles()
    {
        ParticleSystem newPs = Instantiate(particles);
        newPs.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);

        ReturnToPool returnToPool = newPs.gameObject.AddComponent<ReturnToPool>();
        returnToPool.pool = objectPool;

        return newPs;
    }

    void OnTakeFromPool(ParticleSystem ps)
    {
        ps.gameObject.SetActive(true);
    }

    void OnReturnedToPool(ParticleSystem ps)
    {
        ps.gameObject.SetActive(false);
    }

    void OnDestroyPoolObject(ParticleSystem ps)
    {
        Destroy(ps.gameObject);
    }

    void OnEnable()
    {
        EnemyHealth.OnEnemyDeath += SpawnParticles;
    }

    void OnDisable()
    {
        EnemyHealth.OnEnemyDeath -= SpawnParticles;
    }

    void SpawnParticles(Vector3 pos)
    {
        ParticleSystem newParticles = objectPool.Get();
        newParticles.transform.position = pos;
        newParticles.Play();
    }
}
