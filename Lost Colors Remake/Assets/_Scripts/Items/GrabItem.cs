using UnityEngine;
public class GrabItem : MonoBehaviour
{
    public ItemTypeEnum type;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerMain.Instance.Inventory.AddItemToInventory(type);
        }
    }
}