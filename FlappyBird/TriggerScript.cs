using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerScript : MonoBehaviour
{
    public Changer change;

    // Start is called before the first frame update
    void Start()
    {
        change = GameObject.FindGameObjectWithTag("TriggerLogic").GetComponent<Changer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 3)
        {
            change.addScore();
        }
        
    }
}
