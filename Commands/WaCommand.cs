#pragma warning disable CS1998 // 비동기 메서드는 'await' 연산자가 없기 때문에 동기적으로 실행됩니다
using System.Collections.Generic;
using System.Threading.Tasks;
using BotBone.Core;
using BotBone.Core.Api;

namespace Citrine.Core
{
	public class WaCommand : CommandBase
	{
		public override string Name => "wa";

		public override string Usage => "/wa [amount=15]";

		public override string Description => "#우왕ーーーーーーーーーーーーーーー";

		public override async Task<string> OnActivatedAsync(ICommandSender sender, Server core, IShell shell, string[] args, string body)
		{
			var amount = args.Length < 1 ? 15 : int.Parse(args[0]);

			return string.Concat(Wa(amount));
		}

		private IEnumerable<char> Wa(int amount)
		{
			yield return '#';
			yield return '우왕';
			for (var i = 0; i < amount; i++)
				yield return 'ー';
		}
	}
}
