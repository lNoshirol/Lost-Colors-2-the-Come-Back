using UnityEngine.InputSystem;
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

    private void Start()
    {
        attackArea.SetActive(false);
    }

    public void BaseAttack(InputAction.CallbackContext context)
    {
        if (canAttack && PlayerMain.Instance.Inventory.ItemDatabase[ItemTypeEnum.Paintbrush])
        {
            OnAttack();
        }
    }

    public void BaseAttackMobile()
    {
        if (canAttack && PlayerMain.Instance.Inventory.ItemDatabase[ItemTypeEnum.Paintbrush])
        {
            OnAttack();
        }
    }

    public void OnAttack()
    {
        if (isAttacking) return;

        NbOfClicks++;
        StartCoroutine(ComboAttack());
    }

    private IEnumerator ComboAttack()
    {
        isAttacking = true;
        canAttack = false;

        if (NbOfClicks == 1)
        {
            attackDamageAmount = baseDamageAmount;
            animator.SetBool("A1", true);
            couroutineuh = StartCoroutine(DelayCombo());
        }
        else if (NbOfClicks == 2)
        {
            attackDamageAmount = baseDamageAmount * 1.1f;
            StopCoroutine(couroutineuh);
            animator.SetBool("A2", true);
            couroutineuh = StartCoroutine(DelayCombo());

        }
        else if (NbOfClicks == 3)
        {
            attackDamageAmount = baseDamageAmount * 1.3f;
            StopCoroutine(couroutineuh);
            animator.SetBool("A3", true);
            StartCoroutine(DelayCombo());

        }
        yield return new WaitForSeconds(animationDuration);
        yield return new WaitForSeconds(comboDelay);

        canAttack = true;
        isAttacking = false;
    }

    private IEnumerator DelayCombo()
    {
        yield return new WaitForSeconds(comboDelay*2);
        animator.SetBool("A1", false);
        animator.SetBool("A2", false);
        animator.SetBool("A3", false);
        NbOfClicks = 0;
    }
}
