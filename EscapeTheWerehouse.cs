using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameLibrary;

namespace EscapeTheWerehouse_MonoGame
{
    public class EscapeTheWerehouse : Core
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        Color backgroundColor;

        public EscapeTheWerehouse() : base("Escape The Werehouse!", 600, 640, false)
        {

        }

        protected override void Initialize()
        {
            backgroundColor = new Color(30, 30, 30);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            // TODO: use this.Content to load your game content here

            base.LoadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(backgroundColor);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
