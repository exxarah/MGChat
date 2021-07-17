namespace MGChat.Commands
{
    public class SetRemoteDirectionCommand : RemoteCommand
    {
        public string Direction;

        public SetRemoteDirectionCommand(string direction, string netId) : base(netId)
        {
            Direction = direction;
        }
    }
}