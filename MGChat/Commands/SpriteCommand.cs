namespace MGChat.Commands
{
    public class SpriteCommand : Command
    {
        public string Direction;
        public string State;

        public SpriteCommand(string direction="", string state="")
        {
            Direction = direction;
            State = state;
        }
    }
}