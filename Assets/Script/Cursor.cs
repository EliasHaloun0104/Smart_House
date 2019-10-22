using System;
using UnityEngine;

namespace Script
{
    
    public class Cursor : MonoBehaviour
    {
        [SerializeField] private GameObject handle;
        [SerializeField] private float a = 0.0075f;
        [SerializeField] private float b = 0.2525f;
        [SerializeField] private float c = -4f;
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            var position = handle.transform.position;
            var x = position.x;
            var y = (float) (a * Math.Pow(x,2) + b * x + c);
            //y = Mathf.Clamp(y, -13, 13);
            transform.position = new Vector3(x,y,0);
        }
    }
}
