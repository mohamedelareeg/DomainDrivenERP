using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DomainDrivenERP.Domain.Shared.Guard;

namespace DomainDrivenERP.Domain.Shared.Guards;
public static class GuardExtensions
{
    #region For Object
    public static void Null<T>(this IGuardClause guardClause, T argument, string argumentName)
    {
        if (guardClause is null)
        {
            throw new ArgumentNullException(nameof(guardClause));
        }
        if (argument == null)
        {
            throw new ArgumentNullException(argumentName);
        }
    }
    #endregion

    #region For String
    public static void NullOrEmpty(this IGuardClause guardClause, string argument, string argumentName)
    {
        if (guardClause is null)
        {
            throw new ArgumentNullException(nameof(guardClause));
        }
        if (string.IsNullOrEmpty(argument))
        {
            throw new ArgumentNullException(argumentName);
        }
    }

    public static void NullOrWhiteSpace(this IGuardClause guardClause, string argument, string argumentName)
    {
        if (guardClause is null)
        {
            throw new ArgumentNullException(nameof(guardClause));
        }
        if (string.IsNullOrWhiteSpace(argument))
        {
            throw new ArgumentNullException(argumentName);
        }
    }

    public static void LeadingAndTailingSpace(this IGuardClause guardClause, string argument, string argumentName)
    {
        if (guardClause is null)
        {
            throw new ArgumentNullException(nameof(guardClause));
        }
        if (argument.Trim() != argument)
        {
            throw new ArgumentException(string.Format("{0} is not allowing leading and tailing space", argumentName));
        }
    }

    public static void MinimumLength(this IGuardClause guardClause, string argument, string argumentName, int minLength)
    {
        if (guardClause is null)
        {
            throw new ArgumentNullException(nameof(guardClause));
        }
        if (argument.Length < minLength)
        {
            throw new ArgumentException(string.Format("{0} is not allowing characters less than {1}", argumentName, minLength));
        }
    }

    public static void MaximumLength(this IGuardClause guardClause, string argument, string argumentName, int maxLength)
    {
        if (guardClause is null)
        {
            throw new ArgumentNullException(nameof(guardClause));
        }
        if (argument.Length > maxLength)
        {
            throw new ArgumentException(string.Format("{0} is not allowing characters more than {1}", argumentName, maxLength));
        }
    }

    public static void SpecialCharacters(this IGuardClause guardClause, string argument, string argumentName)
    {
        if (guardClause is null)
        {
            throw new ArgumentNullException(nameof(guardClause));
        }
        if (HasSpecialChars(argument))
        {
            throw new ArgumentException(string.Format("{0} is not allowing any special characters", argumentName));
        }
    }

    public static void Digits(this IGuardClause guardClause, string argument, string argumentName)
    {
        if (guardClause is null)
        {
            throw new ArgumentNullException(nameof(guardClause));
        }
        if (HasDigits(argument))
        {
            throw new ArgumentException(string.Format("{0} is not allowing any digits", argumentName));
        }
    }

    public static void Alphabet(this IGuardClause guardClause, string argument, string argumentName)
    {
        if (guardClause is null)
        {
            throw new ArgumentNullException(nameof(guardClause));
        }
        if (HasAlphabets(argument))
        {
            throw new ArgumentException(string.Format("{0} is not allowing any alphabets", argumentName));
        }
    }

    public static void LowerCase(this IGuardClause guardClause, string argument, string argumentName)
    {
        if (guardClause is null)
        {
            throw new ArgumentNullException(nameof(guardClause));
        }
        if (HasLowerCase(argument))
        {
            throw new ArgumentException(string.Format("{0} is not allowing lower case", argumentName));
        }
    }

    public static void UpperCase(this IGuardClause guardClause, string argument, string argumentName)
    {
        if (guardClause is null)
        {
            throw new ArgumentNullException(nameof(guardClause));
        }
        if (HasUpperCase(argument))
        {
            throw new ArgumentException(string.Format("{0} is not allowing upper case", argumentName));
        }
    }

    public static void Space(this IGuardClause guardClause, string argument, string argumentName)
    {
        if (guardClause is null)
        {
            throw new ArgumentNullException(nameof(guardClause));
        }
        if (argument.Contains(" "))
        {
            throw new ArgumentException(string.Format("{0} is not allowing space", argumentName));
        }
    }

