/*
 * Created by: Eugene Agafonov
 * Created: 22 апреля 2007 г.
 */

using System;
using System.Collections.Generic;

namespace R.Server.Common
{
	public class CronPeriod
	{
		private readonly List<int> _minutes;
		private readonly List<int> _hours;
		private readonly List<int> _monthDays;
		private readonly List<int> _months;
		private readonly List<int> _weekDays;
		private readonly CronPeriodEntry _entry;

		public CronPeriod(string period)
		{
			_entry = new CronPeriodEntry(period);
			_minutes = ParseTimes(_entry.Minutes, 0, 59);
			_hours = ParseTimes(_entry.Hours, 0, 23);
			_months = ParseTimes(_entry.Months, 1, 12);

			if (!_entry.MonthDays.Equals("*") && _entry.Months.Equals("*"))
			{
				// every n monthdays, disregarding weekdays
				_monthDays = ParseTimes(_entry.MonthDays, 1, 31);
				_weekDays = new List<int> {-1};
			}
			else if (_entry.MonthDays.Equals("*") && !_entry.Months.Equals("*"))
			{
				// every n weekdays, disregarding monthdays
				_weekDays = ParseTimes(_entry.WeekDays, 1, 7);
				_monthDays = new List<int> {-1};
			}
			else
			{
				// every n weekdays, every m monthdays
				_monthDays = ParseTimes(_entry.MonthDays, 1, 31);
				_weekDays = ParseTimes(_entry.WeekDays, 1, 7);
			}
		}

		public bool IsTimeToStart(DateTime now)
		{
			if (_entry.IsWrong) return false;

			if (Contains(_minutes, now.Minute) &&
				Contains(_hours, now.Hour) &&
					Contains(_monthDays, now.Day) &&
						Contains(_months, now.Month) &&
							Contains(_weekDays, GetWeekDay(now))
				)
			{
				return true;
			}

			return false;
		}

		public bool IsWrongEntry
		{
			get { return _entry.IsWrong; }
		}

		private static List<int> ParseTimes(string period, int bottom, int top)
		{
			// Took this from Cron parser on codeproject
			//

			var result = new List<int>();

			var list = period.Split(new[] {','});

			foreach (var entry in list)
			{
				int start, end, interval;

				var parts = entry.Split(new[] {'-', '/'});

				if (parts[0].Equals("*"))
				{
					if (parts.Length > 1)
					{
						start = bottom;
						end = top;

						interval = int.Parse(parts[1]);
					}
					else
					{
						// put a -1 in place
						start = -1;
						end = -1;
						interval = 1;
					}
				}
				else
				{
					// format is 0-8/2
					start = int.Parse(parts[0]);
					end = parts.Length > 1 ? int.Parse(parts[1]) : int.Parse(parts[0]);
					interval = parts.Length > 2 ? int.Parse(parts[2]) : 1;
				}

				for (var i = start; i <= end; i += interval)
					result.Add(i);
			}

			return result;
		}

		// sort of a macro to keep the if-statement above readable
		private static bool Contains(ICollection<int> list, int val)
		{
			// -1 represents the star * from the crontab
			return list.Contains(val) || list.Contains(-1);
		}

		private static int GetWeekDay(DateTime date)
		{
			if (date.DayOfWeek.Equals(DayOfWeek.Sunday))
				return 7;
			return (int) date.DayOfWeek;
		}

		#region CronPeriodEntry
		internal class CronPeriodEntry
		{
			private readonly string _minutes;
			private readonly string _hours;
			private readonly string _monthDays;
			private readonly bool _isWrong;

			public string Minutes
			{
				get { return _minutes; }
			}

			public string Hours
			{
				get { return _hours; }
			}

			public string MonthDays
			{
				get { return _monthDays; }
			}

			public string Months { get; set; }

			public string WeekDays { get; set; }

			public bool IsWrong
			{
				get { return _isWrong; }
			}

			public CronPeriodEntry(string period)
			{
				var segments = period.Split(new[] {' ', '\t'});
				if (segments.Length != 5)
				{
					_isWrong = true;
				}
				else
				{
					_isWrong = false;
					_minutes = segments[0];
					_hours = segments[1];
					_monthDays = segments[2];
					Months = segments[3];
					WeekDays = segments[4];
				}
			}
		}
		#endregion
	}
}