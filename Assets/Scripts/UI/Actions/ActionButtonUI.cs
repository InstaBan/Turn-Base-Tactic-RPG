using LuminaStudio.Unit;
using LuminaStudio.Unit.Actions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LuminaStudio.UI.Actions
{
    public class ActionButtonUI : MonoBehaviour
    {
        [SerializeField] 
        private TextMeshProUGUI m_textMeshPro;

        [SerializeField] 
        private GameObject m_selectedVisual;

        [SerializeField] 
        private Button _button;

        private BaseAction _baseAction;
        public void SetAction(BaseAction action)
        {
            _baseAction = action;
            m_textMeshPro.text = action.GetActionName().ToUpper();
            _button.onClick.AddListener(() =>
            {
                UnitActionSystem.Instance.SetSelectedAction(action);
            });
        }

        internal void UpdateSelectedVisual()
        {
            m_selectedVisual.SetActive(UnitActionSystem.Instance.IsSelectedAction(_baseAction));
        }
    }
}
