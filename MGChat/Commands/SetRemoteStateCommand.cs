namespace MGChat.Commands
{
    public class SetRemoteStateCommand : RemoteCommand
    {
        public string State;

        public SetRemoteStateCommand(string state, string netId) : base(netId)
        {
            State = state;
        }
    }
}