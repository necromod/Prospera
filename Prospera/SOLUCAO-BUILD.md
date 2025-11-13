# Solução para Erros de Build no GitHub Actions

## Problema Identificado

O arquivo `Prospera.sln` na raiz do repositório inclui **dois projetos**:
1. **Prospera** - Projeto web ASP.NET Core (.NET 8) ?
2. **FrmProspera** - Projeto Windows Forms (.NET Framework 4.8) ?

O projeto FrmProspera **não pode ser compilado no GitHub Actions** porque:
- Usa .NET Framework 4.8 (Windows-only)
- O GitHub Actions roda em ambiente Linux (ubuntu-latest)
- Gera erro: `MSB4216: Could not run the "GenerateResource" task`

## Soluções Disponíveis

### Solução 1: Modificar o arquivo Prospera.sln (RECOMENDADO)

Edite o arquivo `Prospera.sln` na raiz e **remova as linhas** referentes ao FrmProspera:

#### Antes:
```sln
Microsoft Visual Studio Solution File, Format Version 12.00
# Visual Studio Version 17
VisualStudioVersion = 17.3.32929.385
MinimumVisualStudioVersion = 10.0.40219.1
Project("{9A19103F-16F7-4668-BE54-9A1E7A4F7556}") = "Prospera", "Prospera\Prospera.csproj", "{8A0256FF-1747-405E-A3C3-37CB9B5E5F67}"
EndProject
Project("{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}") = "FrmProspera", "FrmProspera\FrmProspera.csproj", "{A0EF26C6-2169-47E8-9933-5FF4D059C908}"
EndProject
Global
	GlobalSection(SolutionConfigurationPlatforms) = preSolution
		Debug|Any CPU = Debug|Any CPU
		Release|Any CPU = Release|Any CPU
	EndGlobalSection
	GlobalSection(ProjectConfigurationPlatforms) = postSolution
		{8A0256FF-1747-405E-A3C3-37CB9B5E5F67}.Debug|Any CPU.ActiveCfg = Debug|Any CPU
		{8A0256FF-1747-405E-A3C3-37CB9B5E5F67}.Debug|Any CPU.Build.0 = Debug|Any CPU
		{8A0256FF-1747-405E-A3C3-37CB9B5E5F67}.Release|Any CPU.ActiveCfg = Release|Any CPU
		{8A0256FF-1747-405E-A3C3-37CB9B5E5F67}.Release|Any CPU.Build.0 = Release|Any CPU
		{A0EF26C6-2169-47E8-9933-5FF4D059C908}.Debug|Any CPU.ActiveCfg = Debug|Any CPU
		{A0EF26C6-2169-47E8-9933-5FF4D059C908}.Debug|Any CPU.Build.0 = Debug|Any CPU
		{A0EF26C6-2169-47E8-9933-5FF4D059C908}.Release|Any CPU.ActiveCfg = Release|Any CPU
		{A0EF26C6-2169-47E8-9933-5FF4D059C908}.Release|Any CPU.Build.0 = Release|Any CPU
	EndGlobalSection
	GlobalSection(SolutionProperties) = preSolution
		HideSolutionNode = FALSE
	EndGlobalSection
	GlobalSection(ExtensibilityGlobals) = postSolution
		SolutionGuid = {2B71BCEF-1F55-4DEE-93C1-580F9D8B2216}
	EndGlobalSection
EndGlobal
```

#### Depois:
```sln
Microsoft Visual Studio Solution File, Format Version 12.00
# Visual Studio Version 17
VisualStudioVersion = 17.8.34330.188
MinimumVisualStudioVersion = 10.0.40219.1
Project("{9A19103F-16F7-4668-BE54-9A1E7A4F7556}") = "Prospera", "Prospera\Prospera.csproj", "{8A0256FF-1747-405E-A3C3-37CB9B5E5F67}"
EndProject
Global
	GlobalSection(SolutionConfigurationPlatforms) = preSolution
		Debug|Any CPU = Debug|Any CPU
		Release|Any CPU = Release|Any CPU
	EndGlobalSection
	GlobalSection(ProjectConfigurationPlatforms) = postSolution
		{8A0256FF-1747-405E-A3C3-37CB9B5E5F67}.Debug|Any CPU.ActiveCfg = Debug|Any CPU
		{8A0256FF-1747-405E-A3C3-37CB9B5E5F67}.Debug|Any CPU.Build.0 = Debug|Any CPU
		{8A0256FF-1747-405E-A3C3-37CB9B5E5F67}.Release|Any CPU.ActiveCfg = Release|Any CPU
		{8A0256FF-1747-405E-A3C3-37CB9B5E5F67}.Release|Any CPU.Build.0 = Release|Any CPU
	EndGlobalSection
	GlobalSection(SolutionProperties) = preSolution
		HideSolutionNode = FALSE
	EndGlobalSection
	GlobalSection(ExtensibilityGlobals) = postSolution
		SolutionGuid = {2B71BCEF-1F55-4DEE-93C1-580F9D8B2216}
	EndGlobalSection
EndGlobal
```

**Linhas a remover:**
- Linha com `Project("{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}") = "FrmProspera"...`
- As 4 linhas dentro de `GlobalSection(ProjectConfigurationPlatforms)` que contêm `{A0EF26C6-2169-47E8-9933-5FF4D059C908}`

### Solução 2: Mover FrmProspera para fora do repositório

Se você não precisa do FrmProspera no controle de versão, mova-o para fora do repositório:

```powershell
Move-Item "FrmProspera" "C:\Temp\FrmProspera"
```

### Solução 3: Criar Solution Filter (.slnf)

Crie um arquivo `Prospera-Web.slnf` com apenas o projeto web:

```json
{
  "solution": {
    "path": "Prospera.sln",
    "projects": [
      "Prospera\\Prospera.csproj"
    ]
  }
}
```

E atualize o workflow para usar este arquivo.

## Status das Correções

? **Warnings de código corrigidos:**
- Views/Shared/Components/Menu/Default.cshtml - CS8605
- Controllers/MenuUsuarioController.cs - CS8602, CS8629

? **Build local:** Compilando com sucesso

? **Pendente:** Modificação do arquivo Prospera.sln

## Próximos Passos

1. Abra o arquivo `Prospera.sln` no Notepad ou Visual Studio
2. Remova as linhas referentes ao FrmProspera conforme indicado acima
3. Salve o arquivo
4. Commit e push das alterações:
   ```bash
   git add Prospera.sln
   git commit -m "Remove FrmProspera do arquivo de solução para corrigir build no CI/CD"
   git push
   ```

## Verificação

Após fazer o commit, o GitHub Actions deve compilar com sucesso sem o erro `MSB4216`.
