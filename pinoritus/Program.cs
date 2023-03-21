var testDictionary = new Dictionary<BusinessPartyKey, string>() {
    [new BusinessPartyKey(id:"item000", role:"role000")] = "item000",
    [new BusinessPartyKey(id: "item001", role: "role000")] = "item001",
};

Console.WriteLine(testDictionary[new BusinessPartyKey(id: "item001", role: "role000")]);


public class BusinessPartyKey : IEquatable<BusinessPartyKey>
{
    public string Id { get; }
    public string Role { get; }

    public override bool Equals(object? obj)
    {
        var other = obj as BusinessPartyKey;
        return Id == other?.Id
            && Role == other?.Role;
    }
    bool IEquatable<BusinessPartyKey>.Equals(BusinessPartyKey? other) => Equals(other);
    public override int GetHashCode()
    {
        var firstPrime = 4049;
        var secondPrime = 3313;
        unchecked // Allow arithmetic overflow
        {
            int hashcode = firstPrime; // this will be the default hash code when the fields are null
                                       // Calculating hash code for the property and XORing with the second prime.
                                       // Then multiplying by previous value. If property is null, the value remains the same.
            hashcode = hashcode * (Id is null ? 1 : (secondPrime + Id.GetHashCode()));
            hashcode = hashcode * (Role is null ? 1 : (secondPrime + Role.GetHashCode()));
            return hashcode;
        }
    }
    public BusinessPartyKey(string id, string role)
    {
        Id = id;
        Role = role;
    }
    public static bool operator ==(BusinessPartyKey? left, BusinessPartyKey? right)
    {
        return left?.Equals(right) ?? false;
    }
    public static bool operator !=(BusinessPartyKey? left, BusinessPartyKey? right)
    {
        return !(left?.Equals(right) ?? false);
    }
}
