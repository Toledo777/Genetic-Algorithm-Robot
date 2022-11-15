using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace RobbyVisualizer
{
    public class SimulationSprite: DrawableGameComponent
    {
        private RobbyVisualizerGame _game;
        private SpriteBatch _spriteBatch;
        private Tile _tile;
        private Texture2D _tileTexture;
        private int _x;
        private int _y;
        public SimulationSprite(RobbyVisualizerGame game, Tile tile, int x, int y) : base(game)
        {
            this._game = game;
            this._tile = tile;
            this._x = x;
            this._y = y;
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
            if(_tile.IsCan){
                _tileTexture = _game.Content.Load<Texture2D>("can");
            }
            if(_tile.IsRobby){
                _tileTexture = _game.Content.Load<Texture2D>("robby");
            }

             //this may be all the updating we need. Assumes we update the tile in the main app.
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin(SpriteSortMode.BackToFront);
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Draw(_tileTexture, new Vector2(_x,_y), Color.White);
            base.Draw(gameTime);
            _spriteBatch.End();
        }
    }
}