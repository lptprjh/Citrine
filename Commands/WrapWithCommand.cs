#pragma warning disable CS1998 // 비동기 메서드는 'await' 연산자가 없기 때문에 동기적으로 실행됩니다
using System.Threading.Tasks;
using BotBone.Core;
using BotBone.Core.Api;
using BotBone.Core.Modules;

namespace Citrine.Core
{
	public class WrapWithCommand : CommandBase
	{
		public override string Name => "wrapwith";

		public override string Usage => "/wrapwith <text-to-wrap> <text>";

		public override string Description => "지정한 텍스트로 문자열을 감쌉니다.";

		public override async Task<string> OnActivatedAsync(ICommandSender sender, Server core, IShell shell, string[] args, string body)
		{
			if (args.Length < 2)
				throw new CommandException();
			body = body[(args[0].Length + 1)..];
			return args[0] + body + args[0];
		}
	}
}
