using System.Collections;
using TMPro;
using UnityEngine;

namespace Script
{
    public class Instruction : MonoBehaviour
    {
        [SerializeField] private AudioClip _audioSource;
        private TextMeshProUGUI _text;
        private string[] _news;
        // Start is called before the first frame update
        void Start()
        {
            //AudioSource.PlayClipAtPoint(_audioSource, new Vector3());
            _text = GetComponent<TextMeshProUGUI>();
            _news = new[]
            {
                "Attention level increases when you focus on a single thought or an external object.",
                "Double blink move the indicator along y axis.",
                "Try to blink as hard as you can in order to differentiate from your normal blink.",
                "blink once to change status of device."
            };
            StartCoroutine(ChangeNews());

        }

        private IEnumerator ChangeNews()
        {
            var i = 0;
            while (true)
            {
                _text.SetText(_news[i++]);
                if (i == _news.Length) i = 0;
                yield return new WaitForSeconds(4);
            }
            // ReSharper disable once IteratorNeverReturns
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