    private static bool HasSpecialChars(string value)
    {
        var rx = new Regex(@"[~`!@#$%^&*()-+=|\{}':;.,<>/?]");
        if (rx.IsMatch(value))
        {
            return true;
        }
        return false;
    }

    private static bool HasDigits(string value)
    {
        var rx = new Regex(@"[0-9]");
        if (rx.IsMatch(value))
        {
            return true;
        }
        return false;
    }

    private static bool HasAlphabets(string value)
    {
        var rx = new Regex(@"[a-zA-Z]");
        if (rx.IsMatch(value))
        {
            return true;
        }
        return false;
    }

    private static bool HasUpperCase(string value)
    {
        var rx = new Regex(@"[A-Z]");
        if (rx.IsMatch(value))
        {
            return true;
        }
        return false;
    }

    private static bool HasLowerCase(string value)
    {
        var rx = new Regex(@"[a-z]");
        if (rx.IsMatch(value))
        {
            return true;
        }
        return false;
    }
    #endregion

    #region For Integer
    public static void NumberLessThan(this IGuardClause guardClause, int argument, string argumentName, int min, string minArgumentName = "")
    {
        if (guardClause is null)
        {
            throw new ArgumentNullException(nameof(guardClause));
        }
        if (argument < min)
        {
            throw new ArgumentException(string.Format("{0} is not allowing less than {1}", argumentName, !string.IsNullOrWhiteSpace(minArgumentName) ? minArgumentName : min.ToString()));
        }
    }

    public static void NumberGreaterThan(this IGuardClause guardClause, int argument, string argumentName, int max, string maxArgumentName = "")
    {
        if (guardClause is null)
        {
            throw new ArgumentNullException(nameof(guardClause));
        }
        if (argument > max)
        {
            throw new ArgumentException(string.Format("{0} is not allowing more than {1}", argumentName, !string.IsNullOrWhiteSpace(maxArgumentName) ? maxArgumentName : max.ToString()));
        }
    }

    public static void NumberLessThanOrEqual(this IGuardClause guardClause, int argument, string argumentName, int min, string minArgumentName = "")
    {
        if (guardClause is null)
        {
            throw new ArgumentNullException(nameof(guardClause));
        }
        if (argument <= min)
        {
            throw new ArgumentException(string.Format("{0} is not allowing less than or equals to {1}", argumentName, !string.IsNullOrWhiteSpace(minArgumentName) ? minArgumentName : min.ToString()));
        }
    }

    public static void NumberGreaterThanOrEqual(this IGuardClause guardClause, int argument, string argumentName, int max, string maxArgumentName = "")
    {
        if (guardClause is null)
        {
            throw new ArgumentNullException(nameof(guardClause));
        }
        if (argument >= max)
        {
            throw new ArgumentException(string.Format("{0} is not allowing greater than or equals to {1}", argumentName, !string.IsNullOrWhiteSpace(maxArgumentName) ? maxArgumentName : max.ToString()));
        }
    }

    public static void NumberZero(this IGuardClause guardClause, int argument, string argumentName)
    {
        if (guardClause is null)
        {
            throw new ArgumentNullException(nameof(guardClause));
        }
        if (argument == 0)
        {
            throw new ArgumentException(string.Format("{0} is not allowing 0", argumentName));
        }
    }

    public static void NumberNegative(this IGuardClause guardClause, int argument, string argumentName)
    {
        if (guardClause is null)
        {
            throw new ArgumentNullException(nameof(guardClause));
        }
        if (argument < 0)
        {
            throw new ArgumentException(string.Format("{0} is not allowing negative value", argumentName));
        }
    }

    public static void NumberNegativeOrZero(this IGuardClause guardClause, int argument, string argumentName)
    {
        if (guardClause is null)
        {
            throw new ArgumentNullException(nameof(guardClause));
        }
        if (argument <= 0)
        {
            throw new ArgumentException(string.Format("{0} is not allowing negative value or 0", argumentName));
        }
    }

    public static void NumberOutOfRange(this IGuardClause guardClause, int argument, string argumentName, int startRange, int endRange)
    {
        if (guardClause is null)
        {
            throw new ArgumentNullException(nameof(guardClause));
        }
        if (argument < startRange || argument > endRange)
        {
            throw new ArgumentException(string.Format("{0} is out of range", argumentName));
        }
    }
    #endregion

