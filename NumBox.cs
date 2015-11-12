using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


public class NumBox
{
    const short BOX_STARTING_LOCATION_X = 100;
    const short BOX_STARTING_LOCATION_Y = 50; 


    TextBox TB = new TextBox();
    private List<short> possibleNums = new List<short>();
    short value = 0;
    short ID;
    short row;
    short column;
    short box;
    public NumBox(short inrow, short incolumn, short inbox, short inID)
    {
        row = inrow;
        column = incolumn;
        box = inbox;
        ID = inID;
        for (short i = 1; i < 10; i++)
        {
            possibleNums.Add(i);
        }

        setupTextBox();
    }
    public List<short> getPV()
    {
        return possibleNums;
    }
    public bool removePotentialValue(short inVal)
    {
        if (ID == 44)
            ID = ID;
        return possibleNums.Remove(inVal);
    }
    private void setupTextBox()
    {
        TB.Name = "SB" + ID;
        int XLocation = BOX_STARTING_LOCATION_X + 30 * column + 10 * (box%3);
        int YLocation = BOX_STARTING_LOCATION_Y + 25 * row + 10 * (box/3);
        TB.Location = new System.Drawing.Point(XLocation, YLocation);
        TB.Size = new System.Drawing.Size(25, 20);
    }
    public TextBox getTB()
    {
        return TB;
    }
    public void setTBText(string word)
    {
        TB.Text = word;
    }
    public string getTBText()
    {
        return TB.Text;
    }
    public void setValue(short inVal)
    {
        //Console.WriteLine("Setting Box " + ID + " to value: " + inVal);
        if (inVal > 0 && inVal < 10)
        {
            setTBText(inVal.ToString());
            for (short i = 1; i < 10; i++)
            {
                removePotentialValue(i);
            }
        }
        value = inVal;
    }
    public short getValue()
    {
        return value;
    }
    public void setTBcolor(Color color)
    {
        TB.BackColor = color;
    }
    public short getID()
    {
        return ID;
    }
    public short getRow()
    {
        return row;
    }
    public short getColumn()
    {
        return column;
    }
    public short getBox()
    {
        return box;
    }
    public short resetPotentialValues()
    {
        if (ID == 44)
            ID = ID;
        for (short i = 1; i < 10; i++)
        {
            possibleNums.Remove(i);
        }
        for (short i = 1; i < 10; i++)
        {
            possibleNums.Add(i);
        }

        return 0;
    }
}
