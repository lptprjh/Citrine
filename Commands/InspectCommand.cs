#pragma warning disable CS1998 // 비동기 메서드는 'await' 연산자가 없기 때문에 동기적으로 실행됩니다
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BotBone.Core;
using BotBone.Core.Api;
using BotBone.Core.Modules;

namespace Citrine.Core
{
	public class InspectCommand : CommandBase
	{
		public override string Name => "inspect";

		public override string Usage => "/inspect [commands]";

		public override string Description => "명령어의 인자를 그대로 나열합니다.";

		public override async Task<string> OnActivatedAsync(ICommandSender sender, Server core, IShell shell, string[] args, string body)
		{
			var builder = new StringBuilder();
			builder.AppendFormat("[{0}]: ", args.Length);
			builder.Append(string.Join(", ", args.Select(a => $"\"{a}\"")));
			return builder.ToString();
		}
	}
}
