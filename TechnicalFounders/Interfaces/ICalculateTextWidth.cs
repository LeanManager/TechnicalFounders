using System;

namespace TechnicalFounders.Interfaces
{
    public interface ICalculateTextWidth
    {
        double CalculateWidth(string text);
        // Do we need font size parameter too?
    }
}
