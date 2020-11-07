using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DialogEventArgs : EventArgs
{
    public Dialogue dialogPayload;

}
public static class GameEvents
{
    public static event EventHandler<DialogEventArgs> DialogInitiated;
    public static event EventHandler DialogFinished;

    public static void InvokeDialogInitiated(Dialogue dialog)
    {
        DialogInitiated(null, new DialogEventArgs { dialogPayload = dialog });
    }
    public static void InvokeDialogFinished()
    {
        DialogFinished(null, EventArgs.Empty);
    }

}
