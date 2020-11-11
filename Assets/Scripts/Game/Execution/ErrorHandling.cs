using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ErrorHandling : MonoBehaviour {
    public static List<Error> errors = new List<Error>();

    public static void AddError(Error err)
    {
        errors.Add(err);
    }
    public static void ResetError()
    {
        errors = new List<Error>();
    }
}
public class Error
{
    public int commandLine;
    public string text;

    public Error(int line, string text)
    {
        this.commandLine = line;
        this.text = text;
    }
}
public enum ErrorType {
    DIG_IN_WRONG_TILE, WALK_ON_WATER, WALK_ON_FIXED_OBJECT
}
