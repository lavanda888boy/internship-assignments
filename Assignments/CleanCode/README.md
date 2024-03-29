# Clean Code Refactoring

* What code smells did you see? 												
	The code, being refactored had various code smells. First of all, I observed the `Rigidity` of the code. The constants which were used validate the speaker were placed in if-statements as some magic numbers. Moreover, code immobility was obvious because of the lack of general conditions. Finally, `Needless Complexity` and `Opacity` were created by the redundant bool variables and if-statements;

* What problems do you think the Speaker class has?
		
	In addition to the problems mentioned earlier, I can also remark the presence of the class-scoped lists inside of the methods (the lists of topics, employers were initialized inside of the `Register` method). Furthermore, the exceptions were not properly handled;

* Which clean code principles (or general programming principles) did it violate?

	Regarding the clean code principles, the code violated the `KISS` principle while it introduced unnecessary variables and verifications. Moreover, the `Register` function was too long and was not divided in separate single-scope functions. Also the code contained commented lines of old implementations (it did not correspond to the `YAGNI` principle);

* What refactoring techniques did you use?

	In order to refactor the code, I organized the data by making the sorting reference lists readonly and private class fields. Moreover, I simplified the conditional statements by creating bool validation functions. Finally, I removed the unnecessary bool markers in order to make the code simplier and more optimized.