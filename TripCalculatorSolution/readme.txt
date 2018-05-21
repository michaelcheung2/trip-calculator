========================================================================
AUTHOR: MICHAEL CHEUNG
========================================================================

========================================================================
PREREQUISITES
========================================================================
This project assumes you have the following installed:
-Visual Studio (VS 2012 was used here) 
-.NET Framework
-IIS

========================================================================
OVERVIEW
========================================================================
-The software solution resides in and can be launched from TripCalculatorSolution.sln, an ASP.NET Web Application.
-Unit Tests are resides in and can be executed from TripCalculatorTest.csproj, an ASP.NET Unit Test Project.
-The project is written using C#, ASP.NET, and HTML/CSS.
-The user interface is responsive and can be used in any browser (IE/Firefox/Chrome).
-HTML5 input fields are utilized for some validation.

========================================================================
INSTRUCTIONS FOR USE
========================================================================
1. Download all the source code from the Github repository that stores this solution.
2. Launch Visual Studio.
3. Open the project/solution "TripCalculatorSolution.sln"
4. (Optional) Open Visual Studio's Test Explorer to view/run any unit tests.
5. In the "TripCalculatorSolution" project, specify the browser of your choice in which to run/debug this project.
6. Run/debug this project. (The page Input.aspx should then load up on your localhost)
7. On the Trip Calculator interface, enter a name in the "Person Name" field. 
8. Enter an amount in the "Expense Amount" field, then press the "Add Expense" button to add the specified amount to that person's total expenses.
9. Press the "Add Person" button to add another person section.
10. When done adding people and expenses, press the "Computer Distribution" button to be redirected to the "Trip Calculator (results)" output page, where the expense distribution results are shown.

