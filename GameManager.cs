using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum Enemys
{
    ENEMY,
    MIDDLEBOSS,
    FINALBOSS
}
public class GameManager : MonoBehaviour
{
    public static int Balance = 100;
    [Header("BossBonus")]
    public int MiddleBossBonusBalance = 150;
    public int FinalBossBonusBalance = 200;
    [Header("MainUI")]
    public GameObject Fullview;
    public GameObject PartialView;
    public GameObject GameoverUI;
    public GameObject ClearUI;
    public GameObject PauseMenuUI;
    public GameObject ShopUI;
    
    public Text BalanceText;
    public Text StageText;
    public Text BossBonusText;
    [Header("EnemySet")]
    public Transform enemyPrefab;
    public float enemystr;
    public float enemydef;
    public float enemyspeed;
    [Header("MiddleBossSet")]
    public Transform MiddleBoss;
    public float MBstr;
    public float MBdef;
    public float MBspeed;
    [Header("FinalBossSet")]
    public Transform FinalBoss;
    public float FBstr;
    public float FBdef;
    public float FBspeed;
    [Header("RewardUI")]
    public Text EnemyDestoryRewardtext;
    public Text BonusRewardtext;
    [Header("ShopUI")]
    public Text TurretStrtext;
    public Text turretDeftext;
    public enum CameraView { FullView, PartialView }
    public CameraView cameraView;
    static int bonusreward = 100;
    static int EnemyAllkillReward = 0;
    static int EnemyKillReward = 20;
    static bool pause = false;
    static bool Shop = false;
    static int Stage = 1;
    static bool ClearCheck;
    public static int STAGE
    {
        get
        {
            return Stage;
        }
        set
        {
            if (0 < Stage && Stage <= 3)
                Stage = value;
            else
                return;
        }
    }
    public static bool Pause
    {
        get
        {
            return pause;
        }
        set
        {
            pause = value;
        }
    }
    public static bool SHOP
    {
        get
        {
            return Shop;
        }
        set
        {
            Shop = value;
        }
    }
    public static Enemys enemys;
    private Enemy _enemy;
    private Enemy _middleboss;
    private Enemy _finalboss;
    private void Awake()
    {
        _enemy = enemyPrefab.GetComponent<Enemy>();
        _middleboss = MiddleBoss.GetComponent<Enemy>();
        _finalboss = FinalBoss.GetComponent<Enemy>();
    }
    private void Init()
    {

        //BalanceText.text = "남은돈 : " +PlayerPrefs.GetInt("Balance").ToString();
        BalanceText.text = "남은돈 : " + Balance.ToString();
        STAGE = 1;
        StageText.text = "Stage : " + Stage.ToString();
        //_enemy = enemyPrefab.GetComponent<Enemy>();
        //_middleboss = MiddleBoss.GetComponent<Enemy>();
        //_finalboss = FinalBoss.GetComponent<Enemy>();
        WaveSpawner.enemy = enemyPrefab;
        Debug.Log("EnemyStr" + _enemy.STR);
        Debug.Log("middlebossStr" + _middleboss.STR);
        Debug.Log("finalbossStr" + _finalboss.STR);
    }
    public void PauseSet()
    {
        Time.timeScale = 0;
        PauseMenuUI.SetActive(true);
    }
    public void PauseUnSet()
    {
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1;
    }
    public static int EnemyKillreward
    {
        get
        {
            return EnemyKillReward;
        }
    }
    private void Start()
    {
        Init();
    }
    void Update()
    {
        if (pause)
        {
            PauseSet();
        }
        else
            PauseUnSet();
        CameraMove();
        ClearCheck = Clear();
        if (GameOver())
        {
            GameoverUI.SetActive(true);
        }
        else if (ClearCheck&&Stage == 1)
        {
            ClearUI.SetActive(true);
            BossBonusText.enabled = false;
            EnemyAllkillReward = (WaveSpawner.WaveIndex * EnemyKillReward);
            Balance += bonusreward;
            
            Balance += EnemyAllkillReward;
            BonusRewardtext.text = bonusreward.ToString();
            EnemyDestoryRewardtext.text = EnemyAllkillReward.ToString();
            WaveSpawner.wavecnt = 0;
            ClearCheck = false;
        }
        else if (ClearCheck && Stage == 2)
        {
            ClearUI.SetActive(true);
            BossBonusText.enabled = true;
            EnemyAllkillReward = (WaveSpawner.WaveIndex * EnemyKillReward);
            Balance += bonusreward;
            Balance += EnemyAllkillReward;
            BonusRewardtext.text = (bonusreward + MiddleBossBonusBalance).ToString();
            EnemyDestoryRewardtext.text = EnemyAllkillReward.ToString();
            BossBonusText.text = "중간보스 : " + MiddleBossBonusBalance.ToString();
            WaveSpawner.wavecnt = 0;
            ClearCheck = false;
        }
        else if (ClearCheck && Stage == 3)
        {
            ClearUI.SetActive(true);
            BossBonusText.enabled = true;
            EnemyAllkillReward = (WaveSpawner.WaveIndex * EnemyKillReward);
            Balance += bonusreward;
            Balance += EnemyAllkillReward;
            Balance += FinalBossBonusBalance;
            BonusRewardtext.text = (bonusreward + FinalBossBonusBalance).ToString();
            EnemyDestoryRewardtext.text = EnemyAllkillReward.ToString();
            BossBonusText.text = "최종보스 : " + FinalBossBonusBalance.ToString();
            WaveSpawner.wavecnt = 0;
            ClearCheck = false;
        }
        else if (Shop)
        {
            ShopUI.SetActive(true);
            TurretStrtext.text = Turret.STR.ToString();
            turretDeftext.text = Turret.DEF.ToString();

        }
        else if (!SHOP)
            ShopUI.SetActive(false);
        if(!ClearCheck)
            BalanceText.text = "남은돈 : " + Balance.ToString();
        if (4 < WaveSpawner.WavesCnt && WaveSpawner.WavesCnt < 5 && Stage == 2)
            enemys = Enemys.MIDDLEBOSS;
        else if (Stage == 3 && WaveSpawner.WaveIndex <= 5 && WaveSpawner.WavesCnt == 5)
            enemys = Enemys.FINALBOSS;
    }
    public static int GetBalance() { return Balance; }
    public static void Buy(int value) 
    { 
        if(0<Balance)
        {
            Balance -= value;
        }
        
    }
    public static void Sale(int value) { Balance += value; }
    public static void SetBalance(int value) { Balance = value; }
    public static void GiveBalance(int value) { EnemyAllkillReward += value; }
    public static void LoseBalance(int value) { Balance -= value; }
    void CameraMove()
    {
        if (Input.GetKey(KeyCode.Alpha1))
        {
            cameraView = CameraView.FullView;
        }
        else if (Input.GetKey(KeyCode.Alpha2))
        {
            cameraView = CameraView.PartialView;
        }
        switch (cameraView)
        {
            case CameraView.FullView:
                Fullview.SetActive(true);
                PartialView.SetActive(false);
                break;
            case CameraView.PartialView:
                Fullview.SetActive(false);
                PartialView.SetActive(true);
                break;
            default:
                break;
        }
    }
    public static bool GameOver()
    {
        if (GameObject.FindGameObjectWithTag("House") == null)
        {
            Time.timeScale = 0;
            return true;
        }
        else
            return false;
    }
    public static bool Clear()
    {
        if (GameObject.FindGameObjectWithTag("Enemy") == null && GameObject.FindGameObjectWithTag("House") != null && WaveSpawner.wavecnt >= WaveSpawner.WaveLimitCount&&!ClearCheck)
        {
            Time.timeScale = 0;
            GiveBalance(150);
            
            return true;
        }
        else
            return false;
    }
    public  void NextStatge()
    {
        ClearUI.SetActive(false);
        Stage += 1;
        StageText.text = "Stage : " + Stage.ToString();
        Time.timeScale = 1;
        WaveSpawner.timeBetweenWaves = 5;
        
    }
    private void OnDestroy()
    {
        WaveSpawner.WaveSpawnerReset();
        //PlayerPrefs.SetInt("Balance", Balance);
        //Debug.Log("OnDestroyMoney : " + PlayerPrefs.GetInt("Balance"));
    }
    private void OnApplicationQuit()
    {
        WaveSpawner.WaveSpawnerReset();
        //PlayerPrefs.SetInt("Balance",Balance);
        //Debug.Log("OnApplicationQuit : " + PlayerPrefs.GetInt("Balance"));
    }
}