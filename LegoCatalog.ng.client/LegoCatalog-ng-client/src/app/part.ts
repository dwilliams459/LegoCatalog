import { PartColor } from './partColor';

export class Part {
  partId: number;
  itemId: string;
  itemName: string;
  itemTypeId: string;
  itemTypeName: string;
  categoryId: number | null;
  categoryName: string | null;
  itemWeight: number | null;
  itemDimensionX: number | null;
  itemDimensionY: number | null;
  itemDimensionZ: number | null;
  imageLink: string | null;
  iconLink: string | null;
  quantity: number | null;
  colorCount: number | null;
  partColors: PartColor[] | null;
}
