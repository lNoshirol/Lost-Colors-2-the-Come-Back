using UnityEditor;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] public float playerBaseHealth;
    [SerializeField] public float playerActualHealth;

    private void Start()
    {
        PlayerMain.Instance.UI.UpdatePlayerHealthUI();
    }

    //public void PlayerHealthChange(int healthChangeAmount)
    //{
    //    playerActualHealth -= healthChangeAmount;

    //    PlayerMain.Instance.UI.UpdatePlayerHealthUI();
    //    if (!PlayerMain.Instance.isColorized && healthChangeAmount > 0)
    //    {
    //        PlayerIsDead();
    //    }
    //    if (!PlayerMain.Instance.isColorized && healthChangeAmount < 0)
    //    {
    //        PlayerMain.Instance.ColorSwitch(true);
    //    }

    //    if (playerActualHealth <= 0 && PlayerMain.Instance.isColorized) {
    //        playerActualHealth = 0;
    //        PlayerMain.Instance.ColorSwitch(false);
    //    }
    //    else if(playerActualHealth >= playerBaseHealth)
    //    {
    //        playerActualHealth = playerBaseHealth;
    //    }
    //}

    public void PlayerLoseHP(float healthLoose)
    {
        playerActualHealth = playerActualHealth - healthLoose;
        JuiceManager.Instance.Stop(0.25f);
        PlayerMain.Instance.UI.UpdatePlayerHealthUI();
        if(playerActualHealth <= 0)
        {
            playerActualHealth = 0;
            PlayerIsDead();
        }
    }

    public void PlayerGainHP(float healthGain)
    {
        playerActualHealth = playerActualHealth + healthGain;
        PlayerMain.Instance.UI.UpdatePlayerHealthUI();
        if (playerActualHealth >= 3)
        {
            playerActualHealth = 3;
        }
    }

    private void PlayerIsDead()
    {
        Debug.Log("GameOver");
    }
}
