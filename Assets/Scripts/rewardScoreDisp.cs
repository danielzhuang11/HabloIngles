﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class rewardScoreDisp : MonoBehaviour
{
    public Text scoreD;

    void Update()
    {
        scoreD.text = "Score:" + globalScore.score;
    }
}
