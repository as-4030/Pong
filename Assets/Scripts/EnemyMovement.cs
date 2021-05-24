using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {
    public float Speed = 10f;
    public Rigidbody2D rb;
    public GameObject ball;

    void Start() {
        this.rb = GetComponent<Rigidbody2D>();
        this.rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        this.ball = GameObject.Find("Ball");
    }

    void FixedUpdate() {
        float positionDifference = CalculatePositionDifference();

        switch (positionDifference > 0.5 ? "Below":
                positionDifference < 0.5 && positionDifference > -0.5 ? "Mid":
                positionDifference < -0.5 ? "Above": "Default") {
            case "Below": // If ball significantly below the enemy;
                this.rb.MovePosition(rb.position + new Vector2(0, -0.085f));
                break;
            case "Mid": // If ball within enemy's middle region;
                break;
            case "Above": // If ball significantly above the enemy;
                this.rb.MovePosition(rb.position + new Vector2(0, 0.085f));
                break;
            case "Default":
                break;
        }
    }

    float CalculatePositionDifference() {
        return this.rb.position.y - this.ball.transform.position.y;
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject == this.ball) {
            collision.rigidbody.AddForce(new Vector2(-3, 1), ForceMode2D.Impulse);
        }
    }

}