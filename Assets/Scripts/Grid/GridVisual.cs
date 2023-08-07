using LuminaStudio.Unit;
using UnityEngine;

namespace LuminaStudio.Grid
{
    public class GridVisual : MonoBehaviour
    {
        [SerializeField]
        private MeshRenderer m_Renderer;

        public void ShowVisual()
        {
            m_Renderer.enabled = true;
        }

        public void HideVisual()
        {
            m_Renderer.enabled = false;
        }
    }
}
