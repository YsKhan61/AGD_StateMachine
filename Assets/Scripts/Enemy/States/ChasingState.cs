using ClassroomIGI.Main;
using ClassroomIGI.Player;
using ClassroomIGI.StateMachine;


namespace ClassroomIGI.Enemy
{
    /// <summary>
    /// Chase state of the ChasingStateOwner
    /// It will chase the target until the target is not in view or the target is reached
    /// It will check if the target is in view and change the state to the next state
    /// </summary>
    public class ChasingState : IState
    {
        private IChasingStateOwner owner;
        private PlayerController target;

        public ChasingState(IChasingStateOwner owner) => this.owner = owner;

        public void OnStateEnter()
        {
            SetTarget();
            SetStoppingDistance();
        }

        public void Update()
        {
            if (!owner.IsTargetInView())
            {
                owner.OnTargetNotInView();
                return;
            }

            MoveTowardsTarget();
            if (ReachedTarget())
            {
                ResetPath();
                owner.OnChasingStateComplete();
            }
        }

        public void OnStateExit() => target = null;

        private void SetTarget() => target = GameService.Instance.PlayerService.GetPlayer();

        private void SetStoppingDistance() => owner.Agent.stoppingDistance = owner.PlayerStoppingDistance;

        private bool MoveTowardsTarget() => owner.Agent.SetDestination(target.Position);

        private bool ReachedTarget() => owner.Agent.remainingDistance <= owner.Agent.stoppingDistance;

        private void ResetPath()
        {
            owner.Agent.isStopped = true;
            owner.Agent.ResetPath();
        }
    }
}