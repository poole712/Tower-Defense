using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NormalBlobEnemy : MonoBehaviour
{
    public int hp;

    public Transform homeBase;

    public float normalBlobSpeed;
    public float stoneBlobSpeed;

    public float Health;

    // Start is called before the first frame update
    void Start()
    {
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        agent.destination = homeBase.position;

        normalBlobSpeed = Random.Range(2f, 6f);
        stoneBlobSpeed = Random.Range(1f, 4f);

        if (gameObject.name == "Normal Blob")
        {
            agent.speed += normalBlobSpeed;
        }
        if (gameObject.name == "Stone Blob")
        {
            agent.speed += stoneBlobSpeed;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (hp == 0)
        {
            Currency.currency++;
            Debug.Log(Currency.currency);
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        hp--;
    }
}
