@FunctionalTests
Feature: QAWorksTests
	Perform some basic tests on the QAWorks website.
	As a user of the QAWorks website

	Scenario: As an end user I want a contact us page so that I can find out more about QAWorks exciting services
	Given I am on the QAWorks web site
	When I click on the "ContactUs" main menu item
	Then I land on the "ContactUs" page
	And I should be able to contact QAWorks with the following information
	| name     | email                | message                                   |
	| j.Bloggs | j.Bloggs@qaworks.com | please contact me I want to find out more |
	And the "ContactUs" page is redisplayed

	Scenario Outline: As an end user when I am on the Contact Us page I want to be told about incorrect or missing data
	Given I am on the QAWorks web site
	When I click on the "ContactUs" main menu item
	Then I land on the "ContactUs" page
	When I populate the ContactUs form with data <name_email_message>
	Then I should see error text <errortext>
	Examples: 
	| name_email_message                                                 | errortext                                                                   |
	| " ~j.Bloggs@qaworks.com~please contact me I want to find out more" | Your name is required                                                       |
	| "j.Bloggs~ ~please contact me I want to find out more"             | An Email address is required                                                |
	| "j.Bloggs~j.Bloggs@qaworks.com~ "                                  | Please type your message                                                    |
	| " ~ ~ "                                                            | Your name is required~An Email address is required~Please type your message |
	| "j.Bloggs~j.Bloggs~please contact me I want to find out more"      | Invalid Email Address                                                       |

#	#This test fails due to unhandled error HTTP error 500
#	Scenario: As an end user when I submit the ContactUs form with an invalid email address the form is not submitted I am informed that my email address is invalid
#	Given I am on the QAWorks web site
#	When I click on the "ContactUs" main menu item
#	Then I land on the "ContactUs" page
#	And I should be able to contact QAWorks with the following information
#	| name     | email    | message                                   |
#	| j.Bloggs | j.Bloggs | please contact me I want to find out more |
#	And the "ContactUs" page is redisplayed
#	And I should see error text "Invalid Email Address"