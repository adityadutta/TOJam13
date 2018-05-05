using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    //[SerializeField]
    //private GameObject gameOverUI;

    private void Start()
    {
        _remainingLives = maxLives;

    }

    public void EndGame()
    {
        Debug.Log("GAME OVER");
        //gameOverUI.SetActive(true);
    }

    public IEnumerator _RespawnPlayer()
    {
        yield return new WaitForSeconds(spawnDelay);

        Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);
        //Transform clone = Instantiate(spawnPrefab, spawnPoint.position, spawnPoint.rotation) as Transform;
        //Destroy(clone.gameObject, 3f);
    }

    public static void KillPlayer(Player player)
    {
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

    public static void KillEnemy(Enemy enemy)
    {
        Instance._KillEnemy(enemy);
    }

    public void _KillEnemy(Enemy _enemy)
    {
       // Transform _clone = Instantiate(_enemy.deathParticles, _enemy.transform.position, Quaternion.identity) as Transform;
        Destroy(_enemy.gameObject);
       // Destroy(_clone.gameObject, 3f);
    }
}

