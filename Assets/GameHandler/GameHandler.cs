using UnityEngine;
using UnityEngine.InputSystem; 

public class GameHandler : MonoBehaviour
{
    [SerializeField] private LayerMask mouseColliderLayerMask;
    public Vector3 MouseCoords { get; private set; }
    public Vector2 MouseScreenCoords { get; private set; }

    private Camera mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        updateMouseScreenCoords();
        updateMouseWorldCoords();
    }

    private void updateMouseScreenCoords()
    {
        Vector2 mouseScreenPosition = Mouse.current.position.ReadValue();

        Vector3 screenPosWithDepth = new Vector3(mouseScreenPosition.x, mouseScreenPosition.y, -mainCamera.transform.position.z);

        Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(screenPosWithDepth);

        MouseScreenCoords = mouseWorldPosition;
    }

    private void updateMouseWorldCoords()
    {
        if (Mouse.current == null) return;

        Vector2 mouseScreenPosition = Mouse.current.position.ReadValue();

        Ray ray = mainCamera.ScreenPointToRay(mouseScreenPosition);

        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, mouseColliderLayerMask))
        {
            MouseCoords = raycastHit.point;
        }
    }
}