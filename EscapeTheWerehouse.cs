using EscapeTheWerehouse_MonoGame.GameBoard;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;
using MonoGameLibrary;
using MonoGameLibrary.Graphics;
using MonoGameLibrary.Input;
using System;

namespace EscapeTheWerehouse_MonoGame
{
    public class EscapeTheWerehouse : Core
    {
        Color backgroundColor;
        
        // Defines the slime animated sprite.
        private Texture2D _player;

        // Tracks the position of the player.
        private Vector2 _playerPosition;

        // Speed multiplier when moving.
        private const float MOVEMENT_SPEED = 5.0f;

        //private TiledMap _tiledMap;

        //private TiledMapRenderer _tiledMapRenderer;

        //private SpriteBatch _spriteBatch;

        // Defines the tilemap to draw.
        private Tilemap _tilemap;

        // Defines the bounds of the room that the slime and bat are contained within.
        private Rectangle _levelBounds;

        public EscapeTheWerehouse() : base("Escape The Werehouse!", 600, 640, false)
        {

        }

        protected override void Initialize()
        {
            backgroundColor = new Color(30, 30, 30);

            base.Initialize();
            Rectangle screenBounds = GraphicsDevice.PresentationParameters.Bounds;

            _levelBounds = new Rectangle(
                 (int)_tilemap.TileWidth,
                 (int)_tilemap.TileHeight,
                 screenBounds.Width - (int)_tilemap.TileWidth * 2,
                 screenBounds.Height - (int)_tilemap.TileHeight * 2
             );


        }

        protected override void LoadContent()
        {
            //_tiledMap = Content.Load<TiledMap>("maps/Blocked");
            //_tiledMapRenderer = new TiledMapRenderer(GraphicsDevice, _tiledMap);
            //_spriteBatch = new SpriteBatch(GraphicsDevice);

            // Create the tilemap from the XML configuration file.
            _tilemap = Tilemap.FromFile(Content, "maps/Blocked.xml");

            base.LoadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            //// Check for keyboard input and handle it.
            //CheckKeyboardInput();

            //// Check for gamepad input and handle it.
            //CheckGamePadInput();

            //_tiledMapRenderer.Update(gameTime);

            base.Update(gameTime);
        }

        //private void CheckKeyboardInput()
        //{
        //    // If the space key is held down, the movement speed increases by 1.5
        //    float speed = MOVEMENT_SPEED;
        //    if (Input.Keyboard.IsKeyDown(Keys.Space))
        //    {
        //        speed *= 1.5f;
        //    }

        //    // If the W or Up keys are down, move the slime up on the screen.
        //    if (Input.Keyboard.IsKeyDown(Keys.W) || Input.Keyboard.IsKeyDown(Keys.Up))
        //    {
        //        _slimePosition.Y -= speed;
        //    }

        //    // if the S or Down keys are down, move the slime down on the screen.
        //    if (Input.Keyboard.IsKeyDown(Keys.S) || Input.Keyboard.IsKeyDown(Keys.Down))
        //    {
        //        _slimePosition.Y += speed;
        //    }

        //    // If the A or Left keys are down, move the slime left on the screen.
        //    if (Input.Keyboard.IsKeyDown(Keys.A) || Input.Keyboard.IsKeyDown(Keys.Left))
        //    {
        //        _slimePosition.X -= speed;
        //    }

        //    // If the D or Right keys are down, move the slime right on the screen.
        //    if (Input.Keyboard.IsKeyDown(Keys.D) || Input.Keyboard.IsKeyDown(Keys.Right))
        //    {
        //        _slimePosition.X += speed;
        //    }
        //}

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

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(backgroundColor);

            // Begin the sprite batch to prepare for rendering.
            SpriteBatch.Begin(samplerState: SamplerState.PointClamp);

            //_tiledMapRenderer.Draw();

            // Draw the tilemap.
            _tilemap.Draw(SpriteBatch);

            // Always end the sprite batch when finished.
            SpriteBatch.End();

            base.Draw(gameTime);
        }

    }
}
