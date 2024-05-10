using ClassroomIGI.StateMachine;
using UnityEngine;


namespace ClassroomIGI.Enemy
{
    /// <summary>
    /// Rotating state of the RotatingStateOwner
    /// It will rotate the owner to a target rotation
    /// It will check if the target is in view and change the state to the next state
    /// </summary>
    public class RotatingState : IState
    {
        private IRotatingStateOwner owner;
        private float targetRotation;

        public RotatingState(IRotatingStateOwner owner) => this.owner = owner;

        public void OnStateEnter() => targetRotation = (owner.Rotation.eulerAngles.y + 180) % 360;

        public void Update()
        {
            if (owner.IsTargetInView())
            {
                owner.OnTargetInView();
                return;
            }

            owner.SetRotation(CalculateRotation());
            if (IsRotationComplete())
                owner.OnRotatingStateComplete();
        }

        public void OnStateExit() => targetRotation = 0;

        private Vector3 CalculateRotation() => Vector3.up * Mathf.MoveTowardsAngle(owner.Rotation.eulerAngles.y, targetRotation, owner.RotationSpeed * Time.deltaTime);

        private bool IsRotationComplete() => Mathf.Abs(Mathf.Abs(owner.Rotation.eulerAngles.y) - Mathf.Abs(targetRotation)) < owner.RotationThreshold;
    }
}