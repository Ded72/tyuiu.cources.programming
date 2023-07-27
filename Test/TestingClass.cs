using tyuiu.cources.programming.interfaces;

namespace Test
{
    public class TestingClass : ISprint0Test0V0
    {
        public string? ReverseString(string str)
        {
            char[] charArray = str.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }
    }
}