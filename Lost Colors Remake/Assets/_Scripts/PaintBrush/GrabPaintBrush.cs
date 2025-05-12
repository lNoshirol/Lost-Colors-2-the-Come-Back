using UnityEngine;

public class GrabPaintBrush : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && PlayerMain.Instance.Inventory.ItemDatabase[ItemTypeEnum.Paintbrush])
        {
            ToileMain.Instance.TriggerToile.EnableToileButton();
            transform.SetParent(PlayerMain.Instance.paintBrushSocket.transform);
            Debug.Log(transform.position);
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.AngleAxis(-90, Vector3.right);


        }
    }
}
