using System;
using MahJong.Controllers;
using MahJong.GameObject;
//using MahJong.GameObject.Obstacles;
using MahJong.GameObject.Mario;
using MahJong.GameObject.Sprites;
using MahJong.GameState;
using MahJong.Sound;

namespace MahJong
{
    internal class ExitCommand : BaseCommand<MarioGame>
    {
        public ExitCommand (MarioGame receiver)
            : base (receiver)
        {

        }

        public override void Execute()
        {
            receiver.Exit();
        }
    }

    internal class ResetLevelCommand : BaseCommand<MarioGame>
    {
        public ResetLevelCommand (MarioGame receiver)
            : base (receiver)
        {

        }

        public override void Execute()
        {
            receiver.ResetGame();
            //SoundManager.Instance.PlayMainThemeSound();
        }
    }

    internal class MoveMarioRightCommand : BaseCommand<Mario>
    {
        public MoveMarioRightCommand (Mario receiver)
            : base (receiver)
        {

        }
        public override void Execute()
        {
            //receiver.ChangeActionState("movingRight");
            receiver.FaceRight();
        }

    }
    internal class MoveMarioRightReleaseCommand : BaseCommand<Mario>
    {
        public MoveMarioRightReleaseCommand(Mario receiver)
            : base(receiver)
        {

        }
        public override void Execute()
        {
            //receiver.ChangeActionState("movingRight");
            receiver.FaceRightDiscontinued();
        }

    }


    internal class MoveMarioLeftCommand : BaseCommand<Mario>
    {
        public MoveMarioLeftCommand(Mario receiver)
            : base(receiver)
        {

        }
        public override void Execute()
        {
            receiver.FaceLeft();
        }

    }

    internal class MoveMarioLeftReleaseCommand : BaseCommand<Mario>
    {
        public MoveMarioLeftReleaseCommand(Mario receiver)
            : base(receiver)
        {

        }
        public override void Execute()
        {
            receiver.FaceLeftDiscontinued();
        }

    }

    internal class MarioJumpCommand : BaseCommand<Mario>
    {
        public MarioJumpCommand(Mario receiver)
            : base(receiver)
        {

        }
        public override void Execute()
        {
            receiver.Jump();
        }

    }
    internal class MarioJumpReleaseCommand : BaseCommand<Mario>
    {
        public MarioJumpReleaseCommand(Mario receiver)
            : base(receiver)
        {

        }
        public override void Execute()
        {
            receiver.Fall();
        }

    }


    internal class MarioCrouchCommand : BaseCommand<Mario>
    {
        public MarioCrouchCommand(Mario receiver)
            : base(receiver)
        {

        }
        public override void Execute()
        {
            receiver.Crouch();
        }

    }
    internal class MarioCrouchReleaseCommand : BaseCommand<Mario>
    {
        public MarioCrouchReleaseCommand(Mario receiver)
            : base(receiver)
        {

        }
        public override void Execute()
        {
            receiver.Idle();
        }

    }
    internal class SuperMarioCommand : BaseCommand<Mario>
    {
        public SuperMarioCommand(Mario receiver)
            : base(receiver)
        {

        }
        public override void Execute()
        {
            receiver.SuperState();
        }

    }

    internal class StarMarioCommand : BaseCommand<Mario>
    {
        public StarMarioCommand(Mario receiver)
            : base(receiver)
        {

        }
        public override void Execute()
        {
            receiver.StarState();
        }

    }

    internal class NormalMarioCommand : BaseCommand<Mario>
    {
        public NormalMarioCommand(Mario receiver)
            : base(receiver)
        {

        }
        public override void Execute()
        {
            receiver.NormalState();
        }

    }

    internal class FireMarioCommand : BaseCommand<Mario>
    {
        public FireMarioCommand(Mario receiver)
            : base(receiver)
        {

        }
        public override void Execute()
        {
            receiver.FairState();
        }

    }

    internal class DamageMarioCommand : BaseCommand<Mario>
    {
        public DamageMarioCommand(Mario receiver)
            : base(receiver)
        {

        }   
        
        public override void Execute()
        {
            receiver.GetDamaged();
        }

        
    }

    internal class VisualizeCollisionBoxCommand : BaseCommand<Scene>
    {
        public VisualizeCollisionBoxCommand(Scene receiver)
            : base(receiver)
        {

        }

        public override void Execute()
        {
            receiver.VisualizeCollisionBox = !receiver.VisualizeCollisionBox;
        }


    }

    /// <summary>
    /// Future Command
    ///     Dash/Throw Fireball [Spacebar or B Button]
    /// </summary>
    internal class ThrowFireballCommand : BaseCommand<Mario>
    {
        public ThrowFireballCommand(Mario receiver)
            : base(receiver)
        {

        }

