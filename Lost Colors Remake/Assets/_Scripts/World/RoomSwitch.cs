using Unity.Cinemachine;
using UnityEngine;

public class RoomSwitch : MonoBehaviour
{
    [SerializeField] private RoomSwitcherDATA roomSwitcherData;
    private void Start()
    {
        gameObject.name = roomSwitcherData.switcherID;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            WorldMain.Instance.SwitchRoom(roomSwitcherData.targetSceneName, roomSwitcherData.targetSwitcherID);
        }
    }
}