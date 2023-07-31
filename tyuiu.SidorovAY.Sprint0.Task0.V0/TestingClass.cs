using tyuiu.cources.programming.interfaces;

namespace tyuiu.SidorovAY.Sprint0.Task0.V0
{
    public class TestingClass : ISprint0Task0V0, ISprint0Task0V1, ISprint0Task0V2
    {
        public int Multiply(int a, int b)
        {
            return a * b;
        }

        public string ReverseString(string str)
        {
            char[] charArray = str.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }

        public int SubFrom100(int value)
        {
            return 100 - value;
        }
    }
}