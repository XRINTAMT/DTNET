using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InjectionManager : MonoBehaviour
{
    public Injection CheckCompletion(Dictionary<string, float> ingredients)
    {
        foreach (Injection mixture in GetComponentsInChildren<Injection>())
        {
            Injection inj = mixture.CheckCompletion(ingredients);
            if(inj != null)
            {
                return inj;
            }
        }
        return null;
    }

}
