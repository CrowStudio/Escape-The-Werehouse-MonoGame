using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EscapeTheWerehouse_MonoGame.GameBoard.Entities
{
    public class Box
    {
        public int Id { get; set; }
        public Vector2 Position { get; set; }
        public Texture2D Texture { get; set; }
        public int BoxInPit { get; set; }
        public bool IsActive { get; set; } = true; // True if not destroyed (e.g., by BottomlessPit)

        public Box(Texture2D texture, Vector2 startPosition)
        {
            Texture = texture;
            Position = startPosition;
        }

        ////Move the box in a direction(called by player or conveyor belts)
        //public bool Move(Vector2 direction, Level level)
        //{
        //    Vector2 newPosition = Position + direction;

        //    // Check if the new position is valid (e.g., not a wall)
        //    if (!level.IsTileWalkable((int)newPosition.X, (int)newPosition.Y))
        //        return false; // Movement blocked

        //    // Check for pits
        //    var tile = level.GetTileAt((int)newPosition.X, (int)newPosition.Y);
        //    if (tile is Pit pit)
        //    {
        //        if (pit.TryFill(this)) // Try to fill the pit
        //        {
        //            IsActive = false; // Box is consumed by the pit
        //            return true;
        //        }
        //    }

        //    Position = newPosition;
        //    return true;
        //}

        // Draw the box
        public void Draw(SpriteBatch spriteBatch)
        {
            if (IsActive)
                spriteBatch.Draw(Texture, Position, Color.White);
        }
    }
}
