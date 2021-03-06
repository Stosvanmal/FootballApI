﻿using FutbolAPI.Business.API;
using FutbolAPI.Business.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace FutbolAPI.Web
{
    public class Worker : IHostedService, IDisposable
    {
        private static readonly HttpClient client = new HttpClient();
        private readonly ILogger<Worker> _logger;
        private readonly IServiceScopeFactory serviceScopeFactory;
        private Timer _timer;
        public Worker(ILogger<Worker> logger, IServiceScopeFactory serviceScopeFactory)
        {

            _logger = logger;
            this.serviceScopeFactory = serviceScopeFactory;
        }


        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(DoWork, null, TimeSpan.Zero,
            TimeSpan.FromSeconds(5));

            return Task.CompletedTask;
        }
        private async void DoWork(object state)
        {
            using (var scope = serviceScopeFactory.CreateScope())
            {
                var matchAPI = scope.ServiceProvider.GetService<IMatchAPI>();
                IEnumerable<int?> allMatch = await matchAPI.GetMatchesNow();

                //Hay un if debug para que busque de verdad los partidos
                if (allMatch.Count() > 0)
                {
                    List <IPerson > notPlay = await matchAPI.GetPlayerNotPlay(allMatch.ToList());
                    notPlay.AddRange(await matchAPI.GetManagerNotPlay(allMatch.ToList()));
                    
                    //Entiendo que enviar debe ser al servicio ya que es post
                    var content = JsonConvert.SerializeObject(notPlay);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(content);
                    var byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    var response = await client.PostAsync("http://interview-api.azurewebsites.net/api/IncorrectAlignment", byteContent);
                    var resultado = response.Content.ReadAsStringAsync().Result;

                    
                }
            }

        }

        public Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Timed Hosted Service is stopping.");

            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }

    }
}
