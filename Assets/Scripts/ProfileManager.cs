using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ProfileManager : MonoBehaviour
{
    public static ProfileManager instance;

    void Awake() {
        instance = this;
    }

    public void SetProfile(Teacher teacher) {
        for(int i = 1; i < transform.childCount; i++) {
            if(transform.GetChild(i).GetComponent<TMP_Text>() == null) continue;
            transform.GetChild(i).GetComponent<TMP_Text>().text += teacher.data[i-1];
        }
    }

    public void ResetProfile() {
        transform.GetChild(1).GetComponent<TMP_Text>().text = "Age: ";
        transform.GetChild(2).GetComponent<TMP_Text>().text = "Name: ";
        transform.GetChild(3).GetComponent<TMP_Text>().text = "Subject: ";
        transform.GetChild(4).GetComponent<TMP_Text>().text = "Difficulty: ";
    }
}
