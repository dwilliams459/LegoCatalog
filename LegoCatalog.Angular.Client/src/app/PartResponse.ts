export class PartResponse {
    partId: number;
    itemId: string;
    itemTypeId: string | null;
    itemType: ItemType;
    itemName: string;
    categoryId: number;
    category: Category;
    itemWeight: number | null;
    dimensionX: number | null;
    dimensionY: number | null;
    dimensionZ: number | null;
    imageLink: string | null;
    iconLink: string | null;
}

export class ItemType 
{
    itemTypeId: string;
    itemTypeName: string
}

export class Category 
{
    categoryId: number;
    name: string;
    subCategory: string
}