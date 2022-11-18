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
        private ContentsOfGrid _square;
        private Texture2D _tileTexture;
        //x & y positions of the square in the grid
        private int _x;
        private int _y;
        public SimulationSprite(RobbyVisualizerGame game, ContentsOfGrid square, int x, int y, bool isRobby) : base(game)
        {
            this._game = game;
            this._square = square;
            this._x = x;
            this._y = y;
            this.IsRobby = isRobby;
        }
        public ContentsOfGrid Square
        {
            get{return this._square;}
            set{this._square = value;}
        }
        public bool IsRobby{
            get;
            set;
        }
     

        public override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {  
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            SetTileTexture();
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            SetTileTexture();
            base.Update(gameTime);
        }
        /// <summary>
        /// Sets _tileTexture depending on tile content. Can be robby, can or empty.
        /// </summary>
        void SetTileTexture(){
            if(IsRobby){
                _tileTexture = _game.Content.Load<Texture2D>("robby");
            }
            else if(this._square.Equals(ContentsOfGrid.Can)){
                _tileTexture = _game.Content.Load<Texture2D>("can");
            }
            else{
                _tileTexture = _game.Content.Load<Texture2D>("empty");
            }
        }

        public override void Draw(GameTime gameTime)
        {   _spriteBatch.Begin(SpriteSortMode.BackToFront);
            _spriteBatch.Draw( _tileTexture, new Vector2(_x,_y), Color.White);
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}