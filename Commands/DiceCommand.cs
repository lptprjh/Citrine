#pragma warning disable CS1998 // 비동기 메서드는 'await' 연산자가 없기 때문에 동기적으로 실행됩니다
using System;
using System.Linq;
using System.Threading.Tasks;
using BotBone.Core;
using BotBone.Core.Api;
using BotBone.Core.Modules;

namespace Citrine.Core
{
	public class DiceCommand : CommandBase
	{
		public override string Name => "dice";

		public override string Usage => @"/dice [times] [max]
/dice [times]d<max>";

		public override string Description => "주사위를 굴립니다. 숫자 또는 1d6과 같은 방식으로 지정할 수 있습니다. 기본값은 1d6입니다.";

		public static readonly Random Rand = new Random();

		public override async Task<string> OnActivatedAsync(ICommandSender sender, Server core, IShell shell, string[] args, string body)
		{
			Assert(args.Length <= 2);
			var (times, max) = (1, 6);
			if (args.Length == 2)
			{
				Assert(int.TryParse(args[0], out times));
				Assert(int.TryParse(args[1], out max));
			}
			else if (args.Length == 1)
			{
				if (args[0].ToLowerInvariant().Contains("d"))
				{
					var d = args[0].ToLowerInvariant().Split('d');
					Assert(d.Length == 2);
					Assert(int.TryParse(d[0] != "" ? d[0] : "1", out times));
					Assert(int.TryParse(d[1], out max));
				}
				else
				{
					Assert(int.TryParse(args[0], out max));
				}
			}
			times = times > 0 ? times : 1;
			max = max > 0 ? max : 6;
			return string.Join(" ", Enumerable.Repeat(0, times).Select(_ => Rand.Next(max) + 1));
		}

		private void Assert(bool condition)
		{
			if (!condition)
			{
				throw new CommandException();
			}
		}
	}
}
