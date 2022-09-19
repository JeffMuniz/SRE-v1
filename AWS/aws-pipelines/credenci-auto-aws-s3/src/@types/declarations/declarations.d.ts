
declare module 'string-mask' {
  declare class StringMask {
    constructor(format: string);

    public apply(value): string;
    public validate(value): boolean;
  };
  export default StringMask;
}
