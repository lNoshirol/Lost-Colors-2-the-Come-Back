using UnityEditor;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] public int playerBaseHealth;
    [SerializeField] public int playerActualHealth;

    private void Start()
    {
        PlayerMain.Instance.UI.UpdatePlayerHealthUI();
    }


    public void PlayerHealthChange(int healthChangeAmount)
    {
        playerActualHealth -= healthChangeAmount;

        PlayerMain.Instance.UI.UpdatePlayerHealthUI();

        if (playerActualHealth <= 0) {
            playerActualHealth = 0;
            PlayerIsDead();
        }
        else if(playerActualHealth >= 100)
        {
            playerActualHealth = 100;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != 8)
        {

        }
    }

    private void PlayerIsDead()
    {
        Debug.Log("GameOver");
    }
}
