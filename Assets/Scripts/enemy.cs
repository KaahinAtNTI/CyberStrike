using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    Transform target;
    public float moveSpeed = 6.0f;
    Rigidbody2D rb;

    void Start() {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
    }


    void Update() {
        Vector2 direction = target.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rb.rotation = angle;
        direction.Normalize();
        rb.position += direction * moveSpeed * Time.deltaTime;
    }


    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "Player") {
            transform.root.GetComponent<TestGame>().StartCoroutine("EndGame");
        } else {
            Destroy(gameObject);
        }
    }
}
