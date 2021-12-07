#pragma warning disable CS1998 // 비동기 메서드는 'await' 연산자가 없기 때문에 동기적으로 실행됩니다
using System.Threading.Tasks;
using BotBone.Core;
using BotBone.Core.Api;
using BotBone.Core.Modules;

namespace Citrine.Core
{
	public class PipeLineCommand : CommandBase
	{
		public override string Name => "pipeline";

		public override string Usage => "/pipeline (실행할 명령어를 개행으로 구분하여 나열)";

		public override string Description => "각각의 명령어를 연결하여 실행합니다. 앞쪽 명령어의 출력을 뒤쪽 명령어의 인자로 덧붙입니다.";

		public override Task<string> OnActivatedAsync(ICommandSender sender, Server core, IShell shell, string[] args, string body)
		{
			return PipeCommand.RunPipeAsync(sender, core, body, '|');
		}
	}
}
