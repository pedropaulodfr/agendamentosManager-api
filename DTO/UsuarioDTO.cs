namespace agendamentosmanager_api.DTO
{
    public class UsuarioAutenticadoDTO
    {
        public long UsuarioId { get; set; }
        public long? PacienteId { get; set; }
        public string Nome { get; set; }
        public string Cpfcnpj { get; set; }
        public string Perfil { get; set; }
        public string Master { get; set; }
        public string Token { get; set; }
    }

    public class UsuarioDTO
    {
        public long Id { get; set; }

        public string Perfil { get; set; } = null!;

        public string Nome { get; set; } = null!;

        public string Cpfcnpj { get; set; } = null!;

        public string Senha { get; set; } = null!;

        public bool? Master { get; set; }

        public bool? Ativo { get; set; }
        public string? Status { get; set; }

        public bool? Deletado { get; set; }
    }
}