using UnityEngine;

namespace Script
{
    
    public class Cursor : MonoBehaviour
    {
        [SerializeField] private GameObject handle;
        [SerializeField] private float yPos;
     
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            var pos = handle.transform.position;
            transform.position = Vector3.MoveTowards(transform.position,new Vector3(pos.x,yPos,0), Time.deltaTime);
        }
        
        
        
    }
}
