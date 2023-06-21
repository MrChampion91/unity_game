using UnityEngine;
public class CapsuleController : MonoBehaviour
{
    public float moveSpeed = 10f;  // �������� �������� �������
    public float rotationSpeed = 120f;  // �������� �������� �������
    public float diveForce = 15f;

    public float vaterResistance = 25f;

    public VariableJoystick variableJoystick;

    private Rigidbody rb;  // ������ �� ��������� Rigidbody
    private DiveBooster boost;
    //private float originalRotation; // �������� �������� �������� �������

    private float vertical;
    private float horizontal;
    private void Awake()
    {
        // �������� ������ �� ��������� Rigidbody
        rb = GetComponent<Rigidbody>();
        //originalRotation = transform.rotation.z;
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
        // �������� �� �����������
        Vector3 movement = new Vector3(horizontal * moveSpeed * Time.fixedDeltaTime, 0f, 0f);
        rb.MovePosition(rb.position + movement);


        float rotationAngle = horizontal * rotationSpeed * Time.fixedDeltaTime;

        if (horizontal != 0) // ���� ������� ��������
        {
            float rotation = horizontal > 0 ? rotationAngle : -rotationAngle; // ��������� ���� ��������
            transform.rotation = Quaternion.Euler(0, 0, rotationAngle); // ������������ ������� Quaternion.Lerp ��� Quaternion.RotateTowards ������ Quaternion.Euler
        }
        else // ���� ������� �� ��������
        {
            transform.rotation = Quaternion.Euler(0, 0, rotationAngle); // ���������� ������� � �������� ���������
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
