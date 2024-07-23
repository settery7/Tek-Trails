using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeSpawn : MonoBehaviour
{
    public GameObject Pipes;
    public float spawnrate = 3;
    private float timer = 0;
    public float heightoffset = 5;

    // Start is called before the first frame update
    void Start()
    {
        SpawnPipe();
    }

    // Update is called once per frame
    void Update()
    {
        if (timer < spawnrate)
        {
            timer = timer + Time.deltaTime;
        }
        else
        {
            SpawnPipe();
            timer = 0;
        }
    }

    void SpawnPipe()
    {
        float lowestpoint = transform.position.y - heightoffset;
        float highestpoint = transform.position.y + heightoffset;
        Instantiate(
            Pipes, 
            new Vector3(transform.position.x, 
            Random.Range(lowestpoint, highestpoint), 0), 
            transform.rotation
            );
    }
}
