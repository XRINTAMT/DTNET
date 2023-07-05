using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Linq;
using ScenarioSystem;
using Autohand;

public class Syringe : MonoBehaviour
{
    [SerializeField] bool setupInInspector = false;
    [SerializeField] InjectionManager Manager;
    [SerializeField] Camera Head;
    [SerializeField] GameObject InnerPart;
    [SerializeField] GameObject Liquid;
    [SerializeField] GameObject Pomp;
    [SerializeField] float MaxInnerPartDisplacement;
    [SerializeField] float MaxLiquidScale;
    [SerializeField] int SyringeSensitivity;
    [SerializeField] int SyringeCapacity;
    [SerializeField] UnityEvent OnRequirementsMet;
    [SerializeField] GameObject MeasurementCanvas;
    [SerializeField] Text AmountText;
    [SerializeField] Text SubstanceText;
    [SerializeField] Vector3 Offset;
    bool Guided;
    [SerializeField] Material LiquidRight;
    [SerializeField] Material LiquidTooMuch;

    TaskSpecificValues DataInterface;
    Ampule med;
    bool inserted;
    bool pulling;
    bool pushing;
    public float totalSubstance;
    Vector3 innerPartPositionInit;
    Vector3 LiquidPositionInit;
    MeshRenderer liquidRenderer;
    public Material LiquidNormal;
    public Material LiquidNormalUncorrect;
    public Material LiquidNormalCorrect;
    public Dictionary<string, float> ingredients { private set; get;}
    public Injection Lable { get; private set; }

    [SerializeField] Transform indicate;
    bool checkPositionInner;
    public float syringeML;
    public float amount;
    public string currentSubstance;
    public float amountDopamine;
    public float amountSaline;
    public float currentAmountSyringe;
    public float currentAmountSubstance;
    public bool dopamine;
    public bool saline;

    float inaccuracy = 0.5f;
    Material indicateMaterial;
  
    void Awake()
    {
        innerPartPositionInit = InnerPart.transform.localPosition;
        LiquidPositionInit = Liquid.transform.localPosition;
        Guided = PlayerPrefs.GetInt("GuidedMode", 1) == 1;

        ingredients = new Dictionary<string, float>();
        med = null;
        if (!setupInInspector)
        {
            DataInterface = GetComponent<TaskSpecificValues>();
            //Debug.Log("Getting syringe capacity: "+DataInterface.TryGetItem("SyringeCapacity", ref SyringeCapacity));
            PlayerObject player = FindObjectOfType<PlayerObject>();
            Head = player.Head;
        }
        if (Guided)
        {
            liquidRenderer = Liquid.GetComponent<MeshRenderer>();
            LiquidNormal = liquidRenderer.material;
        }
    }
    private void Start()
    {
        InnerPart.GetComponent<Grabbable>().onGrab.AddListener(CheckPositionInner);
        InnerPart.GetComponent<Grabbable>().onRelease.AddListener(StopCheckPositionInner);
        indicateMaterial = indicate.GetComponentInChildren<Renderer>().material;
     
    }
    void CheckPositionInner(Hand hand, Grabbable grabbable) 
    {
        //indicate.gameObject.SetActive(true);
        med = GetComponent<NewSyringeMechanic>().bottle.GetComponent<Ampule>();
        currentSubstance = med.Substance;
        if (currentSubstance == "Dopamine")
        {
            dopamine = true;
            amount = 20;
        }
        if (currentSubstance == "Saline")
        {
            saline = true;
            amount = 50;
        }
        currentAmountSyringe = syringeML;
        checkPositionInner = true;
    }
    void StopCheckPositionInner(Hand hand, Grabbable grabbable)
    {
        //indicate.gameObject.SetActive(false);
        if (currentSubstance== "Dopamine")
        {
            amountDopamine = currentAmountSubstance;
            //med.Amount -= currentAmount;
            //currentSubstance = "";
        }
        if (currentSubstance == "Saline")
        {
            amountSaline = currentAmountSubstance;
            //med.Amount -= currentAmount;
            //currentSubstance = "";
        }
        //AmountText.text = syringeML.ToString("0.0");
        if (Mathf.Round(amountDopamine) == 20 && Mathf.Round(amountSaline) == 50)
        {
            Debug.Log("amountCorrect");
            OnRequirementsMet?.Invoke();
        }

        indicate.GetComponentInChildren<Renderer>().material = LiquidNormal;
        dopamine = false;
        saline = false;
        checkPositionInner = false;
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
        UpdateGuidedLiquidColor(med.Substance);
    }

