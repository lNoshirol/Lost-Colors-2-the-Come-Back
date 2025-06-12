using System.Collections;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] public float playerBaseHealth;
    [SerializeField] public float playerActualHealth;

    private bool isInvicible;
    
    //public void PlayerHealthChange(int healthChangeAmount)
    //{
    //    playerActualHealth -= healthChangeAmount;

    //    PlayerMain.Instance.UI.UpdatePlayerHealthUI();
    //    if (!PlayerMain.Instance.IsColorized && healthChangeAmount > 0)
    //    {
    //        PlayerIsDead();
    //    }
    //    if (!PlayerMain.Instance.IsColorized && healthChangeAmount < 0)
    //    {
    //        PlayerMain.Instance.ColorSwitch(true);
    //    }

    //    if (playerActualHealth <= 0 && PlayerMain.Instance.IsColorized) {
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
        if (!isInvicible)
        {
            var SoundHit = SoundsManager.Instance;
            int RandowSound = Random.Range(0, SoundHit.Hit.Length);
            SoundHit.PlaySound(SoundHit.Hit[RandowSound], false);

            playerActualHealth = playerActualHealth - healthLoose;

            StartCoroutine(Invicibility());

            JuiceManager.Instance.PlayerHit(0.25f);
            PlayerMain.Instance.UI.UpdatePlayerHealthUI();
            if(playerActualHealth <= 0)
            {
                playerActualHealth = 0;
                PlayerIsDead();
            }
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
        // A METTRE QUAND LE JOUEUR POURRA SAVOIR QUAND IL EST MORT GENRE LE PANEL
        //var SoundDeath = SoundsManager.Instance;
        //int RandowSound = Random.Range(0, SoundDeath.Death.Length);
        //SoundDeath.PlaySound(SoundDeath.Death[RandowSound], false);
        Debug.Log("GameOver");
    }

    IEnumerator Invicibility()
    {
        isInvicible = true;
        PlayerMain.Instance.playerSprite.color = Color.red;
        yield return new WaitForSeconds(2);
        isInvicible = false;
        PlayerMain.Instance.playerSprite.color = Color.white;
    }
}
