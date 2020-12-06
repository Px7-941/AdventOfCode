namespace AdventOfCode
{
    internal interface IPuzzle
    {
        string PuzzleName { get; }
        void Load();
        void Solve();
    }
}
