using Mono.Cecil.Rocks;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public double money;
    public double dpc;
    public double health;
    public double healthMax;
    public int stage;
    public int stageMax;
    public int kills;
    public int killsMax;
    public TMP_Text MoneyText;
    public TMP_Text DPCText;
    public TMP_Text StageText;
    public TMP_Text KillsText;
    public TMP_Text healthText;

    public Button back;
    public Button forward;

    public Image heatlhBar;

    public void Start()
    {
        dpc = 1;
        stage = 1;
        stageMax = 1;
        killsMax = 10;
        healthMax = 10;
        health = healthMax;
    }

    public void Update()
    {
        MoneyText.text = $"${money:F2}";
        DPCText.text = $"{dpc} Damage Per Click";
        StageText.text = $"Stage - {stage}";
        KillsText.text = $"{kills}/{killsMax} Kills";
        healthText.text = $"{health}/{healthMax} HP";
        heatlhBar.fillAmount = (float)(health / healthMax);

        if(stage > 1)
        {
            back.gameObject.SetActive(true);
        }
        else
        {
            back.gameObject.SetActive(false);
        }

        if(stage != stageMax)
        {
            forward.gameObject.SetActive(true);
        }
        else 
        { 
            forward.gameObject.SetActive(false); 
        }
    }

    public void Hit()
    {
        health -= dpc;
        if(health <= 0)
        {
            money += 1;
            health = healthMax;
            if (stage == stageMax)
            {
                kills += 1;
                if (kills >= killsMax)
                {
                    kills = 0;
                    stage += 1;
                    stageMax += 1;
                }
            }
        }
    }

    public void Back()
    {
        if( stage > 1)
        {
            stage -= 1;
        }
    }
    public void Forward()
    {
        if (stage != stageMax)
        {
            stage += 1;
        }
    }
}
