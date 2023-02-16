using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QuestionSystem
{
    public class InfoPlatesManager : MonoBehaviour
    {
        Coroutine rotation;

        public void Rotate()
        {
            if (rotation != null)
            {
                return;
            }
            rotation = StartCoroutine(IERotationAnimation()); 
        }

        IEnumerator IERotationAnimation()
        {
            float startAngle = transform.localRotation.eulerAngles.y;
            float endAngle = startAngle + 180;
            for (float i = 0; i < 1; i += Time.deltaTime*2)
            {
                transform.localRotation = Quaternion.Euler(0,Mathf.LerpAngle(startAngle, endAngle, i),0);
                yield return 0;
            }
            transform.localRotation = Quaternion.Euler(0, endAngle, 0);
            rotation = null;
        }

        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
