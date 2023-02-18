using System.Text;

namespace LonglongNumbersLibrary;

public class LongNumber
{
    private int[] _digits;

    public LongNumber(string number)
    {
        _digits = new int[number.Length];
        for (int i = 0; i < number.Length; i++)
        {
            _digits[i] = int.Parse(number[i].ToString());
        }
    }

    public LongNumber(int[] digits)
    {
        _digits = new int[digits.Length];
        Array.Copy(digits, _digits, digits.Length);
    }


    public override string ToString()
    {
        StringBuilder stringBuilder = new StringBuilder();
        int dot = 0;

        for (int i = _digits.Length - 1; i >= 0; i--)
        {
            dot++;

            stringBuilder.Append(_digits[i].ToString());

            if (dot == 3 && i > 0)
            {
                stringBuilder.Append(".");
                dot = 0;
            }
        }

        char[] temp = stringBuilder.ToString().ToCharArray();
        Array.Reverse(temp);
        return new string(temp);
    }

    public static LongNumber operator +(LongNumber longNumber1, LongNumber longNumber2)
    {
        int[] resultDigits = new int[Math.Max(longNumber1._digits.Length, longNumber2._digits.Length) + 1];
        Array.Fill(resultDigits, 0);

        for (int i = longNumber1._digits.Length - 1, j = longNumber2._digits.Length - 1, k = 0;
             i >= 0 || j >= 0;
             i--, j--, k++)
        {
            if (i >= 0 && j >= 0)
            {
                resultDigits[k] = longNumber1._digits[i] + longNumber2._digits[j];
            }
            else if (i >= 0)
            {
                resultDigits[k] = longNumber1._digits[i];
            }
            else if (j >= 0)
            {
                resultDigits[k] = longNumber2._digits[j];
            }
        }

        for (int i = 0; i < resultDigits.Length - 1; i++)
        {
            if (resultDigits[i] > 9)
            {
                resultDigits[i + 1] += 1;
                resultDigits[i] -= 10;
            }
        }

        Array.Reverse(resultDigits);

        if (resultDigits[0] == 0)
        {
            int[] cleanDigits = new int[resultDigits.Length - 1];

            for (int i = 1, j = 0; i < resultDigits.Length; i++, j++)
            {
                cleanDigits[j] = resultDigits[i];
            }

            resultDigits = cleanDigits;
        }


        return new LongNumber(resultDigits);
    }

    private static int CompareTo(LongNumber longNumber1, LongNumber longNumber2)
    {
        if (longNumber1._digits.Length > longNumber2._digits.Length)
        {
            return 1;
        }
        else if (longNumber1._digits.Length < longNumber2._digits.Length)
        {
            return -1;
        }
        else
        {
            for (int i = 0; i < longNumber1._digits.Length; i++)
            {
                if (longNumber1._digits[i] > longNumber2._digits[i])
                {
                    return 1;
                }
                else if (longNumber1._digits[i] < longNumber2._digits[i])
                {
                    return -1;
                }
            }

            return 0;
        }
    }

    public static bool operator ==(LongNumber longNumber1, LongNumber longNumber2)
    {
        return CompareTo(longNumber1, longNumber2) == 0;
    }

    public static bool operator !=(LongNumber longNumber1, LongNumber longNumber2)
    {
        return CompareTo(longNumber1, longNumber2) != 0;
    }

    public static bool operator >(LongNumber longNumber1, LongNumber longNumber2)
    {
        return CompareTo(longNumber1, longNumber2) == 1;
    }

    public static bool operator <(LongNumber longNumber1, LongNumber longNumber2)
    {
        return CompareTo(longNumber1, longNumber2) == -1;
    }

    public static bool operator >=(LongNumber longNumber1, LongNumber longNumber2)
    {
        return CompareTo(longNumber1, longNumber2) >= 0;
    }

    public static bool operator <=(LongNumber longNumber1, LongNumber longNumber2)
    {
        return CompareTo(longNumber1, longNumber2) <= 0;
    }
}