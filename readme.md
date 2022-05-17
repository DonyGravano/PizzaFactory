# Pizza Factory

Please see the word document in this solution for the spec of this task

## Assumptions

-	The cooking interval for the pizzas starts after a pizza has finished cooking. A bit confused on what this interval is
-	The toppings cooking time calculation does not include whitespace

## Design notes
I've used dependency injection and interfaces to register everything as it provides good flexiblity for testing and if logic/implementations are ever needed to be swapped out.  
It feels a bit wrong to have the PizzaFactory saving the pizzas as it doesn't feel like it should be it's responsibility, but the requirements stated it had to be written to file after it cooked so it was just simpler to do it in there.  
I've left things in one project however if the project were larger I'd break it down into the following projects/Layers:
-	Application - This would contain any pizza application specific logic, i.e. the Shop, Factory and CookingTimeCalculator
-	DataProviders - These would be implementions of certain interfaces like the DataRepository
-	Abstractions - This would contain the interfaces so that specific implementations could exist in isolation
-	Models - Model files

## Testing
I did not TDD this as I was unsure how much time I'd have to do this, so I added the tests after using red, green, refactor.
I've used NUnit as it's what i'm most familiar with so it was the quickest to get setup, however with more time i'd opt for XUnit
I've made use of FluentValidations, Moq and AutoFixture for the test, it might seem a bit overkill though