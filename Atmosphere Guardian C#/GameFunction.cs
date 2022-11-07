using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityEditor.SceneManagement;

public class GameFunction : MonoBehaviour
{
    public GameObject Enemy;        // 宣告物件，Enemy
    public float time;              // 計算時間

    public Text ScoreText;          // 宣告Text，ScoreText
    public int Score = 0;
    // 設定，Instance，讓其他程式能讀取 GameFunction.cs 裡的東西
    public static GameFunction Instance;

    public GameObject GameStartInterface;
    public GameObject GameTitle;    // 宣告 GameTitle 物件
    public GameObject GameOverTitle;// 宣告 GameOverTitle 物件
    public GameObject PlayButton;   // 宣告 PlayButton 物件
    static public bool IsPlaying = false;
    static public bool InHomePage = true;

    public GameObject RestartButton;// 宣告 開始遊戲文字標題 物件
    public GameObject QuitButton;   // 宣告 退出文字標題 物件

    public GameObject SetButton;    // 宣告 設定按鈕 物件
    public GameObject MajorSoundSlider;
    public GameObject GameSoundSlider;
    public GameObject SoundEffectSlider;
    public GameObject SoundImage;
    public GameObject SettingInterface;
    public GameObject CloseButton;
    public GameObject SoundText;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;            // 指定Instance這個程式

        GameStartInterface.SetActive(true);
        GameTitle.SetActive(true);  
        GameOverTitle.SetActive(false);

        RestartButton.SetActive(false);
        QuitButton.SetActive(false);

        SetButton.SetActive(true);
        MajorSoundSlider.SetActive(false);
        GameSoundSlider.SetActive(false);
        SoundEffectSlider.SetActive(false);
        SoundImage.SetActive(false);
        SettingInterface.SetActive(false);
        CloseButton.SetActive(false);
        SoundText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;     // 增加時間
        if (time > 1.0f && IsPlaying == true)
        {
            // 宣告位置，X : 2.5 ~ -2.5
            Vector3 pos = new Vector3(Random.Range(-5.5f, 5.5f), 4.5f, 0);
            // 生成敵人
            Instantiate(Enemy, pos, transform.rotation);
            time = 0f;
        }
    }

    public void AddScore()
    {
        Score += 10;                // 加分數
        // 更改ScoreText的內容
        ScoreText.text = "Score : " + Score;
    }

    public void GameStart()
    {
        IsPlaying = true;           // true 遊戲正在進行中
        InHomePage = false;

        GameStartInterface.SetActive(false);
        GameTitle.SetActive(false); // false 不顯示
        PlayButton.SetActive(false); 
    }

    public void GameOver()      
    {
        IsPlaying = false;          // 停止產生外星人
        GameOverTitle.SetActive(true);

        RestartButton.SetActive(true); 
        QuitButton.SetActive(true); 
    }

    //[System.Obsolete]
    public void ResetGame()         // RestartButton的功能
    {
        // 讀取關卡(已讀取的關卡)
        Application.LoadLevel(Application.loadedLevel);
        //EditorSceneManager.LoadScene("Scen");
    }

    public void QuitGame()          // QuitButton的功能
    {
        Application.Quit();         // 離開應用程式
    }

    public void SettingUp()
    {
        IsPlaying = false;
        
        MajorSoundSlider.SetActive(true);
        GameSoundSlider.SetActive(true);
        SoundEffectSlider.SetActive(true);
        SoundImage.SetActive(true);
        SettingInterface.SetActive(true);
        CloseButton.SetActive(true);
        SoundText.SetActive(true);
    }

    public void SettingDown()
    {
        MajorSoundSlider.SetActive(false);
        GameSoundSlider.SetActive(false);
        SoundEffectSlider.SetActive(false);
        SoundImage.SetActive(false);
        SettingInterface.SetActive(false);
        CloseButton.SetActive(false);
        SoundText.SetActive(false);

        if(InHomePage == false)
        {
            IsPlaying = true;
        }
    }
}
