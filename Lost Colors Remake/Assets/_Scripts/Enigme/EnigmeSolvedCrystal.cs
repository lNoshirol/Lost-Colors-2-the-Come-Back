using UnityEngine;

public class EnigmeSolvedCrystal : EnigmeSolved
{
    public SpriteRenderer crystal;  
    
    public Sprite crystalUncorrupted;

    public override void Interact()
    {
        crystal.sprite = crystalUncorrupted;

        Debug.Log("Changer couleur via shader");
    }
}