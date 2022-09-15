using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InjectionManager : MonoBehaviour
{
    public void CheckCompletion(Dictionary<string, float> ingredients)
    {
        foreach (Injection mixture in GetComponentsInChildren<Injection>())
        {
            mixture.CheckCompletion(ingredients);
        }
    }

}
