using System.Threading.Tasks;
using UnityEngine;

public class CameraMain : AsyncSingletonPersistent<CameraMain>
{
    protected override async Task OnInitializeAsync()
    {
        await Bootstrap.Instance.WaitUntilInitializedAsync();
    }
    public void CenterCameraAtPosition(Vector2 targetPosition)
    {
        transform.position = targetPosition;
    }
}
