Feature: FirstExample
![Calculator](https://specflow.org/wp-content/uploads/2020/09/calculator.png)

Simple calculator for adding **two** numbers

Link to a feature: [Calculator](Fuji.BDDTests/Features/Calculator.feature)
***Further read***: **[Learn more about how to generate Living Documentation](https://docs.specflow.org/projects/specflow-livingdoc/en/latest/LivingDocGenerator/Generating-Documentation.html)**


Scenario Outline: Calculator can add two numbers
	Given the first number is <leftOp>
	  And the second number is <rightOp>
	When the two numbers are added
	Then the result should be <answer>
	Examples:
	  | leftOp | rightOp | answer |
	  | 45     | 17      | 62     |
	  | 17     | 45      | 62     |
	  | 0      | 17      | 17     |
	  | 17     | 0       | 17     |
	  | 0      | 0       | 0      |

Scenario Outline: Calculator can subtract two numbers
	Given the first number is <leftOp>
	  And the second number is <rightOp>
	When the two numbers are subtracted
	Then the result should be <answer>
	Examples:
	  | leftOp | rightOp | answer |
	  | 45     | 17      | 28     |
	  | 17     | 45      | -28    |
	  | 0      | 17      | -17    |
	  | 17     | 0       | 17     |
	  | 0      | 0       | 0      |


Scenario Outline: Calculator can multiply two numbers
	Given the first number is <leftOp>
	  And the second number is <rightOp>
	When the two numbers are multiplied
	Then the result should be <answer>
	Examples:
	  | leftOp | rightOp | answer |
	  | 45     | 17      | 765    |
	  | 17     | 45      | 765    |
	  | 0      | 17      | 0      |
	  | 1      | 17      | 17     |
	  | 0      | 0       | 0      |