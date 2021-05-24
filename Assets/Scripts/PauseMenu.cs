using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour {
    public GameObject userPlus;
    public GameObject enemyPlus;

    public GameObject pauseSprite;

    void Start() {
        
    }

    void Update() {
        if (Input.GetKeyDown("space")) {
            if (Time.timeScale == 1) {
                this.pauseSprite.transform.localPosition = new Vector3(0, 3, -1);
                Time.timeScale = 0;
            } else {
                this.pauseSprite.transform.localPosition = new Vector3(0, 3, -20);
                Time.timeScale = 1;
            }
        }
    }

}