The program operates per the description of the exercise.
I applied the montra "Code for the future, but also for the present." That means that I used Intefaces in order to help the testability of the program,
but I also didn't go for an over-engineered, over-complicated design, where everything is customizable, everything is using generics etc.

Design:
The application is, as required, a .NET core web api that accept requests for sorting integer arrays.
The controller accept post and get requests.
In order to have a persistent storage, I normally would have gone for a DB (see why I didn't below).
Instead, I went for a simple file storage solution. All requests are stored in files, whose name differs depending on the ID.
The controller of the api only reads and creates new files, by converting their content to and from in-memory JSON objects.
However, in order to make it async, I made the sorting be done via a timer (scheduler).
Normally, this would be another service running and reading from the DB, but for simplicity I made it a scheduler that reads files, sorts, then writes back.

In addition, I have added a basic test, to see how I would test my input.

If I had to make this as a bunch of Production ready services, I would take different decisions such as:
-make the repository a Database (I could not do that here, as I do not know if the machine this will be tested on will have any kind of DB server installed on it)
-make logging, including exception handling
-use dependency injection where needed, to pass the interfaces (I did not do that here, as I did not want to use external NuGet packages, per exercise description)
-make some things more general, such as more sorting algorithms, as well as allow to specify ascending or descending sort order
-transform the scheduler into a standalone service

To run it:
Just copy it on your hard disc, open the solution, click Run.
Then send requests POST to https://localhost:44327/mergesort and GET requests to https://localhost:44327/mergesort/executions
I assume your program will be able to create files in its parent folder, but if you want a diffrent folder, just change the path when creating the repository

Given more time, I would have done things a little better, but I wanted to constrain myself within the recommended 6 hours and show what code I can produce.