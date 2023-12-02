using System;
using System.Collections.Generic;
/*
// Класс для решения лабиринта
class LabyrinthSolver
{
    private Stack<Tuple<int, int>> _path = new Stack<Tuple<int, int>>();
    private int[,] _labyrinth;

    // Конструктор класса, принимает лабиринт
    public LabyrinthSolver(int[,] labyrinth)
    {
        _labyrinth = labyrinth;
    }

    // Метод для запуска процесса поиска пути в лабиринте
    public void Solve(int startI, int startJ)
    {
        Console.WriteLine($"Поиск пути в лабиринте:");
        if (FindPath(startI, startJ))
        {
            Console.WriteLine($"Путь найден!");
        }
        else
        {
            Console.WriteLine($"Пути нет.");
        }
    }

    // Рекурсивный метод поиска пути в лабиринте
    private bool FindPath(int i, int j)
    {
        Console.WriteLine(_labyrinth[i, j]);

        if (_labyrinth[i, j] == 0)
            _path.Push(new(i, j));

        bool pathFound = false;

        while (_path.Count > 0)
        {
            var current = _path.Pop();

            Console.WriteLine($"{current.Item1},{current.Item2} ");

            if (_labyrinth[current.Item1, current.Item2] == 0)
            {
                Console.WriteLine($"Путь найден {current.Item1},{current.Item2} ");
                pathFound = true;  // Установка флага, что хотя бы один путь найден

                // Продолжение поиска других путей
                ExploreNeighbours(current.Item1, current.Item2);
            }

            _labyrinth[current.Item1, current.Item2] = 1;
        }

        if (pathFound)
        {
            Console.WriteLine("Найдены все возможные выходы.");
            return true;
        }

        Console.WriteLine("Пути нет");
        return false;
    }


    // Исследование соседей текущей позиции
    private void ExploreNeighbours(int i, int j)
    {
        ExploreNeighbour(i + 1, j);
        ExploreNeighbour(i, j + 1);
        ExploreNeighbour(i - 1, j);
        ExploreNeighbour(i, j - 1);
    }

    // Исследование конкретного соседа
    private void ExploreNeighbour(int i, int j)
    {
        // Проверка, находится ли сосед в пределах лабиринта и не является ли стеной
        if (IsWithinBounds(i, j) && _labyrinth[i, j] != 1)
            _path.Push(new(i, j));
    }

    // Проверка, находится ли позиция в пределах лабиринта
    private bool IsWithinBounds(int i, int j)
    {
        return i >= 0 && i < _labyrinth.GetLength(0) && j >= 0 && j < _labyrinth.GetLength(1);
    }
}
*/
/*
using System;
using System.Collections.Generic;

class LabyrinthSolver
{
    private Stack<Tuple<int, int>> _path = new Stack<Tuple<int, int>>();
    private int[,] _labyrinth;

    private static readonly int[] DeltaI = { 0, -1, 0, 1 };  // Смещение по вертикали (вверх, вниз)
    private static readonly int[] DeltaJ = { 1, 0, -1, 0 };  // Смещение по горизонтали (вправо, влево)

    public LabyrinthSolver(int[,] labyrinth)
    {
        _labyrinth = labyrinth;
    }

    public void Solve()
    {
        Console.WriteLine("Поиск путей в лабиринте:");
        FindAllPaths();
    }

    private void FindAllPaths()
    {
        for (int i = 0; i < _labyrinth.GetLength(0); i++)
        {
            for (int j = 0; j < _labyrinth.GetLength(1); j++)
            {
                if (_labyrinth[i, j] == 0)
                {
                    Console.WriteLine($"Найден выход: {i},{j}");
                    ExploreNeighbours(i, j);
                }
            }
        }
    }

    private void ExploreNeighbours(int i, int j)
    {
        int currentDirection = 2;  // Начальное направление (вправо)

        _path.Clear();
        _path.Push(new Tuple<int, int>(i, j));

        while (_path.Count > 0)
        {
            var current = _path.Pop();

            Console.WriteLine($"{current.Item1},{current.Item2} ");

            _labyrinth[current.Item1, current.Item2] = 1;

            for (int dir = 0; dir < 4; dir++)
            {
                int newI = current.Item1 + DeltaI[dir];
                int newJ = current.Item2 + DeltaJ[dir];

                if (IsWithinBounds(newI, newJ) && _labyrinth[newI, newJ] == 0)
                {
                    _path.Push(new Tuple<int, int>(newI, newJ));
                    currentDirection = dir;  // Обновление текущего направления
                }
            }
        }
    }

    private bool IsWithinBounds(int i, int j)
    {
        return i >= 0 && i < _labyrinth.GetLength(0) && j >= 0 && j < _labyrinth.GetLength(1);
    }
}
*/
/*
using System;
using System.Collections.Generic;
using System.Linq;

class LabyrinthSolver
{
    private List<List<Tuple<int, int>>> _allPaths = new List<List<Tuple<int, int>>>();
    private Stack<Tuple<int, int>> _path = new Stack<Tuple<int, int>>();
    private int[,] _labyrinth;

    private static readonly int[] DeltaI = { 0, -1, 0, 1 };  // Смещение по вертикали (вверх, вниз)
    private static readonly int[] DeltaJ = { 1, 0, -1, 0 };  // Смещение по горизонтали (вправо, влево)

    public LabyrinthSolver(int[,] labyrinth)
    {
        _labyrinth = labyrinth;
    }

    public void Solve()
    {
        Console.WriteLine("Поиск всех возможных выходов в лабиринте:");
        FindAllPaths();
        DisplayAllPaths();
    }

    private void FindAllPaths()
    {
        for (int i = 0; i < _labyrinth.GetLength(0); i++)
        {
            for (int j = 0; j < _labyrinth.GetLength(1); j++)
            {
                if (_labyrinth[i, j] == 0)
                {
                    Console.WriteLine($"Найден выход: {i},{j}");
                    ExploreNeighbours(i, j);
                }
            }
        }
    }

    private void ExploreNeighbours(int i, int j)
    {
        int currentDirection = 2;  // Начальное направление (вправо)

        _path.Clear();
        _path.Push(new Tuple<int, int>(i, j));

        while (_path.Count > 0)
        {
            var current = _path.Peek();

            _labyrinth[current.Item1, current.Item2] = 1;

            bool moved = false;

            for (int dir = 0; dir < 4; dir++)
            {
                int newI = current.Item1 + DeltaI[dir];
                int newJ = current.Item2 + DeltaJ[dir];

                if (IsWithinBounds(newI, newJ) && _labyrinth[newI, newJ] == 0)
                {
                    _path.Push(new Tuple<int, int>(newI, newJ));
                    currentDirection = dir;  // Обновление текущего направления
                    moved = true;
                    break;  // Прерываем цикл, чтобы двигаться только в одном направлении
                }
            }

            if (!moved)
            {
                // Завершаем текущий путь и сохраняем его
                List<Tuple<int, int>> currentPath = new List<Tuple<int, int>>(_path.Reverse());
                _allPaths.Add(currentPath);

                // Возвращаемся на шаг назад
                _path.Pop();
            }
        }
    }


    private void DisplayAllPaths()
    {
        if (_allPaths.Count == 0)
        {
            Console.WriteLine("Выходы не найдены.");
            return;
        }

        Console.WriteLine("Все возможные пути:");

        foreach (var path in _allPaths)
        {
            foreach (var point in path)
            {
                Console.Write($"{point.Item1},{point.Item2} -> ");
            }
            Console.WriteLine();
        }
    }

    private bool IsWithinBounds(int i, int j)
    {
        return i >= 0 && i < _labyrinth.GetLength(0) && j >= 0 && j < _labyrinth.GetLength(1);
    }
}
*/

