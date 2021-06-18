using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
   
   // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
       
    }

    ////“–‚½‚è”»’èŠm”F—p
    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log(col.gameObject.name + "Enter");
    }
    
}
