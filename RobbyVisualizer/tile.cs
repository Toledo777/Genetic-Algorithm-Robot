namespace RobbyVisualizer
{
    public class Tile{
        private bool _isCan;
        private bool _isRobby;
        public Tile(bool isCan, bool isRobby){
            this._isCan = isCan;
            this._isRobby = isRobby;
        }
        public bool IsCan{
            get{
                return this._isCan;
            }
            set{
                this._isCan = value;
            }
        }
        public bool IsRobby{
            get{
                return this._isRobby;
            }
            set{
                this._isRobby = value;
            }
        }
}
}