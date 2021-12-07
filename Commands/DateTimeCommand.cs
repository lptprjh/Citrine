#pragma warning disable CS1998 // 비동기 메서드는 'await' 연산자가 없기 때문에 동기적으로 실행됩니다
using System;
using System.Threading.Tasks;
using BotBone.Core;
using BotBone.Core.Api;
using BotBone.Core.Modules;

namespace Citrine.Core
{
	public class DateTimeCommand : CommandBase
	{
		public override string Name => "datetime";

		public override string Usage => "/datetime";

		public override string Description => "현재 시각을 출력합니다.";

		public override string[] Aliases => new[] { "date", "time", "dt" };

		public override async Task<string> OnActivatedAsync(ICommandSender sender, Server core, IShell shell, string[] args, string body)
		{
			return DateTime.Now.ToString();
		}
	}
}
