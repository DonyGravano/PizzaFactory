# Pizza Factory

Please see the word document in this solution for the spec of this task

## Assumptions

-	The cooking interval for the pizzas starts after a pizza has finished cooking. A bit confused on what this interval is
-	The toppings cooking time calculation does not include whitespace
-	Nothing is stated about what to do with the text file should it already exist, so the code currently just appends to an existing file
-	That writing to the console when a pizza is written to a file is OK

## Design notes
I've used dependency injection and interfaces to register everything as it provides good flexiblity for testing and if logic/implementations are ever needed to be swapped out.  
The configs could be done as one file however I've split them out just so it's they're smaller and more focussed.  
I've created a wrapper for the random objects so that this can be mocked during the tests  
I've created a wrapper for the delay so that it doesn't impede the tests
It feels a bit wrong to have the PizzaFactory saving the pizzas as it doesn't feel like it should be it's responsibility, but the requirements stated it had to be written to file after it cooked so it was just simpler to do it in there.  
I've left things in one project however if the project were larger I'd break it down into the following projects/Layers:
-	Application - This would contain any pizza application specific logic, i.e. the Shop, Factory and CookingTimeCalculator
-	DataProviders - These would be implementions of certain interfaces like the DataRepository
-	Abstractions - This would contain the interfaces so that specific implementations could exist in isolation
-	Models - Model files

## Testing
I did not TDD this as I was unsure how much time I'd have to do this, so I added the tests after using the red, green, refactor process    
I've used NUnit as it's what i'm most familiar with so it was the quickest to get setup, however with more time i'd opt for XUnit    
I've made use of FluentValidations, Moq and AutoFixture for the tests, it might seem a bit overkill but it's my preferred way  

## What improvements would I make
Sadly I don't have ReSharper on my personal PC so could not use that for code clean-up and formatting.   
I'd also like to make use of JetBrains' dotCover tool to run code coverage over the solution and see what tests I'm missing  
I've used the .net6 new console app template for the program.cs but I can't seem to call it for a test, i'd like investigate this so that I can test that the DI works correctly  
Create a wrapper around the StreamWriter in the FileRepository so it can be tested