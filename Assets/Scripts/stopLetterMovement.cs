using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stopLetterMovement : MonoBehaviour
{
    [SerializeField] private GameObject[] letter;
    [SerializeField] private float delay;
    [SerializeField] private float speedUpPercantage;
    private List<Rigidbody2D> letterRigid = new List<Rigidbody2D>();
    private bool letterReachedBottom;
    private int counter = 0;
    private float setcorrectposition = 0.03f;
    private float timer;
    private float difference;

    


    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < letter.Length; i++ )
        {
            letterRigid.Add(letter[i].GetComponent<Rigidbody2D>());
        }
        speedUpPercantage = speedUpPercantage / 100 +1;
        timer = 1f;
        letterReachedBottom = false;
    }

    private void Update()
    {
        timer += Time.deltaTime;
    }

    private void speedmultiplier()
    {
        letter[1].GetComponent<lettercode>().speedupletters(speedUpPercantage);
        letter[2].GetComponent<lettercode>().speedupletters(speedUpPercantage);

    }

    public void letterreachedbottom()
    {
        letterReachedBottom = true;
    }

    private void stopletters(GameObject letter, Rigidbody2D letterRigid)
    {
        letterRigid.velocity = new Vector2(0, 0);
        speedmultiplier();
        difference = letter.transform.position.y - setcorrectposition;
        letter.GetComponent<lettercode>().stopletters(difference);
        letter.transform.position = new Vector2(letter.transform.position.x, setcorrectposition);
        counter++;
        timer = 0f;
    }



    private void OnTriggerStay2D(Collider2D collision)
    {
        if (letterReachedBottom)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                if (timer > delay)
                {
                    if (counter == 0 && collision.tag == "CorrectLetter1")
                    {
                        stopletters(letter[0], letterRigid[0]);
                    }
                    else if (counter == 1 && collision.tag == "CorrectLetter2")
                    {
                        stopletters(letter[1], letterRigid[1]);
                    }
                    else if (counter == 2 && collision.tag == "CorrectLetter3")
                    {
                        stopletters(letter[2], letterRigid[2]);
                    }
                }
            }
        }
    }

}

