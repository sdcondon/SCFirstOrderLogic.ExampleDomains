using SCFirstOrderLogic.FormulaCreation.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using static SCFirstOrderLogic.FormulaCreation.Linq.Operators;

namespace SCFirstOrderLogic.ExampleDomains.FromAIaMA.Chapter8.UsingLinqFormulaFactory;

/// <summary>
/// The kinship example domain from chapter 8 of Artificial Intelligence: A Modern Approach, Global Edition by Stuart Russel and Peter Norvig.
/// </summary>
public static class KinshipDomain
{
    /// <summary>
    /// Gets the fundamental axioms of the kinship domain.
    /// </summary>
    public static IReadOnlyCollection<Formula> Axioms { get; } = new Expression<Predicate<IEnumerable<IPerson>>>[]
    {
        // One's mother is one's female parent:
        d => d.All((m, c) => Iff(c.Mother == m, m.IsFemale && m.IsParentOf(c))),

        // Ones' husband is one's male spouse:
        d => d.All((w, h) => Iff(h.IsHusbandOf(w), h.IsMale && h.IsSpouseOf(w))),

        // Male and female are disjoint categories:
        d => d.All(x => Iff(x.IsMale, !x.IsFemale)),

        // Parent and child are inverse relations:
        d => d.All((p, c) => Iff(p.IsParentOf(c), c.IsChildOf(p))),

        // A grandparent is a parent of one's parent:
        d => d.All((g, c) => Iff(g.IsGrandparentOf(c), d.Any(p => g.IsParentOf(p) && p.IsParentOf(c)))),

        // A sibling is another child of one's parents:
        d => d.All((x, y) => Iff(x.IsSiblingOf(y), x != y && d.Any(p => p.IsParentOf(x) && p.IsParentOf(y)))),

    }.Select(e => LinqFormulaFactory.Create(e)).ToList().AsReadOnly();

    /// <summary>
    /// Gets some useful theorems of the kinship domain.
    /// Theorems are derivable from axioms, but might be useful for performance.
    /// </summary>
    public static IReadOnlyCollection<Formula> Theorems { get; } = new Expression<Predicate<IEnumerable<IPerson>>>[]
    {
        // Siblinghood is commutative:
        d => d.All((x, y) => Iff(x.IsSiblingOf(y), y.IsSiblingOf(x))),

    }.Select(e => LinqFormulaFactory.Create(e)).ToList().AsReadOnly();
}

/// <summary>
/// Interface for the elements of the <see cref="KinshipDomain"/>. An implementation would only be needed for the <c>Bind</c> scenario mentioned in summary of the <see cref="KinshipDomain"/> class.
/// </summary>
public interface IPerson
{
    //// Unary predicates:
    bool IsMale { get; }
    bool IsFemale { get; }

    //// Binary predicates:
    bool IsParentOf(IPerson person);
    bool IsSiblingOf(IPerson person);
    bool IsBrotherOf(IPerson person);
    bool IsSisterOf(IPerson person);
    bool IsChildOf(IPerson person);
    bool IsDaughterOf(IPerson person);
    bool IsSonOf(IPerson person);
    bool IsSpouseOf(IPerson person);
    bool IsWifeOf(IPerson person);
    bool IsHusbandOf(IPerson person);
    bool IsGrandparentOf(IPerson person);
    bool IsGrandchildOf(IPerson person);
    bool IsCousinOf(IPerson person);
    bool IsAuntOf(IPerson person);
    bool IsUncleOf(IPerson person);

    //// Unary functions:
    IPerson Mother { get; }
    IPerson Father { get; }
}
