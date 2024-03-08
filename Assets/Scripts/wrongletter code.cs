using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wronglettercode : MonoBehaviour
{

    private Rigidbody2D rigid;
    

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        rigid.velocity = new Vector2(rigid.velocity.x, rigid.velocity.y);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
