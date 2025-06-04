using System.Threading.Tasks;
using UnityEngine;

public class CameraMain : AsyncSingletonPersistent<CameraMain>
{
    public new static CameraMain Instance => (CameraMain)AsyncSingleton<CameraMain>.Instance;
    protected override async Task OnInitializeAsync()
    {
        await Bootstrap.Instance.WaitUntilInitializedAsync();
    }
    public void CenterCameraAtPosition(Vector2 targetPosition)
    {
        transform.position = targetPosition;
    }
}
