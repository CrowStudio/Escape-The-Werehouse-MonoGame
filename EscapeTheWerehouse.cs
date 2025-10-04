using EscapeTheWerehouse_MonoGame.GameBoard.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Collisions.Layers;
using MonoGame.Extended.ECS;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;
using MonoGameLibrary.Core;
using MonoGameLibrary.GameObjects;
using MonoGameLibrary.Graphics;
using MonoGameLibrary.Helper;
using MonoGameLibrary.Input;
using MonoGameLibrary.Interface;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection.Emit;


namespace EscapeTheWerehouse_MonoGame
{
    public class EscapeTheWerehouse : Core
    {
        Color backgroundColor;

        private GraphicsDeviceManager _graphics;

        private TiledMap _tiledMap;

        private TiledMapRenderer _tiledMapRenderer;

        private TiledMapObjectLayer _tiledElementObjects;

        private TiledMapObjectLayer _tiledEntityObjects;

        private List<GameObject> _gameObjects = [];

        private IPlayer _player = null;

        private int TiledObjectOffset;

        public EscapeTheWerehouse() : base("Escape The Werehouse!", 600, 640, false)
        {
        }

        protected override void Initialize()
        {
            backgroundColor = new Color(30, 30, 30);

            // Get the existing GraphicsDeviceManager from Services
            _graphics = (GraphicsDeviceManager)this.Services.GetService(typeof(IGraphicsDeviceManager));

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _tiledMap = Content.Load<TiledMap>("maps/zone1/Pitorama");                          // Sort out map path later, to be able to load the maps dynamically
            GameMap.Width = _tiledMap.WidthInPixels;
            GameMap.Height = _tiledMap.HeightInPixels;
            GameMap.HeightOffset = 40;
            TiledObjectOffset = 100 - GameMap.HeightOffset;

            _tiledElementObjects = _tiledMap.GetLayer<TiledMapObjectLayer>("Elements");         // Elements like pits, switches, doors, etc.
            _tiledEntityObjects = _tiledMap.GetLayer<TiledMapObjectLayer>("Entities");          // ATM only player and boxes

            _graphics.PreferredBackBufferWidth = GameMap.Width;
            _graphics.PreferredBackBufferHeight = GameMap.Height + GameMap.HeightOffset;
            _graphics.ApplyChanges();                                                           // Update window size to the game map size + height offset

            _tiledMapRenderer = new TiledMapRenderer(GraphicsDevice, _tiledMap);

            LoadElements();
            LoadEntities();

            #if DEBUG
                        DebuggerTool.PrintAllGameObjects(_gameObjects);                         // Debug print to see that all objects are created with correct variables
            #endif

            ExtractGameObjects.Extract(_gameObjects);

            _player = ExtractGameObjects.Player;

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
                    GameObject gameObject = (GameObject)Activator.CreateInstance(objectType);   // Instantiate the object

                    // Set position and size
                    gameObject.Position = new Vector2(element.Position.X, element.Position.Y - TiledObjectOffset);

                    if (element is TiledMapTileObject tileElement)                              // Only set SourceRectangle if the entity is a TiledMapTileObject
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

                    foreach (var propPair in element.Properties)                                // Set properties from Tiled
                    {
                        string propertyName = propPair.Key;
                        Debug.WriteLine($"Property Name: {propertyName}");
                        string propertyValue = propPair.Value.ToString();
                        Debug.WriteLine($"Property Value: {propertyValue}");

                        var propInfo = objectType.GetProperty(propertyName);                    // Get the property info from the game object's type
                        if (propInfo != null && propInfo.CanWrite)
                        {
                            object value;
                            if (propInfo.PropertyType.IsEnum)                                   // Handle enum types
                            {
                                value = Enum.Parse(propInfo.PropertyType, propertyValue);
                            }
                            else                                                                // Handle other types
                            {
                                value = Convert.ChangeType(propertyValue, propInfo.PropertyType);
                            }
                            propInfo.SetValue(gameObject, value);
                        }
                    }

                    _gameObjects.Add(gameObject);                                               // Add to your game world
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

                    if (entity is TiledMapTileObject tileEntity)                                // Only set SourceRectangle if the entity is a TiledMapTileObject
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

                    foreach (var propPair in entity.Properties)                                 // Set properties from Tiled
                    {
                        string propertyName = propPair.Key;
                        Debug.WriteLine($"Property Name: {propertyName}");
                        string propertyValue = propPair.Value.ToString();
                        Debug.WriteLine($"Property Value: {propertyValue}");

                        var propInfo = objectType.GetProperty(propertyName);                    // Get the property info from the game object's type
                        if (propInfo != null && propInfo.CanWrite)
                        {
                            object value;
                            if (propInfo.PropertyType.IsEnum)                                   // Handle enum types
                            {
                                value = Enum.Parse(propInfo.PropertyType, propertyValue);
                            }
                            else                                                                // Handle other types
                            {
                                value = Convert.ChangeType(propertyValue, propInfo.PropertyType);
                            }
                            propInfo.SetValue(gameObject, value);
                        }
                    }

                    _gameObjects.Add(gameObject);                                               // Add to your game world
                }
            }

        }


        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            _tiledMapRenderer.Update(gameTime);

            _player.Update(gameTime);                                                           // Update player position

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(backgroundColor);

            SpriteBatch.Begin(samplerState: SamplerState.PointClamp);                           // Begin the sprite batch to prepare for rendering.

            DrawTileMap(GameMap.HeightOffset, _tiledMapRenderer);

            foreach (var gameObject in _gameObjects)
            {
                gameObject.Draw(SpriteBatch);
            }

            SpriteBatch.End();

            base.Draw(gameTime);
        }

    }
}
