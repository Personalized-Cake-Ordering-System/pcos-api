namespace CusCake.Domain.Constants;

public static class AvailableCakeTypeConstants
{
    public static readonly string BANH_KEM = "BANH_KEM";
    public static readonly string BANH_MI = "BANH_MI";
    public static readonly string BANH_NGON = "BANH_NGON";
    public static readonly string BANH_MAN = "BANH_MAN";
    public static readonly string BANH_TRUNG_THU = "BANH_TRUNG_THU";
    public static readonly string BANH_CHAY = "BANH_CHAY";
    public static readonly string CUPCAKE = "CUPCAKE";
    public static readonly string BANH_THEO_MUA = "BANH_THEO_MUA";

    public static readonly List<string> AllCakeTypes =
    [
        BANH_KEM,
        BANH_MI,
        BANH_NGON,
        BANH_MAN,
        BANH_TRUNG_THU,
        BANH_CHAY,
        CUPCAKE,
        BANH_THEO_MUA
    ];
}