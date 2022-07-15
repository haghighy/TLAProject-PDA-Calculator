using System;
using System.Collections.Generic;
using System.Linq;

public class q2
{

	public static bool isBalanced(string expression, int size)
	{
		Stack<char> symbols = new Stack<char>();
		for(int i=0 ; i < size; i+=1)
		{
			if(expression[i] == '(')
				symbols.Push(expression[i]);
			else if(expression[i] == ')')
			{
				if(symbols.Count == 0)
					return false;
				char ch=symbols.Pop();
				if(ch=='(' && expression[i]==')')
					continue;
				else
					return false;
			}
				
		}
		if(symbols.Count == 0)
			return true;
		else
			return false;
	}
	public static double CalcByBinaryOperation(char op, double rightHand, double leftHand)
	{
		if (op == '+')
			return leftHand + rightHand;
		if (op == '-')
			return leftHand - rightHand;
		if (op == '*')
			return leftHand * rightHand;
		if (op == '/')
		{
			if (rightHand == 0)
				return double.MaxValue;
			else
				return leftHand / rightHand; 
		}
		if (op == '^')
			return  Math.Pow(leftHand, rightHand);
		return 0;
	}
	public static double CalcByOneOperation(char op, double rightHand)
	{
		if (op == 'c')
			return Math.Cos(rightHand);
		if (op == 's')
			return Math.Sin(rightHand);
		if (op == 'q')
		{
			if (rightHand < 0)
				return double.MaxValue;
			else
				return Math.Sqrt(rightHand);
		}
		if (op == 'l')
		{
			if (rightHand <= 0)
				return double.MaxValue;
			else
				return Math.Log(rightHand);
		}
		if (op == 'a')
			return Math.Abs(rightHand);
		if (op == 't')
			return Math.Tan(rightHand);
		if (op == 'e')
			return Math.Exp(rightHand);
		return 0;
	}
	public static bool IsNumber(char x)
	{
		if (x >= '0' && x <= '9')
			return true;
		return false;
	}
	public static double CalculatePhrase(string AlgebraPhrase)
	{
		char[] phraseChars = AlgebraPhrase.ToCharArray();
		Stack<double> Nums = new Stack<double>();
		Stack<char> Operations = new Stack<char>();
		int i = 0;
		while(i<phraseChars.Length)
		{
			if (phraseChars[i] == ' ')
			{
				i++;
				continue;
			}
			if (IsNumber(phraseChars[i]))
			{
				if (i >= 2 && (phraseChars[i-2] != '+' && phraseChars[i-2] != '-' && phraseChars[i-2] != '*' && phraseChars[i-2] != '/' && phraseChars[i-2] != '^' && phraseChars[i-2] != 'n' && phraseChars[i-2] != 's'&& phraseChars[i-2] != 'p'&& phraseChars[i-2] != 't' && phraseChars[i-2] != ' '))
					return double.MaxValue;
				string twoDigit = "";
				if ((i>4 && phraseChars[i-1] == '-' && (phraseChars[i-3]=='*' || phraseChars[i-3]=='+' || phraseChars[i-3]=='-' || phraseChars[i-3]=='/' || phraseChars[i-3]=='^')) || (i==1 && phraseChars[i-1] == '-' ))
				{
					twoDigit += "-";
					Operations.Pop();
				}
				while ( i < phraseChars.Length && ((IsNumber(phraseChars[i])) || phraseChars[i] == '.') )
				{
					if (i >0 && phraseChars[i] == '.' && phraseChars[i-1] == '.')
						return double.MaxValue;
					twoDigit += phraseChars[i++];
				}
				Nums.Push(double.Parse(twoDigit));
				i--;
			}
			else if (phraseChars[i] == '(')
				Operations.Push(phraseChars[i]);
			else if (phraseChars[i] == ')')
			{
				if (i >0 && phraseChars[i-1] == '(')
					return double.MaxValue;
				while (Operations.Peek() != '(')
				{
					char op = Operations.Pop();
					double x = 0;
					if (op == '+' || op == '-' || op == '*' || op == '/' || op == '^')
						x = CalcByBinaryOperation(op, Nums.Pop(), Nums.Pop());
					if (op == 's' || op == 'c' || op == 't' || op == 'q' || op == 'e' || op == 'l' || op == 'a')
						x = CalcByOneOperation(op, Nums.Pop());
					if (x == double.MaxValue)
						return x;
					Nums.Push(x);
				}
				Operations.Pop();
			}
			else if (phraseChars[i] == '+' || phraseChars[i] == '-' || phraseChars[i] == '*' || phraseChars[i] == '/' || phraseChars[i] == '^' || phraseChars[i] == 's' || phraseChars[i] == 'c' || phraseChars[i] == 't' || phraseChars[i] == 'q' || phraseChars[i] == 'e' || phraseChars[i] == 'l' || phraseChars[i] == 'a')
			{
				if (phraseChars[i] == '-' && (i == 0 || (i>1 && (phraseChars[i-2]=='*' || phraseChars[i-2]=='+' || phraseChars[i-2]=='-' || phraseChars[i-2]=='/' || phraseChars[i-2]=='^' || phraseChars[i-1]=='('))))
					Operations.Push('-');
				else
				{
					if (phraseChars[i] == '+' || phraseChars[i] == '-' || phraseChars[i] == '*' || phraseChars[i] == '/' || phraseChars[i] == '^')
					{
						if (Nums.Count == 0)
							return double.MaxValue;
						if (i >0 && (phraseChars[i-1] == '+' || phraseChars[i-1] == '-' || phraseChars[i-1] == '*' || phraseChars[i-1] == '/' || phraseChars[i-1] == '^'))
							return double.MaxValue;
					}
					while (Operations.Count > 0 && OrderOfPriority(phraseChars[i], Operations.Peek()))
					{
						char op = Operations.Pop();
						double x = 0;
						if (op == '+' || op == '-' || op == '*' || op == '/' || op == '^')
							x = CalcByBinaryOperation(op, Nums.Pop(), Nums.Pop());
						if (op == 's' || op == 'c' || op == 't' || op == 'q' || op == 'e' || op == 'l' || op == 'a')
							x = CalcByOneOperation(op, Nums.Pop());
						if (x == double.MaxValue)
							return x;
						Nums.Push(x);
					}
					if (phraseChars[i] == '+' || phraseChars[i] == '-' || phraseChars[i] == '*' || phraseChars[i] == '/' || phraseChars[i] == '^')
					{
						Operations.Push(phraseChars[i]);
					}
					if (phraseChars[i] == 's' || phraseChars[i] == 'c' || phraseChars[i] == 't' || phraseChars[i] == 'q' || phraseChars[i] == 'e' || phraseChars[i] == 'l' || phraseChars[i] == 'a')
					{
						if (phraseChars[i] == 's' && phraseChars[i+1] == 'q')
							Operations.Push('q');
						else
							Operations.Push(phraseChars[i]);
						if ((phraseChars[i] == 's' && phraseChars[i+1] == 'i') || phraseChars[i] == 'c' || phraseChars[i] == 't' || phraseChars[i] == 'e'  || phraseChars[i] == 'a')
							i += 2;
						if (phraseChars[i] == 'l')
							i += 1;
						if (phraseChars[i] == 's' && phraseChars[i+1] == 'q')
							i += 3;
					}
				}
			}
			i++;
		}
		while (Operations.Count > 0)
		{
			char op = Operations.Pop();
			double x = 0;
			if (op == '+' || op == '-' || op == '*' || op == '/' || op == '^')
				x = CalcByBinaryOperation(op, Nums.Pop(), Nums.Pop());
			if (op == 's' || op == 'c' || op == 't' || op == 'q' || op == 'e' || op == 'l' || op == 'a')
				x = CalcByOneOperation(op, Nums.Pop());
			if (x == double.MaxValue)
				return x;
			Nums.Push(x);
		}
		return Nums.Pop();
	}
	public static bool OrderOfPriority(char op1, char op2)
	{
		if (op2 == '(' || op2 == ')')
			return false;
		if ((op1 == '*' || op1 == '/') && (op2 == '+' || op2 == '-'))
			return false;
		if (( op1 == '^' ) && (op2 == '*' || op2 == '/' || op2 == '+' || op2 == '-'))
			return false;
		if ((op1 == 's' || op1 == 'c' || op1 == 't' || op1 == 'q' || op1 == 'e' || op1 == 'l' || op1 == 'a') && (op2 == '*' || op2 == '/' || op2 == '+' || op2 == '-' || op2 == '^'))
			return false;
		else
			return true;
	}
	public static string FixAshar(string x)
	{
		// double menha = 100*x - (int) (100*x);
		// // return Math.Round(x - (menha/100), 2);
		// x = x - (menha/100);
		// string xx = x.ToString();  
        // xx = String.Format("{0:0.00}", x);
        // return xx;
		// // return Math.Floor(x*100)/100;
		
		double dx = double.Parse(x);
		if(x.Contains("."))
		{
			if(dx*10 - (int)(dx*10) == 0)
				return(x + "0");
			else if (Math.Abs(dx*10 - (int)(dx*10)) > 0)
				return(x.Substring(0,x.IndexOf(".")+3));
		}
		else
			return(x + ".00");
		return String.Format("{0:0.00}", x);
	}

	public static void Main(string[] args)
	{
		string  inputStr = Console.ReadLine();
		if (!isBalanced(inputStr, inputStr.Length))
		{
			System.Console.WriteLine("INVALID");
			return;
		}
		else 
		{
			double x = CalculatePhrase(inputStr);
			if (x == double.MaxValue)
			{
				System.Console.WriteLine("INVALID");
				return;
			}
			else
				System.Console.WriteLine(FixAshar(x.ToString()));
		}
	}
}