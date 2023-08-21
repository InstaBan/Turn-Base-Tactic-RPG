using System;
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

        private void Awake()
        {
            if (!TryGetComponent<MovementAction>(out var movementAction)) return;
            movementAction.OnstartMoving += OnstartMoving;
            movementAction.OnstopMoving += OnstopMoving;
            if (!TryGetComponent<ShootAction>(out var shootAction)) return;
            shootAction.OnstartShoot += OnstartShoot;
        }

        #region Movement

        private void OnstartMoving(object sender, EventArgs evt)
        {
            m_animator.SetBool("IsMoving", true);
        }
        private void OnstopMoving(object sender, EventArgs evt)
        {
            m_animator.SetBool("IsMoving", false);
        }

        #endregion

        #region Shoot

        private void OnstartShoot(object sender, ShootAction.OnShootEventArgs evt)
        {
            m_animator.SetTrigger("Shoot");

            // Quaterion.identity = no rotation
            var arrow = Instantiate(m_arrowProjectilePrefab, m_projectileStartingPoint.position, Quaternion.identity);
            var arrowProjectile = arrow.GetComponent<ArrowProjectile>();

            // pass target unit's position into projectile
            arrowProjectile.Setup(evt.targetUnit.GetWorldPositionBody());
        }

        #endregion
    }
}
