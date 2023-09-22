using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tyuiu.cources.programming.interfaces.Sprint1
{
    public interface ISprint1Task3V0 { double Calculate(double a, double b); }
    public interface ISprint1Task3V1 { double CylinderVolume(double r, double h); }
    public interface ISprint1Task3V2 { double PurchaseAmount(double priceNotebook, int amountNotebook, double pricePencil, int amountPencil); }
    public interface ISprint1Task3V3 { double ParallelepipedVolume(double length, double width, double height); }
    public interface ISprint1Task3V4 { double PurchaseAmount(double priceNotebook, double priceCover, int amount); }
    public interface ISprint1Task3V5 { double DistanceLength(double mapScale, double distanceBetweenPoints);}
    public interface ISprint1Task3V6 { double TravelCost(double distance, double gasFlow, double gasPrice);}
    public interface ISprint1Task3V7 { double VerstsToKilometers(double verst);}
    public interface ISprint1Task3V8 { double IncomeAmount(double startAmount, double percent, double timeDays); }
    public interface ISprint1Task3V9 { double ConvertMinutesToHours(int minutes); }
    public interface ISprint1Task3V10 { double NumberToMoney(double number); }
    public interface ISprint1Task3V11 { double TriangleArea(double x1, double x2, double x3, double y1, double y2, double y3); }
    public interface ISprint1Task3V12 { double TriangleArea(double lengthCathetus1, double lengthCathetus2); }
    public interface ISprint1Task3V13 { double MultiplyOfDigits(double number); }
    public interface ISprint1Task3V14 { double ReverseNumber(double number); }
    public interface ISprint1Task3V15 { double DistanceOverTime(double v1, double v2, double S, double T); }
    public interface ISprint1Task3V16 { double CoeffOfQuadraticEquation(double x1, double x2); }
    public interface ISprint1Task3V17 { bool ZeroCheck(double number); }
    public interface ISprint1Task3V18 { double HowManySquares(double a, double b, double c); }
    public interface ISprint1Task3V19 { bool ElephCanMove(double x1, double x2, double y1, double y2); }

}
