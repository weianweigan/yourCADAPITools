namespace yourCADAPITools
{
    public struct TextViewPosition
    {
        private readonly int _column;
        private readonly int _line;
        private readonly int _postion;
        public TextViewPosition(int line, int column, int postion)
        {
            _line = line;
            _column = column;
            _postion = postion;
        }

        public int Line { get { return _line; } }

        public int Column { get { return _column; } }

        public int Postion { get { return _postion; } }

        public static bool operator <(TextViewPosition a, TextViewPosition b)
        {
            if (a.Line < b.Line)
            {
                return true;
            }
            else if (a.Line == b.Line)
            {
                return a.Column < b.Column;
            }
            else
            {
                return false;
            }
        }

        public static bool operator >(TextViewPosition a, TextViewPosition b)
        {
            if (a.Line > b.Line)
            {
                return true;
            }
            else if (a.Line == b.Line)
            {
                return a.Column > b.Column;
            }
            else
            {
                return false;
            }
        }

        public static TextViewPosition Min(TextViewPosition a, TextViewPosition b)
        {
            return a > b ? b : a;
        }

        public static TextViewPosition Max(TextViewPosition a, TextViewPosition b)
        {
            return a > b ? a : b;
        }
    }
}
