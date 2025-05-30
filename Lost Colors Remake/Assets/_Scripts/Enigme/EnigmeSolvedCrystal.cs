using UnityEngine;

public class EnigmeSolvedCrystal : EnigmeSolved
{
    public SpriteRenderer crystalTrunk;
    public SpriteRenderer crystal;
    public SpriteRenderer smallCrystal;    
    
    public Sprite crystalTrunkUncorrupted;
    public Sprite crystalUncorrupted;
    public Sprite smallCrystalUncorrupted;

    public override void Interact()
    {
        crystal.sprite = crystalUncorrupted;
        crystalTrunk.sprite = crystalUncorrupted;

        if (smallCrystal != null )
        {
            smallCrystal.sprite = smallCrystalUncorrupted;
        }
    }
}