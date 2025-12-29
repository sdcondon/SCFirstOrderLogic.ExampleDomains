using SCFirstOrderLogic.FormulaCreation.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using static SCFirstOrderLogic.FormulaCreation.Linq.Operators;

namespace SCFirstOrderLogic.ExampleDomains.FromAIaMA.Chapter9.UsingLanguageIntegration;

/// <summary>
/// The "crime" example from section 9.3 of Artificial Intelligence: A Modern Approach, Global Edition by Stuart Russel and Peter Norvig.
/// </summary>
public static class CrimeDomain
{
    public static IReadOnlyCollection<Formula> Axioms { get; } = new Expression<Predicate<IDomain>>[]
    {
        // "... it is a crime for an American to sell weapons to hostile nations":
        // ∀x, y, z IsAmerican(x) ∧ IsWeapon(y) ∧ Sells(x, y, z) ∧ IsHostile(z) ⇒ IsCriminal(x).
        d => d.All((seller, item, buyer) => If(seller.IsAmerican && item.IsWeapon && seller.Sells(item, buyer) && buyer.IsHostile, seller.IsCriminal)),

        // "Nono... has some missiles."
        // ∃x IsMissile(x) ∧ Owns(NoNo, x)
        d => d.Any(m => m.IsMissile && d.NoNo.Owns(m)),

        // "All of its missiles were sold to it by Colonel West":
        // ∀x IsMissile(x) ∧ Owns(NoNo, x) ⇒ Sells(West, x, NoNo)
        d => d.All(x => If(x.IsMissile && d.NoNo.Owns(x), d.ColonelWest.Sells(x, d.NoNo))),

        // We will also need to know that missiles are weapons: 
        // ∀x IsMissile(x) ⇒ IsWeapon(x)
        d => d.All(x => If(x.IsMissile, x.IsWeapon)),

        // And we must know that an enemy of America counts as “hostile”:
        // ∀x IsEnemyOf(x, America) ⇒ IsHostile(x)
        d => d.All(x => If(x.IsEnemyOf(d.America), x.IsHostile)),

        // "West, who is American.."
        // IsAmerican(West)
        d => d.ColonelWest.IsAmerican,

        // "The country NoNo, an enemy of America.."
        // IsEnemyOf(NoNo, America).
        d => d.NoNo.IsEnemyOf(d.America)

    }.Select(e => LinqFormulaFactory.Create<IDomain, IElement>(e)).ToList().AsReadOnly();

    public interface IDomain : IEnumerable<IElement>
    {
        IElement America { get; }
        IElement NoNo { get; }
        IElement ColonelWest { get; }
    }

    public interface IElement
    {
        bool IsAmerican { get; }
        bool IsHostile { get; }
        bool IsCriminal { get; }
        bool IsWeapon { get; }
        bool IsMissile { get; }
        bool Owns(IElement item);
        bool Sells(IElement item, IElement buyer);
        bool IsEnemyOf(IElement other);
    }
}
