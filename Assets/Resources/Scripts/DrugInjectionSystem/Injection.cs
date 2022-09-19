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
    public float Proximity { get; private set; } = 1;

    public Injection CheckCompletion(Dictionary<string, float> ingredients)
    {
        Proximity = 1;
        foreach (Pair ingredient in DesiredResults)
        {
            if (!ingredients.ContainsKey(ingredient.Substance))
                return null;
            if (Mathf.Abs(ingredients[ingredient.Substance] - ingredient.Amount) > ingredient.Epsilon)
                return null;
            Proximity -= (Mathf.Abs(ingredients[ingredient.Substance] - ingredient.Amount) / ingredient.Epsilon) / DesiredResults.Length;
        }
        GetComponent<Task>().Complete();
        return this;
    }
}
