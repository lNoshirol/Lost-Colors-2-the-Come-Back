using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Threading.Tasks;

public class WorldMain : SingletonCreatorPersistent<WorldMain>
{

    public List<GameObject> RoomSwitchList = new List<GameObject>();

    public GameObject currentRoomSwitcher;

    public string CurrentRoomName;

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
        Debug.Log("Switch");
        PlayerMain.Instance.Rigidbody2D.linearVelocity = new Vector3(0, 0, 0);
        PlayerMain.Instance.UI.SwitchRoomUI();
        await Task.Delay(1000);
        SceneManager.LoadScene(roomName);
        await Task.Delay(10);
        PlayerMain.Instance.transform.position = FindCorrectSpawn(switcherName).transform.GetChild(0).transform.position;
        //CameraMain.Instance.CenterCameraAtPosition(CameraMain.Instance.transform.position);
        PlayerMain.Instance.UI.SwitchRoomUI();
        await Task.Delay(500);
    }

    public IEnumerator SwitchRoomC(string roomName, string switcherName)
    {
        Debug.Log("Switch");
        PlayerMain.Instance.Rigidbody2D.linearVelocity = new Vector3(0, 0, 0);
        PlayerMain.Instance.UI.SwitchRoomUI();
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(roomName);
        yield return new WaitForSeconds(0.1f);
        PlayerMain.Instance.transform.position = FindCorrectSpawn(switcherName).transform.GetChild(0).transform.position;
        //CameraMain.Instance.CenterCameraAtPosition(CameraMain.Instance.transform.position);
        PlayerMain.Instance.UI.SwitchRoomUI();
        yield return new WaitForSeconds(1f);
    }
}