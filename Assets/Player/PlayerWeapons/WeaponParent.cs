using UnityEngine;

public class WeaponParent : MonoBehaviour
{
    public Vector3 PointerPosition {  get; set; }
    private void Update()
    {
        transform.right = (PointerPosition - (Vector3)transform.position).normalized;
    }
}
