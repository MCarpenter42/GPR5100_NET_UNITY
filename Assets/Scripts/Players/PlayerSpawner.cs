using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class PlayerSpawner : Core
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject namePlatePrefab;

    [SerializeField] float minX;
    [SerializeField] float maxX;
    [SerializeField] float minZ;
    [SerializeField] float maxZ;

    void Awake()
    {
        Player[] currentPlayers = PhotonNetwork.PlayerList;
        string nameCheck = PhotonNetwork.NickName;
        int nameMatches = 0;
        foreach (Player plr in currentPlayers)
        {
            if (nameCheck == plr.NickName)
            {
                nameMatches++;
            }
        }
        if (nameMatches > 0)
        {
            string newName = "";
            int lastPos = nameCheck.LastIndexOf('_');
            if (lastPos > 0)
            {
                string substrA = nameCheck.Substring(0, lastPos + 1);
                string substrB = nameCheck.Substring(lastPos + 1);
                int toInt = 0;
                if (int.TryParse(substrB, out toInt))
                {
                    newName = substrA + (toInt + 1);
                }
                else
                {
                    newName = nameCheck + "_1";
                }
            }
            else
            {
                newName = nameCheck + "_1";
            }
            PhotonNetwork.NickName = newName;
        }
    }

    void Start()
    {
        float randomX = Random.Range(minX, maxX);
        float randomZ = Random.Range(minZ, maxZ);
        Vector3 randomSpawnPos = new Vector3(randomX, 0.0f, randomZ);

        GameObject clientPlayer = PhotonNetwork.Instantiate(playerPrefab.name, randomSpawnPos, Quaternion.identity);
        clientPlayer.name = PhotonNetwork.NickName;
        GameManager.ClientPlayer = clientPlayer.GetComponent<PlayerController>();

        GameObject namePlate = Instantiate(namePlatePrefab, clientPlayer.transform);
        namePlate.GetComponent<Nameplate>().name = PhotonNetwork.NickName;

        KeyValuePair<PlayerController, GameObject> kvp = new KeyValuePair<PlayerController, GameObject>(GameManager.ClientPlayer.GetComponent<PlayerController>(), namePlate);
        GameManager.GameplayController.players.Add(PhotonNetwork.NickName, kvp);
    }
}
