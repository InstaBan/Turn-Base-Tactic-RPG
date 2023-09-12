using System;
using System.Linq;
using LuminaStudio.Unit.Actions;
using LuminaStudio.VFX.Abilities.Projectiles;
using UnityEngine;

namespace LuminaStudio.Unit
{
    public class UnitAnimatorController : MonoBehaviour
    {
        [SerializeField] 
        private Animator m_animator;
        [SerializeField]
        // PLEASE LINK THIS TO WEAPONS LATER
        private Transform m_projectileStartingPoint;
        [SerializeField]
        private Transform m_arrowProjectilePrefab;
        private MovementAction _movementAction;
        private ShootAction _shootAction;

        private void Awake()
        {
            if (!TryGetComponent<UnitManagerAction>(out var unitManagerAction)) return;
            _movementAction = (MovementAction)unitManagerAction._actions.ElementAt(0);
            _movementAction.OnstartMoving += OnstartMoving;
            _movementAction.OnstopMoving += OnstopMoving;
            _shootAction = (ShootAction)unitManagerAction._actions.ElementAt(1);
            _shootAction.OnstartShoot += OnstartShoot;
        }

        #region Movement

        private void OnstartMoving(object sender, EventArgs args)
        {
            m_animator.SetBool("IsMoving", true);
        }
        private void OnstopMoving(object sender, EventArgs args)
        {
            m_animator.SetBool("IsMoving", false);
        }

        #endregion

        #region Shoot

        private void OnstartShoot(object sender, ShootAction.OnShootEventArgs args)
        {
            m_animator.SetTrigger("Shoot");

            // Quaterion.identity = no rotation
            var arrow = Instantiate(m_arrowProjectilePrefab, m_projectileStartingPoint.position, Quaternion.identity);
            var arrowProjectile = arrow.GetComponent<ArrowProjectile>();

            // pass target unit's position into projectile
            arrowProjectile.Setup(args.targetUnit.GetWorldPositionBody());
        }

        #endregion
    }
}
