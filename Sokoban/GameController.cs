using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Sokoban
{
    public class GameController : Game
    {                
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Level _level;
        private Player _player;
        private bool isPressed = false;
        private List<string> levelPaths;
        private string currentLevelPath;

        public GameController()
        {
            levelPaths = Directory.GetFiles(@"../../../Levels/").ToList();
            LoadNextLevel();
            _player = new Player();
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _graphics.IsFullScreen = false;
            _graphics.PreferredBackBufferWidth = GameConstants.WINDOW_SIZE;
            _graphics.PreferredBackBufferHeight = GameConstants.WINDOW_SIZE;
            _graphics.ApplyChanges();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            Player.PlayerTexture = Content.Load<Texture2D>("player");
            Level.WallTexture = Content.Load<Texture2D>("wall");
            Level.FloorTexture = Content.Load<Texture2D>("ground");
            Level.BoxTexture = Content.Load<Texture2D>("box");
            Level.SpotTexture = Content.Load<Texture2D>("spot");
            Level.BoxOnSpot = Content.Load<Texture2D>("boxOnSpot");

            // TODO: use this.Content to load your game content here
        }

        
        protected override void Update(GameTime gameTime)
        {

            
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            else if (Keyboard.GetState().IsKeyDown(Keys.W) && !isPressed)
            {
                _player.Move(GameConstants.UP);
                Level.Update();
                isPressed = true;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.A) && !isPressed)
            {
                _player.Move(GameConstants.LEFT);
                Level.Update();
                isPressed = true;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.S) && !isPressed)
            {
                _player.Move(GameConstants.DOWN);
                Level.Update();
                isPressed = true;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.D) && !isPressed)
            {
                _player.Move(GameConstants.RIGHT);
                Level.Update();
                isPressed = true;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.R) && !isPressed)
            {
                Level.InitLevelPath(currentLevelPath);
                isPressed = true;
            }
            else if (Keyboard.GetState().GetPressedKeyCount() == 0 && isPressed)
            {
                isPressed = false;
            }

            if (IsWin())
            {
                LoadNextLevel();
            }

            base.Update(gameTime);
            
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
            _spriteBatch.Begin();
            for (int i = 0; i < GameConstants.LEVEL_WIDTH; i++)
            {
                for (int j = 0; j < GameConstants.LEVEL_HEIGHT; j++)
                {
                    _spriteBatch.Draw(Level.FloorTexture,
                                    new Vector2(i * GameConstants.TILE_SIZE_WIDTH, j * GameConstants.TILE_SIZE_HEIGHT),
                                    null,
                                    Color.White);
                    switch (Level.LevelMap[i, j])
                    {
                        case GameConstants.WALL:   
                            _spriteBatch.Draw(Level.WallTexture,
                                new Vector2(i * GameConstants.TILE_SIZE_WIDTH, j * GameConstants.TILE_SIZE_HEIGHT),
                                null,
                                Color.White);
                            break;
                           
                        case GameConstants.BOX:
                            _spriteBatch.Draw(Level.BoxTexture,
                                    new Vector2(i * GameConstants.TILE_SIZE_WIDTH, j * GameConstants.TILE_SIZE_HEIGHT),
                                    null,
                                    Color.White);
                            break;

                        case GameConstants.SPOT:
                            _spriteBatch.Draw(Level.SpotTexture,
                                    new Vector2(i * GameConstants.TILE_SIZE_WIDTH, j * GameConstants.TILE_SIZE_HEIGHT),
                                    null,
                                    Color.White);
                            break;

                        case GameConstants.BOX_ON_SPOT:
                            _spriteBatch.Draw(Level.BoxOnSpot,
                                    new Vector2(i * GameConstants.TILE_SIZE_WIDTH, j * GameConstants.TILE_SIZE_HEIGHT),
                                    null,
                                    Color.White);
                            break;
                        case GameConstants.PLAYER:
                            _spriteBatch.Draw(Player.PlayerTexture,
                                    Player.GetPossition(),
                                    null,
                                    Color.White);
                            break;
                    }
                }
            }
            
            _spriteBatch.End();
        }

        private void LoadNextLevel()
        {
            if (levelPaths.Count == 0)
            {
                Exit();
            }
            currentLevelPath = levelPaths.FirstOrDefault();
            if (_level != null)
            {
                Level.InitLevelPath(currentLevelPath);
            }
            else
            {
                _level = new Level(currentLevelPath);     
            }
            levelPaths.Remove(currentLevelPath);

        }

        private static bool IsWin()
        {
            var Spots = Level.GetSpots();
            foreach (var Spot in Spots)
            {
                if (Level.LevelMap[(int)Spot.X, (int)Spot.Y] != GameConstants.BOX_ON_SPOT)
                {
                    return false;
                }
            }
            return true;
        }
    }
}