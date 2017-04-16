using System;
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
            if (!game.leftTurn && game.aiActive)
            {
                int ai_index = game.get_ai_move(Mancala.max_depth, true)[1];

                switch (ai_index)
                {
                    case 7:
                        btnR0.PerformClick();
                        break;
                    case 8:
                        btnR1.PerformClick();
                        break;
                    case 9:
                        btnR2.PerformClick();
                        break;
                    case 10:
                        btnR3.PerformClick();
                        break;
                    case 11:
                        btnR4.PerformClick();
                        break;
                    case 12:
                        btnR5.PerformClick();
                        break;
                }
            }

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
        public const int max_depth = 5;
        public int[] board = { 4, 4, 4, 4, 4, 4, 0, 4, 4, 4, 4, 4, 4, 0 };
        public bool gameOver;
        public bool leftTurn;
        public bool aiActive = true;

        public Mancala()
        {
            Random r = new Random();
            leftTurn = true; // Convert.ToBoolean(r.Next() % 2);
            gameOver = false;
        }

        public bool Move(int start)
        {
            int index = start;
            bool free_turn = false;
            int stones = board[index];
            board[index] = 0;

            if (gameOver)
            {
                return false;
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
                        free_turn = true;
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
            if (stones != 0 && !free_turn)
            {
                NextTurn();
            }
            return free_turn;
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
            if (leftTurn == true && index >= 0 && index <= 5 && board[index] == 0 && board[12 - index] > 0)
            {
                return true;
            }
            else if (leftTurn == false && index >= 7 && index <= 12 && board[index] == 0 && board[12 - index] > 0)
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
        void NextTurn()
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

        int heuristic()
        {
            int ai_marbles = 0;
            int player_marbles = 0;
            int ai_store, player_store;

            for (int i = 0; i < 6; i++)
            {
                ai_marbles += board[i + 7];
                player_marbles += board[i];
            }
            ai_store = board[13];
            player_store = board[6];

            return (ai_marbles - player_marbles) + (ai_store - player_store);
        }
        public int[] get_ai_move(int depth, bool max_turn)
        {
            const int infinity = 999999;
            bool free_turn;
            int[] val = { 0, 0 };
            int[] best = { 0, 0 };

            if (depth == 0 || isGameOver())
            {
                best[0] = heuristic();
                return best;
            }

            if (max_turn && depth < max_depth)
            {
                best[0] = -1 * infinity;
                int[] tmp = new int[14];
                Array.Copy(board, tmp, 14);

                for (int i = 7; i < 13; i++)
                {
                    if (board[i] > 0)
                    {
                        free_turn = Move(i);
                        val = get_ai_move(depth - 1, free_turn);
                        best[0] = Math.Max(best[0], val[0]);
                        if (best[0] == val[0])
                        {
                            best[1] = i;
                        }
                        Array.Copy(tmp, board, 14);
                    }
                }

                return best;
            }
            else if (depth < max_depth)
            {
                best[0] = infinity;
                int[] tmp = new int[14];
                Array.Copy(board, tmp, 14);

                for (int i = 0; i < 6; i++)
                {
                    if (board[i] > 0)
                    {
                        free_turn = Move(i);
                        val = get_ai_move(depth - 1, !free_turn);
                        best[0] = Math.Min(best[0], val[0]);
                        if (best[0] == val[0])
                        {
                            best[1] = i;
                        }
                        Array.Copy(tmp, board, 14);
                    }
                }
                return best;
            }
            else
            {
                int[] return_val = get_ai_move(depth - 1, max_turn);
                leftTurn = false;
                return return_val;
            }
        }
    }
}