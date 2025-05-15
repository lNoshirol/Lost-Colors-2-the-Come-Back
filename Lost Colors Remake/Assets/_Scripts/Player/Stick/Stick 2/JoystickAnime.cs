using UnityEngine;

public class JoystickAnime : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        animator = GetComponentInParent<Animator>();
    }

    public void BoolSwitcher(string boolName, bool value)
    {
        animator.SetBool(boolName, value);
    }
}
