using UnityEngine;

public class DiveBooster : MonoBehaviour
{
    private int diveBoosters = 10; // количество времени ускорителя
    private float diveBoost = 50f; // множитель ускорения погружения

    private Rigidbody rb;  // ссылка на компонент Rigidbody
    private CapsuleProperties LessOxigen;
    //public float currentOxygen;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();// ссылка на компонент Rigidbody
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