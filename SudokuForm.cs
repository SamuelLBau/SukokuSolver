using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.Runtime.InteropServices;

namespace WindowsFormsApplication1
{
    public partial class SudokuForm : Form
    {
        [DllImport("kernel32.dll", EntryPoint = "AllocConsole", SetLastError = true, CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        private static extern int AllocConsole();

        SDBoard gameBoard;
        Button solveButton;
        Button clearButton; //TODO: IMPLEMENT


        bool solved = false;
        ErrorLogger RV = new ErrorLogger();

        public SudokuForm()
        {

            InitializeComponent();
            AllocConsole();

            setupBoard();
            setupSolveButton();
            setupClearButton();
        }
        public SudokuForm(short[] filledArray)
        {
            InitializeComponent();
            AllocConsole();

            setupBoard(filledArray);
            setupSolveButton();
            setupClearButton();
        }
        public bool isSolved()
        {
            return solved;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void setupBoard()
        {
            short[] filledArray = new short[81];
            for (int i = 0; i < 81; i++)
            {
                filledArray[i] = 0;
            }
            setupBoard(filledArray);
        }
        private void setupBoard(short[] filledArray)
        {
            gameBoard = new SDBoard(filledArray);
            for (short i = 0; i < 81; i++)
            {
                TextBox TB = gameBoard.getNumBoxTB(i);
                this.Controls.Add(TB);
                TB.TabIndex = i + 2;
                TB.BringToFront();
            }
        }
        private void setupSolveButton()
        {
            solveButton = new Button();
            solveButton.Size = new Size(88, 25);
            solveButton.Location = new Point(198, 300);
            this.Controls.Add(solveButton);
            solveButton.BringToFront();
            solveButton.Text = "Attempt Solve";
            solveButton.Click += new EventHandler(solveButton_Click);
            solveButton.TabIndex = 0;
        }
        private void setupClearButton()
        {
            clearButton = new Button();
            clearButton.Size = new Size(88, 25);
            clearButton.Location = new Point(198, 10);
            this.Controls.Add(clearButton);
            clearButton.BringToFront();
            clearButton.Text = "Clear Board";
            clearButton.Click += new EventHandler(clearButton_Click);
            clearButton.TabIndex = 1;
        }
        void clearButton_Click(object sender, EventArgs E)
        {
            ErrorLogger RV = gameBoard.resetBoard();
            if (!RV.hasError())
            {
                Console.WriteLine("Board Reset");
            }
            else
            {
                Console.WriteLine("Error during board reset");
            }
            //Todo: resets board and clears GUI inputs
        }
        void solveButton_Click(object sender, EventArgs E)
        {
            bool foundSolution = false;
            ErrorLogger RV = gameBoard.solve(ref foundSolution);
            if (RV.hasError())
            {
                Console.WriteLine("solve returned with errors: ");
                List<string> errorListS = RV.getS();
                List<int> errorListV = RV.getV();
                int ELSC = errorListS.Count;
                int ELVC = errorListV.Count;

                if (ELSC == ELVC)
                {
                    for (int i = 0; i < ELSC; i++)
                    {
                        Console.WriteLine(errorListV[i] + " : " + errorListS[i]);
                    }
                }
                else
                {
                    for (int i = 0; i < ELSC; i++)
                    {
                        Console.WriteLine(errorListS[i]);
                    }
                }
            }
            else
            {
                Console.WriteLine("Solve returned with no errors");
            }


        }
    }
}