    private void UpdateGuidedLiquidColor(string substance)
    {
        if (Guided)
        {
            if (substance == "Dopamine")
            {
                if ( Mathf.Round(ingredients[substance]) == 20)
                {
                    liquidRenderer.material = LiquidRight;
                }
                else
                {
                    if(ingredients[substance] > 20)
                    {
                        liquidRenderer.material = LiquidTooMuch;
                    }
                    else
                    {
                        liquidRenderer.material = LiquidNormal;
                    }
                }
            }
            if (substance == "Solanine")
            {
                if (Mathf.Round(ingredients[substance]) == 50)
                {
                    liquidRenderer.material = LiquidRight;
                }
                else
                {
                    if (ingredients[substance] > 50)
                    {
                        liquidRenderer.material = LiquidTooMuch;
                    }
                    else
                    {
                        liquidRenderer.material = LiquidNormal;
                    }
                }
            }
        }
    }

    private void ReleaseGuidedLiquidColor()
    {
        if (Guided)
        {
            liquidRenderer.material = LiquidNormal;
        }
    }

    public void Ejected()
    {
        inserted = false;
        pulling = false;
        //ReleaseGuidedLiquidColor();
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
        if(Manager!=null)
            Lable = Manager.CheckCompletion(ingredients);
    }

    public void Empty(float time)
    {
       
        if (totalSubstance != 0)
        {
            StartCoroutine(EmptyingAnimation(time, 0.3f));
            Pomp.transform.parent = InnerPart.transform;
            GetComponent<Grabbable>().enabled = false;
        }
    }

