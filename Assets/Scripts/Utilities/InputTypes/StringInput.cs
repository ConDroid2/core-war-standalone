using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StringInput
{
    public delegate string GetValue();

    private GetValue getValue;
    private string value;

    public string Value => getValue();

    public StringInput(string value)
    {
        this.value = value;
        getValue = DefaultGetValue;
    }

    public StringInput(GetValue getValue)
    {
        this.getValue = getValue;
    }

    public string DefaultGetValue()
    {
        return value;
    }
}
