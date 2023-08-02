using UnityEngine;
using TMPro;

namespace LuminaStudio.Grid
{
    public class GridDebugObject : MonoBehaviour
    {
        [SerializeField]
        private TextMeshPro _textMeshPro;
        private GridObject _gridObject;

        private void Update()
        {
            _textMeshPro.text = _gridObject.ToString();
        }
        public void SetGridObject(GridObject gridObject)
        {
            this._gridObject = gridObject;
        }
    }
}
