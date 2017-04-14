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
        int[] LeftSideVals = { 4, 4, 4, 4, 4, 4 };
        int[] RightSideVals = { 4, 4, 4, 4, 4, 4 };
        Label[] LeftSide = new Label[6];
        Label[] RightSide = new Label[6];

        int TopStore = 0;
        int BottomStore = 0;

        bool LeftTurn;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Randomize initial turn
            Random rand = new Random();
            LeftTurn = true; // Convert.ToBoolean(rand.Next() % 2);

            #region DefineLabelArrays
            LeftSide[0] = lblL0;
            LeftSide[1] = lblL1;
            LeftSide[2] = lblL2;
            LeftSide[3] = lblL3;
            LeftSide[4] = lblL4;
            LeftSide[5] = lblL5;

            RightSide[0] = lblR0;
            RightSide[1] = lblR1;
            RightSide[2] = lblR2;
            RightSide[3] = lblR3;
            RightSide[4] = lblR4;
            RightSide[5] = lblR5;
            #endregion

            MoveStones(1, true);
            UpdateBoard();
        }

        bool GameOver()
        {
            return (LeftSideVals.Sum() == 0 || RightSideVals.Sum() == 0) ? true : false;
        }
        void CaptureStones(int loc, bool left)
        {
            int opposite = 5 - loc;
            
            if (left == true)
            {
                if (LeftSideVals[loc] == 0 && RightSideVals[opposite] > 0)
                {
                    BottomStore += LeftSideVals[loc] + RightSideVals[opposite];
                    LeftSideVals[loc] = 0;
                    RightSideVals[opposite] = 0;
                }
            }
            else
            {
                if (RightSideVals[loc] == 0 && LeftSideVals[opposite] > 0)
                {
                    TopStore += RightSideVals[loc] + LeftSideVals[opposite];
                    RightSideVals[loc] = 0;
                    LeftSideVals[opposite] = 0;
                }
            }
        }
        void UpdateBoard()
        {
            if (GameOver())
            {
                CollectAll();
            }
            
            if (LeftTurn == true)
            {
                lblTurn.Text = "Left";
            }
            else
            {
                lblTurn.Text = "Right";
            }

            for (int i = 0; i < 6; i++)
            {
                LeftSide[i].Text = LeftSideVals[i].ToString();
                RightSide[i].Text = RightSideVals[i].ToString();
            }

            lblBottomStore.Text = BottomStore.ToString();
            lblTopStore.Text = TopStore.ToString();
        }
        void CollectAll()
        {
            BottomStore += LeftSideVals.Sum();
            TopStore += RightSideVals.Sum();

            for (int i = 0; i < 6; i++)
            {
                LeftSideVals[i] = 0;
                RightSideVals[i] = 0;
            }
        }

        void MoveStones(int loc, bool left)
        {
            int opposite = 5 - loc;
            int stones;

            if (left)
            {
                stones = LeftSideVals[loc];
                LeftSideVals[loc] = 0;
            }
            else
            {
                stones = RightSideVals[loc];
                RightSideVals[loc] = 0;
            }
            
            if (left)
            {
                int offset = 0;
                while (stones > 0)
                {
                    stones--;
                    if (loc + offset < 6)
                    {
                        LeftSideVals[loc]++;
                    }
                    if (loc + offset == 6)
                    {
                        BottomStore++;
                    }
                    if (loc + offset > 6)
                    {

                    }

                }
            }
            else
            {

            }
        }
    }
}
