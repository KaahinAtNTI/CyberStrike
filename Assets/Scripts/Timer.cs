using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public static Timer instance;
    public float time = 10.0f;
    public TMP_Text text;
    private void Awake() {
        instance = this;
    }
    void Update() {
        text.text = System.Math.Round(time, 2).ToString();
        if(time <= 0) {
            time = 0;
            text.gameObject.SetActive(false);
            return;
        }
        time -= Time.deltaTime;
    }

    public void StartTimer(float timer) {
        text.gameObject.SetActive(true);
        time = timer;
    }

    public void StopTimer() {
        text.gameObject.SetActive(false);
        time = 0.0f;
    }
}
