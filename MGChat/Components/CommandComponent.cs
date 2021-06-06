using System.Collections.Generic;
using System.Linq;
using MGChat.Commands;

namespace MGChat.Components
{
    public class CommandComponent : ECS.Component
    {
        public List<Command> Commands;
        // For Undoing
        private Stack<Command> _pastCommands;
        
        public CommandComponent(int parent) : base(parent)
        {
            Commands = new List<Command>();
            _pastCommands = new Stack<Command>();
        }

        public void AddCommand(Command command)
        {
            Commands.Add(command);
        }

        public T GetCommand<T>() where T: Command
        {
            var command = Commands.OfType<T>().FirstOrDefault();
            _pastCommands.Push(command);
            Commands.Remove(command);
            return command;
        }
    }
}