using UnityEngine;

namespace Script
{
    public class LampManager : MonoBehaviour
    {
        private SpriteRenderer _spriteRenderer;
        [SerializeField] private Sprite lampOn;
        [SerializeField] private Sprite lampOff;
        private bool _isOn;
        private bool _lampStatus;
        
        // Start is called before the first frame update
        void Start()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            //On Initialize
            if (GetLampStatus()) TurnOn();
            else TurnOff();
        
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
            _isOn = true;
        }
        void TurnOff()
        {
            _spriteRenderer.sprite = lampOff;
            _isOn = false;
        }

        public bool IsOn
        {
            get => _isOn;
            set => _isOn = value;
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
            if (_isOn)
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