    #region For Double
    public static void NumberLessThan(this IGuardClause guardClause, double argument, string argumentName, double min, string minArgumentName = "")
    {
        if (guardClause is null)
        {
            throw new ArgumentNullException(nameof(guardClause));
        }
        if (argument < min)
        {
            throw new ArgumentException(string.Format("{0} is not allowing less than {1}", argumentName, !string.IsNullOrWhiteSpace(minArgumentName) ? minArgumentName : min.ToString()));
        }
    }

    public static void NumberGreaterThan(this IGuardClause guardClause, double argument, string argumentName, double max, string maxArgumentName = "")
    {
        if (guardClause is null)
        {
            throw new ArgumentNullException(nameof(guardClause));
        }
        if (argument > max)
        {
            throw new ArgumentException(string.Format("{0} is not allowing more than {1}", argumentName, !string.IsNullOrWhiteSpace(maxArgumentName) ? maxArgumentName : max.ToString()));
        }
    }

    public static void NumberLessThanOrEqual(this IGuardClause guardClause, double argument, string argumentName, double min, string minArgumentName = "")
    {
        if (guardClause is null)
        {
            throw new ArgumentNullException(nameof(guardClause));
        }
        if (argument <= min)
        {
            throw new ArgumentException(string.Format("{0} is not allowing less than or equals to {1}", argumentName, !string.IsNullOrWhiteSpace(minArgumentName) ? minArgumentName : min.ToString()));
        }
    }

    public static void NumberGreaterThanOrEqual(this IGuardClause guardClause, double argument, string argumentName, double max, string maxArgumentName = "")
    {
        if (guardClause is null)
        {
            throw new ArgumentNullException(nameof(guardClause));
        }
        if (argument >= max)
        {
            throw new ArgumentException(string.Format("{0} is not allowing greater than or equals to {1}", argumentName, !string.IsNullOrWhiteSpace(maxArgumentName) ? maxArgumentName : max.ToString()));
        }
    }

    public static void NumberZero(this IGuardClause guardClause, double argument, string argumentName)
    {
        if (guardClause is null)
        {
            throw new ArgumentNullException(nameof(guardClause));
        }
        if (argument == 0)
        {
            throw new ArgumentException(string.Format("{0} is not allowing 0", argumentName));
        }
    }

    public static void NumberNegative(this IGuardClause guardClause, double argument, string argumentName)
    {
        if (guardClause is null)
        {
            throw new ArgumentNullException(nameof(guardClause));
        }
        if (argument < 0)
        {
            throw new ArgumentException(string.Format("{0} is not allowing negative value.", argumentName));
        }
    }

    public static void NumberNegativeOrZero(this IGuardClause guardClause, double argument, string argumentName)
    {
        if (guardClause is null)
        {
            throw new ArgumentNullException(nameof(guardClause));
        }

        if (argument <= 0)
        {
            throw new ArgumentException(string.Format("{0} is not allowing negative value or 0", argumentName));
        }
    }

    public static void NumberOutOfRange(this IGuardClause guardClause, double argument, string argumentName, double startRange, double endRange)
    {
        if (guardClause is null)
        {
            throw new ArgumentNullException(nameof(guardClause));
        }

        if (argument < startRange || argument > endRange)
        {
            throw new ArgumentException(string.Format("{0} is out of range", argumentName));
        }

        if (string.IsNullOrEmpty(argumentName))
        {
            throw new ArgumentException($"'{nameof(argumentName)}' cannot be null or empty.", nameof(argumentName));
        }
    }
    #endregion

    #region For Decimal
    public static void NumberLessThan(this IGuardClause guardClause, decimal argument, string argumentName, decimal min, string minArgumentName = "")
    {
        if (guardClause is null)
        {
            throw new ArgumentNullException(nameof(guardClause));
        }
        if (argument < min)
        {
            throw new ArgumentException(string.Format("{0} is not allowing less than {1}", argumentName, !string.IsNullOrWhiteSpace(minArgumentName) ? minArgumentName : min.ToString()));
        }
    }

    public static void NumberGreaterThan(this IGuardClause guardClause, decimal argument, string argumentName, decimal max, string maxArgumentName = "")
    {
        if (guardClause is null)
        {
            throw new ArgumentNullException(nameof(guardClause));
        }
        if (argument > max)
        {
            throw new ArgumentException(string.Format("{0} is not allowing more than {1}", argumentName, !string.IsNullOrWhiteSpace(maxArgumentName) ? maxArgumentName : max.ToString()));
        }
    }

