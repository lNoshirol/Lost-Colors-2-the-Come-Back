using System;
using UnityEngine;

public class Enigma : MonoBehaviour
{
    [SerializeField] float castDistanceAuthorize;
    [SerializeField] string drawTargetName;

    [SerializeField] bool _isSolved;
    public event Action OnEnigmaSolve;

    private void Start()
    {
        DrawForDollarP.instance.OnDrawFinish += SolveEnigme;
    }

    private void OnDestroy()
    {
        DrawForDollarP.instance.OnDrawFinish -= SolveEnigme;
    }

    private void SolveEnigme(DrawData drawData)
    {
        if (Vector3.Distance(PlayerMain.Instance.transform.position, transform.position) <= castDistanceAuthorize && 
            drawData.result.GestureClass == drawTargetName && 
            drawData.result.Score > PlayerMain.Instance.toileInfo.tolerance && 
            !_isSolved)
        {
            OnEnigmaSolve?.Invoke();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, castDistanceAuthorize);
    }
}
