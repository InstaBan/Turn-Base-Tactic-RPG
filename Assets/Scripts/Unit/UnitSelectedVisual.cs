using System;
using UnityEngine;

namespace LuminaStudio.Unit
{
    public class UnitSelectedVisual : MonoBehaviour
    {
        [SerializeField] 
        private UnitBase _unit;

        private MeshRenderer _renderer;

        private void Awake()
        {
            _renderer = this.transform.Find("SelectedRing").GetComponent<MeshRenderer>();
        }

        private void Start()
        {
            UnitSelection.Instance.OnSelectedUnitChanged += OnselectedUnitChanged;
            UpdateVisual();
        }

        private void OnselectedUnitChanged(object sender, EventArgs empty)
        {
            UpdateVisual();
        }

        private void UpdateVisual()
        {
            //_renderer.enabled = _unit == UnitSelection.GetSelectedUnit();
            if (_unit == UnitSelection.GetSelectedUnit())
            {
                _renderer.enabled = true;
            }
            else
            {
                _renderer.enabled = false;
            }
        }
    }
}
