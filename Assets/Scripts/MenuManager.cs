using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public static MenuManager instance;
    void Awake() {
        instance = this;
    }
    public RawImage currentTeacher;
    public GameObject snakeGame, snakeSplashScreen;
    public GameObject decryptorGame;
    public GameObject decryptorSplashScreen;
    public GameObject testGame;
    public GameObject testSplash;
    bool isInsideSnakeSplash = false, isInsideDecryptorSplash = false, isInsideTestSplash = false;
    public Teacher teacherSelected;
    public bool isInsideHack = false;
    public GameObject checkTag;
    public void SelectTeacher(Teacher teacher) {
        ProfileManager.instance.ResetProfile();
        ProfileManager.instance.SetProfile(teacher);
        teacherSelected = teacher;
        currentTeacher.texture = teacher.transform.GetChild(0).GetComponent<RawImage>().texture;
        for(int i = 0; i < checkTag.transform.childCount; i++) {
            checkTag.transform.GetChild(i).gameObject.SetActive(i == teacher.grade);
        }
    }

    void Start() {
        SelectTeacher(teacherSelected);
    }

    void Update() {
        if(isInsideSnakeSplash) {
            if(Input.GetButtonDown("Jump")) {
                snakeSplashScreen.SetActive(false);
                isInsideSnakeSplash = false;
                StartSnakeHack();
            }
        }
        if(isInsideDecryptorSplash) {
            if(Input.GetButtonDown("Jump")) {
                decryptorSplashScreen.SetActive(false);
                isInsideDecryptorSplash = false;
                StartDecryptorHack();
            }
        }
        if(isInsideTestSplash) {
            if(Input.GetButtonDown("Jump")) {
                testSplash.SetActive(false);
                isInsideTestSplash = false;
                StartTestHack();
            }
        }
    }

    public void StartGame() {
        if(teacherSelected.data[1] == "Per") {
            StartDecryptor();
        } else if(teacherSelected.data[1] == "Rektor Holm") {
            StartSnake();
        } else if(teacherSelected.data[1] == "Douglas") {
            StartTest();
        } else {
            StartSnake();
        }
    }

    public void Exit() {
        Application.Quit();
    }

    public void StartTest() {
        transform.GetChild(0).gameObject.SetActive(false);
        testSplash.SetActive(true);
        isInsideTestSplash = true;
    }
    public void StartTestHack() {
        testGame.SetActive(true);
        testGame.GetComponent<TestGame>().StartGame(teacherSelected);
    }

    public void StartSnake() {
        transform.GetChild(0).gameObject.SetActive(false);
        snakeSplashScreen.SetActive(true);
        isInsideSnakeSplash = true;
    }

    public void StartDecryptor() {
        transform.GetChild(0).gameObject.SetActive(false);
        decryptorSplashScreen.SetActive(true);
        isInsideDecryptorSplash = true;
    }

    public void StartDecryptorHack() {
        isInsideHack = true;
        decryptorGame.SetActive(true);
        decryptorGame.transform.GetChild(0).GetComponent<DecryptGame>().StartGame(teacherSelected.snakeLevels);
    }

    public void StartSnakeHack() {
        isInsideHack = true;
        snakeGame.SetActive(true);
        SnakeRunner snake = snakeGame.transform.GetChild(0).GetComponent<SnakeRunner>();
        snake.StartGame(teacherSelected.snakeLevels, teacherSelected.category, teacherSelected.time);
    }
}
