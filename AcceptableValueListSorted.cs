using BepInEx.Configuration;
using System;

namespace LCMaxSoundFix
{
    public class AcceptableValueListSorted<T> : AcceptableValueList<T> where T : IEquatable<T>, IComparable<T>
    {
        public AcceptableValueListSorted(params T[] acceptableValues)
            : base(acceptableValues)
        {
            Array.Sort(AcceptableValues);
        }

        public override object Clamp(object value)
        {
            if (value is IComparable<T>)
            {
                int closestIndex = 0;
                T comparableValue = (T)value;
                for (int i = 0; i < AcceptableValues.Length; ++i)
                {
                    int comp = AcceptableValues[i].CompareTo(comparableValue);
                    if (comp == 0)
                    {
                        // We found a perfect match!
                        closestIndex = i;
                        break;
                    }
                    else if (comp < 0)
                    {
                        closestIndex = i;
                    }
                    else if (comp > 0)
                    {
                        if (i == 0)
                        {
                            // We are at the start of the array, so we cannot compare to the previous value.
                            closestIndex = i;
                            break;
                        }

                        // The dynamic keyword isn't supported by IL2CPP, so check the type manually...
                        if (typeof(T) == typeof(int))
                        {
                            int currDiff = Math.Abs(Convert.ToInt32(AcceptableValues[i]) - (int)value);
                            int prevDiff = Math.Abs(Convert.ToInt32(AcceptableValues[i - 1]) - (int)value);

                            // Returns the closest value in the list.
                            closestIndex = currDiff < prevDiff ? i : i - 1;
                            break;
                        }

                        // By default, return the previous value.
                        break;
                    }
                }

                return AcceptableValues[closestIndex];
            }

            // Returns the matching value or the first value by default.
            return base.Clamp(value);
        }
    }
}