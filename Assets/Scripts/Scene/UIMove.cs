using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMove : UISpawn
{


    void Start()
    {
        
    }

    void Update()
    {
        
    }
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        player.MoveDone += ActionDetected;

    }
    protected override void OnTriggerExit2D(Collider2D collision)
    {
        base.OnTriggerExit2D(collision);
        player.MoveDone -= ActionDetected;

    }
}
