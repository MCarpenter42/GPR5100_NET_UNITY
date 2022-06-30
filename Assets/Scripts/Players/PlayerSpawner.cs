using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerSpawner : Core
{
    [SerializeField] private GameObject player;

    [SerializeField] float minX;
    [SerializeField] float maxX;
    [SerializeField] float minZ;
    [SerializeField] float maxZ;

    // Start is called before the first frame update
    void Start()
    {
        float randomX = Random.Range(minX, maxX);
        float randomZ = Random.Range(minZ, maxZ);
        Vector3 randomSpawnPos = new Vector3(randomX, 0.0f, randomZ);

        PhotonNetwork.Instantiate(player.name, randomSpawnPos, Quaternion.identity);
    }
}
