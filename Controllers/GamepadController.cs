using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MahJong.Controllers
{
    internal class GamepadController : Controller
    {
        GamePadState previousGamePadState,
                     emptyInput;

        public GamepadController(MarioGame game)
            : base(game)
        {
            previousGamePadState = GamePad.GetState(PlayerIndex.One);
            emptyInput = new GamePadState(Vector2.Zero, Vector2.Zero, 0, 0, new Buttons());
        }

        public override void UpdateInput()
        {
            // Get the current gamepad state.
            GamePadState currentState = GamePad.GetState(PlayerIndex.One);

            // Process input only if connected.
            if (currentState.IsConnected)
            {
                if (currentState != emptyInput) // Button Pressed
                {

                    var buttonList = (Buttons[])Enum.GetValues(typeof(Buttons));

                    foreach (var button in buttonList)
                    {
                        if (currentState.IsButtonDown(button) && !previousGamePadState.IsButtonDown(button))
                        {
                            RunCommand((int)button, KeyBehavior.press);
                        }
                        else if (!currentState.IsButtonDown(button) && previousGamePadState.IsButtonDown(button))
                        {
                            RunCommand((int)button, KeyBehavior.release);
                        }
                        else if (currentState.IsButtonDown(button) && previousGamePadState.IsButtonDown(button))
                        {
                            RunCommand((int)button, KeyBehavior.hold);
                        }
                    }
                }

                // Update previous gamepad state.
                previousGamePadState = currentState;
            }
        }
    }
}
