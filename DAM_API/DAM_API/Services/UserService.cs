using DAM_API.Data;
using DAM_API.Helper;
using DAM_API.Models;
using DAM_API.Models.Dtos;
using DAM_API.Services.Interfaces;
using FreePasses_API.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DAM_API.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<Utilizador> _userManager;
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly IEmailService _mailService;

        public UserService(UserManager<Utilizador> userManager, AppDbContext context, IConfiguration configuration, IEmailService mailservice)
        {
            _userManager = userManager;
            _context = context;
            _configuration = configuration;
            _mailService = mailservice;
        }
        enum Role
        {
            Admin,
            AdminCantina,
            Cliente
        }
        public async Task<Response> LoginAsync(LoginDto viewmodel)
        {
            // Verifica se existe aquele email
            var user = await _userManager.Users.Include(x=>x.Customer).Include(x => x.Admin).Include(x => x.AdminCantina).FirstOrDefaultAsync(x => x.Email == viewmodel.Email);

            if (user == null)
            {
                return new Response
                {
                    Message = "Não existe nenhum utilizador com esse Email",
                    IsSuccess = false
                };

            }
  
            if (user.Suspenso == true)
            {
                var suspensao = _context.Suspensões.FirstOrDefault(x => x.IdU == user.Id);
                Admin admin = null;

                if (suspensao.IdAdm != "BOT")
                {
                     admin = _context.Admins.Include(x => x.IdANavigation).FirstOrDefault(x => x.IdA == suspensao.IdAdm);
                }
                else
                {
                     admin = new Admin { Nome = "BOT Admin Cantinas" };
                }

                return new Response
                {
                    IsSuccess = false,
                    Message = $"Você foi suspenso no dia {suspensao.DataBloqueio} pelo admin {admin.Nome} pelo seguinte motivo: {suspensao.Motivo}"
                };
            }

            // Verifica password
            var result = await _userManager.CheckPasswordAsync(user, viewmodel.Password);

            if (!result)
            {
                return new Response
                {
                    Message = "Palavra passe incorreta",
                    IsSuccess = false
                };
            }

            // Claims são informações do utilizador que vao ser introduzidas no token que lhe é devolvido
            var claims = new List<Claim>();

            // Retorn a role do user
            var userRoles = await _userManager.GetRolesAsync(user);

            // Verificar se o user é cliente ou admin ou admin de cantina
            if (await _userManager.IsInRoleAsync(user, "Cliente"))
            {
                // É um cliente

                // Verificar confirmação do email
                if (!user.EmailConfirmed)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "Email não confirmado, por favor confirme o seu email."
                    };
                }
                claims.Add(new Claim(ClaimTypes.Role, "Cliente"));        
            }

            if (await _userManager.IsInRoleAsync(user, "Admin"))
            {
                // É admin
                claims.Add(new Claim("Nome", user.Admin.Nome));
                claims.Add(new Claim(ClaimTypes.Role, "Admin"));
            }

            if (await _userManager.IsInRoleAsync(user, "AdminCantina"))
            {
                // É admin da cantina
                claims.Add(new Claim(ClaimTypes.Role, "AdminCantina"));
            }

            // Adiciona as tais informações do user a lista das claims
            claims.Add(new Claim("Email", viewmodel.Email));
            claims.Add(new Claim("Id", user.Id.ToString()));
            claims.Add(new Claim(ClaimTypes.Name, user.Id));

            // Geração do token
            var keybuffer = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["AuthSettings:Key"]));

            var token = new JwtSecurityToken(
               issuer: _configuration["AuthSettings:Issuer"],
               audience: _configuration["AuthSettings:Audience"],
               claims: claims,
               expires: DateTime.Now.AddDays(10),
               signingCredentials: new SigningCredentials(keybuffer, SecurityAlgorithms.HmacSha256)
               );

            string tokenAsString = new JwtSecurityTokenHandler().WriteToken(token);

            return new Response
            {
                Message = tokenAsString,
                IsSuccess = true
            };
        }

        public async Task<Response> RegistUserAsync(RegisterDto viewModel)
        {
            if (viewModel == null)
            {
                throw new NullReferenceException("Viewmodel is null");
            }

            // Verifica se o email ja existe
            var checkExistingUser = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == viewModel.Email);

            if (checkExistingUser != null)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = $"Já existe um utlizador com o email {viewModel.Email}"
                };
            }

            // Verifica se as passwords são iguais
            if (viewModel.Password != viewModel.ConfirmPassword)
            {
                return new Response
                {
                    Message = "As palavras passes não coincidem",
                    IsSuccess = false
                };
            }

            IdentityResult result = null;
            Utilizador user = new Utilizador();
            try
            {
                // Criar utilizador
                user.Email = viewModel.Email;
                user.UserName = viewModel.Email;

                result = await _userManager.CreateAsync(user, viewModel.Password);

                switch (viewModel.Role)
                {
                    case "Cliente":
                        await _userManager.AddToRoleAsync(user, Role.Cliente.ToString()); ;

                        Customer c = new Customer
                        {
                            IdC = user.Id
                        };

                        _context.Customers.Add(c);
                        break;

                    case "Admin":
                        await _userManager.AddToRoleAsync(user, Role.Admin.ToString()); ;

                        Admin a = new Admin
                        {
                            IdA = user.Id,
                            Nome = viewModel.Nome
                        };

                        _context.Admins.Add(a);
                        break;

                    case "AdminCantina":
                        await _userManager.AddToRoleAsync(user, Role.AdminCantina.ToString()); ;

                        AdminCantina ac = new AdminCantina
                        {
                            IdAc = user.Id
                        };

                        _context.AdminsCantinas.Add(ac);
                        break;
                }              

                await _context.SaveChangesAsync();

            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return new Response
                {
                    Message = "Erro ao criar o utilizador"
                };
            }

            if (result.Succeeded)
            {

                if (viewModel.Role == "Cliente")
                {
                    //Enviar email de confirmação se for um cliente

                    var confirmEmailToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                    var endodedEmailToken = Encoding.UTF8.GetBytes(confirmEmailToken);

                    var validEmailToken = WebEncoders.Base64UrlEncode(endodedEmailToken);

                    var url = $"{_configuration["AppUrl"]}/api/users/ConfirmarEmail?userid={user.Id}&token={validEmailToken}";

                    await _mailService.sendEmailAsync(user.Email, "Confirme o seu Email", "<h1>Bem vindo(a) às nossas cantinas </h1>" +
                        $"<p>Por favor confira o seu email carregando no seguinte link <a href='{url}'>Confirmar Email</a> </p>");

                    return new Response
                    {
                        Message = "Conta criada com sucesso! Por favor confirme o seu email.",
                        IsSuccess = true
                    };
                }

                return new Response
                {
                    Message = "Conta criada com sucesso!",
                    IsSuccess = true
                };
            }

            return new Response
            {
                Message = "Erro ao criar o utilizador",
                IsSuccess = false
            };
        }

        public async Task<Response> ReciveEmailForgotPasswordAsync(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return new Response
                {
                    IsSuccess = false
                };
            }

            var user = _userManager.Users.FirstOrDefault(x => x.Email == email);

            if (user == null)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = $"Não existe nenhum utilizador associado ao email {email}"
                };
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            var encodedToken = Encoding.UTF8.GetBytes(token);

            var validToken = WebEncoders.Base64UrlEncode(encodedToken);

            var url = $"{_configuration["AppUrl"]}/recuperarpassword.html?permitido=ok&email={email}&token={validToken}";

            await _mailService.sendEmailAsync(email, "Recuperar Password", "<h1>Siga as seguintes intruções para recuperar a sua password</h1>" +
                $"<p>Para recuparar a sua password <a href='{url}'>clique aqui</a> </p>");

            return new Response
            {
                IsSuccess = true,
                Message = "O link para recuperar a sua password foi enviado para o seu email."
            };

        }

        public async Task<Response> ResetPasswordAsync(ResetPasswordViewModel viewmodel)
        {
            var user = _userManager.Users.FirstOrDefault(x => x.Email == viewmodel.Email);

            if (user == null)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = $"Não existe nenhum utilizador associado ao email {viewmodel.Email}"
                };
            }

            if (viewmodel.NewPassword != viewmodel.ConfirmNewPassword)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = "Palavras passe não coincidem",
                };
            }

            var decodedToken = WebEncoders.Base64UrlDecode(viewmodel.Token);
            var normalToken = Encoding.UTF8.GetString(decodedToken);

            var result = await _userManager.ResetPasswordAsync(user, normalToken, viewmodel.NewPassword);

            if (result.Succeeded)
            {
                return new Response
                {
                    IsSuccess = true,
                    Message = "Password reposta com sucesso!",
                };
            }

            return new Response
            {
                IsSuccess = false,
                Message = "Ups, algo correu mal na recuperação da password",
            };
        }
    }
}

