using UnityEngine;

public class GrabPaintBrush : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && PlayerMain.Instance.Inventory.ItemDatabase[ItemTypeEnum.Paintbrush])
        {
            ToileMain.Instance.TriggerToile.EnableToileButton();
            //Penser � changer le sprite
        }
    }
}
