using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainBow : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform maxPoint;
    public Transform minPoint;

    public GameObject pebel;


    void Start()
    {
        InvokeRepeating("dropRandomPebles", 1, 1);
    }


    private Vector3 getRandomPosition()
    {
        float randomZ = Random.Range(minPoint.position.z, maxPoint.position.z);
        return new Vector3(maxPoint.position.x, this.transform.position.y, randomZ);
    }

    private void dropRandomPebles()
    {
        GameObject.Instantiate(pebel, getRandomPosition(), this.transform.rotation);
    }
}
