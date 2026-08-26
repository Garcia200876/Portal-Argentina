# Portal Argentina 🇦🇷

Projeto educacional em **ASP.NET Core MVC + MySQL**, revisado para entregar uma experiência mais profissional, responsiva e organizada.

## O que foi melhorado

- Interface visual completamente refinada, com identidade azul + amarelo inspirada na Argentina.
- Navbar fixa com efeito glass, estado de rolagem e item de navegação ativo.
- Animações de entrada com `IntersectionObserver` e suporte a `prefers-reduced-motion`.
- Cards, botões, formulários, banners e rodapé com novo sistema visual consistente.
- Botão flutuante de voltar ao topo.
- Melhor comportamento do menu em dispositivos móveis.
- Microinterações em cards, imagens, botões e opções do quiz.
- Área do Clube protegida por sessão.
- Quiz protegido também no POST de correção.
- Cadastro agora verifica o e-mail antes de inserir e informa claramente quando ele já está cadastrado.
- E-mails são normalizados em minúsculas para evitar inconsistências.
- Navegação e mensagens de formulário mais claras.
- Referências locais de imagens e estilos conferidas.

## Requisitos

- .NET 10 SDK
- MySQL 8 ou compatível
- Visual Studio 2026, VS Code ou outra IDE compatível

## Banco de dados

Execute:

```sql
SOURCE Database/portalargentina.sql;
```

Ou abra `Database/portalargentina.sql` no MySQL Workbench e execute o script.

A aplicação utiliza a connection string `PortalArgentina` em `WebApplication1/appsettings.json`.

Para evitar guardar credenciais no arquivo de configuração, também é possível utilizar a variável de ambiente:

```text
PORTALARGENTINA_CONNECTION_STRING
```

## Executar

Entre na pasta `WebApplication1` e execute:

```bash
dotnet restore
dotnet run
```

Depois abra a URL HTTPS exibida pelo ASP.NET Core.

## Criar administrador

1. Cadastre uma conta normalmente pelo Portal.
2. No MySQL, execute:

```sql
UPDATE usuarios
SET administrador = TRUE
WHERE email = 'seu-email@exemplo.com';
```

3. Saia e entre novamente no Portal.

## Segurança de senha

As novas contas usam PBKDF2. O projeto também mantém compatibilidade com hashes SHA-256 antigos e migra a senha automaticamente após um login válido.

## Estrutura principal

```text
WebApplication1/
├── Controllers/
├── Data/
├── Models/
├── Repositories/
├── Services/
├── Utils/
├── Views/
└── wwwroot/
    ├── css/
    ├── images/
    ├── js/
    └── lib/
```
