using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlitchController : MonoBehaviour
{
    public float cooldown = 2.0f;
    public float duration = 0.5f;
    void Update()
    {
        if(MenuManager.instance.isInsideHack) {
            GetComponent<AnalogGlitch>().scanLineJitter = 0.3f;
        } else {
            GetComponent<AnalogGlitch>().scanLineJitter = 0.4f;
        }
    }
}
