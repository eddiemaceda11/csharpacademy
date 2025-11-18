/*
* This code creates a DateTime object representing March 11, 2023, 10:30 AM.
*      DateTime date1 = new DateTime(2023, 3, 11, 10, 30, 0);
*
* The DateTime class also provides various methods to format dates in different ways.
* For example, the ToString method can format a DateTime object as a string.
*      The following code formats the date in the “dd MMM yyyy” format (e.g., “11 Mar 2023”):
*
* The DateTimeOffset class is similar to the DateTimd class but includes information about the time zone
* offset. This behavior makes it more useful when working with dates and times across different time zones.
* DateTimeOffset objects can be created using the same constructors as DateTime ibjects but also require a
* time zone offset, as shown below:
*      DateTimeOffset date2 = new DateTimeOffset(2023, 3, 11, 10, 30, 0, TimeSpan.FromHours(-5));
*
* The TimeSpan class represents a duration of time rather than a specific date and time. It can perform
* calculations involving dates and times, such as subtracting and adding time intervals from DateTime objects.
* TimeSpan objects can be created using various constructors, which accept paramters such as hours, minutes,
* seconds, and ms. Sample below which represents a TimeSpan object of 1 hour and 30 minutes:
*      TimeSpan duration = new TimeSpan(1, 30, 0);
*
* USING DATES IN C
* There are many ways to use days in C#. Some examples below:
*
* 1.) Getting the current date and time
*      DateTime currentDateTime = DateTime.Now;
*         -> returns DT object like 11/9/2025 7:06:01PM
*
* 2.) Getting the current date
*      DateTime currentDate = DateTime.Today
*          -> returns DT object like 11/9/2025
*
* 3.) Adding or subtracting time intervals from a date.
*      DateTime date = new DateTime(2023, 3, 11);
*      DateTime newDate = date.AddDays(1);
*          -> sets the newDate var to March 12, 2023
*
*      DateTime date = new DateTime(2023, 3, 11, 10, 30, 0);
*      DateTime newDate = date.Subtract(new TimeSpan(1, 0, 0));
*          -> sets the newDate to March 11, 2023, 9:30AM
*
* 4.) Converting time zones
*      DateTime date = new DateTime(2023, 3, 11, 10, 30, 0);
*      TimeZoneInfo sourceTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
*      TimeZoneInfo targetTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Pacific Standard Time");
*      DateTime newDate = TimeZoneInfo.ConvertTime(date, sourceTimeZone, targetTimeZone);
*          -> The snippet above converts the date from EST to PST
*              and sets the new variable date to March 11, 2023, 7:30 AM
*
* 5.) Working with daylight saving time
*      DateTime date = new DateTime(2023, 3, 11, 10, 30, 0);
*      bool isDST = date.IsDaylightSavingTime(); // true
*      DateTime newDate = date.AddHours(1); // adjusts for DST
*
* 6.) Handling null or undefined dates
*      DateTime? date = null;
*      if (date == null || date == DateTime.MinValue)
*      {
*         // handle null or undefined date
*      }
*
* 7.) Comparing dates
*      DateTime date1 = new DateTime(2023, 3, 11);
*      DateTime date2 = new DateTime(2023, 3, 12);
*      bool isAfter = date1 date2; // true
*
* 8.) Formatting dates
*      DateTime date = new DateTime(2023, 3, 11, 10, 30, 0);
*      string formattedDate1 = date.ToString("d"); // "3/11/2023"
*      string formattedDate2 = date.ToString("D"); // "Saturday, March 11, 2023"
*      string formattedDate3 = date.ToString("t"); // "10:30 AM"
*      string formattedDate4 = date.ToString("T"); // "10:30:00 AM"
*
* 9.) Parsing dates
*      string dateString = "2023-03-11";
*
*      DateTime date = DateTime.Parse(dateString);
*      DateTime parsedDate;
*
*      if (DateTime.TryParse(dateString, out parsedDate))
*      {
*          Console.WriteLine("Parsed Date: " + parsedDate.ToString());
*      }
*      else
*      {
*          Console.WriteLine("Could not parse date: ", dateString);
*      }
*          -> The code above creates a DT object representing March 11, 2023, based on
*            the string "2023-03-11". The parse method will throw an exception if the string
*            cannot be parsed as valid DateTime. The TryParse method will return false if the
*            string cannot be parsed w/o throwing an exception.
*
* 10.) Summary example containing several of the concepts from above:
  */