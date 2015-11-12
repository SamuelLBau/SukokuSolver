using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            short[] testArray = new short[81];
            for (int i = 0; i < 81; i++)
            {
                testArray[i] = 0;
            }/*
            testArray[0] = 4; testArray[1] = 8; testArray[2] = 7; testArray[4] = 5; testArray[7] = 6;
            testArray[9] = 9; testArray[12] = 4; testArray[17] = 3;
            testArray[18] = 2; testArray[20] = 6; testArray[22] = 8; testArray[23] = 9; testArray[24] = 5;
            testArray[29] = 4; testArray[31] = 1; testArray[32] = 5; testArray[33] = 6;
            testArray[36] = 1; testArray[41] = 4; testArray[43] = 5;
            testArray[46] = 7; testArray[47] = 8; testArray[48] = 2;
            testArray[59] = 8; testArray[61] = 7;
            testArray[63] = 7; testArray[64] = 5; testArray[70] = 3;
            testArray[73] = 2; testArray[76] = 3; testArray[77] = 7; testArray[78] = 4; testArray[79] = 1;
              * */
            /*
            testArray[4] = 3; testArray[5] = 7; testArray[6] = 6;
            testArray[12] = 6; testArray[16] = 9;
            testArray[20] = 8; testArray[26] = 4;
            testArray[28] = 9; testArray[35] = 1;
            testArray[36] = 6; testArray[44] = 9;
            testArray[45] = 3; testArray[52] = 4;
            testArray[54] = 7; testArray[60] = 8;
            testArray[64] = 1; testArray[68] = 9;
            testArray[74] = 2; testArray[75] = 5; testArray[76] = 4;  
             * */
            /*
            //http://www.sudoku.ws/hard-1.htm
            testArray[3] = 2; testArray[7] = 6; testArray[8] = 3;
            testArray[9] = 3; testArray[14] = 5; testArray[15] = 4; testArray[17] = 1;
            testArray[20] = 1; testArray[23] = 3; testArray[24] = 9; testArray[25] = 8;
            testArray[34] = 9;
            testArray[39] = 5; testArray[40] = 3; testArray[41] = 8;
            testArray[46] =3;
            testArray[55] = 2; testArray[56] = 6; testArray[57] = 3; testArray[60] = 5;
            testArray[63] = 5; testArray[65] = 3; testArray[66] = 7; testArray[71] = 8;
            testArray[72] = 4; testArray[73] = 7; testArray[77] = 1; 
             * */
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new SudokuForm(testArray));
            Console.ReadLine();
 
        }
    }
}
