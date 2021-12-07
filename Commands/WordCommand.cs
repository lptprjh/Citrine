#pragma warning disable CS1998 // 비동기 메서드는 'await' 연산자가 없기 때문에 동기적으로 실행됩니다
using System;
using System.Text;
using System.Threading.Tasks;
using BotBone.Core;
using BotBone.Core.Api;
using Citrine.Core.Modules;

namespace Citrine.Core
{
	using static FortuneModule;
	using static FortuneExtension;
	public class WordCommand : CommandBase
	{
		public override string Name => "word";

		public override string Usage => "/word";

		public override string Description => "아무 말을 생성합니다.";

		public override async Task<string> OnActivatedAsync(ICommandSender sender, Server core, IShell shell, string[] args, string body)
		{
			if (args.Length > 0)
			{
				if (args[0].ToLowerInvariant() == "total")
				{
					return (ItemPrefixes.Length * ItemSuffixes.Length * Items.Length +
							ItemPrefixes.Length * Items.Length +
							Items.Length).ToString();
				}
				else if (int.TryParse(args[0], out var length) && length > 0)
				{
					var sb = new StringBuilder();
					length = Math.Min(length, 100);
					for (var i = 0; i < length; i++)
					{
						sb.AppendLine(GenerateWord());
					}
					return sb.ToString();
				}
				else
				{
					throw new CommandException();
				}
			}
			return GenerateWord();
		}
	}
}
