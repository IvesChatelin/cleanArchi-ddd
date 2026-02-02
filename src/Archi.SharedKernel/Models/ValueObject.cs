namespace Archi.SharedKernel.Models;

/// <summary>
/// Classe de base des value objects avec la caractéristique d'égalité
/// </summary>
public abstract class ValueObject
{
    public static bool operator ==(ValueObject? valueObjectAGauche, ValueObject? valueObjectADroite) {

        if (valueObjectAGauche is null && valueObjectADroite is null)
        {
            return true;
        }

        if (valueObjectAGauche is null || valueObjectADroite is null)
        {
            return false;
        }

        return valueObjectAGauche.Equals(valueObjectADroite);
    }

    public static bool operator !=(ValueObject? valueObjectAGauche, ValueObject? valueObjectADroite) {
        return !(valueObjectAGauche == valueObjectADroite);
    }

    public override bool Equals(object? obj) {
        return obj is ValueObject valueObject &&
            GetType() == valueObject.GetType() &&
            ValuesAreEqual(valueObject);
    }

    public override int GetHashCode()
    {
        var hash = new HashCode();

        foreach (var value in GetAtomicValues())
        {
            hash.Add(value);
        }

        return hash.ToHashCode();
    }

    protected abstract IEnumerable<object> GetAtomicValues();

    private bool ValuesAreEqual(ValueObject valueObject) =>
        GetAtomicValues().SequenceEqual(valueObject.GetAtomicValues());
}
