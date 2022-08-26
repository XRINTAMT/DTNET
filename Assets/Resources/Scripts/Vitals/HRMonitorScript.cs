using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HRMonitorScript : MonoBehaviour
{
    [SerializeField] ComputeShader computeShader;
    ComputeShader CS;
    RenderTexture RT;
    [SerializeField] Color graphColor;
    [SerializeField] int numberOfValues;
    [SerializeField] float[] values;
    public float CirculationTime;
    float x;
    int iterator;

    void Start()
    {
        x = 0;
        iterator = 0;
        
        /*
        for(int i = 0; i<values.Length; i++)
        {
            values[i] = Mathf.Sin(x);
            x += 0.07f;
        }
        */
        
        float[] colorComponents = new float[4];
        colorComponents[0] = graphColor.r;
        colorComponents[1] = graphColor.g;
        colorComponents[2] = graphColor.b;
        colorComponents[3] = graphColor.a;
        RT = new RenderTexture(185, 40, 0);
        RT.enableRandomWrite = true;
        RT.Create();
        CS = Instantiate(computeShader);
        CS.SetTexture(0, "Result", RT);
        CS.SetFloats("Color", colorComponents);
        CS.SetFloats("Values",values);
        CS.Dispatch(0, RT.width / 8, RT.height / 8, 1);
        GetComponent<Renderer>().material.mainTexture = RT;
        StartCoroutine(Circulate());
    }

    IEnumerator Circulate()
    {
        for (float i = 0; i < (1 / CirculationTime); i += Time.deltaTime)
        {
            yield return 0;
        }
        values[iterator] = Mathf.Sin(x);
        x += 0.06f;
        iterator += 4;
        //iterator = iterator % (numberOfValues*4+1);
        if(iterator > numberOfValues * 4)
        {
            iterator = 0;
        }
        //iterator = iterator % (numberOfValues * 4 + 1);
        values[iterator] = -1;
        //CS.SetTexture(0, "Result", RT);
        CS.SetFloats("Values", values);
        CS.Dispatch(0, RT.width / 8, RT.height / 8, 1);
        StartCoroutine(Circulate());
    }

    void Update()
    {
        
    }
}
