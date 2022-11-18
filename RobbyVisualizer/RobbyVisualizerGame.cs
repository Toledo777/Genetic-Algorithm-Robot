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
        private double _timer = 0;
        private SpriteFont _font;
        readonly int delay = 100; //delay between each game tick.

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
            // Font
            this._font = Content.Load<SpriteFont>("Font");
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
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
            // Delay for Robby to slow down
            if (gameTime.TotalGameTime.TotalMilliseconds >= _timer)
            {
                // read file on first gen before first move
                if (_generationIndex == 0 && _moveCount == 0)
                {
                    _moves = this.readFile(_solutionFiles[_generationIndex]);
                }
                // reset moves if max is reached
                if (_moveCount == _maxMoves)
                {
                    Reset();
                }

                // first move of each generation
                if (_moveCount == 0)
                {
                    InitializeGrid();
                }
                else if (_moveCount > 0)
                {
                    MoveRobby();
                }

                _timer += delay;
            }
            base.Update(gameTime);

        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();
            _spriteBatch.DrawString(_font, "Generation: " + _currentGeneration, new Vector2(500, 0), Color.White);
            _spriteBatch.DrawString(_font, "Score: " + _currentScore, new Vector2(500, 40), Color.White);
            _spriteBatch.DrawString(_font, $"Moves: {_moveCount}/{_maxMoves}", new Vector2(500, 80), Color.White);
            _spriteBatch.End();
            base.Draw(gameTime);
        }

        private int ConvertCoordsToInt(int x, int y, int gridSize)
        {
            return (y * gridSize) + x;
        }

        private void Reset()
        {
            // -1 since generationIndex starts at 0
            if (_generationIndex < _solutionFiles.Length - 1)
            {
                // reset coords to rand position from 0 to 10
                _posX = _rng.Next(10);
                _posY = _rng.Next(10);
                _moveCount = 0;
                _currentScore = 0;
                _generationIndex++;

                // read next file for next gen
                _moves = this.readFile(_solutionFiles[_generationIndex]);
            }
            else
            {
                // _generationIndex = 0;
                // _moveCount = 0;
                // _currentScore = 0;
            }
        }
        /// <summary>
        /// Moves Robby according to ScoreForAllele
        /// </summary>
        private void MoveRobby()
        {
            // Update Robyy Position
            _tiles[ConvertCoordsToInt(_posX, _posY, 10)].IsRobby = false;

            /**
                In Here you need to switch the x and y to match the teachers ScireFirAllele .
            */
            double points = RobbyHelper.ScoreForAllele(_moves, _grid, _rng, ref _posY, ref _posX);
            _currentScore += points;
            _moveCount++;
            //The only way Robby can score points is by picking up a can, which grants 10 points. 
            //Thus we know this is what he has done.
            if (points == 10)
            {
                _tiles[ConvertCoordsToInt(_posX, _posY, 10)].Square = ContentsOfGrid.Empty;
                _grid[_posX, _posY] = ContentsOfGrid.Empty;
            }

            _tiles[ConvertCoordsToInt(_posX, _posY, 10)].IsRobby = true;
        }
        /// <summary>
        /// Called on the first move to create the grid through which Robby will move. 
        /// </summary>
        private void InitializeGrid()
        {
            //don't need to store previous grids
            Components.Clear();
            _tiles = new List<SimulationSprite>();
            // new grid
            _grid = _robot.GenerateRandomTestGrid();
            // populate sprite grid
            for (int i = 0; i < _grid.GetLength(0); i++)
            {
                for (int j = 0; j < _grid.GetLength(1); j++)
                {
                    SimulationSprite s = new SimulationSprite(this, _grid[i, j], j * 40, i * 40, false);
                    Components.Add(s);
                    _tiles.Add(s);
                }
            }

            // Set Previous Robby Position
            int prevX = _posX, prevY = _posY;
            // Set Initial Robby Position
            _tiles[ConvertCoordsToInt(_posX, _posY, 10)].IsRobby = true;

            // call score for allele
            /**
                In Here you need to switch the x and y to match the teachers ScireFirAllele .
            */
            double points = RobbyHelper.ScoreForAllele(_moves, _grid, _rng, ref _posY, ref _posX);

            // Update Robby Position
            _tiles[ConvertCoordsToInt(prevX, prevY, 10)].IsRobby = false;
            _tiles[ConvertCoordsToInt(_posX, _posY, 10)].IsRobby = true;

            _currentScore += points;
            _moveCount++;
        }
        /// <summary>
        /// Reads a solution file and set variables accordingly.
        /// </summary>
        /// <param name="filePath">The path file to read</param>
        /// <returns>int[] representing the genes of that solution </returns>
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
            fileElements = fileElements[3..fileElements.Length];
            // save the moves to an int[]
            int[] moves = Array.ConvertAll(fileElements, s => int.Parse(s));

            return moves;
        }
    }
}
