namespace eShop.ClientApp.Models.Catalog;

public class CatalogItem
{
    public int Id { get; set; }
    public string Name { get; set; }
    
    public string Description { get; set; }
    public decimal Price { get; set; }
    public string PictureUri { get; set; }
    public int CatalogBrandId { get; set; }
    public CatalogBrand CatalogBrand { get; set; }
    /// <summary>
    /// Gets or sets the identifier for the catalog type. This represents the category or type classification of the catalog item.
    /// </summary>
    public int CatalogTypeId { get; set; }
    public CatalogType CatalogType { get; set; }
}
