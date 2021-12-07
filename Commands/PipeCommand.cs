#pragma warning disable CS1998 // 비동기 메서드는 'await' 연산자가 없기 때문에 동기적으로 실행됩니다
using System;
using System.Linq;
using System.Threading.Tasks;
using BotBone.Core;
using BotBone.Core.Api;
using BotBone.Core.Modules;

namespace Citrine.Core
{
	public class PipeCommand : CommandBase
	{
		public override string Name => "pipe";

		public override string Usage => "/pipe (실행할 명령어를 파이프 문자로 |  구분하여 나열합니다)";

		public override string Description => "각각의 명령어를 연결하여 실행합니다. 앞쪽 명령어의 출력을 뒤쪽 명령어의 인자로 덧붙입니다.";

		public override Task<string> OnActivatedAsync(ICommandSender sender, Server core, IShell shell, string[] args, string body)
		{
			return RunPipeAsync(sender, core, body, '|');
		}

		public static async Task<string> RunPipeAsync(ICommandSender sender, Server core, string body, params char[] split)
		{
			var lines = body.Replace("\r", "\n").Replace("\r\n", "\n").Split(split).Where(l => !string.IsNullOrWhiteSpace(l)).Select(l => l.Trim());
			var output = "";
			foreach (var line in lines)
			{
				output = await core.ExecCommand(sender, line + " " + output);
				output = output.Trim();
			}
			return output;
		}
	}
}