    public static void NumberLessThanOrEqual(this IGuardClause guardClause, decimal argument, string argumentName, decimal min, string minArgumentName = "")
    {
        if (guardClause is null)
        {
            throw new ArgumentNullException(nameof(guardClause));
        }
        if (argument <= min)
        {
            throw new ArgumentException(string.Format("{0} is not allowing less than or equals to {1}", argumentName, !string.IsNullOrWhiteSpace(minArgumentName) ? minArgumentName : min.ToString()));
        }
    }

    public static void NumberGreaterThanOrEqual(this IGuardClause guardClause, decimal argument, string argumentName, decimal max, string maxArgumentName = "")
    {
        if (guardClause is null)
        {
            throw new ArgumentNullException(nameof(guardClause));
        }
        if (argument >= max)
        {
            throw new ArgumentException(string.Format("{0} is not allowing greater than or equals to {1}", argumentName, !string.IsNullOrWhiteSpace(maxArgumentName) ? maxArgumentName : max.ToString()));
        }
    }

    public static void NumberZero(this IGuardClause guardClause, decimal argument, string argumentName)
    {
        if (guardClause is null)
        {
            throw new ArgumentNullException(nameof(guardClause));
        }
        if (argument == 0)
        {
            throw new ArgumentException(string.Format("{0} is not allowing 0", argumentName));
        }
    }

    public static void NumberNegative(this IGuardClause guardClause, decimal argument, string argumentName)
    {
        if (guardClause is null)
        {
            throw new ArgumentNullException(nameof(guardClause));
        }
        if (argument < 0)
        {
            throw new ArgumentException(string.Format("{0} is not allowing negative value.", argumentName));
        }
    }

    public static void NumberNegativeOrZero(this IGuardClause guardClause, decimal argument, string argumentName)
    {
        if (guardClause is null)
        {
            throw new ArgumentNullException(nameof(guardClause));
        }
        if (argument <= 0)
        {
            throw new ArgumentException(string.Format("{0} is not allowing negative value or 0", argumentName));
        }
    }

    public static void NumberOutOfRange(this IGuardClause guardClause, decimal argument, string argumentName, decimal startRange, decimal endRange)
    {
        if (guardClause is null)
        {
            throw new ArgumentNullException(nameof(guardClause));
        }
        if (argument < startRange || argument > endRange)
        {
            throw new ArgumentException(string.Format("{0} is out of range", argumentName));
        }
    }
    #endregion

    #region For DateTime
    public static void DateTimeLessThan(this IGuardClause guardClause, DateTime argument, string argumentName, DateTime min, string minArgumentName = "", string format = "dd/MM/yyyy")
    {
        if (guardClause is null)
        {
            throw new ArgumentNullException(nameof(guardClause));
        }
        if (argument < min)
        {
            throw new ArgumentException(string.Format("{0} is not allowing less than {1}", argumentName, !string.IsNullOrWhiteSpace(minArgumentName) ? minArgumentName : min.ToString(format)));
        }
    }

    public static void DateTimeGreaterThan(this IGuardClause guardClause, DateTime argument, string argumentName, DateTime max, string maxArgumentName = "", string format = "dd/MM/yyyy")
    {
        if (guardClause is null)
        {
            throw new ArgumentNullException(nameof(guardClause));
        }
        if (argument > max)
        {
            throw new ArgumentException(string.Format("{0} is not allowing more than {1}", argumentName, !string.IsNullOrWhiteSpace(maxArgumentName) ? maxArgumentName : max.ToString(format)));
        }
    }

    public static void DateTimeLessThanOrEqual(this IGuardClause guardClause, DateTime argument, string argumentName, DateTime min, string minArgumentName = "", string format = "dd/MM/yyyy")
    {
        if (guardClause is null)
        {
            throw new ArgumentNullException(nameof(guardClause));
        }
        if (argument <= min)
        {
            throw new ArgumentException(string.Format("{0} is not allowing less than or equals to {1}", argumentName, !string.IsNullOrWhiteSpace(minArgumentName) ? minArgumentName : min.ToString(format)));
        }
    }

    public static void DateTimeGreaterThanOrEqual(this IGuardClause guardClause, DateTime argument, string argumentName, DateTime max, string maxArgumentName = "", string format = "dd/MM/yyyy")
    {
        if (guardClause is null)
        {
            throw new ArgumentNullException(nameof(guardClause));
        }
        if (argument >= max)
        {
            throw new ArgumentException(string.Format("{0} is not allowing greater than or equals to {1}", argumentName, !string.IsNullOrWhiteSpace(maxArgumentName) ? maxArgumentName : max.ToString(format)));
        }
    }

