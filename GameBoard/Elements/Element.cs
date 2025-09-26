using EscapeTheWerehouse_MonoGame.GameBoard.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EscapeTheWerehouse_MonoGame.GameBoard.Elements
{
    // Board elements
    public enum Element
    {
        Start,
        Floor,
        Wall,
        Pit,                    // Takes one box to fill
        DeepPit,                // Takes two boxes to fill
        BottomlessPit,          // Cannot be filled or walked on
        FloorSwitch,
        TrapDoor,
        WallSwitch,
        SlidingDoor,
        Pusher,
        ConveyorBelt,
        LazerBeam,
        Duplicator,
        Teleporter,
        Elevator,
        Exit

    }

    // Additional enums for different tiles
    public enum PitState
    {
        Empty,                  // No boxes in the pit
        HalfFilled,             // One box in a DeepPit (or unused for Pit/BottomlessPit)
        Filled                  // Pit is fully filled (one box for Pit, two boxes for DeepPit)
    }

    public enum SwitchType
    {
        Latching,
        LatchingExit,
        Momentary,
        MomentaryExit
    }

    public enum ConnectionType
    {
        SlidingDoor,
        TrapDoor,
        Pusher,
        ConveyorBelt,
        LazerBeam,
        Elevator
    }

    public enum PusherType
    {
        Normal,                 // Pushes one tile
        Powerful                // Pushes two tiles
    }

    public enum BeltSpeed
    {
        Normal,                 // Moves one tile/s
        Fast                    // Moves two tiles/s
    }

    public enum DefaultState
    {
        NormallyOn,             // E.g.switch will activate by default and open a connected closed door
        NormallyOff,            // E.g lazer will be deactivate by default
        NormallyOpen,           // E.g sliding door is open and passable by default
        NormallyClosed          // E.g Trap door is closed and passable by default
    }

    public enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }

    public enum Orientation
    {
        Horizontal,
        Vertical
    }

    public abstract class GameObject
    {
        public int Id { get; set; }
        public Rectangle Bounds { get; set; }
        public abstract void Update(GameTime gameTime);
        public abstract void Draw(SpriteBatch spriteBatch);
    }

    internal class ConveyorBelt
    {
        public int Id { get; set; }
        public BeltSpeed BeltSpeed { get; set; }            // Normal / Fast
        public bool IsActive { get; set; }                  // Current state
    }

    internal class DeepPit : GameObject
    {
        //public int Id { get; set; }
        public PitState PitState { get; set; }              // Empty / HalfFilled / Filled
        public bool IsActive { get; set; }                  // Current state

        public bool TryFill(Box box)
        {
            if (PitState == PitState.Filled)
            {
                return false;                               // Already filled
            }

            else if (PitState == PitState.Empty)
            {
                PitState = PitState.HalfFilled;
                return true;                                // DeepPit needs 2 boxes
            }

            else
            {
                PitState = PitState.Filled;                 // DeepPit is now fully filled
                IsActive = false;
                return true;
            }
        }

        public override void Update(GameTime gameTime)
        {
            // Update pit logic
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            // Draw pit
        }
    }

    internal class FloorSwitch : GameObject
    {
        //public int Id { get; set; }
        public SwitchType Type { get; set; }                // MomentaryExit / Momentary
        public DefaultState DefaultState { get; set; }      // NormallyOn / NormallyOff
        public ConnectionType ConnectionType { get; set;  } // SlidingDoor / TrapDoor / Pusher / ConveyorBelt / LazerBeam / Elevator
        public int ConnectionId { get; set; }               // Id of ConnectionType
        public bool IsActive { get; set; }                  // Current state

        public override void Update(GameTime gameTime)
        {
            // Update pit logic
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            // Draw pit
        }
    }

    internal class LazerBeam
    {
        public int Id { get; set; }
        public Direction Direction { get; set; }            // Up / Down / Left / Right
        public DefaultState DefaultState { get; set; }      // NormallyOn / NormallyOff
        public bool IsActive { get; set; }                  // Current state
    }

    internal class Pit : GameObject
    {
        //public int Id { get; set; }
        public PitState PitState { get; set; }              // Empty / Filled
        public bool IsActive { get; set; }                  // Current state

        public bool TryFill(Box box)
        {
            if (PitState == PitState.Filled)
            {
                return false;                               // Already filled
            }

            else
            {
                PitState = PitState.Filled;                 // Pit is now filled
                IsActive = false;
                return true;
            }
        }
        public override void Update(GameTime gameTime)
        {
            // Update pit logic
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            // Draw pit
        }
    }

    internal class Pusher
    {
        public int Id { get; set; }
        public PusherType PusherType { get; set; }          // Normal / Powerful
        public bool IsActive { get; set; }                  // Current state
    }

    internal class SlidingDoor : GameObject
    {
        //public int Id { get; set; }
        public Orientation Orientation { get; set; }        // Horizontal / Vertical
        public DefaultState DefaultState { get; set; }      // NormallyOpen / NormallyClosed
        public bool IsBlocking { get; set; }                // Current state

        public override void Update(GameTime gameTime)
        {
            // Update pit logic
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            // Draw pit
        }
    }

    internal class TrapDoor : GameObject
    {
        //public int Id { get; set; }
        public Direction Direction { get; set; }            // Up / Down / Left / Right
        public DefaultState DefaultState { get; set; }      // NormallyOpen / NormallyClosed
        public bool IsBlocking { get; set; }                // Current state

        public override void Update(GameTime gameTime)
        {
            // Update pit logic
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            // Draw pit
        }
    }

    internal class WallSwitch : GameObject
    {
        //public int Id { get; set; }
        public Direction Direction { get; set; }            // Up / Down / Left / Right
        public SwitchType Type { get; set; }                // LatchingExit / Latching
        public DefaultState DefaultState { get; set; }      // NormallyOn / NormallyOff
        public ConnectionType ConnectionType { get; set; }  // SlidingDoor / TrapDoor / Pusher / ConveyorBelt / LazerBeam / Elevator
        public int ConnectionId { get; set; }               // Id of ConnectionType
        public bool IsActive { get; set; }                  // Current state

        public override void Update(GameTime gameTime)
        {
            // Update pit logic
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            // Draw pit
        }
    }


}