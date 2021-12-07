#pragma warning disable CS1998 // 비동기 메서드는 'await' 연산자가 없기 때문에 동기적으로 실행됩니다
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BotBone.Core;
using BotBone.Core.Api;
using BotBone.Core.Modules;

namespace Citrine.Core
{
	public class HelpCommand : CommandBase
	{
		public override string Name => "help";

		public override string Usage => "/help [name]";

		public override string Description => "명령어의 도움말을 표시합니다.";

		public override string[] Aliases { get; } = { "h" };

		public override async Task<string> OnActivatedAsync(ICommandSender sender, Server core, IShell shell, string[] args, string body)
		{
			if (args.Length == 0)
			{
				var descriptions = core.Commands.Select(GetDescription);

				return string.Join("\n", descriptions);
			}
			else
			{
				var name = args[0];
				var cmd = core.TryGetCommand(name);
				if (cmd == null)
					return $"명령어 {name} 를 찾을 수 없습니다.";
				var sb = new StringBuilder();
				sb.AppendLine(GetDescription(cmd));
				sb.Append("使い方: ");
				sb.AppendLine(cmd.Usage);
				if (cmd.Aliases != null)
					sb.AppendLine(string.Join(", ", cmd.Aliases));
				sb.AppendLine(DumpPermission(cmd.Permission));

				return sb.ToString();
			}
		}

		private string GetDescription(ICommand cmd) => $"/{cmd.Name} - {cmd.Description ?? "에 대한 설명이 없습니다."}";

		private string DumpPermission(PermissionFlag flag)
		{
			var sb = new StringBuilder();
			if (flag.HasFlag(PermissionFlag.AdminOnly))
				sb.Append("(관리자 전용)");
			if (flag.HasFlag(PermissionFlag.LocalOnly))
				sb.Append("(로컬 유저 전용)");
			return sb.ToString();
		}
	}
}
