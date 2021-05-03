Feature: User Logins
**As a registered user I would like to be able to login so I may be able to have a customized experience.**

This feature ensures that users who have previously registered can successfully login and see a personalized message
that confirms they are recognized by the application and logged in.  It also *defines* a set of seeded users for 
future software test engineers to use when performing other kinds of tests.

The steps we define here can be re-used when testing the *register* feature.

To generate living documentation, create a Documentation folder and then run one of these from the project dir: 
    `livingdoc test-assembly -t bin\Debug\net5.0\TestExecution.json -o Documentation bin\Debug\net5.0\Fuji.BDDNunitTests.dll`
    `livingdoc feature-folder -t bin\Debug\net5.0\TestExecution.json -o Documentation .`

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

Scenario Outline: Existing user can login
	Given I am a user with first name '<FirstName>'
	When I login
#	Then I am redirected to '<Url>'
	Then I am redirected to the '<Page>' page
	  And I can see a personalized message in the navbar that includes my email
	Examples:
	| FirstName | Page |
	| Talia     | Home |
	| Zayden    | Home |
	| Hareem    | Home |
	| Krzysztof | Home |

Scenario Outline: Non-user cannot login
	Given I am a user with first name '<FirstName>'
	When I login
	Then I can see a login error message
#	  And an unsuccessful login attempt by '<FirstName>' is logged
	Examples:
	| FirstName |
	| Andre     |
	| Joanna    |

Scenario Outline: Non-user attempting to login is logged
	Given I am a user with first name '<FirstName>'
	When I login
	Then an unsuccessful login attempt by '<FirstName>' is logged
	Examples:
	| FirstName |
	| Andre     |
	| Joanna    |