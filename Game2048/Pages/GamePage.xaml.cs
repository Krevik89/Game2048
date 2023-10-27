using Game2048.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Game2048
{
    /// <summary>
    /// Логика взаимодействия для MenuPage.xaml
    /// </summary>
    public partial class GamePage : Window
    {
        GameLogic logic = new GameLogic();
        private TextBox[,] textBoxArray;
        MenuPage menuPage;
        private int Score1 = 0; 
        private int Score2 = 0;

        public GamePage()
        {
            InitializeComponent();
            logic.FillArray();

            textBoxArray = new TextBox[,]
            {
                { null_null, null_one, null_two, null_three },
                { one_null, one_one, one_two, one_three },
                { two_null, two_one, two_two, two_three },
                { three_null, three_one, three_two, three_three }
            };
            UpdateTextBoxValues();

            // Привязка значений из массива к TextBox
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    int cellValue = logic.GetCellValue(i, j);
                    textBoxArray[i, j].Text = cellValue != 0 ? cellValue.ToString() : string.Empty;
                }
            }
            this.PreviewKeyDown += GamePage_PreviewKeyDown;

            
        }

        private void GamePage_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            bool moved = false; // Флаг для указания, было ли выполнено смещение или объединение

            switch (e.Key)
            {
                case Key.Left:
                    moved = logic.MoveLeft();                                    
                    logic.FillArray();
                    break;
                case Key.Right:
                    moved = logic.MoveRight();                    
                    logic.FillArray();
                    break;
                case Key.Up:
                    moved = logic.MoveUp();                   
                    logic.FillArray();
                    break;
                case Key.Down:
                    moved = logic.MoveDown();                  
                    logic.FillArray();
                    break;
            }

            if (moved)
            {
                
                // Обновление значений в TextBox после операции сдвига или объединения
                if (logic.IsGameOver() == false)
                {
                    e.Handled = true;
                    int maxNumber = GetMaxNumber();
                    Score1 = Math.Max(Score1, maxNumber);

                    // Обновляем TextBox с текущим счетом
                    UpdateTextBoxValues();
                }                                                                  
                else
                {
                    MessageBox.Show("Игра окончена!");
                    if (Score1 > Score2)
                    {
                        Score2 = Score1;
                    }

                    Score1 = 0;

                    UpdateTextBoxValues();

                    menuPage = new MenuPage();
                    Hide();
                    menuPage.Show();
                }                              
            }           
        }

        private void UpdateTextBoxValues()
        {
            for (int row = 0; row < 4; row++)
            {
                for (int col = 0; col < 4; col++)
                {
                    int cellValue = logic.GetCellValue(row, col);
                    textBoxArray[row, col].Text = cellValue != 0 ? cellValue.ToString() : string.Empty;
                }
            }
            Score.Text = Score1.ToString();
            HScore.Text = Score2.ToString();
        }

        private void SetTextBoxValue(int row, int column, int value)
        {
            TextBox textBox = textBoxArray[row, column];
            if (textBox != null)
            {
                textBox.Text = value.ToString();
            }
        }

        private int GetMaxNumber()
        {
            int maxNumber = 0;

            foreach (TextBox textBox in textBoxArray)
            {
                if (!string.IsNullOrEmpty(textBox.Text) && int.TryParse(textBox.Text, out int number))
                {
                    maxNumber = Math.Max(maxNumber, number);
                }
            }

            return maxNumber;
        }
    }
}
