using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{

    public static GameMaster gm;
    public AudioSource respawnAudio;

    [SerializeField]
    private GameObject gameOverUI;

    private int _remainingLives = 3;
    public static int RemainingLives
    {
        get { return gm._remainingLives;  }
    }
     
    void Start()
    {
        if (gm == null)
        {
            gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        }
        respawnAudio = GetComponent<AudioSource>();
    }

    public Transform playerPrefab;
    public Transform spawnPoint;
    public int spawnDelay = 4;
    public Transform spawnPrefab;

    public void EndGame()
    {

        Debug.Log("Game Over");
        gameOverUI.SetActive(true);
    }

    public IEnumerator RespawnPlayer()
    {
        Debug.Log("GM: Respawning Player");
        respawnAudio.Play();
        yield return new WaitForSeconds(spawnDelay);

        Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);
        Transform clone = Instantiate(spawnPrefab, spawnPoint.position, spawnPoint.rotation);
        Destroy(clone.gameObject, 3f);
    }

    public static void KillPlayer(Player player)
    {
        Debug.Log("GM: Killing Player");
        gm._KillPlayer(player);
    }

    public void _KillPlayer(Player player)
    {
        Debug.Log("GM: _Killing Player");
        Destroy(player.gameObject);
        _remainingLives -= 1;
        if(RemainingLives <= 0)
        {
            gm.EndGame();
        }
        else
        {
            Debug.Log("GM: KillPlayer() Respawning Player");
            gm.StartCoroutine(gm.RespawnPlayer());
        }
    }

    public static void KillEnemy(Enemy enemy)
    {
        gm._KillEnemy(enemy);
    }

    public void _KillEnemy(Enemy _enemy)
    {
        Transform _clone = Instantiate(_enemy.deathParticles, _enemy.transform.position, Quaternion.identity);
        Destroy(_clone.gameObject, 2f);
        Destroy(_enemy.gameObject);
    }

}
