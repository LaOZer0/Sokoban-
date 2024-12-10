using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Sokoban
{
    public class GameCycleView : Game, IGameplayView
    {
        

        public event EventHandler CycleFinished = delegate { };
        public event EventHandler<ControlsEventArgs> PlayerMoved = delegate { };

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Vector2 _playerPosition = Vector2.Zero;
        private Texture2D _playerImage;

        public GameCycleView()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        

        public void LoadGameCycleParametrs(Vector2 position)
        {
            _playerPosition = position;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _playerImage = Content.Load<Texture2D>("ball");

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            var keys = Keyboard.GetState().GetPressedKeys();
            if (keys.Length > 0)
            {
                var key = keys[0];
                switch (key)
                {
                    case Keys.W:
                        {
                            PlayerMoved.Invoke(
                                this,
                                new ControlsEventArgs
                                {
                                    Direction = IGameplayModel.Direction.up
                                }
                            );
                            break;
                        }

                    case Keys.S:
                        {
                            PlayerMoved.Invoke(
                                this,
                                new ControlsEventArgs
                                {
                                    Direction = IGameplayModel.Direction.down
                                }
                            );
                            break;
                        }

                    case Keys.A:
                        {
                            PlayerMoved.Invoke(
                                this,
                                new ControlsEventArgs
                                {
                                    Direction = IGameplayModel.Direction.left
                                }
                            );
                            break;
                        }

                    case Keys.D:
                        {
                            PlayerMoved.Invoke(
                                this,
                                new ControlsEventArgs
                                {
                                    Direction = IGameplayModel.Direction.right
                                }
                            );
                            break;
                        }

                    case Keys.Escape:
                        {
                            Exit();
                            break;
                        }
                }
            }
            
            base.Update(gameTime);
            CycleFinished.Invoke(this, new EventArgs());
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
            _spriteBatch.Begin();
            _spriteBatch.Draw(_playerImage, _playerPosition, Color.White);
            _spriteBatch.End();
        }
    }
}