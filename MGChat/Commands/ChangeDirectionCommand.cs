namespace MGChat.Commands
{
    public class ChangeDirectionCommand : Command
    {
        public string Direction;

        public ChangeDirectionCommand(string direction)
        {
            Direction = direction;
        }
    }
}