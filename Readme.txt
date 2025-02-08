// Atualizar banco de dados PRODUÇÃO
dotnet ef dbcontext scaffold "Host=dpg-cuh62tl6l47c73be3h0g-a.oregon-postgres.render.com;Port=5432;Database=agendamentobot;Username=pedro;Password=UwPnUwWoknvbrwclSX1C1BHaIW0r2OHg" Npgsql.EntityFrameworkCore.PostgreSQL -o Models --force --no-onconfiguring
