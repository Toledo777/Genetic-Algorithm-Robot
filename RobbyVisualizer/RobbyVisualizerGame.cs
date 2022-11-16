using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RobbyTheRobot;

namespace RobbyVisualizer
{
    public class RobbyVisualizerGame : Game
    {
        private GraphicsDeviceManager _graphics;
        private ContentsOfGrid[,] _grid;
        private SpriteBatch _spriteBatch;
        private List<SimulationSprite> _tiles;
        private bool _isRobby;
        public RobbyVisualizerGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            
            // TODO: Add your initialization logic here
            // for(int i = 0; i < 2; i++){
            //     for(int z = 0; z < 2; z++){
            //         SimulationSprite s = new SimulationSprite(this, true, true, z * 10, (i * 10) + 20);
            //         Components.Add(s);
            //     }
            // }
            IRobbyTheRobot robby = Robby.createRobby(10,10,10,10,10,10,10,null);
            _grid = robby.GenerateRandomTestGrid();
            // int counter = 0;
            // foreach(ContentsOfGrid c in grid){
            //     Components.Add(new SimulationSprite(this, c, counter,100));
            //     counter+=40;
            // }
            _tiles = new List<SimulationSprite>();
            for(int i = 0; i < _grid.GetLength(0); i++){
                for(int j = 0; j < _grid.GetLength(1); j++){
                    SimulationSprite s = new SimulationSprite(this, _grid[i,j], j * 40, i *40);
                    Components.Add(s);
                    _tiles.Add(s);

                }
            }
            // _graph = new SimulationSprite(this, grid);
            // Components.Add(new SimulationSprite(this,ContentsOfGrid.Wall, 100, 100));
            // Components.Add(new SimulationSprite(this,ContentsOfGrid.Can, 200, 200));
            // Components.Add(new SimulationSprite(this,ContentsOfGrid.Can));

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
                if(_tiles[0].Grid == ContentsOfGrid.Empty){
                    _tiles[0].Grid = ContentsOfGrid.Can;
                }
                else{
                    _tiles[0].Grid = ContentsOfGrid.Empty;
                }
            

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
