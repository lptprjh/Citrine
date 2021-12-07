#pragma warning disable CS1998 // 비동기 메서드는 'await' 연산자가 없기 때문에 동기적으로 실행됩니다
using System.Threading.Tasks;
using BotBone.Core;
using BotBone.Core.Api;
using BotBone.Core.Modules;

namespace Citrine.Core
{
    public class RatingCommand : CommandBase
	{
		public override string Name => "rating";

		public override string Usage => "/rating <set/add/get/status>";

		public override async Task<string> OnActivatedAsync(ICommandSender sender, Server core, IShell shell, string[] args, string body)
		{
			if(sender is not PostCommandSender p)
				return "use from post";
			if (args.Length < 1)
				throw new CommandException();

			switch (args[0].ToLowerInvariant().Trim())
			{
				case "set":
					if (!sender.IsAdmin)
						throw new AdminOnlyException();
					core.SetRatingValueOf(p.User.Id, int.Parse(args[1]));
					break;
				case "add":
					if (!sender.IsAdmin)
						throw new AdminOnlyException();
					core.Like(p.User.Id, int.Parse(args[1]));
					break;
				case "get":
					return core.GetRatingValueOf(p.User.Id).ToString();
				case "status":
					return core.GetRatingOf(p.User.Id).ToString();
			}
			return "ok";
		}
	}
}
