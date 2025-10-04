using EscapeTheWerehouse_MonoGame.GameBoard.Elements;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.GameObjects;

namespace EscapeTheWerehouse_MonoGame.GameBoard.Entities
{
    public class Box : GameObject
    {
        public bool IsActive { get; set; } = true; // True if not destroyed (e.g., by BottomlessPit)

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

        public override void Update(GameTime gameTime)
        {

        }

        // Draw the box
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
