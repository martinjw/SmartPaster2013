# SmartPaster 2015/2017/2019

A port of Alex Papadimoulis' Smart Paster for Visual Studio 2015, 2017 and 2019.
(via SmartPaster 2010 for Visual Studio 2010 and 2012: https://smartpaster2010.codeplex.com/)

A "Paste As" right-click option that allows clipboard text to be:

* Paste as Comment
* Paste as Literal String (regular string literal)
* Paste as @String (in C#, verbatim string; in VB, CData or VB14 multi-line strings depending on version)
* Paste as StringBuilder
* Paste as Byte Array

On Visual Studio Gallery:
http://visualstudiogallery.msdn.microsoft.com/0611a238-7405-4d5f-ace0-5b3d5cf325e0

v1.1 adds Unicode, VS2015 support, both regular and verbatim strings.
v1.2 adds Paste as Byte Array, C++ support (thanks to leg0)
v1.3 adds VS2017 support, renamed to SmartPaster2017
v2 runs as an asynchronous service (so no longer supports Visual Studio 2013)

![Screenshot](Screenshot.png?raw=true)

SmartPaster 2010 Codeplex version (VS2010, VS2012):
https://smartpaster2010.codeplex.com/

## To build

To build, from v2 you need Visual Studio 2019, and include the extensibility SDK.
*  To support VS2015 (Visual Studio 14) you have to Install-Package Madskristensen.VisualStudio.SDK -Version 14.0.81-pre as the official Microsoft.VisualStudio.SDK only supports VS2017 and 2019

## Credits

The original SmartPaster (for VS 2003/2005/2008) by Alex Papadimoulis is here: http://weblogs.asp.net/alex_papadimoulis/archive/2004/05/25/Smart-Paster-1.1-Add-In---StringBuilder-and-Better-C_2300_-Handling.aspx
The original source code is here: http://code.google.com/p/smartpaster/
