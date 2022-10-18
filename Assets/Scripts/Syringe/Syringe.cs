using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Linq;
using ScenarioSystem;

public class Syringe : MonoBehaviour
{
    [SerializeField] bool setupInInspector = false;
    [SerializeField] InjectionManager Manager;
    [SerializeField] Camera Head;
    [SerializeField] GameObject InnerPart;
    [SerializeField] GameObject Pomp;
    [SerializeField] float MaxInnerPartDisplacement;
    [SerializeField] int SyringeSensitivity;
    [SerializeField] int SyringeCapacity;
    [SerializeField] UnityEvent OnRequirementsMet;
    [SerializeField] GameObject MeasurementCanvas;
    [SerializeField] Text AmountText;
    [SerializeField] Text SubstanceText;
    [SerializeField] Vector3 Offset;

    TaskSpecificValues DataInterface;
    Ampule med;
    bool inserted;
    bool pulling;
    bool pushing;
    float totalSubstance;
    Vector3 innerPartPositionInit;
    
    public Dictionary<string, float> ingredients { private set; get;}
    public Injection Lable { get; private set; }

    
    void Awake()
    {
        innerPartPositionInit = InnerPart.transform.localPosition;

        ingredients = new Dictionary<string, float>();
        med = null;
        if (!setupInInspector)
        {
            DataInterface = GetComponent<TaskSpecificValues>();
            //Debug.Log("Getting syringe capacity: "+DataInterface.TryGetItem("SyringeCapacity", ref SyringeCapacity));
            PlayerObject player = FindObjectOfType<PlayerObject>();
            Head = player.Head;
        }
    }

    public void TurnTheHUD(bool OnOff)
    {
        MeasurementCanvas.SetActive(OnOff);
    }

    public void Inserted(Ampule medicine)
    {
        //SubstanceText.text = medicine.Substance;
        inserted = true;
        StopPushing();
        med = medicine;
        if (!ingredients.ContainsKey(med.Substance))
        {
            ingredients.Add(med.Substance, 0);
        }
        //SubstanceText.text = med.Substance;
        //AmountText.text = ingredients[med.Substance].ToString("0.0");
    }

    public void Ejected()
    {
        inserted = false;
        pulling = false;
    }

    public void Pull()
    {
        if (inserted)
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
        if (!inserted)
        {
            pushing = true;
        }
    }

    public void StopPushing()
    {
        pushing = false;
    }

    private void CheckCompletion()
    {
        Lable = Manager.CheckCompletion(ingredients);

    }

    public void Empty(float time)
    {
        Pomp.transform.parent = InnerPart.transform;
        if (totalSubstance != 0)
        {
            StartCoroutine(EmptyingAnimation(time));
        }
    }

    IEnumerator EmptyingAnimation(float time)
    {
        ingredients = new Dictionary<string, float>();
        float initValue = totalSubstance;
        for (; totalSubstance > 0; totalSubstance -= Time.deltaTime / time * initValue)
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
        if (!setupInInspector)
        {
            DataInterface.TryGetItem("SyringeCapacity", ref SyringeCapacity);
            DataInterface.TryGetItem("SyringeSensitivity", ref SyringeSensitivity);
        }
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
            //AmountText.text = ingredients[med.Substance].ToString("0.0");
            AmountText.text = totalSubstance.ToString("0.0");
            CheckCompletion();
            foreach (string ingred in ingredients.Keys.ToList())
            {
                DataInterface.SendDataItem(ingred, (int)ingredients[ingred]);
            }
        }
        else
        {
            if (pushing)
            {
                float delta = Mathf.Min(Time.deltaTime * SyringeSensitivity, totalSubstance);
                if(delta >= totalSubstance)
                {
                    foreach (string ingred in ingredients.Keys.ToList())
                    {
                        ingredients[ingred] = 0;
                    }
                    totalSubstance = 0;
                }
                else
                {
                    foreach (string ingred in ingredients.Keys.ToList())
                    {
                        float deltaLocal = delta * (ingredients[ingred] / totalSubstance);
                        ingredients[ingred] -= deltaLocal;
                        Debug.Log(ingred + ": " + ingredients[ingred]);
                    }
                    totalSubstance -= delta;
                    AmountText.text = totalSubstance.ToString("0.0");
                    InnerPart.transform.localPosition =
                        new Vector3(innerPartPositionInit.x,
                        Mathf.Lerp(innerPartPositionInit.y, innerPartPositionInit.y - MaxInnerPartDisplacement, totalSubstance / SyringeCapacity),
                        innerPartPositionInit.z);
                }
            }
        }
        Vector3 targetPosition = new Vector3(Head.transform.position.x, Head.transform.position.y, Head.transform.position.z);
        MeasurementCanvas.transform.LookAt(targetPosition);
        MeasurementCanvas.transform.position = transform.position + Offset;
    }
}
