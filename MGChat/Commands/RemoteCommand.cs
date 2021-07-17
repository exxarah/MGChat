namespace MGChat.Commands
{
    public class RemoteCommand : Command
    {
        public string NetId;

        public RemoteCommand(string netId)
        {
            NetId = netId;
        }
    }
}