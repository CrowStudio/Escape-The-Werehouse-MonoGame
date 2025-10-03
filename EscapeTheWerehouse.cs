using EscapeTheWerehouse_MonoGame.GameBoard;
using EscapeTheWerehouse_MonoGame.GameBoard.Elements;
using EscapeTheWerehouse_MonoGame.GameBoard.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Collisions.Layers;
using MonoGame.Extended.ECS;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;
using MonoGameLibrary;
using MonoGameLibrary.Graphics;
using MonoGameLibrary.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Xml.Linq;

namespace EscapeTheWerehouse_MonoGame
{
    public class EscapeTheWerehouse : Core
    {
        Color backgroundColor;

        private TiledMap _tiledMap;

        private TiledMapRenderer _tiledMapRenderer;

        private TiledMapObjectLayer _tiledElementObjects;

        private TiledMapObjectLayer _tiledEntityObjects;

        private List<GameObject> _gameObjects = [];

        private const int Offset = 40;
        private const int TiledObjectOffset = 100 - Offset;

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

            _tiledMap = Content.Load<TiledMap>("maps/zone1/Pitorama");                      // Sort out map path later, to be able to load the maps dynamically
            _tiledElementObjects = _tiledMap.GetLayer<TiledMapObjectLayer>("Elements");     // Elements like pits, switches, doors, etc.
            _tiledEntityObjects = _tiledMap.GetLayer<TiledMapObjectLayer>("Entities");      // ATM only player and boxes
            _tiledMapRenderer = new TiledMapRenderer(GraphicsDevice, _tiledMap);

            LoadElements();
            LoadEntities();
            PrintAllGameObjects(_gameObjects);                                              // Debug print to see that all objects are created with correct variables

            base.LoadContent();
        }


        private void LoadElements()
        {
            foreach (TiledMapObject element in _tiledElementObjects.Objects)
            {
                string elementName = element.Name;
                string objectName = System.Text.RegularExpressions.Regex.Replace(elementName, @"[\d-]", string.Empty);
                Debug.WriteLine($"Object Name: {objectName}");
                Type objectType = Type.GetType("EscapeTheWerehouse_MonoGame.GameBoard.Elements." + objectName);
                if (objectType != null)
                {
                    // Instantiate the object
                    GameObject gameObject = (GameObject)Activator.CreateInstance(objectType);

                    // Set position and size
                    gameObject.Position = new Vector2(element.Position.X, element.Position.Y - TiledObjectOffset);

                    // Only set SourceRectangle if the entity is a TiledMapTileObject
                    if (element is TiledMapTileObject tileElement)
                    {
                        int gid = tileElement.Tile.LocalTileIdentifier;
                        var tileset = tileElement.Tileset;
                        if (tileset != null)
                        {
                            Rectangle sourceRect = tileset.GetTileRegion(gid);
                            gameObject.SourceRect = sourceRect;
                            Texture2D texture = tileset.Texture;
                            gameObject.Texture = texture;
                        }
                    }

                    // Set properties from Tiled
                    foreach (var propPair in element.Properties)
                    {
                        string propertyName = propPair.Key; // or propPair.Name, depending on the library
                        Debug.WriteLine($"Property Name: {propertyName}");
                        string propertyValue = propPair.Value.ToString();
                        Debug.WriteLine($"Property Value: {propertyValue}");

                        // Get the property info from the game object's type
                        var propInfo = objectType.GetProperty(propertyName);
                        if (propInfo != null && propInfo.CanWrite)
                        {
                            object value;
                            // Handle enum types
                            if (propInfo.PropertyType.IsEnum)
                            {
                                value = Enum.Parse(propInfo.PropertyType, propertyValue);
                            }
                            // Handle other types
                            else
                            {
                                value = Convert.ChangeType(propertyValue, propInfo.PropertyType);
                            }
                            propInfo.SetValue(gameObject, value);
                        }
                    }

                    // Add to your game world
                    _gameObjects.Add(gameObject);
                }
            }

        }


        private void LoadEntities()
        {
            foreach (TiledMapObject entity in _tiledEntityObjects.Objects)
            {
                string entityName = entity.Name;
                string objectName = System.Text.RegularExpressions.Regex.Replace(entityName, @"[\d-]", string.Empty);
                Debug.WriteLine($"Object Name: {objectName}");
                Type objectType = Type.GetType("EscapeTheWerehouse_MonoGame.GameBoard.Entities." + objectName);
                if (objectType != null)
                {
                    // Instantiate the object
                    GameObject gameObject = (GameObject)Activator.CreateInstance(objectType);

                    // Set position and size
                    gameObject.Position = new Vector2(entity.Position.X, entity.Position.Y - TiledObjectOffset);

                    // Only set SourceRectangle if the entity is a TiledMapTileObject
                    if (entity is TiledMapTileObject tileEntity)
                    {
                        int gid = tileEntity.Tile.LocalTileIdentifier;
                        var tileset = tileEntity.Tileset;
                        if (tileset != null)
                        {
                            Rectangle sourceRect = tileset.GetTileRegion(gid);
                            gameObject.SourceRect = sourceRect;
                            Texture2D texture = tileset.Texture;
                            gameObject.Texture = texture;
                        }
                    }

                    // Set properties from Tiled
                    foreach (var propPair in entity.Properties)
                    {
                        string propertyName = propPair.Key; // or propPair.Name, depending on the library
                        Debug.WriteLine($"Property Name: {propertyName}");
                        string propertyValue = propPair.Value.ToString();
                        Debug.WriteLine($"Property Value: {propertyValue}");

                        // Get the property info from the game object's type
                        var propInfo = objectType.GetProperty(propertyName);
                        if (propInfo != null && propInfo.CanWrite)
                        {
                            object value;
                            // Handle enum types
                            if (propInfo.PropertyType.IsEnum)
                            {
                                value = Enum.Parse(propInfo.PropertyType, propertyValue);
                            }
                            // Handle other types
                            else
                            {
                                value = Convert.ChangeType(propertyValue, propInfo.PropertyType);
                            }
                            propInfo.SetValue(gameObject, value);
                        }
                    }

                    // Add to your game world
                    _gameObjects.Add(gameObject);
                }
            }
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

            foreach (var gameObject in _gameObjects)
            {
                gameObject.Draw(SpriteBatch);
            }

            SpriteBatch.End();

            base.Draw(gameTime);
        }

        private void DrawTileMap()
        {
            // Apply a translation matrix to offset the entire map
            Matrix translationMatrix = Matrix.CreateTranslation(0, Offset, 0); // Offset by 40 pixels down (Y-axis)
            _tiledMapRenderer.Draw(translationMatrix);
        }

        public static void PrintAllGameObjects(List<GameObject> gameObjects)
        {
            foreach (var gameObject in gameObjects)
            {
                if (gameObject == null)
                {
                    Debug.WriteLine("Null object in list.");
                    continue;
                }

                Type type = gameObject.GetType();
                Debug.WriteLine($"--- {type.Name} ---");

                // Print all public properties
                foreach (var property in type.GetProperties())
                {
                    try
                    {
                        object value = property.GetValue(gameObject);
                        Debug.WriteLine($"{property.Name}: {value}");
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine($"{property.Name}: (Error: {ex.Message})");
                    }
                }

                // Print all public fields
                foreach (var field in type.GetFields())
                {
                    try
                    {
                        object value = field.GetValue(gameObject);
                        Debug.WriteLine($"{field.Name}: {value}");
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine($"{field.Name}: (Error: {ex.Message})");
                    }
                }
            }
        }

    }
}
