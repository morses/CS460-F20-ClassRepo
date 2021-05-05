Feature: As a user I can eat an apple so I can be healthy
<img src="https://upload.wikimedia.org/wikipedia/commons/5/53/Apple_in_lightbox.png" width=150 />
On the home page, a user should be able to see:
- all the varieties of apples stored in the system
- with their common name
- and scientific name
- and how many of each they have eaten
They should also be able to click on any apple to indicate they have eaten one
after which they should be able to see that the counter for that apple has incremented
by one.

Background:
	Given the following users exist
	  | UserName   | Email                 | FirstName | LastName | Password  |
	  | TaliaK     | knott@example.com     | Talia     | Knott    | Hello123# |
	  | ZaydenC    | clark@example.com     | Zayden    | Clark    | Hello123# |
	  | DavilaH    | hareem@example.com    | Hareem    | Davila   | Hello123# |
	  | KrzysztofP | krzysztof@example.com | Krzysztof | Ponce    | Hello123# |
	And the following users do not exist
	  | UserName | Email               | FirstName | LastName | Password  |
	  | AndreC   | colea@example.com   | Andre     | Cole     | 0a9dfi3.a |
	  | JoannaV  | valdezJ@example.com | Joanna    | Valdez   | d9u(*dsF4 |

Scenario: A user can see apple varieties
	Given I login
	When they are on the Home page
	Then they should see the apple varieties

Scenario: A user can eat an apple
	Given I have an apple
	When I eat the apple
	Then the apples eaten count should increment by one