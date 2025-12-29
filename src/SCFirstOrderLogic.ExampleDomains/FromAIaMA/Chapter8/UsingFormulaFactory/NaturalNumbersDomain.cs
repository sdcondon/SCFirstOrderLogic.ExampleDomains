using System.Collections.Generic;
using static SCFirstOrderLogic.FormulaCreation.FormulaFactory;

namespace SCFirstOrderLogic.ExampleDomains.FromAIaMA.Chapter8.UsingFormulaFactory;

/// <summary>
/// The natural numbers example domain from chapter 8 of Artificial Intelligence: A Modern Approach, Global Edition by Stuart Russel and Peter Norvig.
/// </summary>
public static class NaturalNumbersDomain
{
    static NaturalNumbersDomain()
    {
        Axioms = new List<Formula>()
        {
            ForAll(X, Not(AreEqual(Successor(X), Zero))),

            ForAll(X, Y, If(Not(AreEqual(X, Y)), Not(AreEqual(Successor(X), Successor(Y))))),

            ForAll(X, AreEqual(Add(Zero, X), X)),

            ForAll(X, Y, AreEqual(Add(Successor(X), Y), Add(Successor(Y), X))),

        }.AsReadOnly();
    }

    /// <summary>
    /// Gets the fundamental axioms of the natural numbers domain.
    /// </summary>
    public static IReadOnlyCollection<Formula> Axioms { get; }

    /// <summary>
    /// Gets the 'Zero' constant of the natural numbers domain.
    /// </summary>
    public static Function Zero { get; } = new(nameof(Zero));

    public static Function Successor(Term t) => new(nameof(Successor), t);

    public static Function Add(Term t, Term other) => new(nameof(Add), t, other);
}
