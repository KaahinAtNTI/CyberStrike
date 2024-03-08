using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SnakeRunner : MonoBehaviour
{
    public float speed = 0.5f;
    public Rigidbody2D rb;
    public GameObject trailRenderer;
    public GameObject levelHolder;
    public GameObject hackFailScreen, hackSuccessScreen;
    GameObject trail;
    bool won = false;
    bool failed = false;
    int gamesLeft = 0;

    public List<int> easyMaps = new List<int>();
    public List<int> hardMaps = new List<int>();
    Vector2 startPoint;
    GameObject currentMap;
    Vector3 trajectory = new Vector3(0,1,0);

    Vector3[] directions = {
        new Vector3(-1,0),
        new Vector3(1,0),
        new Vector3(0,1),
        new Vector3(0,-1)
    };
    public void StartGame(int gamesLeft, int difficulty, float time) {
        hackFailScreen.SetActive(false);
        hackSuccessScreen.SetActive(false);
        won = false; failed = false;
        this.gamesLeft = gamesLeft;
        Invoke("RestartTrail", 0.05f);
        RandomizeMap(difficulty);
        currentMap = GetCurrentMap();
        startPoint = GetStartPointInCurrentMap().transform.position;
        rb.position = startPoint;
        Timer.instance.StartTimer(time);
        trajectory = directions[2];
        transform.rotation = Quaternion.Euler(0.0f,0.0f,0f);
    }

    void Update() {
        if(Timer.instance.time <= 0) {
            StartCoroutine(failedHack());
            return;
        }
        if(Input.GetKeyDown(KeyCode.LeftArrow) && trajectory != directions[1]) {
            trajectory = directions[0];
            transform.rotation = Quaternion.Euler(0.0f,0.0f,90f);
        } else if(Input.GetKeyDown(KeyCode.RightArrow) && trajectory != directions[0]) {
            trajectory = directions[1];
            transform.rotation = Quaternion.Euler(0.0f,0.0f,-90f);
        } else if(Input.GetKeyDown(KeyCode.UpArrow) && trajectory != directions[3]) {
            trajectory = directions[2];
            transform.rotation = Quaternion.Euler(0.0f,0.0f,0f);
        } else if(Input.GetKeyDown(KeyCode.DownArrow) && trajectory != directions[2]) {
            trajectory = directions[3];
            transform.rotation = Quaternion.Euler(0.0f,0.0f,180f);
        }


        float multiplier = Input.GetKey(KeyCode.LeftShift) ? 5.0f : 1.0f;
        if(!failed) rb.position += new Vector2(trajectory.x, trajectory.y) * speed * multiplier * Time.deltaTime;
    }

    IEnumerator failedHack() {
        hackFailScreen.SetActive(true);
        if(trail) Destroy(trail);
        failed = true;
        yield return new WaitForSeconds(2.0f);
        hackFailScreen.SetActive(false);
        MenuManager.instance.transform.GetChild(0).gameObject.SetActive(true);
        if(MenuManager.instance.teacherSelected.grade > 0) MenuManager.instance.teacherSelected.grade--;
        MenuManager.instance.SelectTeacher(MenuManager.instance.teacherSelected);
        MenuManager.instance.isInsideHack = false;
        Timer.instance.StopTimer();
        transform.parent.gameObject.SetActive(false);
    }
    IEnumerator successHack() {
        hackSuccessScreen.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        if(gamesLeft > 0) {
                int childIndex = 0;
                for(int i = 0; i < levelHolder.transform.childCount; i++) {
                    if(levelHolder.transform.GetChild(i) == currentMap.transform) {
                        childIndex = i;
                    }
                }
                int nextChildIndex = 0;
                if(childIndex + 1 >= levelHolder.transform.childCount) nextChildIndex = 0; else nextChildIndex = childIndex + 1;
                SwitchMap(nextChildIndex);
                Destroy(trail);
                Invoke("RestartTrail", 0.05f);
                currentMap = GetCurrentMap();
                startPoint = GetStartPointInCurrentMap().transform.position;
                rb.position = startPoint;
                trajectory = directions[2];
                gamesLeft--;
                Timer.instance.time += 10.0f;
                won = false;
                hackSuccessScreen.SetActive(false);
            } else {
                MenuManager.instance.transform.GetChild(0).gameObject.SetActive(true);
                if(MenuManager.instance.teacherSelected.grade < 5) MenuManager.instance.teacherSelected.grade++;
                MenuManager.instance.SelectTeacher(MenuManager.instance.teacherSelected);
                MenuManager.instance.isInsideHack = false;
                if(trail) Destroy(trail);
                Timer.instance.StopTimer();
                transform.parent.gameObject.SetActive(false);
            }
    }


    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "WinPoint") {
            StartCoroutine(successHack());
            return;
        }
        Destroy(trail);
        rb.position = startPoint;
        trajectory = directions[2];
        Invoke("RestartTrail", 0.05f);
    }

    void RestartTrail() {
        trail = Instantiate(trailRenderer, transform);
        transform.rotation = Quaternion.Euler(0.0f,0.0f,0f);
    }
    
    public void SwitchMap(int map) {
        for(int i = 0; i < levelHolder.transform.childCount; i++) {
            levelHolder.transform.GetChild(i).gameObject.SetActive(i == map);
        }
    }

    public GameObject GetCurrentMap() {
        for(int i = 0; i < levelHolder.transform.childCount; i++) {
            if(levelHolder.transform.GetChild(i).gameObject.activeSelf) {
                return levelHolder.transform.GetChild(i).gameObject;
            }
        }
        return null;
    }
    public GameObject GetStartPointInCurrentMap() {
        for(int i = 0; i < currentMap.transform.childCount; i++) {
            if(currentMap.transform.GetChild(i).gameObject.tag == "Start") {
                return currentMap.transform.GetChild(i).gameObject;
            }
        }
        return null;
    }

    public void RandomizeMap(int diff) {
        int randomMap = 0;
        switch(diff) {
            case 0:
                randomMap = Mathf.FloorToInt(Random.Range(0.0f, easyMaps.Count));
                break;
            case 1:
                randomMap = Mathf.FloorToInt(Random.Range(0.0f, hardMaps.Count));
                break;
        }
        SwitchMap(randomMap);
    }
}
