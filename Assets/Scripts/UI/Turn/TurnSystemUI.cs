using System;
using LuminaStudio.Core.Scene.Combat;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LuminaStudio.UI.Turn
{
    public class TurnSystemUI : MonoBehaviour
    {
        [SerializeField]
        private Button m_endTurnButton;
        [SerializeField]
        private TextMeshProUGUI m_turnNumberText;

        private void Start()
        {
            m_endTurnButton.onClick.AddListener(() =>
            {
                TurnSystem.Instance.NextTurn();
            });

            TurnSystem.Instance.OnEndTurn += OnEndTurn;
        }

        private void OnEndTurn(object sender, EventArgs empty)
        {
            UpdateTurnText();
        }
        public void UpdateTurnText()
        {
            m_turnNumberText.text = "TURN " + TurnSystem.Instance.GetTurnNumber();
        }
    }
}
