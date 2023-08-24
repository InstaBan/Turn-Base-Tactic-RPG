using System;
using UnityEngine;

namespace LuminaStudio.Unit
{
    public class UnitSelectedVisual : MonoBehaviour
    {
        [SerializeField] 
        private Unit _unit;
        [SerializeField]
        private MeshRenderer m_Renderer;

        private void Start()
        {
            UnitActionSystem.Instance.OnSelectedUnitChanged += OnselectedUnitChanged;
            UpdateVisual();
        }

        private void OnselectedUnitChanged(object sender, EventArgs empty)
        {
            UpdateVisual();
        }

        private void UpdateVisual()
        {
            m_Renderer.enabled = _unit == UnitActionSystem.Instance.GetSelectedUnit();
        }

        private void OnDestroy()
        {
            UnitActionSystem.Instance.OnSelectedUnitChanged -= OnselectedUnitChanged;
        }
    }
}
