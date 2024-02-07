using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace TicTacToe
{
    public partial class MainWindow : Window
    {
        private const int razmer = 9;
        private Button[] kletka;
        private bool hod;
        private bool Finishbro;

        private List<List<int>> kombinacia = new List<List<int>>()
        {
            new List<int> { 0, 1, 2 },
            new List<int> { 3, 4, 5 },
            new List<int> { 6, 7, 8 },
            new List<int> { 0, 3, 6 },
            new List<int> { 1, 4, 7 },
            new List<int> { 2, 5, 8 },
            new List<int> { 0, 4, 8 },
            new List<int> { 2, 4, 6 }
        };

        public MainWindow()
        {
            InitializeComponent();
            InitializeGame();
        }

        private void InitializeGame()
        {
            kletka = new Button[razmer];
            for (int i = 0; i < razmer; i++)
            {
                kletka[i] = new Button();
                kletka[i].Content = "";
                kletka[i].Click += Button_Click;
                kletka[i].Tag = i;
                kletka[i].IsEnabled = false;
                Grid.SetRow(kletka[i], i / 3);
                Grid.SetColumn(kletka[i], i % 3);
                gameGrid.Children.Add(kletka[i]);
            }

            hod = true;
            Finishbro = false;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Finishbro)
                return;

            Button button = (Button)sender;
            int index = (int)button.Tag;

            if (button.Content.ToString() == "")
            {
                if (hod)
                {
                    button.Content = "X";
                    CheckForWin("X");
                    hod = false;
                    MakeRobotMove();
                }
            }
        }

        private void MakeRobotMove()
        {
            if (Finishbro)
                return;

            Random random = new Random();
            int index;

            do
            {
                index = random.Next(razmer);
            } while (kletka[index].Content.ToString() != "");

            kletka[index].Content = "O";
            CheckForWin("O");
            hod = true;
        }

        private void CheckForWin(string simvol)
        {
            foreach (List<int> combination in kombinacia)
            {
                if (kletka[combination[0]].Content.ToString() == simvol &&
                    kletka[combination[1]].Content.ToString() == simvol &&
                    kletka[combination[2]].Content.ToString() == simvol)
                {
                    Finishbro = true;
                    Disablekletka();
                    MessageBox.Show(simvol + " Победка!");
                    return;
                }
            }

            if (IsBoardFull())
            {
                Finishbro = true;
                Disablekletka();
                MessageBox.Show("ничья!");
                return;
            }

            if (kletka[0].Content.ToString() == simvol &&
                kletka[4].Content.ToString() == simvol &&
                kletka[8].Content.ToString() == simvol)
            {
                Finishbro = true;
                Disablekletka();
                MessageBox.Show(simvol + " Победка!");
                return;
            }

            if (kletka[2].Content.ToString() == simvol &&
                kletka[4].Content.ToString() == simvol &&
                kletka[6].Content.ToString() == simvol)
            {
                Finishbro = true;
                Disablekletka();
                MessageBox.Show(simvol + " Победка!");
                return;
            }

            for (int i = 0; i < 3; i++)
            {
                if (kletka[i].Content.ToString() == simvol &&
                    kletka[i + 3].Content.ToString() == simvol &&
                    kletka[i + 6].Content.ToString() == simvol)
                {
                    Finishbro = true;
                    Disablekletka();
                    MessageBox.Show(simvol + " Победка!");
                    return;
                }
            }

            for (int i = 0; i < 7; i += 3)
            {
                if (kletka[i].Content.ToString() == simvol &&
                    kletka[i + 1].Content.ToString() == simvol &&
                    kletka[i + 2].Content.ToString() == simvol)
                {
                    Finishbro = true;
                    Disablekletka();
                    MessageBox.Show(simvol + " Победка!");
                    return;
                }
            }

            if (simvol == "X")
            {
                Finishbro = true;
                Disablekletka();
                MessageBox.Show("Лузер!!!!!!!!!!!!");
            }
        }

        private void Disablekletka()
        {
            foreach (Button button in kletka)
            {
                button.IsEnabled = false;
            }
        }

        private bool IsBoardFull()
        {
            foreach (Button button in kletka)
            {
                if (button.Content.ToString() == "")
                    return false;
            }
            return true;
        }

        private void NewGameButton_Click(object sender, RoutedEventArgs e)
        {
            gameGrid.Children.Clear();
            InitializeGame();
        }
    }
}