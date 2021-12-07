#pragma warning disable CS1998 // 비동기 메서드는 'await' 연산자가 없기 때문에 동기적으로 실행됩니다
using System.Linq;
using System.Threading.Tasks;
using BotBone.Core;
using BotBone.Core.Api;
using BotBone.Core.Modules;

namespace Citrine.Core
{
	public class TranslateCommand : CommandBase
	{
		public override string Name => "translate";

		public override string Usage => "/translate <from(자동 선택은 auto)> <to> <text>";

		public override string Description => "문자열을 번역합니다.";

		public override async Task<string> OnActivatedAsync(ICommandSender sender, Server core, IShell shell, string[] args, string body)
		{
			if (args.Length < 3)
				throw new CommandException();
			var from = args[0].ToLowerInvariant();
			if (from == "auto") from = "";
			var to = args[1].ToLowerInvariant();
			var text = string.Join(" ", args.Skip(2));

			var url = $"https://script.google.com/macros/s/AKfycbzBORthiOILTTAOHd778LawGkjp5Lii7p2ttaMADMHSDHuUS3M/exec?text={text}&source={from}&target={to}";
			return await (await Server.Http.GetAsync(url)).Content.ReadAsStringAsync();
		}
	}
}
