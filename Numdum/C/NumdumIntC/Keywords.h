#pragma once
#include <string.h>
#include "Misc.h"
#include <stdbool.h>
#include <stdlib.h>
#include <math.h>
typedef enum Token
{
    ADD, MUL, SUB, DIV, SET, TO, AND, OUTPUT, OUTPUTR, INPUT, INPUTR, GOTO, DEST, END, NUM, PTR
};


struct Line
{
	int *Tokens;
	struct Line* Next;
};
struct Line* Firstline;
struct Line* Finalline;
int numberoflines = 0;


//Adds new line. note that it unconventionally prepends the lines. this is to make the code read back-to-front.
void Addline(int* Tokens, int NumofTokens)
{
	struct Line* newline = malloc(sizeof(struct Line));
	if (numberoflines == 0)
	{
		Firstline = newline;
		Finalline = newline;
		Finalline->Next = NULL;
	}
	else
	{
		newline->Next = Firstline;
		Firstline = newline;
	}
	Firstline->Tokens = malloc(sizeof(int)*NumofTokens);
	for (size_t i = 0; i < NumofTokens; i++)
	{
		Firstline->Tokens[i] = Tokens[i];
	}
	++numberoflines;
}


void BreakintoTokens(const char *line)
{
	int Tokens[6]; //Temporary list of tokens, 6 is the longest amount of tokens that should appear on one line.
	short int TknIDX = 0; //Temporary int, is the index of the next empty space in the Tokens list.
	char* templine = (char*)malloc(strlen(line) + 1);
	for (size_t i = 0; i < strlen(line); i++)
	{
		templine[i] = line[i];
	}
	templine[strlen(line)] = '\0';
	if (atoi(templine) != 0 || templine[0] == '0')
	{
		int Lineval = atoi(templine);
		int Numlen = Lineval == 0 ? 1 : (floor(log10(abs(Lineval))) + 1) + (Lineval < 0 ? 1 : 0);
		if (Lineval == 69)
		{
			Tokens[0] = END;
			strshorten(templine, Numlen);
			TknIDX += 1;
		}
		else {
			Tokens[0] = DEST;
			strshorten(templine, Numlen);
			Tokens[1] = Lineval;
			TknIDX += 2;
		}

	}
	while (TknIDX < 6 && strlen(templine) != 0)
	{
		if (HasPrefix(templine, "ADD")) { strshorten(templine, 3); Tokens[TknIDX] = ADD; ++TknIDX; continue; }
		if (HasPrefix(templine, "SUB")) { strshorten(templine, 3); Tokens[TknIDX] = SUB; ++TknIDX; continue; }
		if (HasPrefix(templine, "MUL")) { strshorten(templine, 3); Tokens[TknIDX] = MUL; ++TknIDX; continue; }
		if (HasPrefix(templine, "DIV")) { strshorten(templine, 3); Tokens[TknIDX] = DIV; ++TknIDX; continue; }
		if (HasPrefix(templine, "SET")) { strshorten(templine, 3); Tokens[TknIDX] = SET; ++TknIDX; continue; }
		if (HasPrefix(templine, "AND")) { strshorten(templine, 3); Tokens[TknIDX] = AND; ++TknIDX; continue; }
		if (HasPrefix(templine, "TO")) { strshorten(templine, 2); Tokens[TknIDX] = TO; ++TknIDX; continue; }
		if (HasPrefix(templine, "OUTPUTR")) { strshorten(templine, 7); Tokens[TknIDX] = OUTPUTR; ++TknIDX; continue; }
		if (HasPrefix(templine, "INPUTR")) { strshorten(templine, 6); Tokens[TknIDX] = INPUTR; ++TknIDX; continue; }
		if (HasPrefix(templine, "INPUT")) { strshorten(templine, 5); Tokens[TknIDX] = INPUT; ++TknIDX; continue; }
		if (HasPrefix(templine, "OUTPUT")) { strshorten(templine, 6); Tokens[TknIDX] = OUTPUT; ++TknIDX; continue; }
		if (HasPrefix(templine, "GOTO")) { strshorten(templine, 4); Tokens[TknIDX] = GOTO; ++TknIDX; continue; }
		if (HasPrefix(templine, "*")) { strshorten(templine, 1); Tokens[TknIDX] = PTR; ++TknIDX; continue; }
		if ((atoi(templine) != 0 || templine[0] == '0'))
		{
			if (Tokens[TknIDX - 1] != PTR && TknIDX < 5)
			{
				Tokens[TknIDX] = NUM;
				TknIDX += 1;
			}
			int Lineval = atoi(templine);
			int Numlen = Lineval == 0 ? 1 : (floor(log10(abs(Lineval))) + 1) + (Lineval < 0 ? 1 : 0);
			strshorten(templine, Numlen);
			Tokens[TknIDX] = Lineval;
			TknIDX += 1;
			continue;
		}
		while (HasPrefix(templine, "\r")) { strshorten(templine, 1); }
		while (HasPrefix(templine, "\n")) { strshorten(templine, 1); }
		break;
	}
	if (Syntaxcheck(Tokens, TknIDX) == 1)
	{
		Addline(Tokens, TknIDX);
	}
	free(templine);
}

int Syntaxcheck(int *Line, int Len)
{
	switch (Line[0])
	{
	case ADD:
		return ((Line[1] == NUM || Line[1] == PTR) && Line[3] == AND && (Line[4] == NUM || Line[4] == PTR)) && Len == 6;
	case MUL:
		return ((Line[1] == NUM || Line[1] == PTR) && Line[3] == AND && (Line[4] == NUM || Line[4] == PTR)) && Len == 6;
	case SUB:
		return ((Line[1] == NUM || Line[1] == PTR) && Line[3] == AND && (Line[4] == NUM || Line[4] == PTR)) && Len == 6;
	case DIV:
		return ((Line[1] == NUM || Line[1] == PTR) && Line[3] == AND && (Line[4] == NUM || Line[4] == PTR)) && Len == 6;
	case SET:
		return ((Line[1] == NUM || Line[1] == PTR) && Line[3] == TO && (Line[4] == NUM || Line[4] == PTR)) && Len == 6;
	case OUTPUT:
		return (Line[1] == NUM || Line[1] == PTR) && Len == 3;
	case OUTPUTR: 
		return (Line[1] == NUM || Line[1] == PTR) && Len == 3;
	case INPUT:
		return (Line[1] == NUM || Line[1] == PTR) && Len == 3;
	case INPUTR:
		return (Line[1] == NUM || Line[1] == PTR) && Len == 3;
	case GOTO:
		return (Line[1] == NUM || Line[1] == PTR) && Len == 3;
	case DEST:
		return Len == 2;
	case END:
		return Len == 1;
	default:
		return false;
	}
}