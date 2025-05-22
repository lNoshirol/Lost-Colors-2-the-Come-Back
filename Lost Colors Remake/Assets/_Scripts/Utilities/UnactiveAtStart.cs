using System.Collections;
using System.Threading.Tasks;
using UnityEngine;


public class UnactiveAtStart : MonoBehaviour
{
    //à mettre sur des objets qui set up des ref au start/awake mais dont le gameObject doit être désactivé au début

    IEnumerator Start()
    {
        yield return new WaitForSeconds(0.01f);
        gameObject.SetActive(false);
    }
}
