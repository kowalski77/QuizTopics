// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

using System;
using System.Diagnostics.CodeAnalysis;

[assembly: CLSCompliant(false)]
[assembly: SuppressMessage("Design", "CA1040:Avoid empty interfaces", Justification = "Interface is used to identify a set of types at compile time.", Scope = "type", Target = "~T:QuizTopics.Common.DomainDriven.IAggregateRoot")]
[assembly: SuppressMessage("Design", "CA1040:Avoid empty interfaces", Justification = "Interface is used to identify a set of types at compile time.", Scope = "type", Target = "~T:QuizTopics.Common.Mediator.IDomainNotification")]
[assembly: SuppressMessage("Design", "CA1040:Avoid empty interfaces", Justification = "Interface is used to identify a set of types at compile time.", Scope = "type", Target = "~T:QuizTopics.Common.Mediator.ICommand`1")]
