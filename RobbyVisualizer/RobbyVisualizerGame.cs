using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.IO;
using RobbyTheRobot;
using System.Windows.Forms;

namespace RobbyVisualizer
{
    public class RobbyVisualizerGame : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private IRobbyTheRobot _robot;
        private ContentsOfGrid[,] _grid;
        private int[] _moves;
        private int _moveCount;
        private int _currentScore;
        private int _maxMoves;
        private int _posX;
        private int _posY;
        private int _currentGeneration;
        private Random _rng;

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
            _robot = Robby.createRobby(1, 2, 3, 4, 5, 6.0, 7.0);
            Random _rng = new Random();
            _moveCount = 0;
            _currentScore = 0;
            _maxMoves = _robot.NumberOfActions;
            _posX = _rng(10);
            _posY = _rng(10);
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            var fileContent = string.Empty;
            var folderPath = string.Empty;

            // open a file dialog box
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    folderPath = openFileDialog.FileName;

                    //Read the contents of the file into a stream
                    var fileStream = openFileDialog.OpenFile();

                    using (StreamReader reader = new StreamReader(fileStream))
                    {
                        fileContent = reader.ReadToEnd();
                    }
                }
            }

            // split string by comma
            String[] fileElements = fileContent.Split(",", StringSplitOptions.RemoveEmptyEntries);
            // TODO take out uneeded values not part of moves
            // send the moves to an int[]
            int[] robbyMoves = Array.ConvertAll(fileElements, s => int.Parse(s));
            // MessageBox.Show(fileContent, "File Content at path: " + filePath, MessageBoxButtons.OK);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == Microsoft.Xna.Framework.Input.ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Escape))
                Exit();
            // reset moves if max is reached
            if (_moveCount == _maxMoves) {
                // reset coords to rand position from 0 to 10
                _posX = _rng.Next(10);
                _posY = _rng.Next(10);
                _moveCount = 0;
                _currentScore = 0;
                // TODO, read next file
            }

            // first move of generation
            if (_moveCount == 0) {
                _grid = _robot.GenerateRandomTestGrid();
                // call score for allele
                _currentScore += RobbyHelper.ScoreForAllele(_moves, _grid, _rng, ref _posX, ref _posY);
                _moveCount++;
            }

            else if (_moveCount > 0) {
                _currentScore += RobbyHelper.ScoreForAllele(_moves, _grid, _rng, ref _posX, ref _posY);
                _moveCount++;
            }


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
