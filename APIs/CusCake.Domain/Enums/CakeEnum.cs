namespace CusCake.Domain.Enums;

public enum CakePartTypeEnum
{
    Size,
    Sponge,
    Filling,
    Icing,
    Goo,
    Extras
}

public enum CakeDecorationTypeEnum
{
    OuterIcing,   // Lớp phủ bên ngoài
    TallSkirt,    // Chân váy cao
    ShortSkirt,   // Chân váy thấp
    Drip,         // Sốt chảy trên bánh
    Bling,        // Trang trí lấp lánh (ví dụ: vàng, bạc)
    Decoration,   // Trang trí chung (hoa, hình ảnh, v.v.)
    Sprinkles     // Hạt rắc trang trí
}


public enum CakeExtraTypeEnum
{
    Candles,    // Nến trang trí
    CakeBoard   // Đế bánh
}
public enum CakeMessageTypeEnum
{
    Text,
    Image
}
