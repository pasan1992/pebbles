using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaitAndPrint());
    }

    // suspend execution for waitTime seconds
    IEnumerator WaitAndPrint()
    {
        yield return new WaitForSeconds(8);
        Destroy(this.gameObject);
    }
}
