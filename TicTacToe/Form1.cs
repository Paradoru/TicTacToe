using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace TicTacToe
{
    public partial class MainForm : Form
    {
        private bool isAgainstBot;
        private int currentPlayer;
        private Label[,] cells;
        private int gridSize = 20;
        private int cellSize = 30;
        private int borderSize = 1;
        private int boardWidth;
        private int boardHeight;
        private int paddingLeft = 10;
        private int paddingTop = 10;
        private Random random = new Random();
        private Button restartButton;
        public MainForm()
        {
            InitializeComponent();
            InitializeGame();
        }
        private void InitializeGame()
        {
            boardWidth = gridSize * cellSize + (gridSize + 1) * borderSize + 110;
            boardHeight = gridSize * cellSize + (gridSize + 1) * borderSize;
            cells = new Label[gridSize, gridSize];
            currentPlayer = 1;
            Width = boardWidth + paddingLeft * 2 + 20;
            Height = boardHeight + paddingTop * 2 + 40;

            for (int i = 0; i < gridSize; i++)
            {
                for (int j = 0; j < gridSize; j++)
                {
                    var cell = new Label
                    {
                        Location = new Point(
                            paddingLeft + j * (cellSize + borderSize) + borderSize,
                            paddingTop + i * (cellSize + borderSize) + borderSize),
                        Size = new Size(cellSize, cellSize),
                        BorderStyle = BorderStyle.FixedSingle,
                        TextAlign = ContentAlignment.MiddleCenter,
                        Font = new Font("Arial", 16, FontStyle.Bold)
                    };
                    cell.Click += CellOnClick;
                    cells[i, j] = cell;
                    Controls.Add(cell);
                }
            }
        }
        private void RestartButton_Click(object sender, EventArgs e)
        {
            ResetGame();
        }
        private void CellOnClick(object sender, EventArgs e)
        {
            var cell = (Label)sender;
            if (cell.Text != "") return;
            cell.Text = currentPlayer == 1 ? "X" : "O";
            if (CheckForWinner())
            {
                string winnerName = currentPlayer == 1 ? "Player 1" : "Player 2";
                MessageBox.Show($"Congratulations, {winnerName} wins!", "Winner", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ResetGame();
                return;
            }
            currentPlayer = currentPlayer == 1 ? 2 : 1;
            if (isAgainstBot && currentPlayer == 2)
            {
                MakeBotMove();
            }
        }
        private bool CheckForWinner()
        {
            for (int i = 0; i < gridSize; i++)
            {
                for (int j = 0; j < gridSize; j++)
                {
                    if (string.IsNullOrEmpty(cells[i, j].Text)) continue;

                    if (CheckRow(i, j) || CheckColumn(i, j) || CheckDiagonal(i, j))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        private bool CheckRow(int row, int col)
        {
            if (col + 4 > gridSize) return false;
            for (int i = 1; i < 5; i++)
            {
                if (cells[row, col + i - 1].Text != cells[row, col].Text) return false;
            }
            return true;
        }
        private bool CheckColumn(int row, int col)
        {
            if (row + 4 > gridSize) return false;
            for (int i = 1; i < 5; i++)
            {
                if (cells[row + i - 1, col].Text != cells[row, col].Text) return false;
            }
            return true;
        }
        private bool CheckDiagonal(int row, int col)
        {
            // Право низ
            if (row + 4 <= gridSize && col + 4 <= gridSize)
            {
                bool allEqual = true;
                for (int i = 1; i < 5; i++)
                {
                    if (cells[row + i - 1, col + i - 1].Text != cells[row, col].Text)
                    {
                        allEqual = false;
                        break;
                    }
                }
                if (allEqual)
                {
                    return true;
                }
            }
            // Лево вверх
            if (row - 4 >= -1 && col + 4 <= gridSize)
            {
                bool allEqual = true;
                for (int i = 1; i < 5; i++)
                {
                    if (cells[row - i + 1, col + i - 1].Text != cells[row, col].Text)
                    {
                        allEqual = false;
                        break;
                    }
                }
                if (allEqual)
                {
                    return true;
                }
            }
            return false;
        }
        private void ResetGame()
        {
            currentPlayer = 1;
            foreach (var cell in cells)
            {
                cell.Text = "";
            }
        }
        private void MakeBotMove()
        {
            if (FindWinningMove(out int winningRow, out int winningCol))
            {
                cells[winningRow, winningCol].Text = "O";
                HandleGameResult();
                return;
            }
            if (FindBlockingMove(out int blockingRow, out int blockingCol))
            {
                cells[blockingRow, blockingCol].Text = "O";
                HandleGameResult();
                return;
            }
            if (FindOptimalMove(out int optimalRow, out int optimalCol))
            {
                cells[optimalRow, optimalCol].Text = "O";
                HandleGameResult();
                return;
            }
            MakeRandomMove();
        }
        private void HandleGameResult()
        {
            if (CheckForWinner())
            {
                string winnerName = currentPlayer == 1 ? "Player 1" : "Player 2";
                MessageBox.Show($"Congratulations, {winnerName} wins!", "Winner", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ResetGame();
            }
            else
            {
                currentPlayer = 1;
            }
        }
        private bool FindWinningMove(out int row, out int col)
        {
            for (int i = 0; i < gridSize; i++)
            {
                for (int j = 0; j < gridSize; j++)
                {
                    if (string.IsNullOrEmpty(cells[i, j].Text))
                    {
                        cells[i, j].Text = "O";
                        if (CheckForWinner())
                        {
                            row = i;
                            col = j;
                            cells[i, j].Text = "";
                            return true;
                        }
                        cells[i, j].Text = "";
                    }
                }
            }
            row = -1;
            col = -1;
            return false;
        }
        private bool FindBlockingMove(out int row, out int col)
        {
            for (int i = 0; i < gridSize; i++)
            {
                for (int j = 0; j < gridSize; j++)
                {
                    if (string.IsNullOrEmpty(cells[i, j].Text))
                    {
                        cells[i, j].Text = "X";
                        if (CheckForWinner())
                        {
                            row = i;
                            col = j;
                            cells[i, j].Text = "";
                            return true;
                        }
                        cells[i, j].Text = "";
                    }
                }
            }
            row = -1;
            col = -1;
            return false;
        }
        private bool FindOptimalMove(out int row, out int col)
        {
            List<(int, int)> availableMoves = new List<(int, int)>();
            for (int i = 0; i < gridSize; i++)
            {
                for (int j = 0; j < gridSize; j++)
                {
                    if (string.IsNullOrEmpty(cells[i, j].Text))
                    {
                        availableMoves.Add((i, j));
                    }
                }
            }
            if (availableMoves.Count > 0)
            {
                if (IsCellEmpty(gridSize / 2, gridSize / 2))
                {
                    row = gridSize / 2;
                    col = gridSize / 2;
                    return true;
                }
                Random random = new Random();
                int randomIndex = random.Next(availableMoves.Count);
                row = availableMoves[randomIndex].Item1;
                col = availableMoves[randomIndex].Item2;
                return true;
            }
            row = -1;
            col = -1;
            return false;
        }
        private bool IsCellEmpty(int row, int col)
        {
            return string.IsNullOrEmpty(cells[row, col].Text);
        }
        private void MakeRandomMove()
        {
            List<(int, int)> availableMoves = new List<(int, int)>();

            for (int i = 0; i < gridSize; i++)
            {
                for (int j = 0; j < gridSize; j++)
                {
                    if (string.IsNullOrEmpty(cells[i, j].Text))
                    {
                        availableMoves.Add((i, j));
                    }
                }
            }
            if (availableMoves.Count > 0)
            {
                Random random = new Random();
                int randomIndex = random.Next(availableMoves.Count);
                cells[availableMoves[randomIndex].Item1, availableMoves[randomIndex].Item2].Text = "O";
            }
        }
    }
}
