using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

namespace Script
{
    public class LampManager : MonoBehaviour
    {
        private SpriteRenderer _spriteRenderer;
        [SerializeField] private Sprite lampOn;
        [SerializeField] private Sprite lampOff;
        private bool _lampStatus;
        
        // Start is called before the first frame update
        void Start()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            //On Initialize
            GetLampStatus();
        
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKey(KeyCode.O))
            {
                TurnOn();
            }

            if (Input.GetKey(KeyCode.F))
            {
                TurnOff();
            }
        }

        void TurnOn()
        {
            _spriteRenderer.sprite = lampOn;
        }
        void TurnOff()
        {
            _spriteRenderer.sprite = lampOff;
        }

        //TODO Connect to the server and Get the status
        //Return true if on
        bool GetLampStatus()
        {
            return true;
        }
        
        //TODO Connect to the server and Update the status
        void UpdateLampStatus()
        {
            
        }


        public void ReverseStatus()
        {
            if (GetLampStatus())
            {
                TurnOff();
            }
            else
            {
                TurnOn();
            }
        }
    }
}
