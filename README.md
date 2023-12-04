# Numdum
## Overview
Numdum is an esoteric programming language revolving around assigning values to numbers. 
This means that if you state
>5=3,

>5+3 would be equal to 6.

Control flow moves bottom to top, rather than the typical top to bottom.
Whitespace except for newlines (tabs, and spaces) will cause an error immediately upon loading the script.
finally, the ending for the code has to be explcitly stated. this is done through the symbol '69'

## Variables
Any number that appears as a part of an expression alongside keywords is a variable. These numbers do not have fixed values like in many other programming languages, and can be reassigned new values. 
Numbers may be assigned any value representable by a floating-point number, but you may not intially declare a variable with a decimal place.

### Pointers
If a northern star (*) appears before a variable, the variable is instead trated as a 'pointer'. Similar to their C/C++ counterparts, they 'point' to something. Unlike their low-level variant, however, they point to another number rather than a memory address. for instance, say 
>4=3

And then

>3=2

If you read the value of 4 it would still be three, but the value of *4 would be the value of 3, which is equal to 2.

### GOTO destinations & end of script
If a number appears at the start of the line rather than inside one, for instance:
>84

It becomes a GOTO destination. These GOTO destinations are completely constant as their values do not change, nor do they support pointers in any way either. In fact, if a line starts with a northern star it will get completely ignored in excecution.
## Keywords
Numdum includes the following keywords:

	SET
	ADD
 	SUB
	MUL
	DIV
 	TO
	AND
 	INPUT
	INPUTR
 	OUTPUT
	OUTPUTR
 	GOTO

Of which can be split into the catergories:

Operators, User interactions and control flow.
TO and AND are in none of these catergories, and are rather 
Numbers used in coordination with these keywords act as variables.

### Operators
SET, ADD, SUB, MUL and DIV are the five operators in numdum.
All operators follow the following syntax:
> OPERATOR{left}AND{right}

with the exception of the SET operator using the following syntax:
> SET{left}TO{right}

Operators set the left variable's value to another value, in proportion to the left and right values.
The equation for all operators is as follows:

	SET: left = right
 	ADD: left = left + right
	SUB: left = left - right
 	MUL: left = left x right
	DIV: left = left ÷ right

### user interactions
User interactions are the four keywords (INPUT, INPUTR, OUTPUT, OUTPUTR) that allow for input to be taken from the person running the script, and all follow the following syntax:
> KEYWORD{variable}

Each user interaction keyword does the following:

	OUTPUT{variable} 

Prints one character to the console, being the value of variable converted to a character from an ASCII code

	OUTPUTR{variable} 

Prints a number to the console, being the value of variable.

Each user interaction keyword does the following:

	INPUT{variable} 

Sets the value of variable to the next character inputted by the user, converted to an ASCII code. It will echo the inputted character back to the console

	INPUTR{variable} 

Sets the value of variable to a number inputted by user. The number can be signed and decimal. The number will be echoed without a trailing newline. If the user inputs a non-valid number, they will be alerted and prompted to reinput a new value until a valid number is entered.

### GOTO
Goto statements are the only available way to change excecution order in numdum, as 
