using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RobbyTheRobot;

namespace RobbyVisualizer
{
    public class SimulationSprite: DrawableGameComponent
    {
        private RobbyVisualizerGame _game;
        private SpriteBatch _spriteBatch;
        private ContentsOfGrid _grid;

        // private Tile _tile;
        private Texture2D _tileTexture;
        private int _x;
        private int _y;
        public SimulationSprite(RobbyVisualizerGame game, ContentsOfGrid grid, int x, int y) : base(game)
        {
            this._game = game;
            this._grid = grid;
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
            if(_grid.Equals(ContentsOfGrid.Can)){
                _tileTexture = _game.Content.Load<Texture2D>("can");
            }
            else{
                _tileTexture = _game.Content.Load<Texture2D>("empty");
            }
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            if(_grid.Equals(ContentsOfGrid.Can)){
                _tileTexture = _game.Content.Load<Texture2D>("can");
            }
            else{
                _tileTexture = _game.Content.Load<Texture2D>("empty");
            }            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {   _spriteBatch.Begin(SpriteSortMode.BackToFront);
            _spriteBatch.Draw( _tileTexture, new Vector2(_x,_y), Color.White);
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}