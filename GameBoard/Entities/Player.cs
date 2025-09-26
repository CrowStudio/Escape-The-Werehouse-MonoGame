using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using EscapeTheWerehouse_MonoGame.GameBoard.Elements;

namespace EscapeTheWerehouse_MonoGame.GameBoard.Entities

{
    public class Player
    {
        // Position and movement
        public Vector2 Position { get; private set; }
        public Direction FacingDirection { get; private set; }
        public Texture2D Texture { get; set; } // Player sprite
        public int MoveSpeed { get; set; } = 1; // Tiles per move

        // Health and status
        public int Lives { get; private set; } = 3;
        public bool IsAlive => Lives > 0;

        //// For animation
        //public Rectangle SourceRectangle { get; private set; }

        public Player(Texture2D texture, Vector2 startPosition)
        {
            Texture = texture;
            Position = startPosition;
            FacingDirection = Direction.Down; // Default direction
            //SourceRectangle = new Rectangle(0, 0, Texture.Width, Texture.Height);
        }

        //// Update player state (called every frame)
        //public void Update(GameTime gameTime, Level level)
        //{
        //    var keyboardState = Keyboard.GetState();

        //    // Movement logic
        //    if (keyboardState.IsKeyDown(Keys.Right))
        //        Move(Direction.Right, level);
        //    else if (keyboardState.IsKeyDown(Keys.Left))
        //        Move(Direction.Left, level);
        //    else if (keyboardState.IsKeyDown(Keys.Up))
        //        Move(Direction.Up, level);
        //    else if (keyboardState.IsKeyDown(Keys.Down))
        //        Move(Direction.Down, level);
        //}

        // Draw the player
        public void Draw(SpriteBatch spriteBatch)
        {
            // Draw() with animation
            //spriteBatch.Draw(Texture, Position, SourceRectangle, Color.White);
            spriteBatch.Draw(Texture, Position, Color.White);
        }

        //// Move the player in a direction
        //public void Move(Direction direction, Level level)
        //{
        //    Vector2 newPosition = Position + GetDirectionVector(direction) * MoveSpeed;

        //    // Check if the new position is valid (e.g., not a wall)
        //    if (level.IsTileWalkable((int)newPosition.X, (int)newPosition.Y))
        //    {
        //        Position = newPosition;
        //        FacingDirection = direction;
        //    }
        //}

        // Helper: Convert direction to a vector
        private Vector2 GetDirectionVector(Direction direction)
        {
            return direction switch
            {
                Direction.Up => new Vector2(0, -1),
                Direction.Down => new Vector2(0, 1),
                Direction.Left => new Vector2(-1, 0),
                Direction.Right => new Vector2(1, 0),
                _ => Vector2.Zero
            };
        }

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
