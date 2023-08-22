using System;
using UnityEngine;
using LuminaStudio.Calculation.Logic;
using LuminaStudio.Combat.Turn;
using LuminaStudio.Core.Input;
using LuminaStudio.Grid;
using LuminaStudio.Unit.Actions;

namespace LuminaStudio.Unit
{
    public class Unit : MonoBehaviour
    {
        #region Attributes

        private const int ACTION_POINTS_MAX = 2;
        [SerializeField]
        private Animator _animator;
        [SerializeField] 
        private Transform _root_body;
        [SerializeField] 
        private int _faction; // replace with actual faction later
        //private GridPosition _gridPosition;
        private int _actionPoints = ACTION_POINTS_MAX;
        #endregion

        #region Events

        public static event EventHandler OnAnyActionPointsChanged;

        #endregion

        #region Actions
        private MovementAction _movementAction;
        private TestAction _testAction;
        private BaseAction[] _actionsArray;
        #endregion

        private void Awake() // replace with OnstartClient when networked
        {
            //base.OnStartClient();
            // replace value with values readed from scriptable objects later
            _movementAction = GetComponent<MovementAction>();
            _testAction = GetComponent<TestAction>();
            _actionsArray = GetComponents<BaseAction>();
        }

        private void Start()
        {
            //_gridPosition = GridLevel.Instance.GetGridPosition(transform.position);
            //GridLevel.Instance.AddUnitAtGridPosition(_gridPosition, this);

            // Listen to Events
            TurnSystem.Instance.OnEndTurn += OnEndTurn;
        }
        private void Update()
        {
            //GridPosition newPos = GridLevel.Instance.GetGridPosition(transform.position);
            //if (newPos != _gridPosition)
            //{
            //    GridLevel.Instance.UpdateUnitAtGridPosition(this, _gridPosition, newPos);
            //    _gridPosition = newPos;
            //}
        }

        public Vector3 GetWorldPosition()
        {
            return transform.position;
        }

        public Vector3 GetWorldPositionBody()
        {
            return _root_body.position;
        }

        public Animator GetAnimator()
        {
            return _animator;
        }

        //public GridPosition GetGridPosition()
        //{
        //    return _gridPosition;
        //}

        public BaseAction[] GetActionArray()
        {
            return this._actionsArray;
        }

        public bool TryExecuteAction(BaseAction action)
        {
            if (CanTakeAction(action))
            {
                SpendActionPoints(action.GetActionResourceCost());
                return true;
            }
            return false;
        }

        // TEST ONLY : REQUIRE UPDATE
        public bool CanTakeAction(BaseAction action)
        {
            return _actionPoints >= action.GetActionResourceCost();
        }

        public int GetActionPoints()
        {
            return _actionPoints;
        }

        private void SpendActionPoints(int amount)
        {
            _actionPoints -= amount;
            OnAnyActionPointsChanged?.Invoke(this, EventArgs.Empty);
        }
        public bool IsPlayerFaction()
        {
            return _faction == 0;
        }

        public void OnDamage()
        {
            Debug.Log(this.name + " Damaged!" );
        }

        #region Turn

        private void OnEndTurn(object sender, EventArgs empty)
        {
            _actionPoints = ACTION_POINTS_MAX;
            OnAnyActionPointsChanged?.Invoke(this, EventArgs.Empty);
        }

        #endregion
    }
}