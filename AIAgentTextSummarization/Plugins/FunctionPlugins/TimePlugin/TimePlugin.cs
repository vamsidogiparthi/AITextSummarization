using System.ComponentModel;
using Microsoft.SemanticKernel;

namespace AIAgentTextSummarization.Plugins.FunctionPlugins.TimePlugin;

public class TimePlugin()
{
    [KernelFunction("get_time")]
    [Description("Get the current time.")]
    public static string GetTime()
    {
        return DateTime.Now.ToString("HH:mm:ss");
    }

    [KernelFunction("get_date")]
    [Description("Get the current date.")]
    public static string GetDate()
    {
        return DateTime.Now.ToString("yyyy-MM-dd");
    }

    [KernelFunction("get_date_with_format")]
    [Description("Get the current date with requested fornmat")]
    public static string GetDate(
        [Description(
            "contains the desired date time format in which the output date time should be"
        )]
            string format
    )
    {
        return DateTime.Now.ToString(format);
    }

    [KernelFunction("get_time_and_date")]
    [Description("Get the current time and date.")]
    public static string GetTimeAndDate()
    {
        return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
    }

    [KernelFunction("get_time_and_date_with_format")]
    [Description("Get the current time and date with requested format.")]
    public static string GetTimeAndDate(string format)
    {
        return DateTime.Now.ToString(format);
    }

    [KernelFunction("get_local_time_zone")]
    [Description("Get the local time zone.")]
    public static string GetLocalTimeZone()
    {
        return TimeZoneInfo.Local.DisplayName;
    }

    [KernelFunction("get_local_time_zone_iana")]
    [Description("Get the local time zone in IANA format.")]
    public static string GetLocalTimeZoneIana()
    {
        return TimeZoneInfo.Local.Id;
    }

    [KernelFunction("convert_to_desired_time_zone")]
    [Description("Convert the given time to the desired time zone.")]
    public static string ConvertToDesiredTimeZone(
        string time,
        string sourceTimeZone,
        string destinationTimeZone
    )
    {
        DateTime dateTime = DateTime.Parse(time);
        TimeZoneInfo sourceTZ = TimeZoneInfo.FindSystemTimeZoneById(sourceTimeZone);
        TimeZoneInfo destinationTZ = TimeZoneInfo.FindSystemTimeZoneById(destinationTimeZone);

        dateTime = TimeZoneInfo.ConvertTime(dateTime, sourceTZ, destinationTZ);
        return dateTime.ToString("yyyy-MM-dd HH:mm:ss");
    }

    [KernelFunction("get_time_in_time_zone")]
    [Description("Get the current time in the given time zone.")]
    public static string GetCurrentTimeInTimeZone(string timeZone)
    {
        DateTime dateTime = DateTime.UtcNow;
        TimeZoneInfo timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(timeZone);
        dateTime = TimeZoneInfo.ConvertTimeFromUtc(dateTime, timeZoneInfo);
        return dateTime.ToString("HH:mm:ss");
    }

    [KernelFunction("get_current_date_time_in_time_zone")]
    [Description("Get the current time in the given time zone.")]
    public static string GetCurrentDateTimeInTimeZone(string timeZone)
    {
        DateTime dateTime = DateTime.UtcNow;
        TimeZoneInfo timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(timeZone);
        dateTime = TimeZoneInfo.ConvertTimeFromUtc(dateTime, timeZoneInfo);
        return dateTime.ToString("yyyy-MM-dd HH:mm:ss");
    }

    [KernelFunction("Get_Date_Time_In_Date_Time_Offset")]
    [Description("Get the Date time offset for the given Date Time.")]
    public static DateTimeOffset GetDateTimeInDateTimeOffset(string dateTime)
    {
        return DateTimeOffset.Parse(dateTime);
    }
}
