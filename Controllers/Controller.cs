using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MahJong.Controllers
{
    internal enum KeyBehavior
    {
        press,
        release,
        hold
    }
    internal interface IController
    {
        void AddCommand(int key, ICommand value, KeyBehavior behavior= KeyBehavior.press);

        void UpdateInput();
        void ClearCommands();
    }

    internal abstract class Controller : IController
    {
        private Dictionary<KeyBehavior, Dictionary<int, ICommand>> commands;
        protected MarioGame game;
        protected ICommand PauseCommands;

        protected Controller(MarioGame game)
        {
            commands = new Dictionary<KeyBehavior, Dictionary<int, ICommand>>
            {
                {
                    KeyBehavior.press, new Dictionary<int, ICommand>()
                },
                                {
                    KeyBehavior.release, new Dictionary<int, ICommand>()
                },
                {
                    KeyBehavior.hold, new Dictionary<int, ICommand>()
                }
            };
            this.game = game;
            PauseCommands = new PauseCommand(game);
        }
        public void AddCommand(int key, ICommand value,KeyBehavior behavior)
        {
            commands[behavior].Add(key, value);
        }

        public abstract void UpdateInput();

        protected void RunCommand(int key, KeyBehavior behavior)
        {
            if (commands[behavior].ContainsKey(key))
            {
                commands[behavior][key].Execute();
            }
        }

        public void ClearCommands()
        {
            foreach (Dictionary<int, ICommand> dict in commands.Values)
            {
                dict.Clear();
            }
        }
    }
}
