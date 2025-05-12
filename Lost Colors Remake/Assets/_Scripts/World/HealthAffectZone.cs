using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class HealthAffectZone : MonoBehaviour
{
    [SerializeField] private int affectHealthValue;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
            PlayerMain.Instance.Health.PlayerHealthChange(affectHealthValue);
    }
}
