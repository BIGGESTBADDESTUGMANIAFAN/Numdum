#include <stdio.h>
#include <stdlib.h>
#include "Variables.h"
#include "Keywords.h"
#include "Misc.h"
#include <conio.h>
int main(int argc, char **argv)
{

	if (argc == 1)
	{
		printf_s("%s", "You must give a file directory as an argument. \nPress any key to close.");
		_getch();
		return 1;
	}
#pragma warning(push)
#pragma warning( disable : 4996)
	FILE* file = fopen(argv[1], "r");
	if (file == NULL)
	{
		printf_s("%s", "No such file exists. \nPress any key to close.");
		_getch();
		return 1;
	}

	while (true)
	{
		int Buffersize = 0;
		char* Line;
		do
		{
			Buffersize += 25;
			Line = (char*)calloc(Buffersize, 1);
			if (Line == NULL)
			{
				printf_s("%s", "Download more ram!!!!!!!!!!! \nPress any key to close.");
				_getch();
				return 1;
			}
			if (fgets(Line, Buffersize, file) == NULL)
			{
				free(Line);
				goto AfterReadingFile;
			}
		} while (Line[Buffersize - 1] != NULL);
		BreakintoTokens(Line);
		free(Line);
	}
	AfterReadingFile:  ;

	struct Line *Lineon = Firstline;
	char Input = '\0';
	float NumInp;
	struct Line *TempLine;
	while (true) {
		int* LineOnTkns = Lineon->Tokens;
		
		switch (LineOnTkns[0])
		{
			case END:
				printf_s("%s", "End of program reached.");
				return 0;
			case ADD:
				if (LineOnTkns[1] == 14) {
					LookupVariable(LineOnTkns[2])->truevalue += (LineOnTkns[4] != 15 ? LookupVariable(LineOnTkns[5])->truevalue : LookupVariable((int)(LookupVariable(LineOnTkns[5])->truevalue))->truevalue);
				}
				else {
					LookupVariable((int)(LookupVariable(LineOnTkns[2])->truevalue))->truevalue += (LineOnTkns[4] != 15 ? LookupVariable(LineOnTkns[5])->truevalue : LookupVariable((int)(LookupVariable(LineOnTkns[5])->truevalue))->truevalue);
				}
				break;
			case MUL:
				if (LineOnTkns[1] == 14) {
					LookupVariable(LineOnTkns[2])->truevalue *= (LineOnTkns[4] != 15 ? LookupVariable(LineOnTkns[5])->truevalue : LookupVariable((int)(LookupVariable(LineOnTkns[5])->truevalue))->truevalue);
				}
				else {
					LookupVariable((int)(LookupVariable(LineOnTkns[2])->truevalue))->truevalue *= (LineOnTkns[4] != 15 ? LookupVariable(LineOnTkns[5])->truevalue : LookupVariable((int)(LookupVariable(LineOnTkns[5])->truevalue))->truevalue);
				}
				break;
			case SUB:
				if (LineOnTkns[1] == 14) {
					LookupVariable(LineOnTkns[2])->truevalue -= (LineOnTkns[4] != 15 ? LookupVariable(LineOnTkns[5])->truevalue : LookupVariable((int)(LookupVariable(LineOnTkns[5])->truevalue))->truevalue);
				}
				else {
					LookupVariable((int)(LookupVariable(LineOnTkns[2])->truevalue))->truevalue -= (LineOnTkns[4] != 15 ? LookupVariable(LineOnTkns[5])->truevalue : LookupVariable((int)(LookupVariable(LineOnTkns[5])->truevalue))->truevalue);
				}
				break;
			case DIV:
				if (LineOnTkns[1] == 14) {
					LookupVariable(LineOnTkns[2])->truevalue /= (LineOnTkns[4] != 15 ? LookupVariable(LineOnTkns[5])->truevalue : LookupVariable((int)(LookupVariable(LineOnTkns[5])->truevalue))->truevalue);
				}
				else {
					LookupVariable((int)(LookupVariable(LineOnTkns[2])->truevalue))->truevalue /= (LineOnTkns[4] != 15 ? LookupVariable(LineOnTkns[5])->truevalue : LookupVariable((int)(LookupVariable(LineOnTkns[5])->truevalue))->truevalue);
				}
				break;
			case SET:
				if (LineOnTkns[1] == 14) {
					LookupVariable(LineOnTkns[2])->truevalue = ((LineOnTkns[4] != 15) ? LookupVariable(LineOnTkns[5])->truevalue : LookupVariable((int)(LookupVariable(LineOnTkns[5])->truevalue))->truevalue);
				}
				else {
					LookupVariable((int)(LookupVariable(LineOnTkns[2])->truevalue))->truevalue = (LineOnTkns[4] != 15 ? LookupVariable(LineOnTkns[5])->truevalue : LookupVariable((int)(LookupVariable(LineOnTkns[5])->truevalue))->truevalue);
				}
				break;
			case OUTPUT:
				if (LineOnTkns[1] == 14) {
				printf_s("%c", (char)(int)LookupVariable(LineOnTkns[2])->truevalue);
				}
				else {
					printf_s("%c", (char)(int)LookupVariable((int)(LookupVariable(LineOnTkns[2])->truevalue))->truevalue);
				}	
				break;
			case OUTPUTR:
				if (LineOnTkns[1] == 14) {
					printf_s("%f", LookupVariable(LineOnTkns[2])->truevalue);
				}
				else {
					printf_s("%f", LookupVariable((int)(LookupVariable(LineOnTkns[2])->truevalue))->truevalue);
				}
				break;
			case INPUT:
				Input = getch();
				if (Input == 13) {
					printf_s("%c", '\n');
				}
				else {
					printf_s("%c", Input);
				}
				if (LineOnTkns[1] == 14) {
					LookupVariable(LineOnTkns[2])->truevalue = Input;
				}
				else {
					LookupVariable((int)(LookupVariable(LineOnTkns[2])->truevalue))->truevalue = Input;
				}
				break;
			case INPUTR:
				scanf_s("%f", &NumInp);
				if (LineOnTkns[1] == 14) {
					LookupVariable(LineOnTkns[2])->truevalue = NumInp;
				}
				else {
					LookupVariable((int)(LookupVariable(LineOnTkns[2])->truevalue))->truevalue = NumInp;
				}
				break;
			case GOTO:
				TempLine = Firstline;
				while (TempLine->Next != NULL)
				{
					if (TempLine->Tokens[0] == DEST && TempLine->Tokens[1] == (int)(LineOnTkns[1] == 15 ? LookupVariable((int)(LookupVariable(LineOnTkns[2])->truevalue))->truevalue : (LookupVariable(LineOnTkns[2])->truevalue)))
					{
						Lineon = TempLine;
					}
					TempLine = TempLine->Next;
				}
				break;
		}
		if (Lineon->Next != NULL)
		{
			Lineon = Lineon->Next;
		}
		else
		{
			Lineon = Firstline;
		}
	}
#pragma warning(pop)
}