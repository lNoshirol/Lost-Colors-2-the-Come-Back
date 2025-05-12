using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class SpawnManager : MonoBehaviour
{
    void Start()
    {
        WorldMain.Instance.CleanSpawnList();
        foreach (Transform child in transform)
        {
            WorldMain.Instance.RoomSwitchList.Add(child.gameObject);
        }
    }
}
