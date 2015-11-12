using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class ErrorLogger
{
    bool errorDetected = false;
    List<string> errorString = new List<string>();
    List<int> errorVal = new List<int>();
    SDBoard completeBoard;

    public static ErrorLogger operator +(ErrorLogger E1, ErrorLogger E2)
    {
        foreach (string s in E2.getS())
        {
            E1.errorString.Add(s);
        }
        foreach (int val in E2.getV())
        {
            E1.errorVal.Add(val);
        }
        E1.completeBoard = E2.getBoard();


        E1.errorDetected = E1.errorDetected | E2.errorDetected;

        return E1;
    }
    public ErrorLogger(int val, string s)
    {
        errorVal.Add(val);
        errorString.Add(s);
        if (val != 0) errorDetected = true;
    }
    public ErrorLogger(string s)
    {
        errorVal.Add(-1);
        errorString.Add(s);
        errorDetected = true;
    }
    public ErrorLogger(int val)
    {
        errorVal.Add(val);
        errorString.Add("");
        if (val != 0) errorDetected = true;
    }
    public ErrorLogger()
    {
    }
    public ErrorLogger(SDBoard SD)
    {
        completeBoard = SD;
        errorVal.Add(0);
        errorString.Add("Board succesfully solved");
    }
    public List<string> getS()
    {
        return errorString;
    }
    public List<int> getV()
    {
        return errorVal;
    }
    public bool hasError()
    {
        return errorDetected;
    }
    public bool hasBoard()
    {
        return !(completeBoard == null);
    }
    public SDBoard getBoard()//hasBoard should be checked before calling this
    {
        return completeBoard;
    }
}
