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
using System.Diagnostics;

namespace EscapeTheWerehouse_MonoGame
{
    public class EscapeTheWerehouse : Core
    {
        Color backgroundColor;

        private TiledMap _tiledMap;

        private TiledMapRenderer _tiledMapRenderer;

        private TiledMapObjectLayer _tiledElementObjects;

        private TiledMapObjectLayer _tiledEntityObjects;

        private static int _offset = 40;
        private static int _tiledObjectOffset = 100 - _offset;

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

            _tiledMap = Content.Load<TiledMap>("maps/Blocked");

            _tiledElementObjects = _tiledMap.GetLayer<TiledMapObjectLayer>("Elements");
            _tiledEntityObjects = _tiledMap.GetLayer<TiledMapObjectLayer>("Entities");

            _tiledMapRenderer = new TiledMapRenderer(GraphicsDevice, _tiledMap);

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

            _tiledMapRenderer.Update(gameTime);

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

            DrawTileMap();

            SpriteBatch.End();

            base.Draw(gameTime);
        }

        private void DrawTileMap()
        {
            // Apply a translation matrix to offset the entire map
            Matrix translationMatrix = Matrix.CreateTranslation(0, _offset, 0); // Offset by 40 pixels down (Y-axis)
            _tiledMapRenderer.Draw(translationMatrix);

            foreach (TiledMapObject obj in _tiledElementObjects.Objects)
            {
                DrawObject(obj);
            }

            foreach (TiledMapObject obj in _tiledEntityObjects.Objects)
            {
                DrawObject(obj);
            }
        }

        private void DrawObject(TiledMapObject obj)
        {
            if (obj is TiledMapTileObject tileObj)
            {
                int gid = tileObj.Tile.LocalTileIdentifier;
                var tileset = tileObj.Tileset;
                if (tileset == null) return;

                Rectangle sourceRect = tileset.GetTileRegion(gid);

                // Adjust position if needed (e.g., subtract tile height)
                Vector2 drawPosition = new Vector2(
                    tileObj.Position.X,
                    tileObj.Position.Y - _tiledObjectOffset
                );

                SpriteBatch.Draw(
                    texture: tileset.Texture,
                    position: drawPosition,
                    sourceRectangle: sourceRect,
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
