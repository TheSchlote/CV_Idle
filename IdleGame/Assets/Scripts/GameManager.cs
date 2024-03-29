using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private const int killMax = 3;
    private const int BossStage = 4;

    public double Money;
    public double DamagePer;
    public double Health;
    public double HealthMax
    {
        get
        {
            return 1 * Math.Pow(2, Stage - 1) * BossMultiplyer; ;
        }
    }
    public int Stage;
    public int StageMax;
    public int Kills;
    public int TotalKills;
    public int KillsMax;
    public int BossMultiplyer;
    public List<int> BossLevels;

    public TMP_Text MoneyText;
    public TMP_Text DamagePerText;
    public TMP_Text StageText;
    public TMP_Text KillsText;
    public TMP_Text HealthText;

    public Button Back;
    public Button Forward;

    public Image HeatlhBar;
    public Image BattleBackHolder;
    public Image EnemyHolder;

    public List<Sprite> BattleBacks = new List<Sprite>();
    [Serializable]
    public struct Enemies
    {
        public int Region;
        public Sprite Enemy;
    }
    public Enemies[] enemies;
    public void Start()
    {
        for (int i = 3; i <= 32; i += BossStage)
        {
            BossLevels.Add(i);
        }
        DamagePer = 100;
        Stage = 1;
        StageMax = 1;
        KillsMax = killMax;
        BossMultiplyer = 1;
        Health = HealthMax;
        EnemyHolder.sprite = enemies[Kills].Enemy;
    }

    public void Update()
    {
        MoneyText.text = $"${Money:F2}";
        DamagePerText.text = $"{DamagePer} Damage Per Click";
        KillsText.text = $"{Kills}/{KillsMax} Kills";
        HealthText.text = $"{Health}/{HealthMax} HP";

        HeatlhBar.fillAmount = (float)(Health / HealthMax);

        Back.enabled = Stage > 1;
        Forward.enabled = Stage != StageMax;
    }

    public void Hit()
    {
        Health -= DamagePer;

        if (Health <= 0)
        {
            EnemyKilled();
        }
    }

    private void EnemyKilled()
    {
        Money += Math.Ceiling(HealthMax / 14);
        Kills++;
        TotalKills++;
        EnemyHolder.sprite = enemies[TotalKills].Enemy;
        

        if (Kills >= KillsMax)
        {
            Kills = 0;
            StageComplete();
        }
        bool isBoss = BossLevels.Contains(TotalKills) || TotalKills >= 32;
        BossMultiplyer = isBoss ? BossStage : 1;
        KillsMax = BossMultiplyer == BossStage ? 1 : killMax;

        StageText.text = isBoss ? $"BOSS - {Stage}" : $"Stage - {Stage}";
        Health = HealthMax;
    }

    private void StageComplete()
    {
        if (KillsMax == 1)
        {
            if(Stage < 9)
            {
                BattleBackHolder.sprite = BattleBacks[Stage];
            }
            Stage++;
            StageMax++;
        }
    }

    public void BackButton()
    {
        if (Stage > 1)
        {
            Stage--;
        }
    }
    public void ForwardButton()
    {
        if (Stage != StageMax)
        {
            Stage++;
        }
    }
}
