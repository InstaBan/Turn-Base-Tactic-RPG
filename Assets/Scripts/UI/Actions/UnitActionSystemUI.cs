using System;
using System.Collections.Generic;
using UnityEngine;
using LuminaStudio.Unit;
using LuminaStudio.Unit.Actions;

namespace LuminaStudio.UI.Actions
{
    public class UnitActionSystemUI : MonoBehaviour
    {
        [SerializeField] 
        private Transform m_actionButtonPrefab;
        [SerializeField]
        private Transform m_actionButtonContainerTransform;

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
    }
}
