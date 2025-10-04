using EscapeTheWerehouse_MonoGame.GameBoard.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.GameObjects;
using MonoGameLibrary.Core;

namespace EscapeTheWerehouse_MonoGame.GameBoard.Elements
{

    internal class BottomlessPit : GameObject
    {
        public int BoxInPit { get; set; }
        public bool IsActive { get; set; }

        public override void Update(GameTime gameTime)
        {
            // Update pit logic
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                texture: Texture,
                position: Position,
                sourceRectangle: SourceRect,
                color: Color.White,
                rotation: 0f,
                origin: Vector2.Zero,
                scale: 1f,
                effects: SpriteEffects.None,
                layerDepth: 0f
            );
        }
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
        public int BoxInPit { get; set; }
        public bool IsActive { get; set; }                  // Current state

        public bool TryFill()
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
            if (IsActive)
            {
                spriteBatch.Draw(
                    texture: Texture,
                    position: Position,
                    sourceRectangle: SourceRect,
                    color: Color.White,
                    rotation: 0f,
                    origin: Vector2.Zero,
                    scale: 1f,
                    effects: SpriteEffects.None,
                    layerDepth: 0f
                );
            }
        }
    }

    internal class Exit : GameObject
    {
        //public int Id { get; set; }
        public bool IsActive { get; set; }                  // Current state

        public override void Update(GameTime gameTime)
        {
            // Update pit logic
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (IsActive)
            {
                spriteBatch.Draw(
                    texture: Texture,
                    position: Position,
                    sourceRectangle: SourceRect,
                    color: Color.White,
                    rotation: 0f,
                    origin: Vector2.Zero,
                    scale: 1f,
                    effects: SpriteEffects.None,
                    layerDepth: 0f
                );
            }
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
            if (IsActive)
            {
                spriteBatch.Draw(
                    texture: Texture,
                    position: Position,
                    sourceRectangle: SourceRect,
                    color: Color.White,
                    rotation: 0f,
                    origin: Vector2.Zero,
                    scale: 1f,
                    effects: SpriteEffects.None,
                    layerDepth: 0f
                );
            }
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
        public int BoxInPit { get; set; }
        public bool IsActive { get; set; }                  // Current state

        public bool TryFill()
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
            if (IsActive)
            {
                spriteBatch.Draw(
                    texture: Texture,
                    position: Position,
                    sourceRectangle: SourceRect,
                    color: Color.White,
                    rotation: 0f,
                    origin: Vector2.Zero,
                    scale: 1f,
                    effects: SpriteEffects.None,
                    layerDepth: 0f
                );
            }
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
            spriteBatch.Draw(
                texture: Texture,
                position: Position,
                sourceRectangle: SourceRect,
                color: Color.White,
                rotation: 0f,
                origin: Vector2.Zero,
                scale: 1f,
                effects: SpriteEffects.None,
                layerDepth: 0f
            );
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
            spriteBatch.Draw(
                texture: Texture,
                position: Position,
                sourceRectangle: SourceRect,
                color: Color.White,
                rotation: 0f,
                origin: Vector2.Zero,
                scale: 1f,
                effects: SpriteEffects.None,
                layerDepth: 0f
            );
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
            if (IsActive)
            {
                spriteBatch.Draw(
                    texture: Texture,
                    position: Position,
                    sourceRectangle: SourceRect,
                    color: Color.White,
                    rotation: 0f,
                    origin: Vector2.Zero,
                    scale: 1f,
                    effects: SpriteEffects.None,
                    layerDepth: 0f
                );
            }
        }
    }


}