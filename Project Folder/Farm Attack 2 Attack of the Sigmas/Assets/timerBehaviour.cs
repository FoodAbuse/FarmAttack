using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class timerBehaviour : MonoBehaviour
{
    public TMP_Text timerText;
    public float timeElapsed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeElapsed += Time.deltaTime; // Increment timer by the time passed since last frame

        // Convert timeElapsed to minutes and seconds
        int minutes = Mathf.FloorToInt(timeElapsed / 60);
        int seconds = Mathf.FloorToInt(timeElapsed % 60);

        // Update the timer text in the format MM:SS
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
