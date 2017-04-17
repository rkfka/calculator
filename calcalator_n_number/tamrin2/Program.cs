using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class TOkenizer
    {
        private int index = 0;
        private String str;
        public TOkenizer(String s) { str = s; }
        public Token getToken()
        {
            Token T = new Token();
            if (Char.IsDigit(str[index]))
            {
                T.Type = TokenType.enumeric;
                T.Value = str[index].ToString();
            }
            else
            {
                T.Type = TokenType.operation;
                T.Value = str[index].ToString();
            }
            index++;
            return T;
        }
        public bool is_finished() { return str.Length != index; }
        
    }
    enum TokenType { enumeric, operation };
    class Token
    {
        public String Value;
        public TokenType Type;
    }
    class Parser
    {
        float number1;
        float number2;
        float result = 0;
        string number_value = "";
        float calcalator(string char_pop)
        {

            if (char_pop == "+")
            {
                return number1 + number2;
            }
            else if (char_pop == "-")
            {
                return number2 - number1;
            }
            else if (char_pop == "*")
            {
                return number1 * number2;
            }
            else if (char_pop == "/")
            {
                return number2 / number1;
            }
            else
                return 0;
        }
        int compare_chars(string now, string pop)
        {
            if ((now == "-") && pop == "+")
            {
                return 1;
            }
            else if ((now == "+") && (pop == "-" || pop == "*" || pop == "/"))
            {
                return -1;
            }
            else if ((now == "-") && (pop == "*" || pop == "/" || pop == "-"))
            {
                return -1;
            }
            else if (now == "*" || now == "/")
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
        public float main_calculator(string ss) {

            TOkenizer obj1 = new TOkenizer(ss);
            Stack<float> nstack = new Stack<float>();
            Stack<string> ostack = new Stack<string>();
            while (obj1.is_finished())
            {
                Token _res = obj1.getToken();
                if (_res.Type == TokenType.enumeric)
                {
                    number_value = _res.Value;
                    if (!obj1.is_finished())
                    {
                        nstack.Push(float.Parse(number_value));
                        break;
                    }
                    _res = obj1.getToken();
                    while (_res.Type == TokenType.enumeric)
                    {
                        number_value += _res.Value;
                        if (!obj1.is_finished())
                        {
                            break;
                        }
                        _res = obj1.getToken();
                    }
                    nstack.Push(float.Parse(number_value));
                }
                
                if (ostack.Count() == 0 && obj1.is_finished())
                {
                    ostack.Push(_res.Value);
                }
                else if(ostack.Count() != 0 && obj1.is_finished())
                {
                    string char_pop = ostack.Peek();
                    int com_res = compare_chars(_res.Value, char_pop);
                    if (com_res == 1)
                    {
                        ostack.Push(_res.Value);
                    }
                    else
                    {
                        char_pop = ostack.Pop();
                        while (compare_chars(_res.Value, char_pop) != 1 || ostack.Count() != 0)
                        {
                            number1 = nstack.Pop();
                            number2 = nstack.Pop();
                            nstack.Push(calcalator(char_pop));
                            if (ostack.Count() == 0)
                            {
                                break;
                            }
                            char_pop = ostack.Pop();
                        }
                        ostack.Push(_res.Value);
                    }
                }
                
            }
            while (ostack.Count() != 0)
            {
                string char_pop = ostack.Pop();
                number1 = nstack.Pop();
                number2 = nstack.Pop();
                nstack.Push(calcalator(char_pop));
                result = calcalator(char_pop);
            }
            return result;
        }
    }
    
    class Program
    {
        static void Main()
        {
            bool _right = true;
            while (_right)
            {
                Console.WriteLine("please enter string\nfor example 12+13-14 or\nplease enter 1 for exit");
                string s = Console.ReadLine();
                if (s == "1")
                {
                    return;
                }
                Parser p = new Parser();
                Console.WriteLine(p.main_calculator(s).ToString());
            }
            
        }
    }
}
