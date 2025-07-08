using Microsoft.Extensions.DependencyInjection;
using barbeariaPro.Services;

namespace barbeariaPro.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddBarbeariaServices(this IServiceCollection services)
        {
            services.AddScoped<ClienteService>();
            services.AddScoped<ServicoService>();
            services.AddScoped<UsuarioService>();
            services.AddScoped<ProfissionalService>();
            services.AddScoped<ProfissionalServicoService>();
            services.AddScoped<CaixaService>();
            services.AddScoped<PagamentoService>();
            services.AddScoped<AgendamentoService>();
            services.AddScoped<MovimentacaoCaixaService>();
        }
    }
}
