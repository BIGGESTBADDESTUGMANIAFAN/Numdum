#pragma once
#include <stdbool.h>
#include <string.h>
#include <stdlib.h>


//Returns true if main is prefixed with prefix, otherwise returns false
bool HasPrefix(const char *Main, char *Prefix )
{
	if (strlen(Main) < strlen(Prefix)) //Would throw error if main was shorter than prefix otherwise.
	{
		return false;
	}
	for (size_t i = 0; i < strlen(Prefix) ; i++) //Loop over every character in the prefix
	{
		if (Main[i] != Prefix[i]) //if the main and prefix character does not match up, return false
		{
			return false;
		}
	}
	return true; //if all corresponding characters in main are true, return true.
}


//will set str to a pointer to a new address, containing itself with the first amount characters removed.
#define strshorten(str, amount) char *newstr = (char*)malloc(strlen(str) - amount + 1); for (size_t i = 0; i < strlen(newstr); i++) { newstr[i] = str[i + amount];} free(str); str = newstr;
/*
//strshorten(str, amount) neatened and commented code:
 
 //Create a new string (char pointer) called newstr, and reserve it a block of memory equal to: 
 //<strlen of str> - <the amount of characters being removed> + 1
 the <strlen of str> - <amount of characters being removed> will be the length of the text component of the new string. Add 1 to make enough space for the null character.
 no need to multiply by sizeof(char) as sizeof(char) = 1U.
char *newstr = (char*)malloc(strlen(str) - amount + 1); 
for (size_t i = 0; i < strlen(str) - amount + 1; i++){ //Start a for loop to iterate over every character in newstr.
	newstr[i] = str[i + amount]; //set every character in newstr to its corresponding character in str.
} 
free(str); //free up memory taken by old str
str = newstr; //set str to the newstr.
//Made a macro, as it is cleaner and more efficient as a macro than a function.
*/