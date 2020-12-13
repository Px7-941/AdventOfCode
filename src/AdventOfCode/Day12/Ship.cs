using System;

namespace AdventOfCode.Day12
{
    public partial class PuzzleDay12
    {
        public sealed class Ship
        {
            private int X { get; set; }
            private int Y { get; set; }
            public int Dir { get; private set; }
            public int WayX { get; private set; } = 10;
            public int WayY { get; private set; } = 1;

            public void Move(int dir, int val)
            {
                (X, Y) = dir switch
                {
                    0 => (X + val, Y),
                    1 => (X, Y - val),
                    2 => (X - val, Y),
                    3 => (X, Y + val),
                    _ => (X, Y)
                };
            }

            public void MoveWaypoint(int dir, int val)
            {
                (WayX, WayY) = dir switch
                {
                    0 => (WayX + val, WayY),
                    1 => (WayX, WayY - val),
                    2 => (WayX - val, WayY),
                    3 => (WayX, WayY + val),
                    _ => (WayX, WayY)
                };
            }

            public void MoveForward(int val)
            {
                Move(Dir, val);
            }

            public void MoveForwardWaypoint(int val)
            {
                X += WayX * val;
                Y += WayY * val;
            }

            public void Rotate(int val)
            {
                Dir = (Dir + val) % 4;
            }

            public void RotateWaypoint(int val)
            {
                (WayX, WayY) = val switch
                {
                    0 => (WayX, WayY),
                    1 => (WayY, -WayX),
                    2 => (-WayX, -WayY),
                    3 => (-WayY, WayX),
                    _ => (WayX, WayY)
                };
            }

            public long ManhattanDistance()
            {
                return (Math.Abs(X) + Math.Abs(Y));
            }
        }
    }
}
