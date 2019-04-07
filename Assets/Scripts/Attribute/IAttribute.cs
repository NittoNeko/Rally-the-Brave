public interface IAttribute
{
    /// <summary>
    /// Return actual value of this attribute
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    float ActualValue { get; }

    /// <summary>
    /// Apply a modifier.
    /// </summary>
    /// <param name="layer"></param>
    /// <param name="type"></param>
    /// <param name="value"></param>
    /// <param name="isRefresh"></param>
    void TakeModifier(float value, EAttrType type, EAttrModifierLayer layer, bool isRefresh = true);

    /// <summary>
    /// Remove a modifier.
    /// </summary>
    /// <param name="layer"></param>
    /// <param name="type"></param>
    /// <param name="value"></param>
    /// <param name="isRefresh"></param>
    void RemoveModifier(float value, EAttrType type, EAttrModifierLayer layer, bool isRefresh = true);

    /// <summary>
    /// Refresh this attribute at once.
    /// </summary>
    /// <param name="isDirtyOnly"></param>
    void Recalculate(bool isForced = false);

    /// <summary>
    /// Clear all modifications of the attributes at once, be careful with it.
    /// </summary>
    void Reset();
}