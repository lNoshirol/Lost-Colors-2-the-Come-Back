using UnityEngine;
public class GrabItem : MonoBehaviour
{
    public ItemTypeEnum type;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerMain.Instance.Inventory.AddItemToInventory(type);
            gameObject.SetActive(false);
        }
    }
}