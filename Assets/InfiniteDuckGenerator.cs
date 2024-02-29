using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteDuckGenerator : MonoBehaviour
{
    [SerializeField] float nextSpawn;
    [SerializeField] float spawnRate;
    public GameObject duckCopies;
    // Start is called before the first frame update
    void Start()
    {
        //gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
            if (Time.time > nextSpawn)
            {
                Instantiate(duckCopies,gameObject.transform.position, Quaternion.identity);
            nextSpawn = Time.time + 1f / spawnRate;
            }
           
    }
}