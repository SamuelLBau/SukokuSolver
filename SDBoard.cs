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

public class SDBoard
{

    NumBox[] numBoxes = new NumBox[81];
    short[] valueArray = new short[81];
    NumBox[][] NBRows = new NumBox[9][];
    NumBox[][] NBColumns = new NumBox[9][];
    NumBox[][] NBBOX = new NumBox[9][];
    public SDBoard(short[] filledArray)//pass this an array of shorts with values 0 through 9
    {
        for (int i = 0; i < 9; i++)
        {
            NBRows[i] = new NumBox[9];
            NBColumns[i] = new NumBox[9];
            NBBOX[i] = new NumBox[9];
        }
        //creates every numBox
        for (short i = 0; i < 81; i++)
        {
            short row = (short)(i / 9);
            short column = (short)(i % 9);
            short box = (short)((row / 3) * 3 + column / 3);
            numBoxes[i] = new NumBox(row, column, box, i);
   

            NBRows[row][column] = numBoxes[i];
            NBColumns[column][row] = numBoxes[i];
            NBBOX[box][(row % 3) * 3 + (column % 3)] = numBoxes[i]; //determines numBox position with largeBox

            //numBoxes[i].setTBText(column.ToString()); //initiates showing numBox position within row
            //numBoxes[i].setTBText(row.ToString()); //initiates showing numBox position within column
            //numBoxes[i].setTBText(((row % 3) * 3 + (column % 3)).ToString()); //initiates showing numBox position with largeBox
            //numBoxes[i].setTBText(box.ToString()); //initiates showing each box association
        }
        for (int i = 0; i < 81; i++) //populates boxes
        {
            if (filledArray[i] != 0)
            {
                insertNum(numBoxes[i], filledArray[i]); //must be done after all boxes initialized
            }
        }
    }
    public ErrorLogger solve(ref bool foundSolution)
    {
        /*
         * This call is recursive, or will create new boards
         * */
        ErrorLogger RV = new ErrorLogger();

        RV = checkBoardValidity();
        if (RV.hasError()) return RV;


        //if program has made it this far, problem is valid
        Console.WriteLine("----BEGIN SOLVE-----");
        bool madeChange = true;

        
        while (madeChange)
        {
            madeChange = false;
            RV = levelOneTests(ref madeChange);
            if (RV.hasError() ) break;
            if (madeChange) continue; //don't bother with level two unless level one made no changes

            RV = levelTwoTests(ref madeChange);
            if (RV.hasError()) break;
            if (madeChange) continue; //don't bother with level two unless level one made no changes
        }
        if (checkForSolution())
        {
            if (!RV.hasError())
            {
                foundSolution = true;
                RV = RV + new ErrorLogger(this);
            }
        }
        else 
        {
            //if code enters here it could not find a solution, it should make a guess and call solve again
            if (!RV.hasError())
            {
                RV = RV + makeGuesses(ref foundSolution);
                if (foundSolution)
                {
                    if (RV.hasBoard())
                    {
                        short[] tArray = RV.getBoard().getArray();
                        //fills in correct board
                        for (int i = 0; i < 81; i++)
                        {
                            if (numBoxes[i].getValue() == 0)
                            {
                                insertNum(numBoxes[i], tArray[i]);
                            }
                        }
                    }
                }
            }
        }
        return RV;
    }
    private ErrorLogger makeGuesses(ref bool foundSolution)
    {
        //Console.WriteLine("Entering make guesses");
        int minValCount = 10;
        int minValID = -1;
        short[] curBoardArray = new short[81];
        short[] nextBoardArray = new short[81];
        //finds ID of box with minimum # potential values
        for (int i = 0; i < 81; i++)
        {
            curBoardArray[i] = numBoxes[i].getValue();
            nextBoardArray[i] = numBoxes[i].getValue();
            int t = numBoxes[i].getPV().Count;
            if (t < minValCount && t >0)
            {
                minValCount = t;
                minValID = i;
            }
        }
        List<short> possibleNums = new List<short>();
        possibleNums.Clear();
        possibleNums.AddRange(numBoxes[minValID].getPV());
        //Console.WriteLine("MinValID=" + minValID + "minValCount=" + minValCount);
        int numAttempts = 0;
        ErrorLogger RV = new ErrorLogger();
        while (numAttempts < minValCount) //presumably should only not have an error if board complete
        {
            RV = new ErrorLogger();
            Console.WriteLine("making guess, box: " + minValID + " value: " + possibleNums[numAttempts]);
            //Console.ReadLine();
            nextBoardArray[minValID] = possibleNums[numAttempts];
            SDBoard newBoard = new SDBoard(nextBoardArray);
            RV = RV + newBoard.solve(ref foundSolution);

            if (foundSolution)
            {
                /*for(int i = 0; i < RV.getS().Count; i++)
                {
                    Console.Write(RV.getV()[i]);
                    Console.WriteLine(RV.getS()[i]);
                }
                Console.Write(RV.hasError());
                Console.WriteLine("Guess box: " + minValID + " value: " + possibleNums[numAttempts] +  " found Solution");
                //Console.ReadLine();*/
                Console.WriteLine("Guess box: " + minValID + " value: " + possibleNums[numAttempts] + " found Solution");
                break;
            }
            else
            {
                /*
                for (int i = 0; i < RV.getS().Count; i++)
                {
                    Console.Write(RV.getV()[i]);
                    Console.WriteLine(RV.getS()[i]);
                }
                Console.Write(RV.hasError());
                Console.WriteLine("Guess  box: " + minValID + " value: " + possibleNums[numAttempts] + " failed due to: " + RV.getS()[0]);
                //Console.ReadLine();
                 * */
                Console.WriteLine("Guess  box: " + minValID + " value: " + possibleNums[numAttempts] + " failed due to: " + RV.getS()[0]);
            }
            numAttempts++;
        }

        return RV;
    }
    private ErrorLogger levelTwoTests(ref bool madeChange)
    {
        ErrorLogger RV = new ErrorLogger();




        return RV;
    }
    private ErrorLogger levelOneTests(ref bool madeChange)
    {
        ErrorLogger RV = new ErrorLogger();


        Console.WriteLine("Single Box inserts: ");
        RV = RV + checkBSP(ref madeChange); //checks every box to see if any have a single valid possiblity, if so, fills it in

        Console.WriteLine("Row inserts");
        RV = RV + checkRCB(NBRows, ref madeChange);
        Console.WriteLine("RColumn inserts");
        RV = RV + checkRCB(NBColumns, ref madeChange);
        Console.WriteLine("Large Box inserts");
        RV = RV + checkRCB(NBBOX, ref madeChange);

        return RV;
    }
    private ErrorLogger checkBoardValidity()
    {
        ErrorLogger RV = new ErrorLogger();

        string[] cuNums = new string[81];
        for (int i = 0; i < 81; i++)
        {
            cuNums[i] = numBoxes[i].getTBText();
        }

        RV = RV + resetBoxes();
        if (RV.hasError()) return RV;

        for (int i = 0; i < 81; i++)
        {
            numBoxes[i].setTBText(cuNums[i]);
        }

        RV = RV + sanitizeInputs();
        if (RV.hasError()) return RV;      //indicates a bad input solution

        RV = RV + colorBoxes();
        if (RV.hasError()) return RV;      //unknown error during box coloring

        RV = RV + validateInstance();
        if (RV.hasError()) return RV;     //invalid instance detected

        return RV;
    }
    private ErrorLogger validateInstance() 
    {
        //this function should add the values to each box, 
        ErrorLogger RV = new ErrorLogger();

        foreach(NumBox NB in numBoxes)
        {
            short val = 0;
            string s = NB.getTBText();
            //this occurs when textbox has an integer in it (values should already be validated)
            if(!s.Equals("") && short.TryParse(s,out val))
            {
                RV = RV + insertNum(NB,val);
            }
        }

        return RV;
        //this function should check all rows / columns / boxes for conflicts
    }
    private ErrorLogger insertNum(NumBox NB, short val)
    {
        //this value insertion verifies that it is not inserting a conflict value
        //colors boxes red if a conflict occurs
        //updates all validNumbers for pertinent boxes


        ErrorLogger RV = new ErrorLogger();

        RV = RV + checkStruct(val, NBRows[NB.getRow()]);
        //if (RV!= 0) return RV;

        RV = RV + checkStruct(val, NBColumns[NB.getColumn()]);
        //if (RV!= 0) return RV;

        RV = RV + checkStruct(val, NBBOX[NB.getBox()]);
        //if (RV != 0) return RV;
        if(NB.getID() == 44)
            val = val;
        //if code makes it here, there are no conflicts, add value to box
        NB.setValue(val);
        valueArray[NB.getID()] = val;
        if (NB.getID() == 38)
            val = val;
        if (!RV.hasError())
        {
            foreach (NumBox NBt in NBRows[NB.getRow()])
            {
                NBt.removePotentialValue(val);
                if (NBt.getID() == 44)
                    val = val;
            }
            foreach (NumBox NBt in NBColumns[NB.getColumn()])
            {
                NBt.removePotentialValue(val);
            }
            foreach (NumBox NBt in NBBOX[NB.getBox()])
            {
                NBt.removePotentialValue(val);
            }
        }
        else { Console.WriteLine("Skipped removing potential vals"); }

        return RV;
    }
    private ErrorLogger checkStruct(short val, NumBox[] NBStruct)
    {
        ErrorLogger RV = new ErrorLogger();
        //if a value exists, remove it from the rest of the row
        foreach (NumBox NBInner in NBStruct)
        {

            //checks if attempting to remove a po
            if (val == NBInner.getValue())
            {

                RV = RV + new ErrorLogger(1, "checkStruct failed: BOXID: " + NBInner.getID()+ " value: " + val);
                NBInner.setTBcolor(Color.Red);
            }
            if (RV.hasError()) break;
        }
        return RV;
    }
    private ErrorLogger colorBoxes()
    {
        //this function colors the text boxes to indicate they were the initial spots
        for (int i = 0; i < 81; i++)
        {
            if (numBoxes[i].getValue() != 0)
            {
                numBoxes[i].setTBcolor(Color.Gray);
            }
            else
            {
                numBoxes[i].setTBcolor(Color.White);
            }
        }

        return new ErrorLogger();
    }
    private ErrorLogger sanitizeInputs()
    {
        ErrorLogger RV = new ErrorLogger();
        short TBval = 0;
        for (int i = 0; i < 81; i++)
        {
            string TBText = numBoxes[i].getTBText();
            if (TBText.Equals(""))
            {
                numBoxes[i].setValue(0);
                continue;
            }
            else if (!short.TryParse(numBoxes[i].getTBText(), out TBval))
            {
                //Console.WriteLine("string in textbox");
                RV = RV + new ErrorLogger(-1, "string in textbox");
                numBoxes[i].setTBcolor(Color.Red);
            }
            else if (TBval < 1 || TBval > 9)
            {
                //Console.WriteLine("textBox holds number other than 1-9");
                RV = RV + new ErrorLogger(-2, "textBox holds number other than 1-9");
                numBoxes[i].setTBcolor(Color.Red);
            }
        }
        return RV;
    }
    private ErrorLogger resetBoxes()
    {
        for (int i = 0; i < 81; i++)
        {
            numBoxes[i].setTBText("");
            numBoxes[i].setValue(0);
            numBoxes[i].setTBcolor(Color.White);
            numBoxes[i].resetPotentialValues();
        }

        return new ErrorLogger();
    }
    public ErrorLogger resetBoard()
    {
        ErrorLogger RV = new ErrorLogger();
        resetBoxes();

        return RV;
    }
    public TextBox getNumBoxTB(short ID)
    {
        return numBoxes[ID].getTB();
    }
    public ErrorLogger checkBSP(ref bool madeChange) //checks for boxes with only 1 possible input
    {
        ErrorLogger RV = new ErrorLogger();

        bool noChange = false;
        List<short> curList = new List<short>();
        while (!noChange)
        {
            noChange = true;
            for (int i = 0; i < 81; i++)
            {
                if (numBoxes[i].getValue() == 0)
                {
                    curList.Clear();
                    curList.AddRange(numBoxes[i].getPV());
                    if (curList.Count == 1)
                    {
                        RV = RV + insertNum(numBoxes[i], curList[0]);
                        Console.WriteLine("Single Box Insert: " + i.ToString() + " inserting value: " + curList[0].ToString());
                        noChange = false;
                        madeChange = true;
                    }
                    else if (curList.Count == 0)
                    {
                        RV = RV + new ErrorLogger(-1, "Box " + i + " has no possible values");
                    }
                }
                if (RV.hasError()) break;
            }
            if (RV.hasError()) break;
        }
        return RV;
    }
    public ErrorLogger checkRCB(NumBox[][] NBstruct,ref bool madeChange)//checks for rows where a val can only go to a single box
    {
        ErrorLogger RV = new ErrorLogger();
        NumBox[] boxArray;
        bool noChange = false;
        while (!noChange)
        {
            noChange = true;
            List<short> PVs = new List<short>();
            for (int i = 0; i < 9; i++) //does for each row
            {
                short[] occuranceCount = new short[9];
                for (int j = 0; j < 9; j++) occuranceCount[j] = 0;
                boxArray = NBstruct[i];
                foreach (NumBox NB in boxArray) //counts the occurances of each potential value in the struct
                {
                    if (NB.getID() == 44)
                        PVs = PVs;
                    PVs.Clear();
                    PVs.AddRange(NB.getPV());
                    for (int j = 0; j < PVs.Count; j++)
                    {
                        int T = PVs[j];
                        occuranceCount[T - 1] = (short)(occuranceCount[T - 1] + 1);
                    }
                }
                for (short j = 0; j < 9; j++)
                {
                    if (occuranceCount[j] == 1) //if there are any instances of a value being possible in only one spot, it is placed
                    {
                        foreach (NumBox NB in boxArray)
                        {
                            if (NB.getID() == 44)
                                PVs = PVs;
                            PVs.Clear();
                            PVs.AddRange(NB.getPV());
                            if(PVs.Contains((short)(j+1)))
                            {
                         
                                for (int k = 0; k < PVs.Count; k++)
                                {
                                    occuranceCount[j] = (short)(occuranceCount[j] - 1);
                                }
                                RV = RV + insertNum(NB,(short)(j + 1));
                                Console.WriteLine("RCB Insert: " + NB.getID() + " inserting value: " + (j + 1).ToString());
                                madeChange = true;
                                noChange = false;
                            }
                            if (RV.hasError()) break;
                        }
                    }
                    if (RV.hasError()) break;
                }
                if (RV.hasError()) break;
            }
            if (RV.hasError()) break;
        }


        return RV;
    }
    public bool checkForSolution()
    {
        bool foundSolution = true;
        for (int i = 0; i < 81; i++)
        {
            if (numBoxes[i].getValue() == 0)
            {
                foundSolution = false;
                break;
            }
        }
        ErrorLogger RV = checkBoardValidity();
        if (RV.hasError()) foundSolution = false;
        return foundSolution;
    }
    public short[] getArray()
    {
        short[] tArray = new short[81];
        for (int i = 0; i < 81; i++)
        {
            tArray[i] = valueArray[i];
        }
        return tArray;
    }
}
