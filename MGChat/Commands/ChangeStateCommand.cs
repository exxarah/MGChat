namespace MGChat.Commands
{
    public class ChangeStateCommand : Command
    {
        public string State;

        public ChangeStateCommand(string state="")
        {
            State = state;
        }
    }
}