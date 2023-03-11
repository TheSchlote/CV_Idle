using Mono.Cecil.Rocks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public double money;
    public double dpc;
    public double health;
    public double HealthMax
    {
        get
        {
            return 10 * Math.Pow(2, stage - 1) * BossMultiplyer; ;
        }
    }
    public int stage;
    public int stageMax;
    public int kills;
    public int killsMax;
    public int BossMultiplyer;

    public TMP_Text MoneyText;
    public TMP_Text DPCText;
    public TMP_Text StageText;
    public TMP_Text KillsText;
    public TMP_Text healthText;
    public TMP_Text timerText;

    public Button back;
    public Button forward;

    public Image heatlhBar;
    public Image timerBar;
    public float timer;
    public float timerCap;

    public void Start()
    {
        dpc = 100;
        stage = 1;
        stageMax = 1;
        killsMax = 10;
        BossMultiplyer = 1;
        health = HealthMax;
    }

    public void Update()
    {
        MoneyText.text = $"${money:F2}";
        DPCText.text = $"{dpc} Damage Per Click";
        KillsText.text = $"{kills}/{killsMax} Kills";
        healthText.text = $"{health}/{HealthMax} HP";
        
        heatlhBar.fillAmount = (float)(health / HealthMax);

        back.enabled = stage > 1;
        forward.enabled = stage != stageMax;
    }

    public void Hit()
    {
        health -= dpc;
        if(health <= 0)
        {
            money += Math.Ceiling(HealthMax / 14);
            if (stage == stageMax)
            {
                kills ++;
                if (kills >= killsMax)
                {
                    kills = 0;
                    stage++;
                    stageMax++;

                    bool isBossStage = stage % 5 == 0;
                    BossMultiplyer = isBossStage ? 10 : 1;
                    StageText.text = isBossStage ? $"BOSS - {stage}" : $"Stage - {stage}";
                    killsMax = BossMultiplyer == 10 ? 1 : 10;
                }
                
            }
            health = HealthMax;
        }
    }

    public void Back()
    {
        if( stage > 1)
        {
            stage--;
        }
    }
    public void Forward()
    {
        if (stage != stageMax)
        {
            stage++;
        }
    }
}
