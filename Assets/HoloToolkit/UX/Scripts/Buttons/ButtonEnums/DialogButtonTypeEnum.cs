// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace HoloToolkit.UX.Dialog
{
    /// <summary>
    /// Enum describing the style (caption) of button on a Dialog.
    /// </summary>
    public enum DialogButtonType
    {
        None = 0,
        Close = 1,
        Text = 2,//Confirm
        Cancel = 4,
        Accept = 8,
        Yes = 16,
        No = 32,
        Graphics = 64,//OK
        //自己加的
        Pattern1 = 128,
        Pattern2 = 256,
    }
}
