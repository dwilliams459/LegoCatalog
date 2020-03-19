export class PartColor {
  itemId: string;
  type: string;
  color: string;
  rgb: string;
  cssColor() {
    return '#' + this.rgb;
  }
}

