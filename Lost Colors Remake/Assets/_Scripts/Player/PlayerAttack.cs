using UnityEngine;
using System.Collections;
using System.Drawing;

public class PlayerAttack : MonoBehaviour
{
    public float baseDamageAmount = 20;
    public float attackDamageAmount;
    [SerializeField] private float animationDuration = 1.25f;

    [SerializeField] private bool canAttack = true;

    [SerializeField] private GameObject attackArea;

    [SerializeField] private Animator animator;

    private Coroutine couroutineuh;

    public float comboDelay = 0.5f;
    private bool isAttacking = false;

    public static int NbOfClicks = 0;

    private float[] comboMultipliers = { 1f, 1.1f, 1.3f };

    //private void Start()
    //{
    //    attackArea.SetActive(false);
    //}

    public void BaseAttack()
    {
        if (!canAttack || !PlayerHasPaintbrush()) return;
            OnAttack();
    }

    public void BaseAttackMobile()
    {
        if (!canAttack || !PlayerHasPaintbrush()) return;
        OnAttack();
    }

    private bool PlayerHasPaintbrush()
    {
        return PlayerMain.Instance.Inventory.ItemDatabase[ItemTypeEnum.Paintbrush];
    }

    public void OnAttack()
    {
        if (isAttacking) return;

        NbOfClicks++;
        attackArea.SetActive(true);
        StartCoroutine(ComboAttack());
    }

    private IEnumerator ComboAttack()
    {
        Debug.Log("Bombo");
        isAttacking = true;
        canAttack = false;

        int comboIndex = Mathf.Clamp(NbOfClicks - 1, 0, comboMultipliers.Length - 1);
        attackDamageAmount = baseDamageAmount * comboMultipliers[comboIndex];

        if (couroutineuh != null)
        {
            StopCoroutine(couroutineuh);
        }

        couroutineuh = StartCoroutine(DelayCombo(comboIndex + 1));

        Debug.Log($"Combo {comboIndex + 1}");

        yield return new WaitForSeconds(animationDuration);
        attackArea.SetActive(false);
        canAttack = true;
        isAttacking = false;
    }

    private IEnumerator DelayCombo(int combo)
    {
        //animator.SetTrigger("Attack" + combo);
        yield return new WaitForSeconds(comboDelay);
        NbOfClicks = 0;
    }

}
