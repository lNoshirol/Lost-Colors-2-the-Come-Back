using UnityEngine;

public class Enigma : MonoBehaviour
{
    [SerializeField] float castDistanceAuthorize;
    [SerializeField] string drawTargetName;

    private void Start()
    {
        DrawForDollarP.instance.OnDrawFinish += SolveEnigme;
    }

    private void SolveEnigme(DrawData drawData)
    {
        if (Vector3.Distance(PlayerMain.Instance.transform.position, transform.position) <= castDistanceAuthorize && drawData.result.GestureClass == drawTargetName)
        {
            //Debug.Log("Choix numéro 2 ça marche");
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, castDistanceAuthorize);
    }
}
