CONTENTS OF THIS FILE
---------------------
 * Programming Exercise Overciew
 * General Instructions 
 * Import to note
 * TimeLog


 Programming Exercise Overciew
------------
A design flaw has been found in a type of HVAC unit. The unit can be in one of four stages (0-3) depending on environmental conditions. 
The manufacturer has noted a design flaw where the unit can accidentally go through a specific sequence of stages leading to possible damage. 
The manufacturer has noted that a fault is indicated by four operations that occur in sequence: 

1. Stage 3 for five minutes or more 
2. Stage 2, 
3. Any number of cycles between stage 2 and 3 for any duration 
4. Stage 0 

My task was to implement a log parser that detects and counts occurrences of this fault sequence.


General Instructions 
---------------------
1) Open the project with your favorite code editor , I used visual studio
2) Go into the Program.cs file, and you may edit any files you see, or use the default ones provided.
3) type dotnet into the terminal, to start the program


Import to note
---------------------
The issue with streamreader, is that it was designed for a single thread use, and should be replaced immediately with a better option.
Due to the nature of the project, I had to go off the instructions provided, and believe there is a lot of room for improvement.

TimeLog
---------------------
I have also included a csv file with my timelog, and more details in how I spent my hours working through the coding challenge.
