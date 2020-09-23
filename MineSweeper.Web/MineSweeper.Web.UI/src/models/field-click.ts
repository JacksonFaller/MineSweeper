import { Cell } from './cell';
import { MouseButton } from './mouse-button';

export class FieldClick {
  constructor(public mouseButton: MouseButton, public cell: Cell) {}
}
