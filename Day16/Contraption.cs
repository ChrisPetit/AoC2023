namespace Day16;

public class Contraption()
{
    private readonly char[,]? _contraption;
    private readonly Dictionary<(int, int), bool> _energizedCells = new();
    private List<Beam> _beams = [];
    
    private const char MirrorSlantedLeft = '/';
    private const char MirrorSlantedRight = '\\';
    private const char SplitterVertical = '|';
    private const char SplitterHorizontal = '-';
    private const char Empty = '.';
    
    
    private readonly Dictionary<(int x, int y, Direction dir), List<Beam>> _memo = new();

    public Contraption(IReadOnlyList<string> input) : this()
    {
        _contraption = ConvertStringArrayTo2DCharArray(input);
        _beams.Add(new Beam(-1, 0, Direction.Right));
    }
    
    public int Energize()
    {
        while (_beams.Count > 0)
        {
            var newBeams = new List<Beam>();
            // process all current beams
            foreach (var beam in _beams)
            {
                MoveBeam(beam);
                
                if (!IsInsideGrid(beam.X, beam.Y))
                    continue;

                var tile = _contraption![beam.Y, beam.X]; // Get what is at the beam's current position
                HandleTile(tile, beam, newBeams); // Let the beam behave based on the tile
                EnergizeCell(beam.X, beam.Y);
            }
            // replace the list of beams with the new beams for the next step
            _beams = newBeams;
        }

        // Count how many cells got energized
        var energizedCellsCount = _energizedCells.Count(x => x.Value.Equals(true));
        // Clear _energizedCells for the next simulation
        _energizedCells.Clear();
        // returns the total number of energized cells
        return energizedCellsCount;
    }
    
    private static char[,] ConvertStringArrayTo2DCharArray(IReadOnlyList<string> arr)
    {
        var rows = arr.Count;
        var cols = arr[0].Length;
    
        var twoDArray = new char[rows, cols];
    
        for (var y = 0; y < rows; y++)
        {
            for (var x = 0; x < cols; x++)
            {
                twoDArray[x, y] = arr[x][y];
            }
        }
    
        return twoDArray;
    }
    
    private static void MoveBeam(Beam beam)
    {
        switch (beam.Direction)
        {
            case Direction.Right:
                beam.X += 1;
                break;
            case Direction.Down:
                beam.Y += 1;
                break;
            case Direction.Left:
                beam.X -= 1;
                break;
            case Direction.Up:
                beam.Y -= 1;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
    
    private void HandleTile(char tile, Beam beam, List<Beam> newBeams)
     {
         var key = (beam.X, beam.Y, beam.Direction);

         if (_memo.TryGetValue(key, out var value))
         {
             newBeams.AddRange(value);
             return;
         }

         List<Beam> beams = [];
         
         switch (tile)
         {
             case Empty:
                 newBeams.Add(beam);
                 break;
             case MirrorSlantedLeft:
                 HandleMirrorSlantedLeft(beam);
                 newBeams.Add(beam);
                 break;
             case MirrorSlantedRight:
                 HandleMirrorSlantedRight(beam);
                 newBeams.Add(beam);
                 break;
             case SplitterVertical:
                 HandleSplitterVertical(beam, newBeams);
                 break;
             case SplitterHorizontal:
                 HandleSplitterHorizontal(beam, newBeams);
                 break;
         }
         
         _memo[key] = beams; // Store computed beams in memo

         newBeams.AddRange(beams);
     }

    private static void HandleMirrorSlantedLeft(Beam beam)
    {
        beam.Direction = beam.Direction switch
        {
            Direction.Right => Direction.Up,
            Direction.Down => Direction.Left,
            Direction.Left => Direction.Down,
            Direction.Up => Direction.Right,
            _ => beam.Direction
        };
    }

    private static void HandleMirrorSlantedRight(Beam beam)
    {
        beam.Direction = beam.Direction switch
        {
            Direction.Right => Direction.Down,
            Direction.Down => Direction.Right,
            Direction.Left => Direction.Up,
            Direction.Up => Direction.Left,
            _ => beam.Direction
        };
    }

    private static void HandleSplitterVertical(Beam beam, List<Beam> newBeams)
    {
        switch (beam.Direction)
        {
            case Direction.Left:
            case Direction.Right:
                // Add a beam going upwards and downwards
                newBeams.Add(new Beam(beam.X, beam.Y, Direction.Up));
                newBeams.Add(new Beam(beam.X, beam.Y, Direction.Down));
                break;
            case Direction.Up:
                newBeams.Add(new Beam(beam.X, beam.Y, Direction.Down));
                newBeams.Add(beam);
                break;
            case Direction.Down:
                newBeams.Add(new Beam(beam.X, beam.Y , Direction.Up));
                newBeams.Add(beam);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private static void HandleSplitterHorizontal(Beam beam, List<Beam> newBeams)
    {
        switch (beam.Direction)
        {
            case Direction.Up:
            case Direction.Down:
                // Add a beam going right and left
                newBeams.Add(new Beam(beam.X , beam.Y, Direction.Left));
                newBeams.Add(new Beam(beam.X, beam.Y, Direction.Right));
                break;
            case Direction.Right:
                newBeams.Add(new Beam(beam.X, beam.Y, Direction.Left));
                newBeams.Add(beam);
                break;
            case Direction.Left:
                newBeams.Add(new Beam(beam.X, beam.Y, Direction.Right));
                newBeams.Add(beam);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void EnergizeCell(int x, int y)
    {
        _energizedCells[(x, y)] = true;
    }
    
    private bool IsInsideGrid(int x, int y)
    {
        return x >= 0 && x < _contraption!.GetLength(1) && y >= 0 && y < _contraption.GetLength(0);
    }
}