using System;
using UnityEngine;

namespace LuminaStudio.Unit
{
    public class UnitSelectedVisual : MonoBehaviour
    {
        [SerializeField] 
        private UnitBase _unit;
        [SerializeField]
        private MeshRenderer m_Renderer;

        private void Start()
        {
            UnitAction.Instance.OnSelectedUnitChanged += OnselectedUnitChanged;
            UpdateVisual();
        }

        private void OnselectedUnitChanged(object sender, EventArgs empty)
        {
            UpdateVisual();
        }

        private void UpdateVisual()
        {
            m_Renderer.enabled = _unit == UnitAction.GetSelectedUnit();
        }
    }
}
