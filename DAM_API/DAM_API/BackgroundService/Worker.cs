using DAM_API.Data;
using DAM_API.Models;
using DAM_API.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DAM_API.BackgroundService
{
    public class Worker : IWorker
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly ILogger<Worker> _logger;
        private int number = 0;
        private int number2 = 10;
        public Worker(ILogger<Worker> logger, IServiceScopeFactory serviceScopeFactory)
        {
            _logger = logger;
            _serviceScopeFactory = serviceScopeFactory;
        }

        public async Task DoWork(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                //await AtualizarFaltas();
                await VerificarFaltas();

                await Task.Delay(1000 * 500);
            }
        }

        public async Task AtualizarFaltas()
        {
            var currentTime = DateTime.Now;

            // Executa uma vez por dia as 23h
            if (currentTime.Hour == 15)
            {

                var hoje = DateTime.Now;

                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var _context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                    var dia = hoje.Day;
                    var mes = hoje.Month;
                    var ano = hoje.Year;
                    var dataEmString = ano + "-" + mes + "-" + dia;

                    var refeicoesHoje = await _context.Reservas.Where(x => x.Data == dataEmString).ToListAsync();

                    foreach (var refeicao in refeicoesHoje)
                    {
                        if (refeicao.FoiLido == false)
                        {
                            var userAtualizarFaltas = await _context.Customers.FirstOrDefaultAsync(x => x.IdC == refeicao.CustomerId);
                            if (userAtualizarFaltas.Faltas < 5)
                            {
                                userAtualizarFaltas.Faltas++;
                            }
                            _context.Update(userAtualizarFaltas);
                            await _context.SaveChangesAsync();
                        }
                    }

                }
            }
        }

        public async Task VerificarFaltas()
        {
            var currentTime = DateTime.Now;

            // Executa uma vez por dia as 23h
            if (currentTime.Hour == 15)
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var _context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                    var customers = await _context.Customers.Include(x=> x.IdCNavigation).ToListAsync();

                    foreach (var customer in customers)
                    {
                        if (customer.Faltas == 3)
                        {
                            // Enviar email de aviso que so pode dar mais duas faltas

                            using(var scope2 = _serviceScopeFactory.CreateScope())
                            {
                                var _mailService = scope2.ServiceProvider.GetRequiredService<IEmailService>();

                                await _mailService.sendEmailAsync(customer.IdCNavigation.Email, "Aviso faltas", "<h1>Já atingiu as 3 faltas de refeições, apenas pode dar mais 2 faltas</p>");
                            }
                        }

                        if (customer.Faltas == 5)
                        {
                            try
                            {
                                var jaEstaSuspenso = await _context.Suspensões.FirstOrDefaultAsync(x => x.IdU == customer.IdC);

                                if (jaEstaSuspenso == null)
                                {
                                    var utilizadorBloquear = await _context.Utilizadores.FirstOrDefaultAsync(x => x.Id == customer.IdC);
                                    Suspensões novaSuspensao = new Suspensões()
                                    {
                                        IdAdm = "BOT",
                                        IdU = utilizadorBloquear.Id,
                                        Motivo = "Excedeu o numero de faltas às refeições (5)",
                                        DataBloqueio = DateTime.Now
                                    };

                                    utilizadorBloquear.Suspenso = true;

                                    _context.Utilizadores.Update(utilizadorBloquear);
                                    _context.Suspensões.Add(novaSuspensao);
                                    await _context.SaveChangesAsync();
                                }
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e);
                            }
                        }
                    }

                }
            }
        }

        //public async Task VerificarFaltas(CancellationToken cancellationToken)
        //{
        //    while (!cancellationToken.IsCancellationRequested)
        //    {
                
        //        await Task.Delay(1000 * 500);
        //    }
        //}

        public async Task DoWork2(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                Interlocked.Increment(ref number);
                _logger.LogInformation($"Worker printing number:  { number}");
                await Task.Delay(1000 * 5);
            }
        }
    }
}