class LabyrinthSolver
{
    private List<List<Tuple<int, int>>> _allPaths = new List<List<Tuple<int, int>>>();
    private Stack<Tuple<int, int>> _path = new Stack<Tuple<int, int>>();
    private int[,] _labyrinth;

    private static readonly int[] DeltaI = { 0, -1, 0, 1 };  // Смещение по вертикали (вверх, вниз)
    private static readonly int[] DeltaJ = { 1, 0, -1, 0 };  // Смещение по горизонтали (вправо, влево)

    public LabyrinthSolver(int[,] labyrinth)
    {
        _labyrinth = labyrinth;
    }

    public void Solve()
    {
        Console.WriteLine("Поиск всех возможных выходов в лабиринте:");
        FindAllPaths();
        DisplayAllPaths();
    }

    private void FindAllPaths()
    {
        for (int i = 0; i < _labyrinth.GetLength(0); i++)
        {
            for (int j = 0; j < _labyrinth.GetLength(1); j++)
            {
                if (_labyrinth[i, j] == 0)
                {
                    Console.WriteLine($"Найден выход: {i},{j}");
                    ExploreNeighbours(i, j);
                }
            }
        }
    }

    private void ExploreNeighbours(int startI, int startJ)
    {
        int currentDirection = 0;  // Начальное направление (вправо)

        _path.Clear();
        _path.Push(new Tuple<int, int>(startI, startJ));

        while (_path.Count > 0)
        {
            var current = _path.Peek();
            int i = current.Item1;
            int j = current.Item2;

            _labyrinth[i, j] = 1;

            bool moved = false;

            for (int dir = 0; dir < 4; dir++)
            {
                int newI = i + DeltaI[currentDirection];
                int newJ = j + DeltaJ[currentDirection];

                // Изменение направления налево
                currentDirection = (currentDirection + 3) % 4;

                if (IsWithinBounds(newI, newJ) && _labyrinth[newI, newJ] == 0)
                {
                    _path.Push(new Tuple<int, int>(newI, newJ));
                    moved = true;
                    break;
                }
            }

            if (!moved)
            {
                // Возвращаемся на шаг назад
                _path.Pop();
            }
        }
    }

    private void DisplayAllPaths()
    {
        if (_allPaths.Count == 0)
        {
            Console.WriteLine("Выходы не найдены.");
            return;
        }

        Console.WriteLine("Все возможные пути:");

        foreach (var path in _allPaths)
        {
            foreach (var point in path)
            {
                Console.Write($"{point.Item1},{point.Item2} -> ");
            }
            Console.WriteLine();
        }
    }

    private bool IsWithinBounds(int i, int j)
    {
        return i >= 0 && i < _labyrinth.GetLength(0) && j >= 0 && j < _labyrinth.GetLength(1);
    }
}

