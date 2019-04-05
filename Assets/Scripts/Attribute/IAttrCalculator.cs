using System.Collections.Generic;

public interface IAttrModCalculator
{
    float Recalculate(float[] modifiers, List<float> independents, float initial);
}