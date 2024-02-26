using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisuals : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject spawnParticles;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnDisable()
    {
        Instantiate(spawnParticles, transform.position, Quaternion.identity);
    }
}
