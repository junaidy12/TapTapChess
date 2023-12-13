using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseWorld : MonoBehaviour
{
    //SINGLETON
    public static MouseWorld Instance;
    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    [SerializeField] LayerMask mousePlaneLayerMask;
    public static Vector3 GetMousePosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, Instance.mousePlaneLayerMask))
        {
            return hit.point;
        }
        return Vector3.zero;
    }

}
