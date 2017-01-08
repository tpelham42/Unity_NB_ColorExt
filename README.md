# NB ColorExt For Unity Game Engine

## Description
Unity NB ColorExt is a set of C# extension methods for the Unity Color class that adds the ability to easily go to/from Html Hex Color or HSV (Hue, Saturation, Color). 

## Demo
[Sample Demo](http://tim.nitrousbutterfly.com/nbcolorext.html)

## Usage

NB ColorExt is available under the NB.ColorUtil namespace. The following code snippet shows how to use the included functions.

```c#
//Use following namespace in scripts
using NB.ColorExt;

//Normal Color creation
Color c = Color.red;

//From HEX Value
c = c.FromHexStr("#FF0000"); //Red Color

//To HEX Value
string hexStr = c.ToHexStr();

//Hue (float): 0 - 359
//Saturation (float): 0 - 1
//Vibrance (float): 0 - 1

//From HSV
c = c.FromHSV(0f, 1f, 1f); //Red Color

//To HSV
float[3] hsv = c.HSV();
//hsv[0] = Hue, hsv[1] = Saturation, hsv[2] = Vibrance

float hue = c.Hue();
float saturation = c.Saturation();
float vibrance = c.Vibrance();

```
The only required file is the 'Assets/NB.ColorExt/NB.ColorExt.cs'. Example project included in repository.
