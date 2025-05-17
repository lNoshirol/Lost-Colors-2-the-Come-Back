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
        if (!PlayerMain.Instance.isColorized && healthChangeAmount > 0)
        {
            PlayerIsDead();
        }
        if (!PlayerMain.Instance.isColorized && healthChangeAmount < 0)
        {
            PlayerMain.Instance.ColorSwitch(true);
        }

        if (playerActualHealth <= 0 && PlayerMain.Instance.isColorized) {
            playerActualHealth = 0;
            PlayerMain.Instance.ColorSwitch(false);
        }
        else if(playerActualHealth >= playerBaseHealth)
        {
            playerActualHealth = playerBaseHealth;
        }



    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject);
    }

    private void PlayerIsDead()
    {
        Debug.Log("GameOver");
    }
}
