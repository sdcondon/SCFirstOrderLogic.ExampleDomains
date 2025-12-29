
using SCFirstOrderLogic.FormulaCreation.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using static SCFirstOrderLogic.FormulaCreation.Linq.Operators;

namespace SCFirstOrderLogic.ExampleDomains.FromAIaMA.Chapter8.UsingLanguageIntegration;

/// <summary>
/// The natural numbers example domain from chapter 8 of Artificial Intelligence: A Modern Approach, Global Edition by Stuart Russel and Peter Norvig.
/// </summary>
public static class NaturalNumbersDomain
{
    /// <summary>
    /// Gets the fundamental axioms of the natural numbers domain.
    /// </summary>
    public static IReadOnlyCollection<Formula> Axioms { get; } = new Expression<Predicate<INaturalNumbers>>[]
    {
        d => d.All(x => x.Successor != d.Zero),
        d => d.All((x, y) => If(x != y, x.Successor != y.Successor)),
        d => d.All(x => d.Zero.Add(x) == x),
        d => d.All((x, y) => x.Successor.Add(y) == x.Add(y).Successor),

    }.Select(e => LinqFormulaFactory.Create<INaturalNumbers, INaturalNumber>(e)).ToList().AsReadOnly();
}

/// <summary>
/// The domain type for the <see cref="NaturalNumbersDomain"/>.
/// </summary>
public interface INaturalNumbers : IEnumerable<INaturalNumber>
{
    /// <summary>
    /// Gets the 'Zero' constant of the natural numbers domain.
    /// </summary>
    INaturalNumber Zero { get; }
}

/// <summary>
/// Interface for the elements of the <see cref="NaturalNumbersDomain"/>. An implementation would only be needed for the <c>Bind</c> scenario mentioned in summary of the <see cref="NaturalNumbersDomain"/> class.
/// </summary>
public interface INaturalNumber
{
    INaturalNumber Successor { get; }

    INaturalNumber Add(INaturalNumber x);
}
