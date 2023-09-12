using System;
using LuminaStudio.Core.Scene.Combat;
using LuminaStudio.Unit;
using UnityEngine;

namespace LuminaStudio.UI.Indicators
{
    /// <summary>
    /// Global System
    /// Generate visual indicators when needed
    /// </summary>
    public class IndicatorSystem : MonoBehaviour
    {
        public static IndicatorSystem Instance { get; private set; }

        #region Colors

        [Header("Color")]
        [SerializeField]
        private Color m_PlayerFactionColor;
        [SerializeField]
        private Color m_FriendlyFactionColor;
        [SerializeField]
        private Color m_NeutralFactionColor;
        [SerializeField]
        private Color m_HostileFactionColor;

        #endregion

        #region Indicator Prefabs
        [Header("Unit")]
        [SerializeField]
        private GameObject m_UnitSelectionIndicator;

        #endregion

        #region Instance Records

        private GameObject selectRing;

        #endregion

        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogError("Error: Duplicate IndicatorSystem Found in: " + transform + " / " + Instance);
                Destroy(gameObject);
                return;
            }
            Instance = this;
        }

        private void Start()
        {
            UnitActionSystem.Instance.OnSelectedUnitChanged += OnselectedUnitChanged;
        }

        private void OnselectedUnitChanged(object sender, EventArgs empty)
        {
            UpdateVisual();
        }

        private void UpdateVisual()
        {
            Destroy(selectRing);
            var unit = UnitActionSystem.Instance.GetSelectedUnit();
            selectRing = Instantiate(m_UnitSelectionIndicator);
            selectRing.transform.SetParent(unit.transform, false);
            var material = selectRing.GetComponent<MeshRenderer>().material;

            // Currently only player faction, will expand
            if (unit.IsPlayerFaction())
            {
                material.color = m_PlayerFactionColor;
            }
            else
            {
                material.color = m_NeutralFactionColor;
            }
        }
    }
}
