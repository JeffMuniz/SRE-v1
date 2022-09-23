// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("Minor Code Smell", "S101:Types should be named in PascalCase", Justification = "Declaração padrão para mensagens utilizado pelo Masstransit", Scope = "type", Target = "~T:Shared.Messaging.Configuration.Command")]
[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Declaração padrão para mensagens utilizado pelo Masstransit", Scope = "type", Target = "~T:Shared.Messaging.Configuration.Command")]

[assembly: SuppressMessage("Minor Code Smell", "S101:Types should be named in PascalCase", Justification = "Declaração padrão para mensagens utilizado pelo Masstransit", Scope = "type", Target = "~T:Shared.Messaging.Configuration.Event")]
[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Declaração padrão para mensagens utilizado pelo Masstransit", Scope = "type", Target = "~T:Shared.Messaging.Configuration.Event")]
