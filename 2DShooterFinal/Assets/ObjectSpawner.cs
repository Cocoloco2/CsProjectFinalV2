using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject playerPrefab;

    [SerializeField]
    private GameObject enemyPrefab;

    [SerializeField]
    private GameObject heartBar;

    private void Start()
    {
       // heartBar = GameObject.FindGameObjectWithTag("HeartBar");
    }
    public void SpawnPlayer(Vector2 pos, Quaternion rotation)
    {
        GameObject player = Instantiate(playerPrefab, pos, rotation);

        heartBar.SetActive(true);
    }

    public void SpawnEnemy(Vector2 pos, Quaternion rotation)
    {
        GameObject enemy = Instantiate(enemyPrefab, pos, rotation);
        
    }
    public GameObject GetPlayerPrefab()
    {
        return playerPrefab;
    }
}
