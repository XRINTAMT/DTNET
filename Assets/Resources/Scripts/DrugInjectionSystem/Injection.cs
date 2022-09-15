using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using ScenarioTaskSystem;

[RequireComponent(typeof(Task))]
public class Injection : MonoBehaviour
{
    [Serializable]
    struct Pair
    {
        public string Substance;
        public float Amount;
        public float Epsilon;
    }
    [SerializeField] Pair[] DesiredResults;

    public void CheckCompletion(Dictionary<string, float> ingredients)
    {

        foreach (Pair ingredient in DesiredResults)
        {
            if (!ingredients.ContainsKey(ingredient.Substance))
                return;
            if (Mathf.Abs(ingredients[ingredient.Substance] - ingredient.Amount) > ingredient.Epsilon)
                return;
        }
        GetComponent<Task>().Complete();
    }
}
