#pragma warning disable CS1998 // 비동기 메서드는 'await' 연산자가 없기 때문에 동기적으로 실행됩니다
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using BotBone.Core;
using BotBone.Core.Api;
using BotBone.Core.Modules;
using Newtonsoft.Json;

namespace Citrine.Core
{
	public class OjisanCommand : CommandBase
	{
		public override string Name => "ojisan";

		public override string Usage => "/ojisan [name]";

		public override string Description => "おじさん構文を返します。(일본어 전용)"; //i18n

		public override async Task<string> OnActivatedAsync(ICommandSender sender, Server core, IShell shell, string[] args, string body)
		{
			var req = !string.IsNullOrEmpty(body) ? new FormUrlEncodedContent(new[]{
				new KeyValuePair<string, string>("name", body)
			}) : new StringContent("") as HttpContent;
			var res = await (await Server.Http.PostAsync("https://ojichat.appspot.com/post", req)).Content.ReadAsStringAsync();
			return JsonConvert.DeserializeObject<OjichatResponse>(res).Message ?? "";
		}

		class OjichatResponse
		{
			[JsonProperty("message")]
			public string? Message { get; set; }
		}
	}
}
