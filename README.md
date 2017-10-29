MetroSet UI Framework
=====================
### The Windows Forms Metro Style Skin for applications.

## 

## Install via NuGet

    Install-Package MetroSet_UI

## Direct to NuGet

> [Link](https://www.nuget.org/packages/MetroSet_UI/)

## 

Supported platforms
-------------------
:arrow_right: Windows XP SP1/SP2/SP3

:arrow_right: Windows Vista

:arrow_right: Windows 7

:arrow_right: Windows 8

:arrow_right: Windows 10

## 

Dependency
------------
:arrow_right_hook: .NET Framework 2.0 or higher.

## 

## Components Available

:arrow_down: | Components | Skin Support | Custom Theme Support | Smart Tag
:---:|:---|:---:|:---:|:---:|
:arrow_right: | StyleManager |:heavy_check_mark:|:heavy_multiplication_x:|:heavy_check_mark:
:arrow_right: | MetroSetToolTip |:heavy_check_mark:|:heavy_check_mark:|:heavy_check_mark:

## 

## Controls Available


:arrow_down: | Controls | Skin Support | Custom Theme Support | Animation | Disabling | Custom Smart Tags
:---:|:---|:---:|:---:|:---:|:---:|:---:|
:arrow_right: | MetroSetForm |:heavy_check_mark:|:heavy_check_mark:|:heavy_check_mark:|:heavy_multiplication_x:|:heavy_multiplication_x:
:arrow_right: | MetroSetButton |:heavy_check_mark:|:heavy_check_mark:|:heavy_multiplication_x:|:heavy_check_mark:|:heavy_check_mark:
:arrow_right: | MetroSetBadge |:heavy_check_mark:|:heavy_check_mark:|:heavy_multiplication_x:|:heavy_check_mark:|:heavy_check_mark:
:arrow_right: | MetroSetEllipse |:heavy_check_mark:|:heavy_check_mark:|:heavy_multiplication_x:|:heavy_check_mark:|:heavy_check_mark:
:arrow_right: | MetroSetLabel |:heavy_check_mark:|:heavy_check_mark:|:heavy_multiplication_x:|:heavy_multiplication_x:|:heavy_check_mark:
:arrow_right: | MetroSetCheckBox |:heavy_check_mark:|:heavy_check_mark:|:heavy_check_mark:|:heavy_check_mark:|:heavy_check_mark:
:arrow_right: | MetroSetRadioButton |:heavy_check_mark:|:heavy_check_mark:|:heavy_check_mark:|:heavy_check_mark:|:heavy_check_mark:
:arrow_right: | MetroSetSwitch |:heavy_check_mark:|:heavy_check_mark:|:heavy_check_mark:|:heavy_check_mark:|:heavy_check_mark:
:arrow_right: | MetroSetLink |:heavy_check_mark:|:heavy_check_mark:|:heavy_multiplication_x:|:heavy_multiplication_x:|:heavy_check_mark:
:arrow_right: | MetroSetDevider |:heavy_check_mark:|:heavy_check_mark:|:heavy_multiplication_x:|:heavy_multiplication_x:|:heavy_check_mark:
:arrow_right: | MetroSetTextBox |:heavy_check_mark:|:heavy_check_mark:|:heavy_multiplication_x:|:heavy_check_mark:|:heavy_check_mark:
:arrow_right: | MetroSetRichTextBox |:heavy_check_mark:|:heavy_check_mark:|:heavy_multiplication_x:|:heavy_check_mark:|:heavy_check_mark:
:arrow_right: | MetroSetComboBox |:heavy_check_mark:|:heavy_check_mark:|:heavy_multiplication_x:|:heavy_check_mark:|:heavy_check_mark:
:arrow_right: | MetroSetNumeric |:heavy_check_mark:|:heavy_check_mark:|:heavy_multiplication_x:|:heavy_check_mark:|:heavy_check_mark:
:arrow_right: | MetroSetTile |:heavy_check_mark:|:heavy_check_mark:|:heavy_multiplication_x:|:heavy_check_mark:|:heavy_check_mark:
:arrow_right: | MetroSetProgress |:heavy_check_mark:|:heavy_check_mark:|:heavy_multiplication_x:|:heavy_check_mark:|:heavy_check_mark:
:arrow_right: | MetroSetControlBox |:heavy_check_mark:|:heavy_check_mark:|:heavy_multiplication_x:|:heavy_check_mark:|:heavy_check_mark:
:arrow_right: | MetroSetTabControl |:heavy_check_mark:|:heavy_check_mark:|:heavy_check_mark:|:heavy_check_mark:|:heavy_multiplication_x:
:arrow_right: | MetroSetScrollBar |:heavy_check_mark:|:heavy_check_mark:|:heavy_multiplication_x:|:heavy_multiplication_x:|:heavy_check_mark:
:arrow_right: | MetroSetPanel |:heavy_check_mark:|:heavy_check_mark:|:heavy_multiplication_x:|:heavy_multiplication_x:|:heavy_multiplication_x:
:arrow_right: | MetroSetTrackBar |:heavy_check_mark:|:heavy_check_mark:|:heavy_multiplication_x:|:heavy_multiplication_x:|:heavy_check_mark:
:arrow_right: | MetroSetScrollBar |:heavy_check_mark:|:heavy_check_mark:|:heavy_multiplication_x:|:heavy_check_mark:|:heavy_check_mark:
:arrow_right: | MetroSetListBox |:heavy_check_mark:|:heavy_check_mark:|:heavy_multiplication_x:|:heavy_check_mark:|:heavy_check_mark:
## 

## Using MetroSetForm


## C#

```cs

using MetroSet_UI.Controls;

public partial class Form1 : MetroSetForm
    {
        public Form1()
        {
            InitializeComponent();
        }
    }

```

## VB.NET

```vb
Imports MetroSet_UI.Controls

public class Form1 : Inherits MetroSetForm
    
    Sub New()
    
    End Sub
    
End Class
```
## Credits

>  MCF.Goodwin : [Form Fading](https://www.codeproject.com/Articles/30255/C-Fade-Form-Effect-With-the-AnimateWindow-API-Func)

>  Burak Ozdiken : [TabControlDesigner](https://github.com/N-a-r-w-i-n/MetroSet-UI/blob/master/MetroSet%20UI/Design/MetroSetTabControlDesigner.cs)

>  Mavamaarten : [TabControl Transition Method](https://github.com/N-a-r-w-i-n/MetroSet-UI/blob/29e65d1d2e4d12105f9b9639e9def96af0b93da2/MetroSet%20UI/Controls/MetroSetTabControl.cs#L363-L463)

## License

>  [MIT License](https://github.com/N-a-r-w-i-n/MetroSet-UI/blob/master/LICENSE.md)

