using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject playerPrefab;
public void SpawnPlayer(Vector2 pos, Quaternion rotation)
    {
        GameObject player = Instantiate(playerPrefab, pos, rotation);
    }
}
