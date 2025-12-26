using System.Collections.Generic;
using static SCFirstOrderLogic.FormulaCreation.FormulaFactory;

namespace SCFirstOrderLogic.ExampleDomains.FromAIaMA.Chapter9.UsingFormulaFactory;

/// <summary>
/// <para>
/// The "crime" example from section 9.3 of Artificial Intelligence: A Modern Approach, Global Edition by Stuart Russel and Peter Norvig.
/// </para>
/// <para>
/// Example usage:
/// </para>
/// <code>
/// using static SCFirstOrderLogic.ExampleDomains.FromAIaMA.Chapter9.CrimeDomain;
/// ..
/// IKnowledgeBase kb = .. // a knowledge base implementation
/// kb.Tell(Axioms);
/// var answer = kb.Ask(IsCriminal(West)); // should return true
/// </code>
/// </summary>
public static class CrimeDomain
{
    static CrimeDomain()
    {
        Axioms = new List<Formula>()
        {
            // "... it is a crime for an American to sell weapons to hostile nations":
            // American(x) ∧ Weapon(y) ∧ Sells(x, y, z) ∧ Hostile(z) ⇒ Criminal(x).
            ForAll(X, Y, Z, If(And(IsAmerican(X), IsWeapon(Y), Sells(X, Y, Z), IsHostile(Z)), IsCriminal(X))),

            // "Nono... has some missiles."
            // ∃x IsMissile(x) ∧ Owns(NoNo, x)
            ThereExists(X, And(IsMissile(X), Owns(NoNo, X))),

            // "All of its missiles were sold to it by Colonel West":
            // Missile(x) ∧ Owns(NoNo, x) ⇒ Sells(West, x, NoNo)
            ForAll(X, If(And(IsMissile(X), Owns(NoNo, X)), Sells(ColonelWest, X, NoNo))),

            // We will also need to know that missiles are weapons: 
            ForAll(X, If(IsMissile(X), IsWeapon(X))),

            // And we must know that an enemy of America counts as “hostile”:
            // Enemy(x, America) ⇒ Hostile(x)
            ForAll(X, If(IsEnemyOf(X, America), IsHostile(X))),

            // "West, who is American..": American(West)
            IsAmerican(ColonelWest),

            // "The country Nono, an enemy of America..": Enemy(NoNo, America).
            IsEnemyOf(NoNo, America),

        }.AsReadOnly();
    }

    /// <summary>
    /// Gets the fundamental axioms of the crime domain.
    /// </summary>
    public static IReadOnlyCollection<Formula> Axioms { get; }

    public static Function America { get; } = new(nameof(America));
    public static Function NoNo { get; } = new(nameof(NoNo));
    public static Function ColonelWest { get; } = new(nameof(ColonelWest));

    public static Predicate IsAmerican(Term t) => new(nameof(IsAmerican), t);
    public static Predicate IsHostile(Term t) => new(nameof(IsHostile), t);
    public static Predicate IsCriminal(Term t) => new(nameof(IsCriminal), t);
    public static Predicate IsWeapon(Term t) => new(nameof(IsWeapon), t);
    public static Predicate IsMissile(Term t) => new(nameof(IsMissile), t);
    public static Predicate Owns(Term owner, Term owned) => new(nameof(Owns), owner, owned);
    public static Predicate Sells(Term seller, Term item, Term buyer) => new(nameof(Sells), seller, item, buyer);
    public static Predicate IsEnemyOf(Term t, Term other) => new(nameof(IsEnemyOf), t, other);
}
