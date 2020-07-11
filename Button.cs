using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour
{
    GameManager GM;
    private void Start()
    {
        if(SceneManager.GetActiveScene().name=="main")
            GM = GameObject.Find("GameMaster").GetComponent<GameManager>();
    }
    public void StartBt() { SceneManager.LoadScene("main"); }
    public void Restart() { GM.GameoverUI.SetActive(false); SceneManager.LoadScene("main"); }
    public void QuitBt() { Application.Quit(); }
    public void Option() { }
    public void NextStage() { GM.NextStatge(); }
    public void TitleScene() { SceneManager.LoadScene("Title"); }
    public void Pause() { GameManager.Pause = true; }
    public void UnPause() { GameManager.Pause = false; }
    public void Shop() { GameManager.SHOP = true; }
    public void ShopClose() { GameManager.SHOP = false; }
    public void TRStrUP() { 
        if(5<=GameManager.Balance)
        {
            Turret.STR += 3; 
            GameManager.Buy(5);
        }    
    }
    public void TRStrDown() { Turret.STR -= 3;GameManager.Sale(5);}
    public void TRDefUP() {
        if (5 <= GameManager.Balance&&Turret.DEF<98)
        {
            Turret.DEF += 3;
            GameManager.Buy(5);
        }
       }
    public void TRDefDown() { Turret.DEF -= 3; GameManager.Sale(5);}
}
