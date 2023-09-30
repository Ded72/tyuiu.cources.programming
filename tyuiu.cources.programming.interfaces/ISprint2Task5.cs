using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tyuiu.cources.programming.interfaces.Sprint2
{
    public interface ISprint2Task5V0 { string FindMonthName(int value); }
    public interface ISprint2Task5V1 { int FindMonthDaysCount(int value); }
    public interface ISprint2Task5V2 { string FindMonthSeason(int value); }
    public interface ISprint2Task5V3 { string FindDayName(int value); }
    public interface ISprint2Task5V4 { string FindCardSuit(int value); }
    public interface ISprint2Task5V5 { string FindCardValue(int value); }
    public interface ISprint2Task5V6 { string FindCardNameAndValue(int value1, int value2); }
    public interface ISprint2Task5V7 { string FindMonthName(int startYear, int n); }
    public interface ISprint2Task5V8 { int FindDateOfPreviousDay(int m, int n); }
    public interface ISprint2Task5V9 { int FindDateOfNextDay(int m, int n); }
    public interface ISprint2Task5V10 { int FindDateOfPreviousDay(int g, int m, int n); }
    public interface ISprint2Task5V11 { int FindDateOfNextDay(int g, int m, int n); }
    public interface ISprint2Task5V12 { double FindDateOfPreviousDay(int g, int m, int n); }
    public interface ISprint2Task5V13 { double FindDateOfNextDay(int g, int m, int n); }
    public interface ISprint2Task5V14 { string FindDayName(int k, int d); }
    public interface ISprint2Task5V15 { string FindDayName(int k); }

}
