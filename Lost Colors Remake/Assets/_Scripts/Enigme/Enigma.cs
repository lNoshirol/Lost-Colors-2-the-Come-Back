using System;
using UnityEngine;

public class Enigma : MonoBehaviour
{
    [SerializeField] float castDistanceAuthorize;
    [SerializeField] string drawTargetName;

    public event Action OnEnigmaSolve;

    private void Start()
    {
        DrawForDollarP.instance.OnDrawFinish += SolveEnigme;
    }

    private void SolveEnigme(DrawData drawData)
    {
        if (Vector3.Distance(PlayerMain.Instance.transform.position, transform.position) <= castDistanceAuthorize && drawData.result.GestureClass == drawTargetName)
        {
            OnEnigmaSolve?.Invoke();
            //Debug.Log("Choix num�ro 2 �a marche");
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, castDistanceAuthorize);
    }
}
