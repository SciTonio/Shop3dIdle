using UnityEngine;

public class FreeCameraController : MonoBehaviour
{
    [Header("Vitesse de déplacement de base")]
    public float baseMoveSpeed = 10f;
    [Header("Vitesse de pan souris")]
    public float panSpeed = 5f;
    [Header("Vitesse de rotation souris")]
    public float lookSpeed = 2f;
    [Header("Puissance de zoom")]
    public float zoomSpeed = 5f;
    [Header("Hauteur de référence pour l'ajustement de la vitesse")]
    public float referenceHeight = 10f;
    [Header("Limites de zoom (hauteur minimale et maximale)")]
    public float minHeight = 5f;
    public float maxHeight = 50f;

    void Start()
    {
        // On prend la hauteur initiale comme référence
        referenceHeight = transform.position.y;
    }

    void Update()
    {
        HandleMovement();
        HandlePan();
        HandleRotation();
        HandleZoom();
    }

    void HandleMovement()
    {
        float h = Input.GetAxis("Horizontal");   // A/D ou flèches
        float v = Input.GetAxis("Vertical");     // W/S ou flèches

        Vector3 forward = new Vector3(transform.forward.x, 0f, transform.forward.z).normalized;
        Vector3 right = new Vector3(transform.right.x, 0f, transform.right.z).normalized;

        float speedFactor = transform.position.y / referenceHeight;
        float dynamicSpeed = baseMoveSpeed * speedFactor;

        Vector3 move = (right * h + forward * v) * dynamicSpeed * Time.deltaTime;
        if (move.magnitude > 0f)
        {
            transform.position += move;
        }
    }

    void HandlePan()
    {
        // Pan sur molette clic (Input.GetMouseButton(2))
        if (Input.GetMouseButton(2))
        {
            float deltaX = Input.GetAxis("Mouse X");
            float deltaY = Input.GetAxis("Mouse Y");

            Vector3 forward = new Vector3(transform.forward.x, 0f, transform.forward.z).normalized;
            Vector3 right = new Vector3(transform.right.x, 0f, transform.right.z).normalized;

            float speedFactor = transform.position.y / referenceHeight;
            float dynamicSpeed = baseMoveSpeed * speedFactor;

            // Inversion de deltaX pour donner l'impression de tirer le terrain
            Vector3 pan = ((-right * deltaX) - forward * deltaY) * dynamicSpeed * panSpeed * Time.deltaTime;
            if (pan.magnitude > 0f)
            {
                transform.position += pan;
            }
        }
    }

    void HandleRotation()
    {
        // Rotation sur clic droit
        if (Input.GetMouseButton(1))
        {
            float mouseX = Input.GetAxis("Mouse X") * lookSpeed;
            float mouseY = Input.GetAxis("Mouse Y") * lookSpeed;

            transform.Rotate(Vector3.up, mouseX, Space.World);
            transform.Rotate(transform.right, -mouseY, Space.World);
        }
    }

    void HandleZoom()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0f)
        {
            // Zoom avant/arrière le long de l'axe de la caméra
            Vector3 newPos = transform.position + transform.forward * scroll * baseMoveSpeed * zoomSpeed;
            // On limite la hauteur pour ne pas descendre/enlever trop bas
            newPos.y = Mathf.Clamp(newPos.y, minHeight, maxHeight);
            transform.position = newPos;
        }
    }
}
