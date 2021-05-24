using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallMovement : MonoBehaviour {
    public bool debounce = true;

    public float Speed = 16f;
    public Rigidbody2D rb;

    public GameObject userScore;
    public GameObject enemyScore;

    public Text userScoreText;
    public Text enemyScoreText;

    public int userScoreInt;
    public int enemyScoreInt;

    public GameObject user;
    public GameObject enemy;

    public GameObject topWall;
    public GameObject bottomWall;

    public GameObject userPlus;
    public GameObject enemyPlus;

    public enum Struck {
        User = 0,
        Enemy = 1,
    }

    public Struck struckStatus {get; set;}

    void Start() {
        this.userScoreInt = 0;
        this.enemyScoreInt = 0;

        this.struckStatus = 0;

        this.userScoreText = userScore.GetComponent<Text>();
        this.enemyScoreText = enemyScore.GetComponent<Text>();

        this.user = GameObject.Find("User");
        this.enemy = GameObject.Find("Enemy");

        this.topWall = GameObject.Find("TopWall");
        this.bottomWall = GameObject.Find("BottomWall");

        this.rb = GetComponent<Rigidbody2D>();
        this.rb.velocity = new Vector2(this.Speed, 0);

        this.rb.AddForce(new Vector2(4, 0), ForceMode2D.Impulse);
    }

    void FixedUpdate() {
        transform.Rotate(new Vector3(0, 0, 180 * Time.deltaTime));

        if ((this.rb.position.x < -8.75) && (this.debounce == true)) { // Checking the position of the ball to see who to award points to.
            this.debounce = false;
            this.enemyScoreInt++;
            this.enemyScoreText.text = this.enemyScoreInt.ToString();
            StartCoroutine("EnemyScore");
        } else if (this.rb.position.x > 8.75 && (this.debounce == true)) {
            this.debounce = false;
            this.userScoreInt++;
            this.userScoreText.text = this.userScoreInt.ToString();
            StartCoroutine("UserScore");
        }
    }

    IEnumerator RestartGame() {
        this.rb.velocity = new Vector2(0, 0); // Cancel the ball's speed/momentum/etc.

        int randomNum = Random.Range(1, 3);

        // Transport it back to either the enemy or user, depending on a random number.
        if (randomNum == 1) {
            this.rb.position = new Vector2(-7.5f, 0); // Near user.
        } else {
            this.rb.position = new Vector2(7.5f, 0); // Near enemy.
        }

        yield return new WaitForSeconds(1); // Chill for a bit.

        this.debounce = true;

        if (randomNum == 1) {
            this.rb.velocity = new Vector2(this.Speed, 0); // Towards enemy.
        } else {
            this.rb.velocity = new Vector2(-this.Speed, 0); // Towards user.
        }
        
    }

    IEnumerator UserScore() {
        float timePassed = 0;

        Time.timeScale = 0.5f;
        while (timePassed < 1) {
            timePassed += 0.5f;
            this.userPlus.transform.localPosition = new Vector3(-8.4f, 3.35f, -2);

            yield return new WaitForSeconds(0.5f);
        }

        Time.timeScale = 1;
        this.userPlus.transform.localPosition = new Vector3(-8.4f, 3.35f, -10);

        StartCoroutine("RestartGame");
    }

    IEnumerator EnemyScore() {
        float timePassed = 0;

        Time.timeScale = 0.5f;
        while (timePassed < 1) {
            timePassed += 0.5f;
            this.enemyPlus.transform.localPosition = new Vector3(8.5f, 3.35f, -2);

            yield return new WaitForSeconds(0.5f);
        }

        Time.timeScale = 1;
        this.enemyPlus.transform.localPosition = new Vector3(-8.4f, 3.35f, -10);

        StartCoroutine("RestartGame");
    }

    void OnCollisionEnter2D(Collision2D collision) { 
        if (collision.gameObject == this.user) { 
            this.struckStatus = Struck.User;
        } else if (collision.gameObject == this.enemy) {
            this.struckStatus = Struck.Enemy;
        }

        if ((this.struckStatus == Struck.User) && (collision.gameObject == this.topWall)) { // Coming from the left, going to the top.
            this.rb.AddForce(new Vector2(2, -2), ForceMode2D.Impulse);
        } else if ((this.struckStatus == Struck.User) && (collision.gameObject == this.bottomWall)) { // Coming from the left, going to the bottom.
            this.rb.AddForce(new Vector2(2, 2), ForceMode2D.Impulse);
        } else if ((this.struckStatus == Struck.Enemy) && (collision.gameObject == this.topWall)) { // Coming from the right, going to the top.
            this.rb.AddForce(new Vector2(-2, -2), ForceMode2D.Impulse);
        } else if ((this.struckStatus == Struck.Enemy) && (collision.gameObject == this.bottomWall)) { // Coming from the right, going to the bottom.
            this.rb.AddForce(new Vector2(-2, 2), ForceMode2D.Impulse);
        }
    }

}