using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DecryptGame : MonoBehaviour
{
    public TMP_Text textfield;
    public TMP_InputField input;
    public GameObject successScreen, failedScreen;
    string alphabet = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ123456789='.!@#$%^&*()_+{}[]|:;?/>.<,`~";

    string currentPassword = "";
    int currentRounds = 0;
    int currentRound = 0;

    public void StartGame(int rounds) {
        successScreen.SetActive(false);
        failedScreen.SetActive(false);
        StartCoroutine(typingCode());
        currentRounds = rounds;
        currentRound = 0;
        Timer.instance.StartTimer(MenuManager.instance.teacherSelected.time);
    }

    void Update() {
        if(Timer.instance.time <= 0.0) {
            StartCoroutine(FailedScreen());
            return;
        }
        if(Input.GetKeyDown(KeyCode.Return)) {
            if(input.text.Contains(currentPassword)) {
                StartCoroutine(FinishScreen());
            }
        }
    }



    IEnumerator typingCode() {
        textfield.text = "";
        input.text = "";
        int codeLength = Mathf.RoundToInt(Random.Range(900, 1200));
        int password = Mathf.RoundToInt(Random.Range(0, codeLength));
        string passwordString = "" + alphabet[Mathf.RoundToInt(Random.Range(0, alphabet.Length))] + alphabet[Mathf.RoundToInt(Random.Range(0, alphabet.Length))] + alphabet[Mathf.RoundToInt(Random.Range(0, alphabet.Length))] + alphabet[Mathf.RoundToInt(Random.Range(0, alphabet.Length))];
        string pass = "pAsSWoRd=" + passwordString;
        currentPassword = passwordString;
        Debug.Log(currentPassword);
        int increment = 0;
        for(int i = 0; i < codeLength; i++) {
            if(i >= password && i < password + pass.Length) {
                textfield.text += pass[increment];
                increment++;
            } else textfield.text += alphabet[Mathf.RoundToInt(Random.Range(0, alphabet.Length))];
            yield return new WaitForSeconds(0.01f);
        }
    }


    IEnumerator FinishScreen() {
        successScreen.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        successScreen.SetActive(false);
        if(currentRound < currentRounds) {
            currentRound++;
            StartCoroutine(typingCode());
        } else {
            MenuManager.instance.transform.GetChild(0).gameObject.SetActive(true);
            if(MenuManager.instance.teacherSelected.grade < 5) MenuManager.instance.teacherSelected.grade++;
            MenuManager.instance.SelectTeacher(MenuManager.instance.teacherSelected);
            MenuManager.instance.isInsideHack = false;
            Timer.instance.StopTimer();
            transform.parent.gameObject.SetActive(false);
        }
    }
    IEnumerator FailedScreen() {
        failedScreen.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        failedScreen.SetActive(false);
        MenuManager.instance.transform.GetChild(0).gameObject.SetActive(true);
        if(MenuManager.instance.teacherSelected.grade > 0) MenuManager.instance.teacherSelected.grade--;
        MenuManager.instance.SelectTeacher(MenuManager.instance.teacherSelected);
        MenuManager.instance.isInsideHack = false;
        Timer.instance.StopTimer();
        transform.parent.gameObject.SetActive(false);
    }
}
