using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class teleporttotop : MonoBehaviour
{
    [SerializeField] private GameObject stopBox;
    private Rigidbody2D rigid;
    


    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();

    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.transform.position = new Vector2(collision.transform.position.x, 4.65f);
        stopBox.GetComponent<stopLetterMovement>().letterreachedbottom();
    }
    
}
