using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Line.Messaging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace a982181_linebot.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LineBotController : ControllerBase
    {
        
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly HttpContext _httpContext;
        private readonly LineBotConfig _lineBotConfig;
        
        public LineBotController(IServiceProvider serviceProvider)
        {
            _httpContextAccessor = serviceProvider.GetRequiredService<IHttpContextAccessor>();
            _httpContext = _httpContextAccessor.HttpContext;
            _lineBotConfig = new LineBotConfig();
            _lineBotConfig.channelSecret = "b5be9111dfe4da3c0d463ec356b297e0";
            _lineBotConfig.accessToken = "JHqaq8/4khiwv3e171bFABHu2LnhzGzSZFvKwtClTiozndm3yMuBCJ++HBp/l8qSsVs6E8y7LpuvcCVdKaE9qhWzF9CFtk/fuhwVJTk5ADZ4ECWEZK+SzUGqs9lQVf+HgwU5OHzWLmxN4ifZAR6VNAdB04t89/1O/w1cDnyilFU=";
        }
        
        //完整的路由網址就是 https://xxx/api/linebot/run
        [HttpPost("run")]
        public async Task<IActionResult> Post()
        {
            try
            {
                var events = await _httpContext.Request.GetWebhookEventsAsync(_lineBotConfig.channelSecret);
                var lineMessagingClient = new LineMessagingClient(_lineBotConfig.accessToken);
                var lineBotApp = new LineBotApp(lineMessagingClient);
                await lineBotApp.RunAsync(events);
            } catch (Exception ex)
            {
                //需要 Log 可自行加入
                //_logger.LogError(JsonConvert.SerializeObject(ex));
            }
            return Ok();
        }
    }
}
