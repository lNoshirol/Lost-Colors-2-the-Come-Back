using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine.InputSystem;

public class WorldMain : MonoBehaviour
{
    public static WorldMain Instance { get; private set; }

    public List<GameObject> RoomSwitchList = new List<GameObject>();

    public GameObject currentRoomSwitcher;

    public string CurrentRoomName;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        CurrentRoomName = SceneManager.GetActiveScene().name;
    }

    public void CleanSpawnList()
    {
        RoomSwitchList.Clear();
    }

    public GameObject FindCorrectSpawn(string switcherName)
    {
        foreach (GameObject spawn in RoomSwitchList) {

            if (spawn.name == switcherName)
            {
                currentRoomSwitcher = spawn;
            }
        }
        return currentRoomSwitcher;
    }
    public async void SwitchRoom(string roomName, string switcherName)
    {
        PlayerMain.Instance.playerInput.DeactivateInput();
        PlayerMain.Instance.Rigidbody.linearVelocity = new Vector3(0, 0, 0);
        PlayerMain.Instance.UI.SwitchRoomUI();
        await Task.Delay(1000);
        SceneManager.LoadScene(roomName);
        await Task.Delay(10);
        PlayerMain.Instance.transform.position = FindCorrectSpawn(switcherName).transform.GetChild(0).transform.position;
        CameraMain.Instance.CenterCameraAtPosition(CameraMain.Instance.transform.position);
        PlayerMain.Instance.UI.SwitchRoomUI();
        await Task.Delay(500);
        PlayerMain.Instance.playerInput.ActivateInput();
    }
}