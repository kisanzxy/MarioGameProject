using MahJong.GameState;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;

namespace MahJong.Controllers
{
    internal class KeyboardController : Controller
    {
        private KeyboardState previousKeyboardState;
        public KeyboardController(MarioGame game)
            : base(game)
        {
            previousKeyboardState = Keyboard.GetState();
        }

        public override void UpdateInput()
        {
            // Get the current Keyboard state.
            KeyboardState currentState = Keyboard.GetState();

            if (currentState.IsKeyDown(Keys.P) && !previousKeyboardState.IsKeyDown(Keys.P))
            {
                PauseCommands.Execute();
                Debug.WriteLine("Pause!");
            }

            if (!(game.CurrgameState is PauseCommand))
            {
                Keys[] keysPressed = currentState.GetPressedKeys();
                foreach (Keys key in keysPressed)
                {
                    if (!previousKeyboardState.IsKeyDown(key))
                    {
                        RunCommand((int)key, KeyBehavior.press);
                    }
                    else
                    {
                        RunCommand((int)key, KeyBehavior.hold);
                    }
                }
                foreach (Keys key in previousKeyboardState.GetPressedKeys())
                {
                    if (!currentState.IsKeyDown(key))
                    {
                        RunCommand((int)key, KeyBehavior.release);
                    }
                }
            }            

            // Update previous Keyboard state.
            previousKeyboardState = currentState;
        }
    }
}
