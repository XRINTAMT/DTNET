using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Syringe : MonoBehaviour
{
    [SerializeField] Camera Head;
    [SerializeField] GameObject InnerPart;
    [SerializeField] float MaxInnerPartDisplacement;
    [SerializeField] float SyringeSensitivity;
    [SerializeField] float SyringeCapacity;
    [Serializable]
    struct Pair
    {
        public string Substance;
        public float Amount;
        public float Epsilon;
    }
    [SerializeField] Pair[] DesiredResults;
    [SerializeField] UnityEvent OnRequirementsMet;

    [SerializeField] GameObject MeasurementCanvas;
    [SerializeField] Text AmountText;
    [SerializeField] Text SubstanceText;
    [SerializeField] Vector3 Offset;

    Ampule med;
    bool inserted;
    bool pulling;
    bool pushing;
    float totalSubstance;
    Vector3 innerPartPositionInit;
    Dictionary<string, float> ingredients;

    void Awake()
    {
        innerPartPositionInit = InnerPart.transform.localPosition;
        ingredients = new Dictionary<string, float>();
        med = null;
    }

    public void Inserted(Ampule medicine)
    {
        MeasurementCanvas.SetActive(true);
        SubstanceText.text = medicine.Substance;
        inserted = true;
        med = medicine;
        if (!ingredients.ContainsKey(med.Substance))
        {
            ingredients.Add(med.Substance, 0);
        }
        SubstanceText.text = med.Substance;
        AmountText.text = ingredients[med.Substance].ToString("0.0");
    }

    public void Ejected()
    {
        MeasurementCanvas.SetActive(pushing);
        inserted = false;
        pulling = false;
    }

    public void Pull()
    {
        if (inserted && !pushing)
        {
            pulling = true;
        }
    }

    public void StopPulling()
    {
        pulling = false;
    }

    public void Push()
    {
        if (!pulling)
        {
            pushing = true;
        }
        MeasurementCanvas.SetActive(true);
    }

    public void StopPushing()
    {
        pushing = false;
        MeasurementCanvas.SetActive(inserted);
    }

    private void CheckCompletion()
    {
        foreach(Pair ingredient in DesiredResults)
        {
            if (!ingredients.ContainsKey(ingredient.Substance))
                return;
            if (Mathf.Abs(ingredients[ingredient.Substance] - ingredient.Amount) > ingredient.Epsilon)
                return;
        }
        OnRequirementsMet.Invoke();
    }

    public void Empty()
    {
        if(totalSubstance != 0)
        {
            StartCoroutine(EmptyingAnimation());
        }
    }

    IEnumerator EmptyingAnimation()
    {
        ingredients = new Dictionary<string, float>();
        for (; totalSubstance > 0; totalSubstance -= Time.deltaTime * SyringeSensitivity)
        {
            InnerPart.transform.localPosition =
                new Vector3(innerPartPositionInit.x,
                Mathf.Lerp(innerPartPositionInit.y, innerPartPositionInit.y - MaxInnerPartDisplacement, totalSubstance / SyringeCapacity),
                innerPartPositionInit.z);
            yield return 0;
        }
        totalSubstance = 0;
        InnerPart.transform.localPosition = innerPartPositionInit;
    }

    // Update is called once per frame
    void Update()
    {
        if (pulling)
        {
            float pullAmount = Mathf.Min(med.Amount, Time.deltaTime * SyringeSensitivity, SyringeCapacity - totalSubstance);
            totalSubstance += pullAmount;
            InnerPart.transform.localPosition = 
                new Vector3(innerPartPositionInit.x, 
                Mathf.Lerp(innerPartPositionInit.y, innerPartPositionInit.y - MaxInnerPartDisplacement, totalSubstance / SyringeCapacity), 
                innerPartPositionInit.z);
            ingredients[med.Substance] += pullAmount;
            med.Amount -= pullAmount;
            AmountText.text = ingredients[med.Substance].ToString("0.0");
            CheckCompletion();
        }
        else
        {
            if (pushing)
            {
                if (med != null)
                {
                    float pushAmount = Mathf.Min(ingredients[med.Substance], Time.deltaTime * SyringeSensitivity);
                    totalSubstance -= pushAmount;
                    InnerPart.transform.localPosition =
                        new Vector3(innerPartPositionInit.x,
                        Mathf.Lerp(innerPartPositionInit.y, innerPartPositionInit.y - MaxInnerPartDisplacement, totalSubstance / SyringeCapacity),
                        innerPartPositionInit.z);
                    ingredients[med.Substance] -= pushAmount;
                    med.Amount += pushAmount;
                    AmountText.text = ingredients[med.Substance].ToString("0.0");
                    CheckCompletion();
                }
            }
        }
        Vector3 targetPosition = new Vector3(Head.transform.position.x, Head.transform.position.y, Head.transform.position.z);
        MeasurementCanvas.transform.LookAt(targetPosition);
        MeasurementCanvas.transform.position = transform.position + Offset;
    }
}
