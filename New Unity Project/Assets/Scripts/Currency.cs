using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Currency : MonoBehaviour
{
    public static int currency;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("currencyTick");
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator currencyTick()
    {
        currency++;
        yield return new WaitForSeconds(1);

        StartCoroutine("currencyTick");
    }
}