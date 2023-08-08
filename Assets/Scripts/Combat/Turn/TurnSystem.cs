using System;
using UnityEngine;

namespace LuminaStudio.Combat.Turn
{
    public class TurnSystem : MonoBehaviour
    {
        public static TurnSystem Instance { get; private set; }

        public event EventHandler OnEndTurn;
        private int _turnNumber;

        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogError("Error: Duplicate TurnSystem Found in: " + transform + " / " + Instance);
                Destroy(gameObject);
                return;
            }
            Instance = this;
        }

        public void NextTurn()
        {
            _turnNumber++;
            OnEndTurn?.Invoke(this, EventArgs.Empty);
        }

        public int GetTurnNumber()
        {
            return _turnNumber;
        }
    }
}
