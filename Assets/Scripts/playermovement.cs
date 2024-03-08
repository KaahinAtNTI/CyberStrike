using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class playermovement : MonoBehaviour
{
    public float speed = 12.0f;
    public GameObject bullet;
    public float maxShootSPeed = 0.3f;
    float nextBullet = 0.0f;

    Rigidbody2D rb;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }



    void Update() {
        // Get the mouse position in screen coordinates
        Vector3 mousePosition = Input.mousePosition;

        // Convert the mouse position to world coordinates
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        // Calculate the direction from the transform to the mouse position
        Vector2 direction = new Vector2(
            mousePosition.x - transform.position.x,
            mousePosition.y - transform.position.y
        );
        if(Input.GetButton("Jump") && Time.time > nextBullet) {
            nextBullet = Time.time + maxShootSPeed;
            GameObject newBullet = Instantiate(bullet, transform.position, transform.rotation);
            newBullet.GetComponent<Rigidbody2D>().velocity = direction * 10.0f;
            Destroy(newBullet, 2.0f);
        }

        // Calculate the angle between the sprite and the mouse position
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Apply the rotation to the sprite
        transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle-90.0f));
        rb.position += new Vector2(Input.GetAxis("Horizontal")*speed*Time.deltaTime, Input.GetAxis("Vertical")*speed*Time.deltaTime);
    }
}