        public override void Execute()
        {
            receiver.ThrowFireBall();
        }
    }

    /// <summary>
    /// Future Command
    ///     Pause [[Pp] or Start Button]
    /// </summary>
    internal class PauseCommand : BaseCommand<MarioGame>
    {
        public PauseCommand(MarioGame receiver)
            : base(receiver)
        {

        }

        public override void Execute()
        {
            if (receiver.CurrgameState is PlayGameState)
            {
                receiver.Pause();
                SoundManager.Instance.MuteAndUnmute();
            }

            else
            {
                receiver.UnPause();
                SoundManager.Instance.MuteAndUnmute();
            }
               
        }

    }

    internal class MuteCommand : BaseCommand<MarioGame>
    {
        public MuteCommand(MarioGame receiver)
            : base(receiver)
        {

        }

        public override void Execute()
        {
            receiver.MuteGame();

        }

    }

    internal class StartNormalLevelCommand : BaseCommand<MarioGame>
    {
        public StartNormalLevelCommand(MarioGame receiver)
            : base(receiver)
        {
            
        }

        public override void Execute()
        {
            receiver.StartNormalLevel();

        }

    }
    internal class StartRandomLevelCommand : BaseCommand<MarioGame>
    {
        public StartRandomLevelCommand(MarioGame receiver)
            : base(receiver)
        {

        }

        public override void Execute()
        {
            receiver.StartRandomLevel();

        }

    }

    internal class BacktoStartPageCommand : BaseCommand<MarioGame>
    {
        public BacktoStartPageCommand(MarioGame receiver)
            : base(receiver)
        {

        }

        public override void Execute()
        {
            receiver.BacktoStartPage();

        }

    }

    internal class StartChallengeModeCommand : BaseCommand<MarioGame>
    {
        public StartChallengeModeCommand(MarioGame receiver)
            : base(receiver)
        {

        }

        public override void Execute()
        {
            receiver.StartChallengeMode();

        }

    }

    internal class StartLastmapCommand : BaseCommand<MarioGame>
    {
        public StartLastmapCommand(MarioGame receiver)
            : base(receiver)
        {

        }

        public override void Execute()
        {
            receiver.StartLastLevel();
        }
    }

    internal class SaveMapCommand : BaseCommand<MarioGame>
    {
        public SaveMapCommand(MarioGame receiver)
            : base(receiver)
        {

        }

        public override void Execute()
        {
            receiver.SaveMap();
        }
    }

    //internal class HitQuestionBlockCommand : BaseCommand<QuestionCoinBlock>
    //{
    //    public HitQuestionBlockCommand(QuestionCoinBlock receiver)
    //        : base (receiver)
    //    {

    //    }

    //    public override void Execute()
    //    {
    //        // Index is 10

    //       receiver.Bounce();
    //    }

    //    public override void Stop()
    //    {

    //    }
    //}

    //internal class HitBrickBlockCommand : BaseCommand<BrickCoinBlock>
    //{
    //    public HitBrickBlockCommand(BrickCoinBlock receiver)
    //        : base (receiver)
    //    {

    //    }

    //    public override void Execute()
    //    {
    //       /* // Index is 9

    //        if ((Script.Scene1.Sprites[1] as MarioSprite).CurrMarioState.State == MarioSprite.States.Super ||
    //            (Script.Scene1.Sprites[1] as MarioSprite).CurrMarioState.State == MarioSprite.States.Fire)
    //        //if ((int) (script.Scene1.Sprites[1] as MarioSprite).CurrMarioState.State == 3 ||
    //            //(int) (script.Scene1.Sprites[1] as MarioSprite).CurrMarioState.State == 5)
    //        {
    //           receiver.Explode();
    //        }
    //        else
    //        {
    //           receiver.Bounce();
    //        }*/
    //    }

    //    public override void Stop()
    //    {

    //    }
    //}

    //internal class QuestionToUsedBlockCommand : BaseCommand<QuestionCoinBlock>
    //{
    //    public QuestionToUsedBlockCommand(QuestionCoinBlock receiver)
    //        : base(receiver)
    //    {

    //    }

    //    public override void Execute()
    //    {
    //        receiver.Bounce();
    //    }

    //    public override void Stop()
    //    {

    //    }
    //}

    //internal class HiddenToVisibleblockCommand : BaseCommand<HiddenBlock>
    //{
    //    public HiddenToVisibleblockCommand(HiddenBlock receiver)
    //        : base(receiver)
    //    {

    //    }

    //    public override void Execute()
    //    {
    //        receiver.ChangeToUesd();
    //    }

    //    public override void Stop()
    //    {

    //    }
    //}



}
