namespace ClassroomIGI.UI
{
    public interface IUIView
    {
        public void SetController(IUIController controllerToSet);
        public void EnableView();
        public void DisableView();
    }
}