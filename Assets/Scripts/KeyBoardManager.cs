using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyBoardManager : MonoBehaviour
{
    [SerializeField] InputField input;


    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void InputText(InputField inputField) 
    {
        input = inputField;
    
    }
    public void Key(Text key) 
    {
        if (input!=null)
        {
            input.text +=key.text;
        }
    }

    public void DelKey() 
    {
        int x1 = input.text.Length - 1;
        input.text = input.text.Remove(x1);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
