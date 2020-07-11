using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class WaveSpawner : MonoBehaviour
{
    public Transform spawnPoint;
    public static float timeBetweenWaves = 5f;
    private static float countdown = 2f;
    public Text waveCountdownText;
    public Text wavecnttext;
    public Text MiddleBossAlarmText;
    public Text FinalBossAlarmText;
    public static int WaveLimitCount = 2;
    public static int WavesCnt = 0;
    static int waveIndex = 0;
    public static WaveSpawner Instance;
    public static Transform enemy;
   
    public static int WaveIndex
    {

        get
        {
            return waveIndex;
        }
    }
    public static int wavecnt
    {
        set
        {
            WavesCnt = value;
        }
        get
        {
            return WavesCnt;
        }
    }
    public static float CountDown
    {
        get
        {
            return countdown;
        }
    }
    public static void WaveSpawnerReset()
    {
        WavesCnt = 0;
    }
    void Update()
    {
        wavecnttext.text = "Wave" + WavesCnt.ToString();
        if (GameObject.FindGameObjectWithTag("Enemy") == null)
        {
            waveCountdownText.enabled = true;
            if (countdown <= 0f && WavesCnt <= WaveLimitCount && !GameManager.Clear() && !GameManager.GameOver())
            {
                StartCoroutine(SpawnWave());
                countdown = timeBetweenWaves;
            }
            
            else if (WaveLimitCount <= WavesCnt)
            {
                waveCountdownText.enabled = false;
                MiddleBossAlarmText.enabled = false;
                FinalBossAlarmText.enabled = false;
            }
            else if (WavesCnt <= WaveLimitCount)
            {
                if (GameManager.STAGE == 2 && !MiddleBossAlarmText.enabled&&waveIndex<=2)
                {
                    MiddleBossAlarmText.enabled = true;
                }
                if (GameManager.STAGE == 3 && !FinalBossAlarmText.enabled && waveIndex <= 2)
                {
                    FinalBossAlarmText.enabled = true;
                }
                countdown -= Time.deltaTime;
                waveCountdownText.text = Mathf.Round(countdown).ToString();
            }
        }
        else
        {
            waveCountdownText.enabled = false;
            MiddleBossAlarmText.enabled = false;
            FinalBossAlarmText.enabled = false;
        }
            
    }
    IEnumerator SpawnWave()
    {
        waveIndex++;
        WavesCnt++;
        for (int i = 0; i < waveIndex; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(2f);
        }
    }
  
    void SpawnEnemy()
    {
        Transform enemypre;
        enemypre = Instantiate(enemy, new Vector3(spawnPoint.position.x, enemy.position.y, spawnPoint.position.z), spawnPoint.rotation);
        if(GameManager.STAGE==1)
        {
            enemy.gameObject.GetComponent<Enemy>().enemys = Enemys.ENEMY;
        }
        else if (GameManager.STAGE == 2)
        {
            enemy.gameObject.GetComponent<Enemy>().enemys = Enemys.MIDDLEBOSS;
        }
        else if (GameManager.STAGE == 3)
        {
            enemy.gameObject.GetComponent<Enemy>().enemys = Enemys.FINALBOSS;
        }
    }
    private void OnDestroy()
    {

    }
}
