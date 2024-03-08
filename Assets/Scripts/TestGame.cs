using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGame : MonoBehaviour
{
    public GameObject enemy;
    public GameObject enemyHolder;
    public GameObject hackSuccessScreen, hackFailScreen;
    public float startSpawnRate = 4.0f;
    public float minimumSpawnRate = 0.5f;
    float nextSpawn = 0.0f;
    public void StartGame(Teacher teacher) {
        for(int i = 0; i < enemyHolder.transform.childCount; i++) {
            Destroy(enemyHolder.transform.GetChild(i).gameObject);
        }
        float time = teacher.grade > 3 ? teacher.time+15.0f : teacher.time;
        Timer.instance.StartTimer(time);
        hackFailScreen.SetActive(false);
        hackSuccessScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Timer.instance.time <= 0.0) {
            StartCoroutine(Won());
            return;
        }
        if(Time.time >= nextSpawn) {
            nextSpawn = Time.time + startSpawnRate;
            startSpawnRate = Mathf.Clamp(startSpawnRate - 0.3f, minimumSpawnRate, startSpawnRate);
            Vector2 rPos = new Vector3(Random.Range(-8.0f, 8.0f), Random.Range(-4.0f, 4.0f));
            Instantiate(enemy, new Vector3(rPos.x, rPos.y, 0), Quaternion.identity, enemyHolder.transform);
        }
    }

    IEnumerator Won() {
        hackSuccessScreen.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        MenuManager.instance.transform.GetChild(0).gameObject.SetActive(true);
        if(MenuManager.instance.teacherSelected.grade < 5) MenuManager.instance.teacherSelected.grade++;
        MenuManager.instance.SelectTeacher(MenuManager.instance.teacherSelected);
        MenuManager.instance.isInsideHack = false;
        Timer.instance.StopTimer();
        transform.gameObject.SetActive(false);
    }

    IEnumerator EndGame() {
        hackFailScreen.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        hackFailScreen.SetActive(false);
        MenuManager.instance.transform.GetChild(0).gameObject.SetActive(true);
        if(MenuManager.instance.teacherSelected.grade > 0) MenuManager.instance.teacherSelected.grade--;
        MenuManager.instance.SelectTeacher(MenuManager.instance.teacherSelected);
        MenuManager.instance.isInsideHack = false;
        Timer.instance.StopTimer();
        transform.gameObject.SetActive(false);
    }
}
