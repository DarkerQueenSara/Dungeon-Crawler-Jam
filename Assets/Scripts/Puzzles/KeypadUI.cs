using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Puzzles
{
    public class KeypadUI : MonoBehaviour
    {
        public Keypad keypad;
        
        public List<Button> numberButtons;
        public Button closeButton;
        
        public int[] solution = new int[4];

        private Queue<int> _lastFourInputs;

        private void Start()
        {
            _lastFourInputs = new Queue<int>();
            for (int i = 0; i < numberButtons.Count; i++)
            {
                numberButtons[i].onClick.AddListener(() => AddNumber(i + 1));
            }
            closeButton.onClick.AddListener(CloseWindow);
        }

        private void AddNumber(int number)
        {
            _lastFourInputs.Enqueue(number);
            if (_lastFourInputs.Count > 4)
            {
                _lastFourInputs.Dequeue();
            }

            if (_lastFourInputs.ToArray() == solution)
            {
                keypad.EndPuzzle();
            }
        }

        private void CloseWindow()
        {
            gameObject.SetActive(false);
        }
    }
}