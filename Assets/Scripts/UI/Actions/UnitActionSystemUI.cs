using System;
using System.Collections.Generic;
using LuminaStudio.Combat.Turn;
using UnityEngine;
using LuminaStudio.Unit;
using LuminaStudio.Unit.Actions;
using TMPro;

namespace LuminaStudio.UI.Actions
{
    public class UnitActionSystemUI : MonoBehaviour
    {
        [SerializeField] 
        private Transform m_actionButtonPrefab;
        [SerializeField]
        private Transform m_actionButtonContainerTransform;
        [SerializeField]
        private TextMeshProUGUI m_actionPointsText;

        private List<ActionButtonUI> _actionButtonUIList;
        private Unit.Unit _selectedUnit;

        private void Awake()
        {
            _actionButtonUIList = new List<ActionButtonUI>();
        }
        private void Start()
        {
            UnitActionSystem.Instance.OnSelectedUnitChanged += OnSelectedUnitChanged;
            UnitActionSystem.Instance.OnSelectedActionChanged += OnSelectedActionChanged;
            UnitActionSystem.Instance.OnBusyChanged += OnBusyChanged;
            UnitActionSystem.Instance.OnActiontarted += OnActionStarted;
            TurnSystem.Instance.OnEndTurn += OnEndTurn;
            Unit.Unit.OnAnyActionPointsChanged += OnAnyActionPointsChanged;
        }
        private void CreateUnitActionButtons()
        {
            // Clean Up
            foreach (Transform buttonTransform in m_actionButtonContainerTransform)
            {
                Destroy(buttonTransform.gameObject);
            }
            _actionButtonUIList.Clear();

            // Instantiate
            _selectedUnit = UnitActionSystem.Instance.GetSelectedUnit();
            foreach (BaseAction baseAction in _selectedUnit.GetActionArray())
            {
             var actionButton = Instantiate(m_actionButtonPrefab, m_actionButtonContainerTransform);
             var actionButtonUI = actionButton.GetComponent<ActionButtonUI>();
             actionButtonUI.SetAction(baseAction);
             _actionButtonUIList.Add(actionButtonUI);
            }
        }

        private void OnSelectedUnitChanged(object sender, EventArgs evt)
        {
            CreateUnitActionButtons();
            UpdateActionPoints();
        }
        private void OnSelectedActionChanged(object sender, EventArgs evt)
        {
            UpdateSelectedVisual();
        }

        private void UpdateSelectedVisual()
        {
            foreach (ActionButtonUI buttonUI in _actionButtonUIList)
            {
                buttonUI.UpdateSelectedVisual();
            }
        }
        private void UpdateActionPoints()
        {
            m_actionPointsText.text = "Action Points: " + _selectedUnit.GetActionPoints();
        }

        private void OnBusyChanged(object sender, bool isBusy)
        {
            if (!isBusy && _selectedUnit != null)
            {
                this.gameObject.SetActive(true);
            }
            else
            {
                this.gameObject.SetActive(false);
            }
        }

        private void OnActionStarted(object sender, EventArgs empty)
        {
            UpdateActionPoints();
        }

        private void OnEndTurn(object sender, EventArgs empty)
        {
            UpdateActionPoints();
        }

        private void OnAnyActionPointsChanged(object sender, EventArgs empty)
        {
            UpdateActionPoints();
        }
    }
}
