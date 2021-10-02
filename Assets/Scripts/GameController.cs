using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject gameOverScreen;
    public GameObject titleScreen;

    float duration;
    public bool started;

    public Text lastedDuration;
    public Text hs;

    public static GameController instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0;
        started = false;
        hs.text = PlayerPrefs.GetFloat("Highscore").ToString();
        titleScreen.SetActive(true);
        gameOverScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (started == true)
        {
            titleScreen.SetActive(false);
            duration += Time.deltaTime;
            lastedDuration.text = Mathf.Round(duration).ToString();
        }

    }

    public void GameOver()
    {
        if (PlayerPrefs.GetFloat("Highscore") <= duration)
        {
            PlayerPrefs.SetFloat("Highscore", Mathf.Round(duration));
        }
        Time.timeScale = 0;
        gameOverScreen.SetActive(true);
    }

    public void BackToSelect()
    {
        started = false;
        gameOverScreen.SetActive(false);
        titleScreen.SetActive(true);
        ResetLevel();

    }

    public void StartGame()
    {
        started = true;
        Time.timeScale = 1;
    }

    public void ResetLevel()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
