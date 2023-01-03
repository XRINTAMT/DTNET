using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceAnimationController : MonoBehaviour
{
    public SkinnedMeshRenderer skinnedMeshRenderer;
    public bool setBadMood;
    public bool setNeutralMood;
    public bool setGoodMood;
    [Range(0, 200)] [SerializeField] private int setMood = 100;
    [SerializeField] float speedChangeMood;
    [SerializeField] bool interpolate;
    [SerializeField] int indexInerpolate;
    bool startInterpolateMod;

    float mouseFrown;
    float mouseSmile;
    float browDropL;
    float browDropR;

    float delta;
    bool updateMood;
    float nextCountSetMood;

    public int setIndexMood;
    public bool startUpdateIndexMood;

    // Start is called before the first frame update
    void Start()
    {
        nextCountSetMood = setMood;
        
    }

    public void SetMoodFromIndex(int indexMood)
    {
        mouseFrown = skinnedMeshRenderer.GetBlendShapeWeight(36);
        mouseSmile = skinnedMeshRenderer.GetBlendShapeWeight(33);
        browDropL = skinnedMeshRenderer.GetBlendShapeWeight(12);
        browDropR = skinnedMeshRenderer.GetBlendShapeWeight(13);

        if (indexMood > 100)
        {

            if (mouseFrown > 0) skinnedMeshRenderer.SetBlendShapeWeight(36, mouseFrown - speedChangeMood * Time.deltaTime);
            if (browDropL > 0) skinnedMeshRenderer.SetBlendShapeWeight(12, browDropL - speedChangeMood * Time.deltaTime);
            if (browDropR > 0) skinnedMeshRenderer.SetBlendShapeWeight(13, browDropR - speedChangeMood * Time.deltaTime);
            if (mouseSmile < indexMood - 100)
            {
                skinnedMeshRenderer.SetBlendShapeWeight(33, mouseSmile + speedChangeMood * Time.deltaTime);
                setMood = (int)Math.Round(mouseSmile) + 100;
            }
            if (mouseFrown <= 0 && mouseSmile >= indexMood && browDropL <= 0 && browDropR <= 0)
            {
                delta = 0;
                startUpdateIndexMood = false;
                setMood = indexMood;
                nextCountSetMood = setMood;
            }
        }
        if (indexMood < 100 && mouseFrown > indexMood)
        {

            if (mouseFrown > indexMood)
            {
                skinnedMeshRenderer.SetBlendShapeWeight(36, mouseFrown - speedChangeMood * Time.deltaTime);
                skinnedMeshRenderer.SetBlendShapeWeight(12, mouseFrown);
                skinnedMeshRenderer.SetBlendShapeWeight(13, mouseFrown);
                setMood = 100 - (int)Math.Round(mouseFrown);
            }

            if (mouseSmile > 0) skinnedMeshRenderer.SetBlendShapeWeight(33, mouseSmile - speedChangeMood * Time.deltaTime);

            if (mouseFrown <= indexMood && mouseSmile >= 0 && browDropL <= indexMood && browDropR <= indexMood)
            {
                delta = 0;
                startUpdateIndexMood = false;
                setMood = 0;
                nextCountSetMood = setMood;
            }

        }
        if (indexMood < 100 && mouseFrown < indexMood)
        {
            skinnedMeshRenderer.SetBlendShapeWeight(36, mouseFrown + speedChangeMood * Time.deltaTime);
            skinnedMeshRenderer.SetBlendShapeWeight(12, mouseFrown);
            skinnedMeshRenderer.SetBlendShapeWeight(13, mouseFrown);
            setMood = 100 - (int)Math.Round(mouseFrown);

            if (mouseSmile > 0) skinnedMeshRenderer.SetBlendShapeWeight(33, mouseSmile - speedChangeMood * Time.deltaTime);
            if ((int)Math.Round (mouseFrown) >= indexMood && (int)Math.Round (mouseSmile) >= 0 && (int)Math.Round (browDropL) >= indexMood && (int)Math.Round(browDropR) >= indexMood)
            {
                delta = 0;
                startUpdateIndexMood = false;
                setMood = 0;
                nextCountSetMood = setMood;
            }
        }


    }
    // Update is called once per frame
    void Update()
    {

        if (startUpdateIndexMood)
        {
                SetMoodFromIndex(setIndexMood);
        }

        if (setMood!= nextCountSetMood && !setBadMood && !setGoodMood && !setNeutralMood)
        {
            updateMood = true;
            nextCountSetMood = setMood;
        }
        else
        {
            updateMood = false;
        }
        if (updateMood && setMood >= 100)
        {
            skinnedMeshRenderer.SetBlendShapeWeight(33, setMood-100);
        }
        if (updateMood && setMood <= 100)
        {
            skinnedMeshRenderer.SetBlendShapeWeight(36, 100 - setMood);
            skinnedMeshRenderer.SetBlendShapeWeight(12, 100 - setMood);
            skinnedMeshRenderer.SetBlendShapeWeight(13, 100 - setMood);
        }

        if (setBadMood)
        {
            mouseFrown = skinnedMeshRenderer.GetBlendShapeWeight(36);
            mouseSmile = skinnedMeshRenderer.GetBlendShapeWeight(33);
            browDropL = skinnedMeshRenderer.GetBlendShapeWeight(12);
            browDropR = skinnedMeshRenderer.GetBlendShapeWeight(13);

            if (mouseFrown < 100)
            {
                skinnedMeshRenderer.SetBlendShapeWeight(36, mouseFrown + speedChangeMood * Time.deltaTime);
                setMood = 100 - (int)Math.Round(mouseFrown);
            }

            if (browDropL < 100) skinnedMeshRenderer.SetBlendShapeWeight(12, browDropL + speedChangeMood * Time.deltaTime);
            if (browDropR < 100) skinnedMeshRenderer.SetBlendShapeWeight(13, browDropR + speedChangeMood * Time.deltaTime);
            if (mouseSmile > 0) skinnedMeshRenderer.SetBlendShapeWeight(33, mouseSmile - speedChangeMood * Time.deltaTime);
          
        }
        if (setBadMood && mouseFrown >= 100 && mouseSmile <= 0 && browDropL >= 100 && browDropR >= 100)
        {
            delta = 0;
            setBadMood = false;
            setMood = 0;
            nextCountSetMood = setMood;
            if (interpolate) startInterpolateMod = true;
        }

        if (setGoodMood)
        {
            mouseFrown = skinnedMeshRenderer.GetBlendShapeWeight(36);
            mouseSmile = skinnedMeshRenderer.GetBlendShapeWeight(33);
            browDropL = skinnedMeshRenderer.GetBlendShapeWeight(12);
            browDropR = skinnedMeshRenderer.GetBlendShapeWeight(13);

            if (mouseFrown > 0) skinnedMeshRenderer.SetBlendShapeWeight(36, mouseFrown - speedChangeMood * Time.deltaTime);
            if (browDropL > 0) skinnedMeshRenderer.SetBlendShapeWeight(12, browDropL - speedChangeMood * Time.deltaTime);
            if (browDropR > 0) skinnedMeshRenderer.SetBlendShapeWeight(13, browDropR - speedChangeMood * Time.deltaTime);
            if (mouseSmile < 100) 
            { 
                skinnedMeshRenderer.SetBlendShapeWeight(33, mouseSmile + speedChangeMood * Time.deltaTime);
                setMood = (int)Math.Round(mouseSmile) +100;
            }
        }
        if (setGoodMood && mouseFrown <= 0 && mouseSmile >= 100 && browDropL <= 0 && browDropR <= 0)
        {
            delta = 0;
            setGoodMood = false;
            setMood = 200;
            nextCountSetMood = setMood;
            if (interpolate) startInterpolateMod = true;
        }

        if (startInterpolateMod)
        {
            mouseFrown = skinnedMeshRenderer.GetBlendShapeWeight(36);
            mouseSmile = skinnedMeshRenderer.GetBlendShapeWeight(33);
            browDropL = skinnedMeshRenderer.GetBlendShapeWeight(12);
            browDropR = skinnedMeshRenderer.GetBlendShapeWeight(13);
            if (mouseFrown > indexInerpolate) skinnedMeshRenderer.SetBlendShapeWeight(36, mouseFrown - speedChangeMood * Time.deltaTime);
            if (browDropL >  indexInerpolate) skinnedMeshRenderer.SetBlendShapeWeight(12, browDropL - speedChangeMood * Time.deltaTime);
            if (browDropR > indexInerpolate) skinnedMeshRenderer.SetBlendShapeWeight(13, browDropR - speedChangeMood * Time.deltaTime);
            if (mouseSmile > indexInerpolate) skinnedMeshRenderer.SetBlendShapeWeight(33, mouseSmile - speedChangeMood * Time.deltaTime);
        }
        if (startInterpolateMod && mouseFrown <= indexInerpolate && mouseSmile <= indexInerpolate && browDropL <= indexInerpolate && browDropR <= indexInerpolate)
        {
            delta = 0;
            startInterpolateMod = false;
        }


        if (setNeutralMood)
        {
            mouseFrown = skinnedMeshRenderer.GetBlendShapeWeight(36);
            mouseSmile = skinnedMeshRenderer.GetBlendShapeWeight(33);
            browDropL = skinnedMeshRenderer.GetBlendShapeWeight(12);
            browDropR = skinnedMeshRenderer.GetBlendShapeWeight(13);

            if (mouseFrown > 0) skinnedMeshRenderer.SetBlendShapeWeight(36, mouseFrown - speedChangeMood * Time.deltaTime);
            if (browDropL > 0) skinnedMeshRenderer.SetBlendShapeWeight(12, browDropL - speedChangeMood * Time.deltaTime);
            if (browDropR > 0) skinnedMeshRenderer.SetBlendShapeWeight(13, browDropR - speedChangeMood * Time.deltaTime);
            if (mouseSmile > 0) skinnedMeshRenderer.SetBlendShapeWeight(33, mouseSmile - speedChangeMood * Time.deltaTime);

            setMood = 100;
            nextCountSetMood = setMood;

        }
        if (setNeutralMood && mouseFrown <= 0 && mouseSmile <= 0 && browDropL <= 0 && browDropR <= 0)
        {
            delta = 0;
            setNeutralMood = false;
        }
    }
}
