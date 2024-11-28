@PLPRegression
@RegressionLive
Feature: Footer


Background: 
	Given I have navigated to the desktop home page
	Then I am redirected to the home page 


Scenario Outline: 00120 - Desktop Footer ABOUT US Navigation
     When I click on footer <footerlink> link
	 #Then I check the URL <URL> is correct - Internal Link
	 Examples: 
	 | footerlink         | url          |
	 | About Us           | about-us     |
	 | Contact Us         | contact-us   |
	 | Careers            | careers      |
	 | About Sharaf Group | sharaf-group |
	 | About Sharaf Group | sharaf-group |
	 | About Sharaf Group | sharaf-group |

