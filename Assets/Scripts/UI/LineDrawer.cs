using System.Collections;
using System.Collections.Generic;
using LuminaStudio.Core.Input;
using LuminaStudio.Unit;
using UnityEngine;

namespace LuminaStudio.UI
{
    public class LineDrawer : MonoBehaviour
    {
        private LineRenderer lineRenderer;
        private bool isActive;

        // Start is called before the first frame update
        void Start()
        {
            lineRenderer = GetComponent<LineRenderer>();
            lineRenderer.enabled = false;
        }

        // Update is called once per frame
        void Update()
        {
            var unit = GameObject.Find("UnitController").GetComponent<UnitActionSystem>().GetSelectedUnit();

            if(unit != null){
                lineRenderer.enabled = true;

                Vector3 unitPosition = unit.gameObject.transform.position;
                Vector3 mousePositition = InputManager.GetMousePosition();

                float distance = Mathf.Clamp(Vector3.Distance(unitPosition, mousePositition), 0, unit.GetMoveAction().GetMovementRange());

                lineRenderer.SetPosition(0, unitPosition);
                lineRenderer.SetPosition(1, unitPosition + (mousePositition - unitPosition).normalized * distance);
            } else {
                lineRenderer.enabled = false;
            }
        }

        
    }
}
