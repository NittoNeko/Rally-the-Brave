public interface IAttribute
{
    float ActualValue { get; }

    void Recalculate(bool isForced);

    void RemoveModifier(EAttrType attrType, EAttrModLayer layer, float value);

    void Reset();

    void TakeModifier(EAttrType attrType, EAttrModLayer layer, float value);
}