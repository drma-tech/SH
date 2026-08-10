namespace SH.Shared.Core.Helper;

public static class SubscriptionHelper
{
    public static Restrictions GetRestrictions(this AccountProduct product)
    {
        return product switch
        {
            AccountProduct.Basic => new BasicRestrictions(),
            AccountProduct.Premium => new PremiumRestrictions(),
            _ => new BasicRestrictions(),
        };
    }

    public static void ValidateWishList(AccountProduct? product, int qtd)
    {
        product ??= AccountProduct.Basic;
        var restriction = product.Value.GetRestrictions();

        if (qtd > restriction.Wishlist)
            throw new NotificationException("Your current plan does not support this operation. Consider upgrading to premium for more benefits.");
    }
}

public abstract class Restrictions
{
    public abstract int MySuggestions { get; }
    public abstract int Wishlist { get; } //4x
}

public class BasicRestrictions : Restrictions
{
    public override int MySuggestions => 0;
    public override int Wishlist => 7;
}

public class PremiumRestrictions : Restrictions
{
    public override int MySuggestions => 20;
    public override int Wishlist => 80;
}
