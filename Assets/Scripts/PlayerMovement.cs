using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    public float Speed = 10f;
    public Rigidbody2D rb;
    public GameObject ball;

    void Start() {
        this.rb = GetComponent<Rigidbody2D>();
        this.rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        this.ball = GameObject.Find("Ball");
    }

    void FixedUpdate() {
        Vector2 movementVect = new Vector2(0, Input.GetAxis("Vertical"));
        rb.MovePosition(rb.position + (movementVect * this.Speed * Time.deltaTime));
    }

    void OnCollisionEnter2D(Collision2D collision) { 
        if (collision.gameObject == this.ball) {
            collision.rigidbody.AddForce(new Vector2(3, 1), ForceMode2D.Impulse);
        }
    }

}