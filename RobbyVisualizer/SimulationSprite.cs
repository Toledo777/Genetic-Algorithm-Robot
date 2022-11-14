using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace RobbyVisualizer
{
    public class SimulationSprite: DrawableGameComponent
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Tile _tile;
        private RobbyVisualizerGame _game;
        private Texture2D _tileTexture;
        public SimulationSprite(RobbyVisualizerGame game, Tile tile)
        {
            // this._tile = new Tile(isCan, isRobby);
            this._game = game;
            this._tile = tile;
        }
     

        public override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            if(_tile.IsCan){
                _tileTexture = _game.Content.Load<Texture2D>("can");
            }
            if(_tile.IsRobby){
                _tileTexture = _game.Content.Load<Texture2D>("robby");
            }
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // LoadContent(); //this may be all the updating we need. Assumes we update the tile in the main app.
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Draw(_tileTexture, new Vector2(10,10));
            base.Draw(gameTime);
        }
    }
}