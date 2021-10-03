using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject thePlayer;

    public GameObject gameOverScreen;
    public GameObject titleScreen;

    public GameObject deathfx;

    float duration;
    public bool started, bouncyWalls;
    public float bouncyDuration;

    public Text lastedDuration;
    public Text hs;

    public static GameController instance;

    public Image[] healthUI;

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
        StartCoroutine(ggfx());
    }

    public IEnumerator ggfx()
    {
        Destroy(thePlayer.gameObject);
        Instantiate(deathfx, thePlayer.transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0.5f);
        Time.timeScale = 0;
        //started = false;
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

    public void UpdateHealth(int _health)
    {
        for (int i = 0; i < healthUI.Length; i++)
        {
            if (i < _health)
            {
                healthUI[i].gameObject.SetActive(true);
            }
            else
            {
                healthUI[i].gameObject.SetActive(false);
            }
        }
    }

    public void SetBounce()
    {
        bouncyWalls = true;
        CancelInvoke();
        Invoke("UnsetBounce", bouncyDuration);
    }

    void UnsetBounce()
    {
        bouncyWalls = false;
    }
}
