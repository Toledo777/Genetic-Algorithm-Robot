using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.IO;
using RobbyTheRobot;
using System.Windows.Forms;
using System.Collections.Generic;

namespace RobbyVisualizer
{
    public class RobbyVisualizerGame : Game
    {
        private GraphicsDeviceManager _graphics;
        private List<SimulationSprite> _tiles;
        private SpriteBatch _spriteBatch;
        private IRobbyTheRobot _robot;
        private ContentsOfGrid[,] _grid;
        private int[] _moves;
        private int _moveCount;
        private double _currentScore;
        private int _maxMoves;
        private int _posX;
        private int _posY;
        private int _currentGeneration;
        private Random _rng;
        private String[] _solutionFiles;
        private int _generationIndex;

        public RobbyVisualizerGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            base.Initialize();
            // fill robby with preset values, not important as they are not used
            _robot = Robby.createRobby(1000, 243, 200, 7, 7, 6.0, 7.0);
            _rng = new Random();
            _moveCount = 0;
            _currentScore = 0;
            _posX = _rng.Next(10);
            _posY = _rng.Next(10);
            // count of how many generations have been displayed
            _generationIndex = 0;
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _tiles = new List<SimulationSprite>();


            // open a file dialog box
            using (var folderDialog = new FolderBrowserDialog())
            {
                if (folderDialog.ShowDialog() == DialogResult.OK && !string.IsNullOrWhiteSpace(folderDialog.SelectedPath))
                {
                    // Get paths of all files in folder (full path)
                    _solutionFiles = Directory.GetFiles(folderDialog.SelectedPath);
                }
            }
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == Microsoft.Xna.Framework.Input.ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Escape))
                Exit();

            // read file on first gen before first move
            if (_generationIndex == 0 && _moveCount == 0) {
                _moves = this.readFile(_solutionFiles[_generationIndex]);
            }
            // reset moves if max is reached
            if (_moveCount == _maxMoves)
            {
                // -1 since generationIndex starts at 0
                if (_generationIndex < _solutionFiles.Length -1) {
                    // reset coords to rand position from 0 to 10
                    _posX = _rng.Next(10);
                    _posY = _rng.Next(10);
                    _moveCount = 0;
                    _currentScore = 0;
                    _generationIndex++;

                    // read next file for next gen
                    _moves = this.readFile(_solutionFiles[_generationIndex]);
                }
                else {
                    // exit when no more files left, may change later
                    Exit();
                }
        
            }

            // first move of each generation
            double points = 0;
            if (_moveCount == 0)
            {
                // new grid
                _grid = _robot.GenerateRandomTestGrid();
                // populate grid
                for(int i = 0; i < _grid.GetLength(0); i++){
                    for(int j = 0; j < _grid.GetLength(1); j++){

                        if(i == 3 && j == 5){
                        }
                        SimulationSprite s = new SimulationSprite(this, _grid[i,j], j * 40, i *40, false);
                        Components.Add(s);
                        _tiles.Add(s);

                }
            }

                // read solution file
                
                // call score for allele
                points = RobbyHelper.ScoreForAllele(_moves, _grid, _rng, ref _posX, ref _posY);
                _currentScore += points;
                _moveCount++;
            }
            else if (_moveCount > 0)
            {
            _tiles[ConvertCoordsToInt(_posX, _posY, 10)].IsRobby = false;

                points = RobbyHelper.ScoreForAllele(_moves, _grid, _rng, ref _posX, ref _posY);
                _currentScore += points;
                _moveCount++;
            }
            if(points == 10){
                _tiles[ConvertCoordsToInt(_posX, _posY, 10)].Grid = ContentsOfGrid.Empty;

            }
            _tiles[ConvertCoordsToInt(_posX, _posY, 10)].IsRobby = true;
            // add move delay as needed
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }

        private int ConvertCoordsToInt(int x, int y, int gridSize){
            return (y * gridSize) + x;
        }
        private int[] readFile(String filePath)
        {
            string contents = File.ReadAllText(filePath);

            // split file
            String[] fileElements = contents.Split(",", StringSplitOptions.RemoveEmptyEntries);

            // set last element from file to generation
            _currentGeneration = int.Parse(fileElements[0]);
            // index 1 is fitness, not needed

            // set max moves, before last element in file
            _maxMoves = int.Parse(fileElements[2]);
            // TODO take out uneeded values not part of moves

            // resize array, set array to be from index 3 to end inclusive
            fileElements = fileElements[3 .. fileElements.Length];
            // save the moves to an int[]
            int[] moves = Array.ConvertAll(fileElements, s => int.Parse(s));

            return moves;
        }
    }
}
