using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PointsScript : MonoBehaviour
{
    public GameObject teacherCollection;

    void Update() {
        int currentPoints = 0;
        for(int i = 0; i < teacherCollection.transform.childCount; i++) {
            if(teacherCollection.transform.GetChild(i).GetComponent<Teacher>().grade == 5) currentPoints++;
        }
        GetComponent<TMP_Text>().text = currentPoints + "/4";
    }
}
