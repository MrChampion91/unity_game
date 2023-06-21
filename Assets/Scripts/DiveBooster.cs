using UnityEngine;

public class DiveBooster : MonoBehaviour
{
    private int diveBoosters = 10; // ���������� ������� ����������
    private float diveBoost = 50f; // ��������� ��������� ����������

    private Rigidbody rb;  // ������ �� ��������� Rigidbody
    private CapsuleProperties LessOxigen;
    //public float currentOxygen;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();// ������ �� ��������� Rigidbody
        LessOxigen = GetComponent<CapsuleProperties>();
        //currentOxygen = gameObject.GetComponent<CapsuleProperties>().CurrentOxygen;
        //LessOxigen
    }
    public void UseDiveBooster(Vector3 movementDirection)//boost
    {
        LessOxigen.LessOxigen();
        while (diveBoosters > 0)
        {
            diveBoosters--;
            rb.AddForce(movementDirection * diveBoost);
        }
        diveBoosters = 10;
    }
}