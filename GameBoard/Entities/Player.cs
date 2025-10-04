using EscapeTheWerehouse_MonoGame.GameBoard.Elements;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Collisions.Layers;
using MonoGameLibrary.Core;
using MonoGameLibrary.GameObjects;
using MonoGameLibrary.Interface;
using System.Diagnostics;

namespace EscapeTheWerehouse_MonoGame.GameBoard.Entities

{
    public class Player : GameObject, IPlayer
    {
        // Position and movement
        public Direction FacingDirection { get; set; }
        public int MoveSpeed { get; set; } = 100; // Tiles per move

        // Health and status
        public int Lives { get; set; }

        public bool IsAlive { get; set; }

        private int playerWidth = 100;  // Replace with your player's actual width
        private int playerHeight = 100; // Replace with your player's actual height

        public override void Update(GameTime gameTime)
        {
            IsAlive = Lives != 0;  // As long as Lives NOT equals 0 the game can continue

            //if (Core.Input.Keyboard.IsKeyDown(Keys.Space))
            //{

            //}

            // Calculate new position for each movement
            Vector2 newPosition = Position;

            // If the W or Up keys are down, move up
            if (Core.Input.Keyboard.WasKeyJustPressed(Keys.W) || Core.Input.Keyboard.WasKeyJustPressed(Keys.Up))
            {
                newPosition = new Vector2(Position.X, Position.Y - MoveSpeed);
                Debug.WriteLine($"Player Position: {Position - new Vector2(0, 40)}");
            }
            // If the S or Down keys are down, move down
            if (Core.Input.Keyboard.WasKeyJustPressed(Keys.S) || Core.Input.Keyboard.WasKeyJustPressed(Keys.Down))
            {
                newPosition = new Vector2(Position.X, Position.Y + MoveSpeed);
                Debug.WriteLine($"Player Position: {Position - new Vector2(0, 40)}");
            }
            // If the A or Left keys are down, move left
            if (Core.Input.Keyboard.WasKeyJustPressed(Keys.A) || Core.Input.Keyboard.WasKeyJustPressed(Keys.Left))
            {
                newPosition = new Vector2(Position.X - MoveSpeed, Position.Y);
                Debug.WriteLine($"Player Position: {Position - new Vector2(0, 40)}");
            }
            // If the D or Right keys are down, move right
            if (Core.Input.Keyboard.WasKeyJustPressed(Keys.D) || Core.Input.Keyboard.WasKeyJustPressed(Keys.Right))
            {
                newPosition = new Vector2(Position.X + MoveSpeed, Position.Y);
                Debug.WriteLine($"Player Position: {Position - new Vector2(0, 40)}");
            }

            ValidateMove(newPosition);

            //private void CheckGamePadInput()
            //{
            //    GamePadInfo gamePadOne = Input.GamePads[(int)PlayerIndex.One];

            //    // If the A button is held down, the movement speed increases by 1.5
            //    // and the gamepad vibrates as feedback to the player.
            //    float speed = MOVEMENT_SPEED;
            //    if (gamePadOne.IsButtonDown(Buttons.A))
            //    {
            //        speed *= 1.5f;
            //        gamePadOne.SetVibration(1.0f, TimeSpan.FromSeconds(1));
            //    }
            //    else
            //    {
            //        gamePadOne.StopVibration();
            //    }

            //    // Check thumbstick first since it has priority over which gamepad input
            //    // is movement.  It has priority since the thumbstick values provide a
            //    // more granular analog value that can be used for movement.
            //    if (gamePadOne.LeftThumbStick != Vector2.Zero)
            //    {
            //        _slimePosition.X += gamePadOne.LeftThumbStick.X * speed;
            //        _slimePosition.Y -= gamePadOne.LeftThumbStick.Y * speed;
            //    }
            //    else
            //    {
            //        // If DPadUp is down, move the slime up on the screen.
            //        if (gamePadOne.IsButtonDown(Buttons.DPadUp))
            //        {
            //            _slimePosition.Y -= speed;
            //        }

            //        // If DPadDown is down, move the slime down on the screen.
            //        if (gamePadOne.IsButtonDown(Buttons.DPadDown))
            //        {
            //            _slimePosition.Y += speed;
            //        }

            //        // If DPapLeft is down, move the slime left on the screen.
            //        if (gamePadOne.IsButtonDown(Buttons.DPadLeft))
            //        {
            //            _slimePosition.X -= speed;
            //        }

            //        // If DPadRight is down, move the slime right on the screen.
            //        if (gamePadOne.IsButtonDown(Buttons.DPadRight))
            //        {
            //            _slimePosition.X += speed;
            //        }
            //    }
            //}
        }

        // Draw the player
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (IsAlive)
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


        // Move the player in a direction
        public void ValidateMove(Vector2 newPosition)
        {
            // Clamp the new position to the map boundaries, accounting for the Y-offset
            newPosition.X = MathHelper.Clamp(newPosition.X, 0, GameMap.Width - playerWidth);
            newPosition.Y = MathHelper.Clamp(newPosition.Y, GameMap.HeightOffset, GameMap.Height - playerHeight + GameMap.HeightOffset);

            // Apply the clamped position
            Position = newPosition;
        }

        // Helper: Convert direction to a vector
        private Vector2 GetDirectionVector(Direction direction) => direction switch
        {
            Direction.Up => new Vector2(0, -1),
            Direction.Down => new Vector2(0, 1),
            Direction.Left => new Vector2(-1, 0),
            Direction.Right => new Vector2(1, 0),
            _ => Vector2.Zero
        };

        //// Handle damage
        //public void TakeDamage(int damage)
        //{
        //    Lives -= damage;
        //    if (!IsAlive)
        //    {
        //        // Handle death (e.g., respawn or game over)
        //        OnDeath?.Invoke(this, EventArgs.Empty);
        //    }
        //}

        //// Event for when the player dies
        //public event EventHandler OnDeath;
    }
}