    public static void DateTimeOutOfRange(this IGuardClause guardClause, DateTime argument, string argumentName, DateTime startRange, DateTime endRange)
    {
        if (guardClause is null)
        {
            throw new ArgumentNullException(nameof(guardClause));
        }
        if (argument < startRange || argument > endRange)
        {
            throw new ArgumentException(string.Format("{0} is out of range", argumentName));
        }
    }
    #endregion

    #region For DateTimeOffset
    public static void DateTimeOffsetLessThan(this IGuardClause guardClause, DateTimeOffset argument, string argumentName, DateTimeOffset min, string minArgumentName = "", string format = "dd/MM/yyyy")
    {
        if (guardClause is null)
        {
            throw new ArgumentNullException(nameof(guardClause));
        }
        if (argument < min)
        {
            throw new ArgumentException(string.Format("{0} is not allowing less than {1}", argumentName, !string.IsNullOrWhiteSpace(minArgumentName) ? minArgumentName : min.ToString(format)));
        }
    }

    public static void DateTimeOffsetGreaterThan(this IGuardClause guardClause, DateTimeOffset argument, string argumentName, DateTimeOffset max, string maxArgumentName = "", string format = "dd/MM/yyyy")
    {
        if (guardClause is null)
        {
            throw new ArgumentNullException(nameof(guardClause));
        }
        if (argument > max)
        {
            throw new ArgumentException(string.Format("{0} is not allowing more than {1}", argumentName, !string.IsNullOrWhiteSpace(maxArgumentName) ? maxArgumentName : max.ToString(format)));
        }
    }

    public static void DateTimeOffsetLessThanOrEqual(this IGuardClause guardClause, DateTimeOffset argument, string argumentName, DateTimeOffset min, string minArgumentName = "", string format = "dd/MM/yyyy")
    {
        if (guardClause is null)
        {
            throw new ArgumentNullException(nameof(guardClause));
        }
        if (argument <= min)
        {
            throw new ArgumentException(string.Format("{0} is not allowing less than or equals to {1}", argumentName, !string.IsNullOrWhiteSpace(minArgumentName) ? minArgumentName : min.ToString(format)));
        }
    }

    public static void DateTimeOffsetGreaterThanOrEqual(this IGuardClause guardClause, DateTimeOffset argument, string argumentName, DateTimeOffset max, string maxArgumentName = "", string format = "dd/MM/yyyy")
    {
        if (guardClause is null)
        {
            throw new ArgumentNullException(nameof(guardClause));
        }
        if (argument >= max)
        {
            throw new ArgumentException(string.Format("{0} is not allowing greater than or equals to {1}", argumentName, !string.IsNullOrWhiteSpace(maxArgumentName) ? maxArgumentName : max.ToString(format)));
        }
    }

    public static void DateTimeOffsetOutOfRange(this IGuardClause guardClause, DateTimeOffset argument, string argumentName, DateTimeOffset startRange, DateTimeOffset endRange)
    {
        if (guardClause is null)
        {
            throw new ArgumentNullException(nameof(guardClause));
        }
        if (argument < startRange || argument > endRange)
        {
            throw new ArgumentException(string.Format("{0} is out of range", argumentName));
        }
    }
    #endregion

    #region For Timespan
    public static void TimeSpanLessThan(this IGuardClause guardClause, TimeSpan argument, string argumentName, TimeSpan min, string minArgumentName = "", string format = @"hh\:mm\:ss")
    {
        if (guardClause is null)
        {
            throw new ArgumentNullException(nameof(guardClause));
        }
        if (argument < min)
        {
            throw new ArgumentException(string.Format("{0} is not allowing less than {1}", argumentName, !string.IsNullOrWhiteSpace(minArgumentName) ? minArgumentName : min.ToString(format)));
        }
    }

    public static void TimeSpanGreaterThan(this IGuardClause guardClause, TimeSpan argument, string argumentName, TimeSpan max, string maxArgumentName = "", string format = @"hh\:mm\:ss")
    {
        if (guardClause is null)
        {
            throw new ArgumentNullException(nameof(guardClause));
        }
        if (argument > max)
        {
            throw new ArgumentException(string.Format("{0} is not allowing more than {1}", argumentName, !string.IsNullOrWhiteSpace(maxArgumentName) ? maxArgumentName : max.ToString(format)));
        }
    }

