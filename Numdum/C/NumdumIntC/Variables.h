#include <stdio.h>
#include <stdlib.h>
#include <stdbool.h>

//structure that represents 1 variable. Contains a number, a floating point value and a pointer to the next variable.
struct Variable { int statedvalue; float truevalue; struct Variable* Next; };
static int AmtofVariables = 0;

//FirstVar (first) variable
static struct Variable *FirstVar;
//FinalVar (final) variable
static struct Variable *FinalVar;

//Adds a new variable to the array. NEVER directly call this, it will automatically get called where appropriate.
void CreatenewVariable(int Statedvalue)
{
	struct Variable *newvar = (struct Variable*)malloc(sizeof(struct Variable));
	if (AmtofVariables > 0)
	{
		FinalVar->Next = newvar;
		FinalVar = newvar;
		FinalVar->truevalue = Statedvalue;
		FinalVar->statedvalue = Statedvalue;
	}
	else
	{
		FirstVar = newvar;
		FinalVar = newvar;
		FinalVar->truevalue = Statedvalue;
		FinalVar->statedvalue = Statedvalue;
	}
	AmtofVariables += 1;
}



struct Variable* LookupVariable(int Key)
{
	struct Variable *Varon = FirstVar;
	for(int Idx = 0; Idx < AmtofVariables; Idx++)
	{
		if (Varon->statedvalue == Key)
		{
			return Varon;
		}
		Varon = Varon->Next;
	}
	CreatenewVariable(Key);
	return FinalVar;
}