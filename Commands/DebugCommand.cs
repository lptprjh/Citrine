#pragma warning disable CS1998 // 비동기 메서드는 'await' 연산자가 없기 때문에 동기적으로 실행됩니다

using System.Linq;
using System.Threading.Tasks;
using BotBone.Core;
using BotBone.Core.Api;
using BotBone.Core.Modules;

namespace Citrine.Core
{
	public class DebugCommand : CommandBase
	{
		public override string Name => "debug";

		public override string Usage => "/debug";

		public override async Task<string> OnActivatedAsync(ICommandSender sender, Server core, IShell shell, string[] args, string body)
		{
			if (sender is not PostCommandSender p) return "call from post";

			switch (args[0])
			{
				case "set":
					core.Storage[p.User].Set(args[1], args[2]);
					return "success";
				case "get":
					return (core.Storage[p.User].Get<object>(args[1], (object)"null")).ToString();
				case "has":
					return core.Storage[p.User].Has(args[1]).ToString();
				default:
					return "set / get / has";
			}
		}
	}
}
