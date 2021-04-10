#pragma warning disable CS1998 // 非同期メソッドは、'await' 演算子がないため、同期的に実行されます / 비동기 메소드는 'await' 연산자가 없어서 동기로 실행
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

		public override string Description => "현재 시각을 반환합니다.";

		public override string[] Aliases => new[] { "date", "time", "dt" };

		public override async Task<string> OnActivatedAsync(ICommandSender sender, Server core, IShell shell, string[] args, string body)
		{
			return DateTime.Now.ToString();
		}
	}
}
