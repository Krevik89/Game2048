using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game2048
{
    internal class GameLogic
    {
        private int[,] array;
        private const int Length = 4;
        private Random random;
        public GameLogic()
        {
            array = new int[Length, Length];
            FillArray();           
            Initialize();
        }
        private void Initialize()
        {
            // Генерируем несколько чисел при начале игры
            for (int i = 0; i < 3; i++) // Измените количество генерируемых чисел, если необходимо
            {
                FillArray();
            }
        }

        public void FillArray()
        {
            random = new Random();
            List<int[]> emptyCells = new List<int[]>();

            for (int row = 0; row < array.GetLength(0); row++)
            {
                for (int col = 0; col < array.GetLength(1); col++)
                {
                    if (array[row, col] == 0)
                    {
                        emptyCells.Add(new int[] { row, col });
                    }
                }
            }

            if (emptyCells.Count > 0)
            {
                int randomCellIndex = random.Next(emptyCells.Count);
                int[] randomCell = emptyCells[randomCellIndex];

                // Генерация значения 2 с вероятностью 90% и значения 4 с вероятностью 10%
                int randomValue = random.Next(1, 101) <= 90 ? 2 : 4;

                array[randomCell[0], randomCell[1]] = randomValue;
            }
        }

        public int GetCellValue(int row, int col)
        {
            return array[row, col];
        }

        public bool MoveLeft()
        {
            bool moved = false;

            for (int row = 0; row < Length; row++)
            {
                for (int col = 1; col < Length; col++)
                {
                    if (array[row, col] != 0)
                    {
                        int mergeIndex = col;
                        while (mergeIndex > 0 && array[row, mergeIndex - 1] == 0)
                        {
                            // Сдвиг числа к стенке влево
                            array[row, mergeIndex - 1] = array[row, mergeIndex];
                            array[row, mergeIndex] = 0;
                            mergeIndex--;
                            moved = true;
                        }
                        if (mergeIndex > 0 && array[row, mergeIndex - 1] == array[row, mergeIndex])
                        {
                            // Объединение чисел, если возможно
                            array[row, mergeIndex - 1] += array[row, mergeIndex];
                            array[row, mergeIndex] = 0;
                            moved = true;
                        }
                    }
                }
            }

            if (moved)
            {
                FillArray();
            }

            return moved;
        }

        public bool MoveRight()
        {
            bool moved = false;

            for (int row = 0; row < Length; row++)
            {
                for (int col = Length - 2; col >= 0; col--)
                {
                    if (array[row, col] != 0)
                    {
                        int mergeIndex = col;
                        while (mergeIndex < Length - 1 && array[row, mergeIndex + 1] == 0)
                        {
                            // Сдвиг числа к стенке вправо
                            array[row, mergeIndex + 1] = array[row, mergeIndex];
                            array[row, mergeIndex] = 0;
                            mergeIndex++;
                            moved = true;
                        }
                        if (mergeIndex < Length - 1 && array[row, mergeIndex + 1] == array[row, mergeIndex])
                        {
                            // Объединение чисел, если возможно
                            array[row, mergeIndex + 1] += array[row, mergeIndex];
                            array[row, mergeIndex] = 0;
                            moved = true;
                        }
                    }
                }
            }

            if (moved)
            {
                FillArray();
            }

            return moved;
        }

        public bool MoveUp()
        {
            bool moved = false;

            for (int col = 0; col < Length; col++)
            {
                for (int row = 1; row < Length; row++)
                {
                    if (array[row, col] != 0)
                    {
                        int mergeIndex = row;
                        while (mergeIndex > 0 && array[mergeIndex - 1, col] == 0)
                        {
                            // Сдвиг числа вверх
                            array[mergeIndex - 1, col] = array[mergeIndex, col];
                            array[mergeIndex, col] = 0;
                            mergeIndex--;
                            moved = true;
                        }
                        if (mergeIndex > 0 && array[mergeIndex - 1, col] == array[mergeIndex, col])
                        {
                            // Объединение чисел, если возможно
                            array[mergeIndex - 1, col] += array[mergeIndex, col];
                            array[mergeIndex, col] = 0;
                            moved = true;
                        }
                    }
                }
            }

            if (moved)
            {
                FillArray();
            }

            return moved;
        }

        public bool MoveDown()
        {
            bool moved = false;

            for (int col = 0; col < Length; col++)
            {
                for (int row = Length - 2; row >= 0; row--)
                {
                    if (array[row, col] != 0)
                    {
                        int mergeIndex = row;
                        while (mergeIndex < Length - 1 && array[mergeIndex + 1, col] == 0)
                        {
                            // Сдвиг числа вниз
                            array[mergeIndex + 1, col] = array[mergeIndex, col];
                            array[mergeIndex, col] = 0;
                            mergeIndex++;
                            moved = true;
                        }
                        if (mergeIndex < Length - 1 && array[mergeIndex + 1, col] == array[mergeIndex, col])
                        {
                            // Объединение чисел, если возможно
                            array[mergeIndex + 1, col] += array[mergeIndex, col];
                            array[mergeIndex, col] = 0;
                            moved = true;
                        }
                    }
                }
            }

            if (moved)
            {
                FillArray();
            }

            return moved;
        }

        public bool IsGameOver()
        {
            // Проверка наличия пустых ячеек
            for (int row = 0; row < Length; row++)
            {
                for (int col = 0; col < Length; col++)
                {
                    if (array[row, col] == 0)
                    {
                        return false; // Есть пустая ячейка, игра не завершена
                    }
                }
            }

            // Проверка наличия возможных сдвигов
            for (int row = 0; row < Length; row++)
            {
                for (int col = 0; col < Length; col++)
                {
                    int current = array[row, col];

                    // Проверка соседних ячеек на равенство текущему числу
                    if ((row < Length - 1 && array[row + 1, col] == current) ||
                        (col < Length - 1 && array[row, col + 1] == current))
                    {
                        return false; // Есть возможность сдвига, игра не завершена
                    }
                }
            }

            return true; // Нет пустых ячеек и возможных сдвигов, игра завершена
        }

        private int FindMergeIndex(int row, int col, int rowOffset, int colOffset)
        {
            int mergeIndex = col;

            while (mergeIndex + colOffset >= 0 && mergeIndex + colOffset < Length &&
                   row + rowOffset >= 0 && row + rowOffset < Length &&
                   array[row + rowOffset, mergeIndex + colOffset] == 0)
            {
                mergeIndex += colOffset;
            }

            return mergeIndex;
        }

    }
}
