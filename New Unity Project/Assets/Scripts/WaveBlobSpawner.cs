using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveBlobSpawner : MonoBehaviour {

    public enum SpawnState { SPAWNING, WAITING, COUNTING};

    [System.Serializable]
    public class Wave
    {
        public string name;
        public Transform NormalBlobEnemy;
        public Transform StoneBlobEnemy;
        public int NormalBlobCount;
        public int StoneBlobCount;
        public float rate;
    }

    public Wave[] waves;
    private int nextWave = 0;

    public float timeBetweenWaves = 5f;
    public float waveCountDown;

    private float searchCountdown = 1f;

    private SpawnState state = SpawnState.COUNTING;

    void Start()
    {
        waveCountDown = timeBetweenWaves;
    }

    void Update()
    {
        if(state == SpawnState.WAITING)
        {
            if (!EnemyIsAlive())
            {
                WaveCompleted();
            }
            else
            {
                return;
            }
        }

        if(waveCountDown <= 0)
        {
            if(state != SpawnState.SPAWNING)
            {
                StartCoroutine(SpawnWave(waves[nextWave]));
            }
        }
        else
        {
            waveCountDown -= Time.deltaTime;
        }
    }

    void WaveCompleted()
    {
        Debug.Log("Wave Completed");

        state = SpawnState.COUNTING;
        waveCountDown = timeBetweenWaves;

        if(nextWave + 1 > waves.Length - 1)
        {
            nextWave = 0;
            Debug.Log("ALL WAVES COMPLETE! Looping...");
        }
        else
        {
            nextWave++;
        }


    }

    bool EnemyIsAlive()
    {
        searchCountdown -= Time.deltaTime;
        if(searchCountdown <= 0f)
        {
            searchCountdown = 1f;
            if(GameObject.FindGameObjectsWithTag("Enemy") == null)
            {

                return false;
            }
        }
        return true;
    }

    IEnumerator SpawnWave(Wave _wave)
    {
        Debug.Log("Spawning Wave:" + _wave.name);
        state = SpawnState.SPAWNING;


        for (int i = 0; i < _wave.NormalBlobCount; i++)
        {
            SpawnEnemy(_wave.NormalBlobEnemy);

            yield return new WaitForSeconds(1f / _wave.rate);
        }

        for (int i = 0; i < _wave.StoneBlobCount; i++)
        {
            SpawnEnemy(_wave.StoneBlobEnemy);

            yield return new WaitForSeconds(1f / _wave.rate);
        }


        state = SpawnState.WAITING;

        yield break;
    }

    void SpawnEnemy(Transform _enemy)
    {
        Debug.Log("Spawning Enemy:" + _enemy.name);
        Instantiate(_enemy, transform.position, transform.rotation);

    }
}
