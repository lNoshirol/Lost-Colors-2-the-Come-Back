using UnityEngine;

public class GloabalAnimatorBoolSwitcher : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    public void BoolSwitch(string _boolName, bool _bool)
    {
        if (TryGetComponent(out _animator))
        {
            _animator.SetBool(_boolName, _bool);
        }
        else
        {
            Debug.LogError("No Animator on the object");
        }
    }
}
