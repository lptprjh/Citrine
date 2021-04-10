#pragma warning disable CS1998 // 非同期メソッドは、'await' 演算子がないため、同期的に実行されます / 비동기 메소드는 'await' 연산자가 없어서 동기로 실행
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BotBone.Core;
using BotBone.Core.Api;
using BotBone.Core.Modules;

namespace Citrine.Core
{
    public class BinCommand : CommandBase
	{
		public override string Name => "tobyte";

		public override string Usage => "/tobyte <data>";

		public override string Description => "텍스트를 바이너리 형식으로 전환합니다.";

		public override string[] Aliases => new[] { "bin" };

		public override async Task<string> OnActivatedAsync(ICommandSender sender, Server core, IShell shell, string[] args, string body)
		{
			var sb = new StringBuilder();
			var cur = 0;
			foreach (var ch in Encoding.UTF8.GetBytes(body))
			{
				sb.Append($"{ch:x2} ");
				cur++;
				if (cur == 16)
				{
					cur = 0;
					sb.AppendLine();
				}
			}
			return sb.ToString();
		}
	}

    public class RepeatCommand : CommandBase
    {
        public override string Name => "repeat";

        public override string Usage => "/repeat <count>, <command-text>";

        public override string Description => "명령어를 반복 실행하여 결과를 개행으로 나누어 반환합니다.";

        public override async Task<string> OnActivatedAsync(ICommandSender sender, Server core, IShell shell, string[] args, string body)
        {
			if (args.Length < 2) throw new CommandException();
            var count = int.TryParse(args[0], out var c) ? c : throw new CommandException();
            var cmd = string.Join(' ', args.Skip(1));
            var output = "";
            for (var _ = 0; _ < count; _++)
            {
                output += await core.ExecCommand(InternalCommandSender.Instance, cmd) + "\n";
            }
            return output;
        }
    }
}
