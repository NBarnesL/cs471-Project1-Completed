This is my submission for CS471 Spring 2021 Project 1. Code is found in cs471-Project1-w-multipledatagrids folder.

This project was developed in C# in Visual Studio 2019. I used Windows Forms because learning Qt is a pain.

Form1.cs: Contains functions for actions taken on the GUI.

Form1.Designer.cs: Windows form Layout

Process.cs: Contains the Process class that mimics a computer process. It has its own burst time, and can start and stop at the call of the dispatcher.

Dispatcher.cs: Contains the Dispatcher Class that runs the Priority Queue. Communicates with the GUI to run and display processes. Also contains a Priority Queue class that sorts programs based on their priority with the Process' CompareTo() function.

Program.cs: Contains the main function that drives the whole program. Its kind of small since this is OOP.


