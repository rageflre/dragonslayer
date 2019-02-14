using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpacityHandler : MonoBehaviour
{
    public SpriteRenderer[] boxes;
    public GameObject[] candles;
    int totalCandles;
    float maxOpacity = 0.6666667f;

    // Update is called once per frame
    void Update()
    {
        totalCandles = 0;
        foreach (GameObject g in candles) {
            if (g == null) continue;
            if (GameObject.Find(g.name))
            {
                totalCandles++;
            }
        }
        
        foreach (SpriteRenderer render in boxes)
        {
            float darkness = maxOpacity - ((maxOpacity / candles.Length) * totalCandles);
            render.color = new Color(render.color.r, render.color.g, render.color.b, darkness);
        }
    }
}
