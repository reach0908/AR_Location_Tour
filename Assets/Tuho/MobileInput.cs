using UnityEngine;
using UnityEngine.EventSystems;

public class MobileInput : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{

    [SerializeField]
    float force = 10f;

    Rigidbody rb;
    bool isDragging = false;
    Vector3 direction;

    public Material hitMaterial;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void OnBeginDrag(PointerEventData pointerEventData)
    {
        isDragging = true;
    }

    public void OnEndDrag(PointerEventData pointerEventData)
    {
        isDragging = false;
    }

    public void OnDrag(PointerEventData pointerEventData)
    {
        direction = new Vector3(Input.GetAxis("Mouse Y"), -Input.GetAxis("Mouse X"), 0f);
    }

    void FixedUpdate()
    {
        if (isDragging)
        {
            rb.AddTorque(direction * force * Time.fixedDeltaTime);
        }
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if(Physics.Raycast(ray,out hitInfo))
            {
                var rig = hitInfo.collider.GetComponent<Rigidbody>();
                if(rig != null)
                {
                    rig.GetComponent<MeshRenderer>().material = hitMaterial;
                    rig.AddForceAtPosition(ray.direction * 50f, hitInfo.point, ForceMode.VelocityChange);
                }
            }
        }
    }
}

