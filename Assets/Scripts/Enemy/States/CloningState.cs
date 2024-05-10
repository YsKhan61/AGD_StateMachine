using ClassroomIGI.Main;
using ClassroomIGI.StateMachine;

namespace ClassroomIGI.Enemy
{
    public class CloningState : IState
    {
        private ICloningStateOwner owner;

        public CloningState(ICloningStateOwner owner) => this.owner = owner;

        public void OnStateEnter()
        {
            CreateAClone();
            CreateAClone();
        }

        public void Update() { }

        public void OnStateExit() { }

        private void CreateAClone()
        {
            RobotController clonedRobot = GameService.Instance.EnemyService.CreateEnemy(owner.Data) as RobotController;
            clonedRobot.SetCloneCount((owner as RobotController).CloneCountLeft - 1);
            clonedRobot.Teleport();
            clonedRobot.SetDefaultColor(EnemyColorType.Clone);
            clonedRobot.ChangeColor(EnemyColorType.Clone);
            GameService.Instance.EnemyService.AddEnemy(clonedRobot);
        }
    }
}