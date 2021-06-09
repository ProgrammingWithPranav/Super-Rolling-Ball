using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    private const float TIME_BEFORE_START = 3f;

    private static LevelManager instance;
    public static LevelManager Instance{get{ return instance;}}

    public GameObject pauseMenu;
    public GameObject endMenu;
    public Transform respawnPoint;
    public Text timerText;
    private GameObject player;

    private float startTime;
    private float levelDuration;
    public float silverTime;
    public float goldTime;

    void Start()
    {
        instance = this;
        pauseMenu.SetActive(false);
        endMenu.SetActive(false);
        startTime = Time.time;
        player = GameObject.FindGameObjectWithTag("Player");
        player.transform.position = respawnPoint.position;
    }

    public void RestartLevel()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void Update()
    {
        if(player.transform.position.y < -10)
        {
            Death();
        }

        if(Time.time - startTime < TIME_BEFORE_START)
        {
            return;
        }

        levelDuration = Time.time - (startTime + TIME_BEFORE_START);
        string minutes = ((int) levelDuration / 60).ToString("00");
        string seconds = (levelDuration % 60).ToString("00.00");

        timerText.text = minutes + ":" + seconds;
    }

    public void TogglePauseMenu()
    {
        pauseMenu.SetActive(!pauseMenu.activeSelf);
        Time.timeScale = (pauseMenu.activeSelf) ? 0 : 1;
    }

    public void ToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }

    public void Victory()
    {
        foreach(Transform t in endMenu.transform.parent)
        {
            t.gameObject.SetActive(false);
        }

        endMenu.SetActive(true);

        Rigidbody rb = player.GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.useGravity = false;


        if(Time.time - startTime < goldTime)
        {
            GameManager.Instance.currency += 50;
        }
        if(Time.time - startTime < silverTime)
        {
            GameManager.Instance.currency += 25;
        }
        else{
            GameManager.Instance.currency += 10;
        }
        GameManager.Instance.Save();

        string saveString = "";
        LevelData level = new LevelData(SceneManager.GetActiveScene().name);
        saveString += (level.BestTime > levelDuration || level.BestTime == 0.0f) ? levelDuration.ToString() : level.BestTime.ToString();
        saveString += '&';
        saveString += silverTime.ToString();
        saveString += '&';
        saveString += goldTime.ToString();
        PlayerPrefs.SetString(SceneManager.GetActiveScene().name, saveString);

    }

    public void Death()
    {
        // player.transform.position = respawnPoint.position;
        // Rigidbody rigid = player.GetComponent<Rigidbody>();
        // rigid.velocity = Vector3.zero;
        // rigid.angularVelocity = Vector3.zero;
        RestartLevel();
    }
}
