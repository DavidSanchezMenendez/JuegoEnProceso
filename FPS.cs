using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FPS : MonoBehaviour
{
    public Text fpsText;
    private float pollingTime = 1f;
    private float time;
    private int framecount;

    private void Update()
    {
        time += Time.deltaTime;
        framecount++;

        if (time >= pollingTime)
        {
            int frameRate = Mathf.RoundToInt(framecount / time);
            fpsText.text = frameRate.ToString() + " FPS";
            time -= pollingTime;
            framecount = 0;
        }
    }

}
