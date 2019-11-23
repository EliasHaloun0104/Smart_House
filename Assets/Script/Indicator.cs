using UnityEngine;
using UnityEngine.UI;

namespace Script
{
    public class Indicator : MonoBehaviour
    {
        [SerializeField] private GameObject hor;
        [SerializeField] private GameObject ver;
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            transform.position = new Vector3(hor.transform.position.x, ver.transform.position.y,0);
        }
    }
}
