using System.Collections.Generic;
using System.Linq;
using MGChat.Commands;

namespace MGChat.Components
{
    public class CommandComponent : ECS.Component
    {
        private List<Command> _commands;
        // For Undoing
        //private Stack<Command> _pastCommands;
        
        public CommandComponent(int parent) : base(parent)
        {
            _commands = new List<Command>();
            //_pastCommands = new Stack<Command>();
        }

        public void AddCommand(Command command)
        {
            _commands.Add(command);
        }

        public T GetCommand<T>() where T: Command
        {
            var command = _commands.OfType<T>().FirstOrDefault();
            //_pastCommands.Push(command);
            _commands.Remove(command);
            return command;
        }

        public List<T> GetAllCommands<T>() where T : Command
        {
            var list = _commands.OfType<T>().ToList();
            foreach (var command in list)
            {
                //_pastCommands.Push(command);
                _commands.Remove(command);
            }

            return list;
        }
    }
}