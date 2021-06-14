namespace MGChat.Commands
{
    public class CollisionCommand : Command
    {
        public int OtherEntity;

        public CollisionCommand(int otherEntity)
        {
            OtherEntity = otherEntity;
        }
    }
}