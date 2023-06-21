using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "NewData", menuName = "ScriptableObjects/NewData")]
public class NEW_Data : ScriptableObject
{
    public int intValue;
    public float floatValue;
    public string stringValue;

    public void CustomFunction()
    {
        // Реализуйте нужную функциональность
    }
}