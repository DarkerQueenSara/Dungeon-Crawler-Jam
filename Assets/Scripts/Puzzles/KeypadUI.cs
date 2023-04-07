using System;
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
            numberButtons[0].onClick.AddListener(() => AddNumber(1));
            numberButtons[1].onClick.AddListener(() => AddNumber(2));
            numberButtons[2].onClick.AddListener(() => AddNumber(3));
            numberButtons[3].onClick.AddListener(() => AddNumber(4));
            numberButtons[4].onClick.AddListener(() => AddNumber(5));
            numberButtons[5].onClick.AddListener(() => AddNumber(6));
            numberButtons[6].onClick.AddListener(() => AddNumber(7));
            numberButtons[7].onClick.AddListener(() => AddNumber(8));
            numberButtons[8].onClick.AddListener(() => AddNumber(9));

            closeButton.onClick.AddListener(CloseWindow);
        }

        private void AddNumber(int number)
        {
            _lastFourInputs.Enqueue(number);
            if (_lastFourInputs.Count > 4)
            {
                _lastFourInputs.Dequeue();
            }

            if (_lastFourInputs.Count != 4) return;
       
            int[] inputs = _lastFourInputs.ToArray();
            for (int i = 0; i < inputs.Length; i++)
            {
                if (inputs[i] != solution[i]) return;
            }
            keypad.EndPuzzle();
        }

        private void CloseWindow()
        {
            gameObject.SetActive(false);
        }
    }
}