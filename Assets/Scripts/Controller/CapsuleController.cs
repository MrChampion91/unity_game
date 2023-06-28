using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(DiveBooster))]
public class CapsuleController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f;  // скорость движения капсулы
    [SerializeField] private float rotationSpeed = 120f;  // скорость поворота капсулы
    [SerializeField] private float diveForce = 15f;
    [SerializeField] private float vaterResistance = 25f;

    private VariableJoystick variableJoystick;

    private Rigidbody rb;
    private DiveBooster boost;

    private float vertical;
    private float horizontal;

    
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        boost = GetComponent<DiveBooster>();
    }

    private void Update()
    {
        if (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor)
        {
           vertical = Input.GetAxis("Vertical");
           horizontal = Input.GetAxis("Horizontal");
        }
        else
        {
            vertical = variableJoystick.Vertical;
            horizontal = variableJoystick.Horizontal;
        }
    }

        private void FixedUpdate()
    {
        // смещение по горизонтали
        Vector3 movement = new Vector3(horizontal * moveSpeed * Time.fixedDeltaTime, 0f, 0f);
        rb.MovePosition(rb.position + movement);


        float rotationAngle = horizontal * rotationSpeed * Time.fixedDeltaTime;

        if (horizontal != 0) // если капсула движется
        {
            float rotation = horizontal > 0 ? rotationAngle : -rotationAngle; // вычисляем угол поворота
            transform.rotation = Quaternion.Euler(0, 0, rotationAngle); // поворачиваем капсулу Quaternion.Lerp или Quaternion.RotateTowards вместо Quaternion.Euler
        }
        else // если капсула не движется
        {
            transform.rotation = Quaternion.Euler(0, 0, rotationAngle); // возвращаем капсулу в исходное положение
        }

        if (vertical > 0 && transform.position.y < -1)
        {
            rb.AddForce(Vector3.up * (diveForce - vaterResistance));
        }

        if (vertical < 0)
        {
            rb.AddForce(Vector3.down * diveForce);
        }

    }
    public void BoostButton()
    {
        Vector3 movementDirection = new Vector3(horizontal, vertical, 0f);
        movementDirection.Normalize();

        if (vertical != 0 || horizontal != 0)
        {
            boost.UseDiveBooster(movementDirection);
        }
    }
}
