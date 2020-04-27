using System;
using System.Collections.Generic;
using System.Text;

namespace GameBoard
{
    class BoardException : ApplicationException
    {
        public BoardException(string message) : base(message)
        {
        }
    }
}
