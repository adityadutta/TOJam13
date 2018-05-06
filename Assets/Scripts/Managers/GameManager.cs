using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;

    [SerializeField]
    private int maxLives = 3;

    private static int _remainingLives = 3;
    public static int RemainingLives
    {
        get { return _remainingLives; }
    }

    private void Awake()
    {
        if (Instance == null)
            Instance = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    public bool isNormal = true;
    public bool isBouncy = false;
    public bool isHard = false;

    public Transform playerPrefab;
    public Transform spawnPoint;
    public int spawnDelay = 2;
    public Transform spawnPrefab;
    [SerializeField]
    private GameObject gameOverUI;

    private float startTime;
    [HideInInspector]
    public float roundDuration;
    [HideInInspector]
    public string minutes;
    [HideInInspector]
    public string seconds;

    public bool isGameOver = false;

    public Text livesText;

    //Sound
    AudioManager audioManager;
    public string gameOverSound;
    public string playerDeath;
    public string enemyDeath;
    public string playerRespawn;

    private void Start()
    {
        _remainingLives = maxLives;
        Time.timeScale = 1;
        startTime = Time.time;

        audioManager = AudioManager.Instance;
    }

    private void Update()
    {
        UpdateTime();
    }

    public void EndGame()
    {
        isGameOver = true;
        audioManager.PlaySound(gameOverSound);
        Debug.Log("GAME OVER");
        gameOverUI.SetActive(true);
        float bestTime = PlayerPrefs.GetFloat("BestTime");
        if (roundDuration < bestTime || bestTime == 0.0f)
        {
            PlayerPrefs.SetFloat("BestTime", roundDuration);
        }
        Time.timeScale = 0;
    }

    public IEnumerator _RespawnPlayer()
    {
        livesText.text = "Souls x" + RemainingLives;
        yield return new WaitForSeconds(spawnDelay);

        audioManager.PlaySound(playerRespawn);
        Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);
        Transform clone = Instantiate(spawnPrefab, spawnPoint.position, spawnPoint.rotation) as Transform;
        Destroy(clone.gameObject, 3f);
        livesText.text = "";
    }

    public static void KillPlayer(Player player)
    {
        Instance._KillPlayer(player);
    }

    public void _KillPlayer(Player player)
    {
        audioManager.PlaySound(playerDeath);
        Destroy(player.gameObject);
        _remainingLives -= 1;
        if (_remainingLives <= 0)
        {
            Instance.EndGame();
        }
        else
        {
            Instance.StartCoroutine(Instance._RespawnPlayer());
        }
    }


    private void UpdateTime()
    {
        roundDuration = Time.time - startTime;
        minutes = ((int)roundDuration / 60).ToString("00");
        seconds = (roundDuration % 60).ToString("00.00");
    }

    public static void KillEnemy(Enemy enemy)
    {
        Instance._KillEnemy(enemy);
    }

    public void _KillEnemy(Enemy _enemy)
    {
        audioManager.PlaySound(enemyDeath);
        Transform _clone = Instantiate(_enemy.enemyDeathParticles, _enemy.transform.position, Quaternion.identity) as Transform;
        Destroy(_enemy.gameObject);
        Destroy(_clone.gameObject, 3f);
    }
}

