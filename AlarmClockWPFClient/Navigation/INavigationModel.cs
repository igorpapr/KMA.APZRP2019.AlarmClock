namespace AlarmClockWPFClient.Navigation
{
    internal enum ViewType
    {
        SignIn,
        SignUp,
        Main
    }

    interface INavigationModel
    {
        void Navigate(ViewType viewType);
    }
}
