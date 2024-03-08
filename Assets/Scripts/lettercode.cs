using System.Collections;
using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class lettercode : MonoBehaviour
{
    private Rigidbody2D rigid;
    [SerializeField] private float speed;
    [SerializeField] GameObject[] WrongLetters;
    private List<Rigidbody2D> wronglettersrigid = new List<Rigidbody2D>();

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < WrongLetters.Length; i++)
        {
            wronglettersrigid.Add(WrongLetters[i].GetComponent<Rigidbody2D>());
            wronglettersrigid[i].velocity = new Vector2(wronglettersrigid[i].velocity.x, speed * -1);
        }
        rigid = GetComponent<Rigidbody2D>();
        rigid.velocity = new Vector2(rigid.velocity.x, speed * -1);
    }

 
    public void stopletters(float difference)
    {
        for (int i = 0; i < WrongLetters.Count(); i++)
        {
            wronglettersrigid[i].velocity = new Vector2(0,0);
            WrongLetters[i].transform.position = new Vector2(WrongLetters[i].transform.position.x, WrongLetters[i].transform.position.y - difference);
        }
    }

    public void speedupletters(float mulitplier)
    {
        rigid.velocity = new Vector2(rigid.velocity.x, rigid.velocity.y * mulitplier);
        for (int i = 0; i < WrongLetters.Count(); i++)
        {
            wronglettersrigid[i].velocity = new Vector2(wronglettersrigid[i].velocity.x, wronglettersrigid[i].velocity.y * mulitplier);
        }
    }

}

