using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AIProjectForm
{
    public partial class Form1 : Form
    {
        Label[] LeftSide = new Label[7];
        Label[] RightSide = new Label[7];
        Mancala game;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            game = new Mancala();
            LeftSide[0] = lblL0;
            LeftSide[1] = lblL1;
            LeftSide[2] = lblL2;
            LeftSide[3] = lblL3;
            LeftSide[4] = lblL4;
            LeftSide[5] = lblL5;
            LeftSide[6] = lblBottomStore;

            RightSide[0] = lblR0;
            RightSide[1] = lblR1;
            RightSide[2] = lblR2;
            RightSide[3] = lblR3;
            RightSide[4] = lblR4;
            RightSide[5] = lblR5;
            RightSide[6] = lblTopStore;
            
            UpdateBoard(ref game);
        }

        public void UpdateBoard(ref Mancala game)
        {
            if (game.isGameOver())
            {
                game.captureAll();
            }
            for (int i = 0; i < 7; i++)
            {
                LeftSide[i].Text = game.board[i].ToString();
                RightSide[i].Text = game.board[i + 7].ToString();
            }
            if (!game.gameOver)
            {
                lblTurn.Text = game.leftTurn ? "Left" : "Right";
            }
            else
            {
                label1.Visible = false;
                lblTurn.Visible = false;
                lblGameOver.Visible = true;
            }
        }

        private void btnL0_Click(object sender, EventArgs e)
        {
            if (game.leftTurn == true)
            {
                game.Move(0);
                UpdateBoard(ref game);
            }
        }
        private void btnL1_Click(object sender, EventArgs e)
        {
            if (game.leftTurn == true)
            {
                game.Move(1);
                
                UpdateBoard(ref game);
            }
        }
        private void btnL2_Click(object sender, EventArgs e)
        {
            if (game.leftTurn == true)
            {
                game.Move(2);
                UpdateBoard(ref game);
            }
        }
        private void btnL3_Click(object sender, EventArgs e)
        {
            if (game.leftTurn == true)
            {
                game.Move(3);
                UpdateBoard(ref game);
            }
        }
        private void btnL4_Click(object sender, EventArgs e)
        {
            if (game.leftTurn == true)
            {
                game.Move(4);
                UpdateBoard(ref game);
            }
        }
        private void btnL5_Click(object sender, EventArgs e)
        {
            if (game.leftTurn == true)
            {
                game.Move(5);
                UpdateBoard(ref game);
            }
        }

        private void btnR0_Click(object sender, EventArgs e)
        {
            if (game.leftTurn == false)
            {
                game.Move(7);
                UpdateBoard(ref game);
            }
        }
        private void btnR1_Click(object sender, EventArgs e)
        {
            if (game.leftTurn == false)
            {
                game.Move(8);
                UpdateBoard(ref game);
            }
        }
        private void btnR2_Click(object sender, EventArgs e)
        {
            if (game.leftTurn == false)
            {
                game.Move(9);
                UpdateBoard(ref game);
            }
        }
        private void btnR3_Click(object sender, EventArgs e)
        {
            if (game.leftTurn == false)
            {
                game.Move(10);
                UpdateBoard(ref game);
            }
        }
        private void btnR4_Click(object sender, EventArgs e)
        {
            if (game.leftTurn == false)
            {
                game.Move(11);
                UpdateBoard(ref game);
            }
        }
        private void btnR5_Click(object sender, EventArgs e)
        {
            if (game.leftTurn == false)
            {
                game.Move(12);
                UpdateBoard(ref game);
            }
        }
    }

    public class Mancala
    {
        public int[] board = { 4, 4, 4, 4, 4, 4, 0, 4, 4, 4, 4, 4, 4, 0 };
        public bool leftTurn;
        public bool gameOver;

        public Mancala()
        {
            Random r = new Random();
            leftTurn = true; // Convert.ToBoolean(r.Next() % 2);
            gameOver = false;
        }

        public void Move(int start)
        {
            int index = start;
            int stones = board[index];
            board[index] = 0;

            if (gameOver)
            {
                return;
            }

            for (int i = stones; i > 0; i--)
            {
                if (index == 13)
                {
                    index = 0;
                }
                else
                {
                    index++;
                }

                if (index == 13 && leftTurn == true)
                {
                    index = 0;
                }
                else if (index == 6 && leftTurn == false)
                {
                    index++;
                }

                if (i == 1)
                {
                    if (isPlayerStore(index))
                    {
                        board[index]++;
                        NextTurn();
                    }
                    else if (isCapture(index))
                    {
                        capture(index);
                    }
                    else
                    {
                        board[index]++;
                    }
                }
                else
                {
                    board[index]++;
                }
            }
            NextTurn();
        }
        bool isPlayerStore(int index)
        {
            if ((index == 6 && leftTurn == true) || (index == 13 && leftTurn == false))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        bool isCapture(int index)
        {
            if (leftTurn == true && index >= 0 && index <= 5 && board[index] == 0 && board[12-index] > 0)
            {
                return true;
            }
            else if (leftTurn == false && index >= 7 && index <= 12 && board[index] == 0 && board[12-index] > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        void capture(int index)
        {
            int opposite = 12 - index;
            if (leftTurn == true)
            {
                board[6] += board[opposite] + 1;
                board[opposite] = 0;
            }
            else if (leftTurn == false)
            {
                board[13] += board[opposite] + 1;
                board[opposite] = 0;
            }
        }
        public void NextTurn()
        {
            leftTurn = leftTurn ? false : true;
        }
        public bool isGameOver()
        {
            int leftSum = 0, rightSum = 0;
            for (int i = 0; i < 6; i++)
            {
                leftSum += board[i];
                rightSum += board[i + 7];
            }
            if (leftSum == 0 || rightSum == 0)
            {
                gameOver = true;
                return true;
            }
            else
            {
                gameOver = false;
                return false;
            }
        }
        public void captureAll()
        {
            int leftSum = 0, rightSum = 0;
            for (int i = 0; i < 6; i++)
            {
                leftSum += board[i];
                rightSum += board[i + 7];
                board[i] = 0;
                board[i + 7] = 0;
            }
            board[7] += leftSum;
            board[13] += rightSum;
        }
    }
}
