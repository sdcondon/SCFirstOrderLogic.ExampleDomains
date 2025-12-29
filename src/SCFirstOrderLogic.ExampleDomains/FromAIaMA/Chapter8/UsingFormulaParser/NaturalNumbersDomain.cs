using System.Collections.Generic;
using System.Linq;
using SCFirstOrderLogic.FormulaCreation;

namespace SCFirstOrderLogic.ExampleDomains.FromAIaMA.Chapter8.UsingFormulaParser;

/// <summary>
/// The natural numbers example domain from chapter 8 of Artificial Intelligence: A Modern Approach, Global Edition by Stuart Russel and Peter Norvig.
/// </summary>
public static class NaturalNumbersDomain
{
    public static IReadOnlyCollection<Formula> Axioms { get; } = new[]
    {
        "∀ x, ¬(Successor(x) = 0)",
        "∀ x, y, ¬[x = y] => ¬[Successor(x) = Successor(y)]",
        "∀ x, Add(0, x) = x",
        "∀ x, y, Add(Successor(x), y) = Add(Successor(y), x)",

    }.Select(s => FormulaParser.Default.Parse(s)).ToList().AsReadOnly();
}