    IEnumerator EmptyingAnimation(float time, float val)
    {
        ingredients = new Dictionary<string, float>();
        float initValue = totalSubstance;
        for (; totalSubstance > initValue * (1 - val); totalSubstance -= Time.deltaTime / time * initValue)
        {
            InnerPart.transform.localPosition =
                new Vector3(innerPartPositionInit.x,
                Mathf.Lerp(innerPartPositionInit.y, innerPartPositionInit.y - MaxInnerPartDisplacement, totalSubstance / SyringeCapacity),
                innerPartPositionInit.z);
            Liquid.transform.localPosition =
             new Vector3(LiquidPositionInit.x,
             Mathf.Lerp(LiquidPositionInit.y, LiquidPositionInit.y - (MaxInnerPartDisplacement / 2), totalSubstance / SyringeCapacity),
             LiquidPositionInit.z);
            Liquid.transform.localScale =
                new Vector3(Liquid.transform.localScale.x,
                Mathf.Lerp(0, MaxLiquidScale, totalSubstance / SyringeCapacity),
                Liquid.transform.localScale.z);
            yield return 0;
        }
        totalSubstance = initValue * (1 - val);
        InnerPart.transform.localPosition =
                new Vector3(innerPartPositionInit.x,
                Mathf.Lerp(innerPartPositionInit.y, innerPartPositionInit.y - MaxInnerPartDisplacement, totalSubstance / SyringeCapacity),
                innerPartPositionInit.z);
        Liquid.transform.localPosition =
                new Vector3(LiquidPositionInit.x,
                Mathf.Lerp(LiquidPositionInit.y, LiquidPositionInit.y - (MaxInnerPartDisplacement / 2), totalSubstance / SyringeCapacity),
                LiquidPositionInit.z);
        Liquid.transform.localScale =
            new Vector3(Liquid.transform.localScale.x,
            Mathf.Lerp(0, MaxLiquidScale, totalSubstance / SyringeCapacity),
            Liquid.transform.localScale.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (checkPositionInner)
        {
            //float scaleDelta=Mathf.
            indicate.localScale = new Vector3(1, InnerPart.transform.localPosition.y*(-1), 1);

            syringeML = InnerPart.transform.localPosition.y * -50 ;
            AmountText.text = syringeML.ToString("0.0");

            if (dopamine)
            {
                currentAmountSubstance = amountDopamine + (syringeML - currentAmountSyringe);
            }
            if (saline)
            {
                currentAmountSubstance = amountSaline + (syringeML - currentAmountSyringe);
            }



            if (currentAmountSubstance > amount - inaccuracy && currentAmountSubstance < amount + inaccuracy)
            {
                indicate.GetComponentInChildren<Renderer>().material = LiquidNormalCorrect;
            }
            if (currentAmountSubstance < amount - inaccuracy /*&& indicateMaterial == LiquidNormalCorrect*/)
            {
                indicate.GetComponentInChildren<Renderer>().material = LiquidNormal;
            }
            if (currentAmountSubstance > amount + inaccuracy /*&& indicateMaterial == LiquidNormalCorrect*/)
            {
                indicate.GetComponentInChildren<Renderer>().material = LiquidNormalUncorrect;
            }
   
        }

        //if (checkPositionInner)
        //{
        //    //float scaleDelta=Mathf.
        //    indicate.localScale = new Vector3(1, InnerPart.transform.localPosition.y * (-1), 1);

        //    syringeML = InnerPart.transform.localPosition.y * -50;
        //    AmountText.text = syringeML.ToString("0.0");

        //    if (syringeML > amount - inaccuracy && syringeML < amount + inaccuracy)
        //    {
        //        indicate.GetComponentInChildren<Renderer>().material = LiquidNormalCorrect;
        //    }
        //    if (syringeML < amount - inaccuracy /*&& indicateMaterial == LiquidNormalCorrect*/)
        //    {
        //        indicate.GetComponentInChildren<Renderer>().material = LiquidNormal;
        //    }
        //    if (syringeML > amount + inaccuracy /*&& indicateMaterial == LiquidNormalCorrect*/)
        //    {
        //        indicate.GetComponentInChildren<Renderer>().material = LiquidNormalUncorrect;
        //    }
        //    //if (syringeML < amount + inaccuracy && indicateMaterial == LiquidNormalUncorrect)
        //    //{
        //    //    indicateMaterial = LiquidNormalCorrect;
        //    //}
        //}

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
            Liquid.transform.localPosition =
                new Vector3(LiquidPositionInit.x,
                Mathf.Lerp(LiquidPositionInit.y, LiquidPositionInit.y - (MaxInnerPartDisplacement/2), totalSubstance / SyringeCapacity),
                LiquidPositionInit.z);
            Liquid.transform.localScale =
                new Vector3(Liquid.transform.localScale.x,
                Mathf.Lerp(0, MaxLiquidScale, totalSubstance / SyringeCapacity),
                Liquid.transform.localScale.z);
            ingredients[med.Substance] += pullAmount;
            if(!med.Infinite)
                med.Amount -= pullAmount;
            //AmountText.text = ingredients[med.Substance].ToString("0.0");
            AmountText.text = totalSubstance.ToString("0.0");
            CheckCompletion();
            foreach (string ingred in ingredients.Keys.ToList())
            {
                DataInterface.SendDataItem(ingred, (int)ingredients[ingred]);
            }
            UpdateGuidedLiquidColor(med.Substance);
        }
        else
        {
            if (pushing && (med != null))
            {
                float delta = Mathf.Min(Time.deltaTime * SyringeSensitivity, totalSubstance);
                if(delta >= totalSubstance)
                {
                    foreach (string ingred in ingredients.Keys.ToList())
                    {
                        ingredients[ingred] = 0;
                    }
                    totalSubstance = 0;
                    AmountText.text = totalSubstance.ToString("0.0");
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
                    Liquid.transform.localPosition =
                new Vector3(LiquidPositionInit.x,
                Mathf.Lerp(LiquidPositionInit.y, LiquidPositionInit.y - (MaxInnerPartDisplacement / 2), totalSubstance / SyringeCapacity),
                LiquidPositionInit.z);
                    Liquid.transform.localScale =
                        new Vector3(Liquid.transform.localScale.x,
                        Mathf.Lerp(0, MaxLiquidScale, totalSubstance / SyringeCapacity),
                        Liquid.transform.localScale.z);
                }
                foreach (string ingred in ingredients.Keys.ToList())
                {
                    DataInterface.SendDataItem(ingred, (int)ingredients[ingred]);
                }
                UpdateGuidedLiquidColor(med.Substance);
            }
        }
        Vector3 targetPosition = new Vector3(Head.transform.position.x, Head.transform.position.y, Head.transform.position.z);
        MeasurementCanvas.transform.LookAt(targetPosition);
        MeasurementCanvas.transform.position = transform.position + Offset;
    }
}