    public static void TimeSpanLessThanOrEqual(this IGuardClause guardClause, TimeSpan argument, string argumentName, TimeSpan min, string minArgumentName = "", string format = @"hh\:mm\:ss")
    {
        if (guardClause is null)
        {
            throw new ArgumentNullException(nameof(guardClause));
        }
        if (argument <= min)
        {
            throw new ArgumentException(string.Format("{0} is not allowing less than or equals to {1}", argumentName, !string.IsNullOrWhiteSpace(minArgumentName) ? minArgumentName : min.ToString(format)));
        }
    }

    public static void TimeSpanGreaterThanOrEqual(this IGuardClause guardClause, TimeSpan argument, string argumentName, TimeSpan max, string maxArgumentName = "", string format = @"hh\:mm\:ss")
    {
        if (guardClause is null)
        {
            throw new ArgumentNullException(nameof(guardClause));
        }
        if (argument >= max)
        {
            throw new ArgumentException(string.Format("{0} is not allowing greater than or equals to {1}", argumentName, !string.IsNullOrWhiteSpace(maxArgumentName) ? maxArgumentName : max.ToString(format)));
        }
    }

    public static void TimeSpanOutOfRange(this IGuardClause guardClause, TimeSpan argument, string argumentName, TimeSpan startRange, TimeSpan endRange)
    {
        if (guardClause is null)
        {
            throw new ArgumentNullException(nameof(guardClause));
        }
        if (argument < startRange || argument > endRange)
        {
            throw new ArgumentException(string.Format("{0} is out of range", argumentName));
        }
    }
    #endregion

    #region For Others

    /// <summary>
    /// Only Absolute URLs are validated.
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static void InValidURL(this IGuardClause guardClause, string argument, string argumentName)
    {
        if (guardClause is null)
        {
            throw new ArgumentNullException(nameof(guardClause));
        }
        Guard.Against.NullOrEmpty(argument, argumentName);
        if (!IsValidURL(argument))
        {
            throw new ArgumentException(string.Format("{0} is not valid URL", argumentName));
        }
    }

    public static void InValidEmailId(this IGuardClause guardClause, string argument, string argumentName)
    {
        if (guardClause is null)
        {
            throw new ArgumentNullException(nameof(guardClause));
        }
        Guard.Against.NullOrEmpty(argument, argumentName);
        if (!IsValidEmailId(argument))
        {
            throw new ArgumentException(string.Format("{0} is not valid emailid", argumentName));
        }
    }

    public static void InValidGuid(this IGuardClause guardClause, string argument, string argumentName)
    {
        if (guardClause is null)
        {
            throw new ArgumentNullException(nameof(guardClause));
        }
        Guard.Against.NullOrEmpty(argument, argumentName);
        if (!IsValidGuid(argument))
        {
            throw new ArgumentException(string.Format("{0} is not valid Guid", argumentName));
        }
    }

    private static bool IsValidURL(string value)
    {
        return Uri.TryCreate(value, UriKind.Absolute, out Uri? result) &&
               (result.Scheme == "http" || result.Scheme == "https");
    }

    private static bool IsValidEmailId(string value)
    {
        return Regex.IsMatch(value,
           @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
           @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-0-9a-z]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
           RegexOptions.IgnoreCase);
    }

    private static bool IsValidGuid(string value)
    {
        return Guid.TryParse(value, out Guid result);
    }
    #endregion

    #region For bool
    public static void True(this IGuardClause guardClause, bool argument, string argumentName)
    {
        if (guardClause is null)
        {
            throw new ArgumentNullException(nameof(guardClause));
        }
        if (argument == true)
        {
            throw new ArgumentException(string.Format("{0} is not allowing to be true", argumentName));
        }
    }

    public static void False(this IGuardClause guardClause, bool argument, string argumentName)
    {
        if (guardClause is null)
        {
            throw new ArgumentNullException(nameof(guardClause));
        }
        if (argument == false)
        {
            throw new ArgumentException(string.Format("{0} is not allowing to be false", argumentName));
        }
    }
    #endregion

    #region For IEnumerable
    public static void Empty<T>(this IGuardClause guardClause, IEnumerable<T> argument, string argumentName)
    {
        if (guardClause is null)
        {
            throw new ArgumentNullException(nameof(guardClause));
        }
        if (argument != null && argument.Any())
        {
            return;
        }
        throw new ArgumentException(string.Format("{0} is not allowing to be empty", argumentName));
    }
    #endregion
}
