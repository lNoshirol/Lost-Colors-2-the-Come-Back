using System.Threading.Tasks;
using UnityEngine;

public class UnactiveAtStart : MonoBehaviour
{
    //à mettre sur des objets qui set up des ref au start/awake mais dont le gameObject doit être désactivé au début

    async void Start()
    {
        await Task.Delay(10);
        gameObject.SetActive(false);
    }
}
