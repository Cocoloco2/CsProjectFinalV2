//Original code from: https://www.youtube.com/watch?v=tIfC00BE6z8
//!!!This has not been implemented as of this iteration!!!


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField]
    private List<Detector> detectors;

    [SerializeField]
    private AIData aiData;

    [SerializeField]
    private float detectionDelay = 0.05f;

    private void Start()
    {
        //detecting Player and Obstacle around
        InvokeRepeating("PerformDetection", 0, detectionDelay);

    }
    private void PerformDetection()
    {
        foreach (Detector detector in detectors)
        {
            detector.Detect(aiData);
        }
    }
}
