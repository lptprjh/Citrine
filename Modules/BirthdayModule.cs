using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Timers;
using BotBone.Core;
using BotBone.Core.Api;
using BotBone.Core.Modules;

namespace Citrine.Core.Modules
{
	public class BirthdayModule : ModuleBase
	{
		public BirthdayModule()
		{
			timer = new Timer(1000)
			{
				AutoReset = true,
				Enabled = true,
			};
			timer.Elapsed += OnTick;
		}

		public override async Task<bool> ActivateAsync(IPost n, IShell shell, Server core)
		{
			if (n.Text == null)
				return false;

			var storage = core.Storage[n.User];

			var m = patternQueryBirthday.Match(n.Text);

			if (m.Success)
			{
				var birthday = storage.Get(StorageKey.Birthday, DateTime.MinValue);
				// orig: "知らないよ〜?" "--だよね" "--の誕生日は"
				var output = birthday == DateTime.MinValue ? "언제인지 모르겠어요..." : $"{birthday:yyyy년 MM월 dd일} 맞죠?";
				await shell.ReplyAsync(n, $"{core.GetNicknameOf(n.User)}의 생일이 " + output);
				return true;
			}

			m = patternSetBirthday.Match(n.Text);
			if (m.Success)
			{
				// 싫어하는 사람에게는 말하지 않는다
				if (core.GetRatingOf(n.User) <= Rating.Hate)
					return false;

				await SetBirthday(n, shell, core, m.Groups[1].Value);
				return true;
			}

			m = patternStartBirthdayRegister.Match(n.Text);
			if (m.Success)
			{
				// 싫어하는 사람에게는 말하지 않는다
				if (core.GetRatingOf(n.User) <= Rating.Hate)
					return false;

				var res = await shell.ReplyAsync(n, "네! 생일이 언제인지 가르쳐주세요 (년월일 순으로 적어주세요)");
				if (res != null)
					core.RegisterContext(res, this, null);
				return true;
			}

			return false;
		}

		public override async Task<bool> OnTimelineAsync(IPost n, IShell shell, Server core)
		{
			(this.core, this.shell) = (core, shell);
			await Task.Delay(0);
			return false;
		}

		public override async Task<bool> OnRepliedContextually(IPost n, IPost? context, Dictionary<string, object> store, IShell shell, Server core)
		{
			if (n.Text == null)
				return false;

			var m = patternBirthday.Match(n.Text);

			if (!m.Success)
			{
				await shell.ReplyAsync(n, "어... 날짜를 못 읽겠어요...");
				return true;
			}

			await SetBirthday(n, shell, core, m.Groups[1].Value);
			return true;
		}
		private async void OnTick(object sender, ElapsedEventArgs e)
		{
			if (core == null || shell == null)
				return;

			// 축하할 대상을 찾음
			var birthDayPeople = core.Storage.Records.Where(kv =>
			{
				var (userId, storage) = kv;

				// 호감도가 Like 이상이고
				var isLike = core.GetRatingOf(userId) >= Rating.Like;

				// 오늘이 생일이며
				var birthday = storage.Get(StorageKey.Birthday, DateTime.MinValue);
				if (birthday == DateTime.MinValue) return false;
				var today = DateTime.Today;
				var birthdayIsToday = birthday.Month == today.Month && birthday.Day == today.Day;

				// 아직 축하해 주지 않았을 때
				var isNotCelebratedYet = storage.Get(keyLastCelebratedYear, 0) != today.Year;

				return isLike && birthdayIsToday && isNotCelebratedYet;
			});

			foreach (var (id, storage) in birthDayPeople)
			{
				var user = await shell.GetUserAsync(id);
				if (user == null) continue;

				await shell.SendDirectMessageAsync(user, $"생일 축하해요，{core.GetNicknameOf(user)}");
				storage.Set(keyLastCelebratedYear, DateTime.Today.Year);
			}

		}

		private async Task SetBirthday(IPost n, IShell shell, Server core, string value)
		{
			var storage = core.Storage[n.User];
			try
			{
				var birthday = DateTime.Parse(value);
				storage.Set(StorageKey.Birthday, birthday);
				await shell.ReplyAsync(n, "알았어요");
			}
			catch (FormatException)
			{
				await shell.ReplyAsync(n, "어... 날짜를 못 읽겠어요...");
			}
		}

		private const string date = @"(\d{1,4}[年/\-년]\d{1,2}[月/\-월]\d{1,2}[日/\-일]?)";
		private static readonly Regex patternBirthday = new Regex(date);
		private static readonly Regex patternSetBirthday = new Regex($"(생일[은이]?\s*{date})|({date}이 (.+의|내|얘)?\s*생일)");
		private static readonly Regex patternStartBirthdayRegister = new Regex("생일을?\s*?(기억|외|말)");
		private static readonly Regex patternQueryBirthday = new Regex("생일((을?\s*(기억하|외(우고|웠)|알[고아]|말[해할]))|(이?\s*(기억[하나])))");
		private static readonly string keyLastCelebratedYear = "birthday.last-celebrated";

		private readonly Timer timer;
		private Server? core;
		private IShell? shell;
	}
}
